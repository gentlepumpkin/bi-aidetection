using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AITool;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

public class LogFileWriter:IDisposable
{
	//Private m_instance As LogWriter
	private ConcurrentQueue<ClsLogDetailItem> logQueue;
	private DateTime LastLogFlushedTimeUTC = DateTime.UtcNow; //UTC date is faster to obtain
	private DateTime LastLogWrittenTime = DateTime.Now;
	private DateTime LastLogRotateCheckTimeUTC = DateTime.MinValue;
	private bool IsFlushing = false;
	private bool FlushRightAway = false;
	private long LogWriteCount = 0;
	private bool RotateLogs = true;
	//private bool m_ProcessAsCSV = false;
	public int MaxLogFileAgeDays { get; set; } = 30;
	public int MaxLogQueueAgeSecs { get; set; } = 10;
	public long MaxLogQueueSize { get; set; } = 64;

	public string LogFile { get; set; } = "";
	public bool NoLog { get; set; } = false;
	public long MaxLogSize { get; set; } = ((1024 * 2024) * 5); //5mb
	/// <summary>
	/// Private constructor to prevent instance creation
	/// </summary>
	public LogFileWriter(string InLogFile, bool RotateLogsSetting = true)
	{

		if (InLogFile.ToLower() == "nolog")
		{
			NoLog = true;
			return;
		}


		if (!string.IsNullOrWhiteSpace(InLogFile))
		{
			this.LogFile = InLogFile;
		}

		//if (!IsDirWritable(this.LogFile, true))
		//{
		//	NoLog = true;
		//	Console.WriteLine("Error: Cannot write to folder: " + this.LogFile);
		//}

		RotateLogs = RotateLogsSetting;
		try
		{

			logQueue = new ConcurrentQueue<ClsLogDetailItem>();

			//Dim myCallback2 As New System.Threading.TimerCallback(AddressOf TimerTask)
			//LogTimer = New System.Threading.Timer(myCallback2, Nothing, New TimeSpan(0, 0, 1), New TimeSpan(0, 0, 0, CInt(MaxLogAgeSecs)))
			string Folder = Path.GetDirectoryName(this.LogFile);

			if (!Directory.Exists(Folder))
			{
				Console.WriteLine("LogWriter: Creating folder " + Folder);
				Directory.CreateDirectory(Folder);
			}

			Task.Run(CheckLogEntriesToFlushLoop);

			//Console.WriteLine("LogWriter: New", , , False)

		}
		catch (Exception ex)
		{
			NoLog = true;
			Console.WriteLine("Error: " + ex.Message);
		}
	}

	private async Task CheckLogEntriesToFlushLoop()
	{
		try
		{
			Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;

			Stopwatch sw = new Stopwatch();
			while (true)
			{
				sw.Reset();
				sw.Start();
				while (!(FlushRightAway || (sw.ElapsedMilliseconds / 1000.0) >= MaxLogQueueAgeSecs))
				{
					await Task.Delay(50); //Thread.Sleep(250)
				}
				if (RotateLogs) //this only runs once a day
				{
					await Task.Run(CleanAndRotateLogsFolder);
				}
				await FlushLog();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error: " + ex.Message);
		}
		Console.WriteLine("Out of CheckLogEntriesToFlushLoop()");
	}



	public void CleanAndRotateLogsFolder()
	{

		if (LastLogRotateCheckTimeUTC.DayOfYear == DateTime.UtcNow.DayOfYear)
		{
			return;
		}

		try
		{

			//filename_2015-08-05_13_08_46_(info)_[info]

			string Folder = Path.GetDirectoryName(this.LogFile);
			string Ext = Path.GetExtension(this.LogFile);

			string[] files = Directory.GetFiles(Folder, "*" + Ext);
			int DelCnt = 0;
			int RenCnt = 0;
			foreach (string CurFile in files)
			{
				FileInfo fi = new FileInfo(CurFile);
				DateTime FileNameDate = Global.GetTimeFromFileName(CurFile);
				if (!FileNameDate.Equals(DateTime.MinValue) && (fi.LastWriteTimeUtc < DateTime.UtcNow.AddDays(-MaxLogFileAgeDays)))
				{
					DelCnt = DelCnt + 1;
					fi.Delete();
				}
				else if (FileNameDate.Equals(DateTime.MinValue) && (CurFile.ToLower() == this.LogFile.ToLower())) //this is the main log filename if it doesnt have a date in it.
				{

					if ((MaxLogSize > 0 && fi.Length >= MaxLogSize) || (DateTime.UtcNow.DayOfYear != fi.LastWriteTimeUtc.DayOfYear))
					{
						Console.WriteLine("LogWriter.New: Renaming log file because size or age:  Size: " + fi.Length + ", Max: " + MaxLogSize + "...");
						string NewFileName = Path.GetFileNameWithoutExtension(CurFile);
						NewFileName = NewFileName + "_" + fi.LastWriteTimeUtc.ToString("s").Replace(":", "_").Replace("T", "_"); //& "_{UTC}"
						NewFileName = Folder + "\\" + NewFileName + Ext;
						try
						{
							if (File.Exists(NewFileName))
							{
								File.Delete(NewFileName);
							}
							fi.MoveTo(NewFileName);
							RenCnt = RenCnt + 1;
						}
						catch (Exception ex)
						{
							Console.WriteLine("Error: " + ex.Message);
						}
					}

				}
			}

			if (DelCnt > 0)
			{
				Console.WriteLine("Deleted " + DelCnt + " log files older than " + MaxLogFileAgeDays + " days old in " + Folder);
			}
			if (RenCnt > 0)
			{
				Console.WriteLine("Renamed " + RenCnt + " log files in " + Folder);
			}

		}
		catch (Exception ex)
		{
			Console.WriteLine("Error: " + ex.Message);
		}
		finally
		{
			LastLogRotateCheckTimeUTC = DateTime.UtcNow;
		}


	}

	#region IDisposable Support
	private bool disposedValue; // To detect redundant calls

	// IDisposable
	protected virtual async Task Dispose(bool disposing)
	{
		if (!this.disposedValue)
		{
			if (disposing)
			{
				// TODO: dispose managed state (managed objects).
				//Console.WriteLine("LogWriter: Disposing...", , , False)
				if (!NoLog)
				{
					await this.FlushLog();
				}
			}

			// TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
			// TODO: set large fields to null.
		}
		this.disposedValue = true;
	}


	// This code added by Visual Basic to correctly implement the disposable pattern.
	public async void Dispose()
	{
		// Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
		await Dispose(true);
		GC.SuppressFinalize(this);
	}
	#endregion

	/// <summary>
	/// The single instance method that writes to the log file
	/// </summary>
	/// <param name="message">The message to write to the log</param>
	public void WriteToLog(string message, bool Flush = false)
	{

		try
		{
			if (NoLog)
			{
				return;
			}
			ClsLogDetailItem DI = new ClsLogDetailItem();
			DI.Message = message;
			this.WriteToLog(DI, Flush);
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error: " + ex.Message);
		}
		finally
		{

		}
	}


	//public void WriteAllLines(string[] DetailItms)
	//{

	//	try
	//	{
	//		if (NoLog)
	//		{
	//			return;
	//		}

	//		foreach (string lin in DetailItms)
	//		{
	//			if (lin != null)
	//			{
	//				// Create the entry and push to the Queue
	//				ClsLogDetailItem DetailItm = new ClsLogDetailItem();
					
	//				LogWriteCount = LogWriteCount + 1;
				
	//				DetailItm.Idx = LogWriteCount;
	//				DetailItm.TimeUTC = DateTime.UtcNow;
	//				DetailItm.Message = lin;
	//				logQueue.Enqueue(DetailItm);
	//				if (LogWriteCount == long.MaxValue - 8)
	//				{
	//					LogWriteCount = 1;
	//				}
	//			}
	//		}
			
	//		//// If we have reached the Queue Size then flush the Queue
	//		//if (Flush) 
	//		//{
	//		//	//Await FlushLog()
	//		//	FlushRightAway = true;
	//		//}
	//	}
	//	catch (Exception ex)
	//	{
	//		Console.WriteLine("Error: " + ex.Message);
	//	}
	//	finally
	//	{

	//	}
	//}
	public void WriteToLog(ClsLogDetailItem DetailItm, bool Flush = false)
	{

		try
		{
			if (NoLog)
			{
				return;
			}
			// Create the entry and push to the Queue
			LogWriteCount = LogWriteCount + 1;

			DetailItm.Idx = LogWriteCount;
			DetailItm.TimeUTC = DateTime.UtcNow;
			logQueue.Enqueue(DetailItm);
			if (LogWriteCount == long.MaxValue - 8)
			{
				LogWriteCount = 1;
			}
			// If we have reached the Queue Size then flush the Queue
			if (LogWriteCount == 1 || Flush || logQueue.Count >= MaxLogQueueSize) //OrElse DoPeriodicFlush() Then
			{
				//Await FlushLog()
				FlushRightAway = true;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error: " + ex.Message);
		}
		finally
		{

		}
	}

	/// <summary>
	/// Flushes the Queue to the physical log file
	/// </summary>
	private async Task FlushLog()
	{


		try
		{
			if (IsFlushing)
			{
				Console.WriteLine("(AlreadyFlushing?)");
				
				return;
			}
			IsFlushing = true;
			long LastQCnt = logQueue.Count;
			if (logQueue.Count > 0)
			{
				//Console.WriteLine("LogWriter:FlushLog: Flushing " & logQueue.Count & " items on thread " & Thread.CurrentThread.ManagedThreadId & ", Secs since last flush: " & (Now - LastFlushed).TotalSeconds & "...", , , False)
				Stopwatch sw = Stopwatch.StartNew();
				using (IOFile FileWriter = new IOFile(LogFile))
				{
					FileWriter.WriteQueue = logQueue;
					await FileWriter.WriteFile();
					LastLogWrittenTime = FileWriter.LastLogWriteTime;
				}
				sw.Stop();
				if (sw.ElapsedMilliseconds >= 1000)
				{
					Console.WriteLine($"LogWriter:FlushLog: DONE Flushing in {sw.ElapsedMilliseconds}ms, LogQueue: {LastQCnt} items, File: {this.LogFile}");
				}
			}
			LastLogFlushedTimeUTC = DateTime.UtcNow;
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error: " + Global.ExMsg(ex));
		}
		finally
		{

			IsFlushing = false;
			LastLogFlushedTimeUTC = DateTime.UtcNow;

		}
		FlushRightAway = false;

	}

}

public class ClsLogDetailItem
{
	public DateTime TimeUTC { get; set; } = DateTime.MinValue;
	public long Idx { get; set; } = 0;
	public string MemberName { get; set; } = "";
	public string Message { get; set; } = "";
	public bool IsDbg { get; set; } = false;
	public bool HasInfo { get; set; } = false;
	public bool HasWarn { get; set; } = false;
	public bool HasErr { get; set; } = false;
	public double QueueWaitMS { get; set; } = 0;

}

public class IOFile:IDisposable
{
	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	private extern static SafeFileHandle CreateFile(string lpFileName, FileSystemRights dwDesiredAccess, FileShare dwShareMode, IntPtr securityAttrs, FileMode dwCreationDisposition, FileOptions dwFlagsAndAttributes, IntPtr hTemplateFile);

	private const int ERROR_SHARING_VIOLATION = 32;
	private const int ERROR_LOCK_VIOLATION = 33;

	//Public Property MaxFileSize As Long = 0
	// Fields
	private Encoding m_encoding = new UTF8Encoding(); //AsciiEncoding
	private FileStream m_fileStream;
	private SafeFileHandle m_fileHandle;
	private bool m_WasLocked = false;
	private string m_path = string.Empty;
	private List<string> m_strings;
	//private bool m_ProcessAsCSV = false;

	public bool FileExists = false;

	public DateTime LastLogWriteTime = DateTime.MinValue;
	public ConcurrentQueue<ClsLogDetailItem> WriteQueue { get; set; }

	private bool IsFileInUse(System.IO.FileShare fshare = FileShare.Read, System.Security.AccessControl.FileSystemRights fSysRights = FileSystemRights.Read, System.IO.FileAccess fAccess = FileAccess.Read, System.IO.FileMode fMode = FileMode.Open, System.IO.FileOptions fOptions = FileOptions.None)
	{
		bool inUse = false;


		try
		{
			do
			{
				this.m_fileHandle = CreateFile(m_path, fSysRights, fshare, IntPtr.Zero, fMode, FileOptions.None, IntPtr.Zero);

				if (this.m_fileHandle.IsInvalid)
				{
					int LastErr = Marshal.GetLastWin32Error();
					//Console.WriteLine("IsFileInUse LastErr: " & LastErr & ", " & New Win32Exception(LastErr).Message, , , False)
					inUse = true;
					if (LastErr == ERROR_SHARING_VIOLATION || LastErr == ERROR_LOCK_VIOLATION)
					{
						if (!this.m_fileHandle.IsClosed)
						{
							m_fileHandle.Close();
						}
					}
					else
					{
						//Me.m_fileStream = New FileStream(m_fileHandle, fAccess)
					}
				}
				else
				{
					this.m_fileStream = new FileStream(m_fileHandle, fAccess);

				}
				break;
			} while (true);

		}
		catch (Exception ex)
		{
			Console.WriteLine("IsFileInUse Error: " + Global.ExMsg(ex));
			inUse = true;
		}
		finally
		{

		}

		return inUse;
	}
	// Methods
	public IOFile(string LogPath)
	{


		try
		{
			this.m_path = LogPath;
			//this.m_ProcessAsCSV = ProcessAsCSV;
			CloseHandles();
			FileExists = File.Exists(this.m_path);
			if (FileExists)
			{
				LastLogWriteTime = (new FileInfo(this.m_path)).LastWriteTime;
			}

		}
		catch (Exception ex)
		{
			Console.WriteLine("Error creating IOFile logger: " + ex.Message);
		}
	}

	public bool Delete()
	{
		try
		{
			if (FileExists)
			{
				File.Delete(m_path);
				LastLogWriteTime = DateTime.MinValue;
				FileExists = false;
			}

		}
		catch (Exception ex)
		{
			Console.WriteLine("Error deleting file: " + ex.Message);
		}
		//INSTANT C# NOTE: Inserted the following 'return' since all code paths must return a value in C#:
		return false;
	}

	#region IDisposable Support
	private bool disposedValue; // To detect redundant calls

	// IDisposable
	protected virtual void Dispose(bool disposing)
	{
		if (!this.disposedValue)
		{
			if (disposing)
			{
				// TODO: dispose managed state (managed objects).
				//Console.WriteLine("IOFile: Disposing...", , , False)
				CloseHandles();
			}

			// TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
			// TODO: set large fields to null.
		}
		this.disposedValue = true;
	}

	// TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
	//Protected Overrides Sub Finalize()
	//    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
	//    Dispose(False)
	//    MyBase.Finalize()
	//End Sub

	// This code added by Visual Basic to correctly implement the disposable pattern.
	public void Dispose()
	{
		// Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
		Dispose(true);
		GC.SuppressFinalize(this);
	}
	#endregion
	private void CloseHandles()
	{
		try
		{
			if (m_fileHandle != null && !m_fileHandle.IsClosed)
			{
				//Console.WriteLine("FileIO:CloseHandles: m_FileHandle was not closed, closing...", , , False)
				m_fileHandle.Close();
				m_fileHandle.Dispose();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("FileIO: Error disposing filehandle: " + ex.Message);
		}
		try
		{
			if (m_fileStream != null)
			{
				//Console.WriteLine("FileIO:CloseHandles: m_Filestream was not closed, closing...", , , False)
				m_fileStream.Close();
				m_fileStream.Dispose();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("FileIO: Error disposing filestream: " + ex.Message);
		}
	}
	private async Task<bool> LockFile(System.IO.FileShare fshare = FileShare.Read, System.Security.AccessControl.FileSystemRights fSysRights = FileSystemRights.Read, System.IO.FileAccess fAccess = FileAccess.Read, System.IO.FileMode fMode = FileMode.Open, System.IO.FileOptions fOptions = FileOptions.None)
	{


		int num = 0;
		bool flag = false;
		Stopwatch SW = new Stopwatch();
		SW.Start();
		while (!flag && (num < 2000) && (SW.ElapsedMilliseconds < 30000))
		{
			//Try
			if (!IsFileInUse(fshare, fSysRights, fAccess, fMode, fOptions))
			{
				if (fMode == FileMode.OpenOrCreate || fAccess == FileAccess.ReadWrite || fAccess == FileAccess.Write)
				{
					try
					{
						this.m_fileStream.Lock(this.m_fileStream.Length, 0xFFFF);
						m_WasLocked = true;
						FileExists = true;
					}
					catch (Exception ex)
					{
						Console.WriteLine("  LockFile Error: " + ex.Message);
					}
				}
			}
			else
			{
				num += 1;
				await Task.Delay(10); //'Thread.Sleep(10)
				continue;
			}
			//Catch exception1 As Exception
			//    num += 1
			//    Thread.Sleep(10)
			//    Continue Do
			//End Try
			flag = true;
		}
		SW.Stop();
		if (num > 0)
		{
			Console.WriteLine("  Lockfile lock time: " + SW.ElapsedMilliseconds + "ms, errcount=" + num);
		}

		return flag;
	}

	//public virtual async Task<bool> ReadFile()
	//{
	//	long position = 0;
	//	try
	//	{
	//		return await this.ReadFile(position);
	//	}
	//	catch (Exception exception1)
	//	{
	//		return false;
	//	}
	//}

	//public virtual async Task<bool> ReadFile(long position)
	//{
	//	this.m_strings = new List<string>();
	//	if (!(await FileExistsAsync(this.m_path)))
	//	{
	//		return false;
	//	}
	//	FileStream stream = null;
	//	try
	//	{
	//		stream = new FileStream(this.m_path, FileMode.Open, FileAccess.Read, (FileShare.Delete | FileShare.ReadWrite));
	//	}
	//	catch (Exception obj1)
	//	{
	//		return false;
	//	}
	//	StreamReader reader = null;
	//	try
	//	{
	//		stream.Seek(position, SeekOrigin.Begin);
	//		if (position == 0)
	//		{
	//			reader = new StreamReader(stream, true);
	//		}
	//		else
	//		{
	//			reader = new StreamReader(stream, this.m_encoding);
	//		}
	//	}
	//	catch (Exception obj2)
	//	{
	//		stream.Close();
	//		return false;
	//	}
	//	bool flag = false;
	//	string item = string.Empty;
	//	try
	//	{
	//		do
	//		{
	//			item = await reader.ReadLineAsync();
	//			if (item != null)
	//			{
	//				this.m_strings.Add(item);
	//			}
	//			else
	//			{
	//				break;
	//			}
	//		} while (true);
	//		if (position == 0)
	//		{
	//			this.m_encoding = reader.CurrentEncoding;
	//		}
	//		position = stream.Position;
	//		flag = true;
	//	}
	//	catch (Exception exception1)
	//	{
	//	}
	//	reader.Close();
	//	stream.Close();
	//	return flag;
	//}

	public async Task<bool> WriteFile()
	{


		bool Ret = false;
		Stopwatch SW = new Stopwatch();
		SW.Start();
		try
		{
			bool Locked = await LockFile(FileShare.Read, (System.Security.AccessControl.FileSystemRights)(FileSystemRights.CreateFiles & FileSystemRights.Write), FileAccess.Write, FileMode.OpenOrCreate);
			if (Locked)
			{
				StreamWriter writer = null;
				bool Err = false;
				try
				{
					writer = new StreamWriter(this.m_fileStream, this.m_encoding); //With {.AutoFlush = True}
				}
				catch (Exception obj2)
				{
					Console.WriteLine("Error creating streamwriter: " + obj2.Message);
					if (writer != null)
					{
						writer.Close();
					}
					this.m_fileStream.Close();
					Err = true;
				}
				if (Err == true)
				{
					return Ret;
				}
				long position = 0;
				try
				{
					position = this.m_fileStream.Length;
					this.m_fileStream.Seek(0, SeekOrigin.End);
					ClsLogDetailItem OutQueueItem = new ClsLogDetailItem();

					//if (position == 0)
					//{
					//	//Convert the class to tab delimited format - HEADER:
					//	string OutCsv = null;
					//	if (this.m_ProcessAsCSV)
					//	{
					//		OutCsv = ClsToCSV(OutQueueItem, true);
					//		//write header
					//		await writer.WriteLineAsync(OutCsv);
					//	}
					//	else
					//	{
					//		if (OutQueueItem.Message.Length > 0) //for some reason we are getting an empty line?
					//		{
					//			OutCsv = OutQueueItem.Message;
					//			//write header
					//			await writer.WriteLineAsync(OutCsv);
					//		}
					//	}
					//}

					while (WriteQueue.TryDequeue(out OutQueueItem))
					{
						//update time in queue
						OutQueueItem.QueueWaitMS = (DateTime.UtcNow - OutQueueItem.TimeUTC).TotalMilliseconds;
						//Convert the class to tab delimited format:
						string OutCsv = null;
						//if (this.m_ProcessAsCSV)
						//{
						//	OutCsv = ClsToCSV(OutQueueItem, false);
						//}
						//else
						//{
							OutCsv = OutQueueItem.Message;
						//}
						await writer.WriteLineAsync(OutCsv);
					}
					await writer.FlushAsync();
					Ret = true;
				}
				catch (Exception exception1)
				{
					Console.WriteLine("Error writing to streamwriter: " + exception1.Message);
				}
				try
				{
					if (position > 0 && m_WasLocked)
					{
						this.m_fileStream.Unlock(position, 0xFFFF);
					}
				}
				catch (IOException exception)
				{
					Ret = false;
					Console.WriteLine("Error unlocking file: " + exception.Message); //already unlocked
				}

				writer.Close();

			}
			else
			{
				Ret = false;
			}

		}
		catch (Exception ex)
		{
			Console.WriteLine("Error: " + ex.Message);
		}
		finally
		{

			if (Ret)
			{
				LastLogWriteTime = DateTime.Now;
			}
		}

		return Ret;
	}

	// Properties
	public List<string> Strings
	{
		get
		{
			return this.m_strings;
		}
	}


}




