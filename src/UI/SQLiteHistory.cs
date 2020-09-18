using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using SQLite;
//using Microsoft.Data.Sqlite;

namespace AITool
{

	public class SQLiteHistory : IDisposable
	{
		private bool disposedValue;

		public string Filename { get; set; } = "";
		public SQLiteAsyncConnection sqlite_conn { get; set; } = null;
		public bool ReadOnly { get; } = false;
		public SQLiteHistory(string Filename, bool ReadOnly)
		{
			if (string.IsNullOrEmpty(Filename))
				throw new System.ArgumentException("Parameter cannot be empty", "Filename");

			this.Filename = Filename;
			this.ReadOnly = ReadOnly;


		}

		private bool IsSQLiteDBConnected()
		{
			try
			{
				return (sqlite_conn != null && !string.IsNullOrEmpty(sqlite_conn.DatabasePath));
			}
			catch (Exception ex)
			{
				Global.Log("Error: " + Global.ExMsg(ex));

				return false;
			}
		}
		
		private async void DisposeConnection()
		{
			try
			{
				await sqlite_conn.CloseAsync();
				sqlite_conn = null;

			}
			catch (Exception ex)
			{
				Global.Log("Error: " + Global.ExMsg(ex));

			}
		}
		private async System.Threading.Tasks.Task<bool> CreateConnectionAsync()
		{
			bool ret = false;

			try
			{
				Stopwatch sw = Stopwatch.StartNew();

				SQLiteOpenFlags flags = SQLiteOpenFlags.Create;
				if (this.ReadOnly)
				{
					flags = flags | SQLiteOpenFlags.ReadOnly;
				}
				else
				{
					flags = flags | SQLiteOpenFlags.ReadWrite;
				}

				if (this.IsSQLiteDBConnected())
				{
					this.DisposeConnection();
				}

				//If the database file doesn't exist, the default behaviour is to create a new file
				sqlite_conn = new SQLiteAsyncConnection(this.Filename, flags, true);


				await sqlite_conn.ExecuteAsync(@"PRAGMA journal_mode = 'WAL'");
				await sqlite_conn.ExecuteAsync(@"PRAGMA busy_timeout = 30000");

				//make sure table exists:
				CreateTableResult ctr = await sqlite_conn.CreateTableAsync<History>();
				

				sw.Stop();

				Global.Log($"Created connection to SQLite db v{sqlite_conn.LibVersionNumber} in {sw.ElapsedMilliseconds}ms - TableCreate={ctr.ToString()}");

			}
			catch (Exception ex)
			{

				Global.Log("Error: " + Global.ExMsg(ex));
			}

			return ret;
		}

		public async void InsertHistoryItem(History hist)
		{
			try
			{
				if (!this.IsSQLiteDBConnected())
					await this.CreateConnectionAsync();

				int ret = await this.sqlite_conn.InsertAsync(hist);

			}
			catch (Exception ex)
			{

				Global.Log("Error: " + Global.ExMsg(ex));
			}

		}
		public async void DeleteHistoryItem(string Filename)
		{
			try
			{
				if (!this.IsSQLiteDBConnected())
					await this.CreateConnectionAsync();

				int ret = await this.sqlite_conn.DeleteAsync(Filename);

			}
			catch (Exception ex)
			{

				Global.Log("Error: " + Global.ExMsg(ex));
			}

		}

		public async Task<List<History>> GetList()
		{
			List<History> ret = new List<History>();

			try
			{

				if (!this.IsSQLiteDBConnected())
					await this.CreateConnectionAsync();

				AsyncTableQuery<History> query = sqlite_conn.Table<History>();
				ret = await query.ToListAsync();

			}
			catch (Exception ex)
			{

				Global.Log("Error: " + Global.ExMsg(ex));
			}

			return ret;

		}

		protected virtual async void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					if (this.IsSQLiteDBConnected())
					{
						this.DisposeConnection();
					}
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				disposedValue = true;
			}
		}


		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}

