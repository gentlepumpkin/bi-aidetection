using System;
using System.Diagnostics;
using System.IO;

namespace AITool
{

    public class ClsImageQueueItem
    {

        public string image_path { get; set; } = "";
        public DateTime TimeAdded { get; set; } = DateTime.MinValue;
        public DateTime TimeCreated { get; set; } = DateTime.MinValue;
        public DateTime TimeCreatedUTC { get; set; } = DateTime.MinValue;
        public long QueueWaitMS { get; set; } = 0;
        public long TotalTimeMS { get; set; } = 0;
        public long LifeTimeMS { get; set; } = 0;
        public long DeepStackTimeMS { get; set; } = 0;
        public long FileLockMS { get; set; } = 0;
        public long FileLoadMS { get; set; } = 0;
        public long FileLockErrRetryCnt { get; set; } = 0;
        public long CurQueueSize { get; set; } = 0;
        public ThreadSafe.Integer ErrCount { get; set; } = new ThreadSafe.Integer(0);
        public ThreadSafe.Integer RetryCount { get; set; } = new ThreadSafe.Integer(0);
        public string ResultMessage { get; set; } = "";
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public byte[] ImageByteArray { get; set; } = null;
        private bool _valid { get; set; } = false;
        private bool _loaded { get; set; } = false;
        public bool IsValid()
        {
            if (!this._loaded)
                this.LoadImage();
            return this._valid;
        }

        public bool CopyFileTo(string outputFilePath)
        {
            bool ret = false;
            
            int bufferSize = 1024 * 1024;
            string copydir = "";

            try
            {
                if (!outputFilePath.Contains("\\"))
                {
                    AITOOL.Log($"Error: Must specify a full path: {outputFilePath}");
                    return false;
                }
                
                if (this.IsValid())  //loads into memory if not already loaded
                {

                    copydir = Path.GetDirectoryName(outputFilePath);

                    DirectoryInfo d = new DirectoryInfo(copydir);
                    if (d.Root != null && !d.Exists)
                    {
                        //dont try to create if working off root drive
                        d.Create();
                    }


                    //If the destination file exists, wait for exclusive access
                    Global.WaitFileAccessResult result2 = new Global.WaitFileAccessResult();
                    if (File.Exists(outputFilePath))
                    {
                        result2 = Global.WaitForFileAccess(outputFilePath, FileAccess.ReadWrite, FileShare.None, 3000, 50, MinFileSize: 0);
                        if (result2.Success)
                            File.Delete(outputFilePath);
                    }
                    else
                        result2.Success = true;

                    if (result2.Success)
                    {
                        Stream inStream = this.ToStream();

                        using (FileStream fileStream = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            fileStream.SetLength(inStream.Length);
                            int bytesRead = -1;
                            byte[] bytes = new byte[bufferSize];

                            while ((bytesRead = inStream.Read(bytes, 0, bufferSize)) > 0)
                            {
                                fileStream.Write(bytes, 0, bytesRead);
                            }
                        }
                        ret = true;
                    }
                    else
                    {
                        AITOOL.Log($"Error: Could not gain access to destination file ({result2.TimeMS}ms, '{result2.ResultString}') {outputFilePath}");
                    }
                }
                else
                {
                    AITOOL.Log($"Error: File not valid: {this.image_path}");
                }

            }
            catch (Exception ex)
            {
                AITOOL.Log($"Error: Copying to {outputFilePath}: {Global.ExMsg(ex)}");
            }

            return ret;

        }
        public ClsImageQueueItem(String FileName, long CurQueueSize)
        {
            this.image_path = FileName;
            this.TimeAdded = DateTime.Now;
            this.CurQueueSize = CurQueueSize;
            FileInfo fi = new FileInfo(this.image_path);
            if (fi.Exists)
            {
                this.TimeCreated = fi.CreationTime;
                this.TimeCreatedUTC = fi.CreationTimeUtc;
            }

        }
        public MemoryStream ToStream()
        {
            MemoryStream ms = new MemoryStream();

            if (this.IsValid())
            {
                try
                {
                    ms = new MemoryStream(this.ImageByteArray, false);
                    ms.Flush();
                }
                catch (Exception ex)
                {
                    AITOOL.Log($"Error: Cannot convert to MemoryStream: {ex.Message}");
                }
            }
            else
            {
                AITOOL.Log($"Error: Cannot convert to MemoryStream because image is not valid.");
            }
            return ms;
        }
        public void LoadImage()
        {
            //since having a lot of trouble with image access problems, try to wait for image to become available, validate the image and load
            //a single time rather than multiple
            Global.WaitFileAccessResult result = new Global.WaitFileAccessResult();
            string LastError = "";
            this._valid = false;
            try
            {
                if (!string.IsNullOrEmpty(this.image_path) && File.Exists(this.image_path))
                {
                    Stopwatch sw = Stopwatch.StartNew();
                    do
                    {
                        result = Global.WaitForFileAccess(this.image_path, FileAccess.Read, FileShare.None, 10000, 50, true, 4096);

                        this.FileLockMS = sw.ElapsedMilliseconds;
                        this.FileLockErrRetryCnt += result.ErrRetryCnt;

                        if (result.Success)
                        {

                            try
                            {
                                sw.Restart();
                                // Open a FileStream object using the passed in safe file handle.
                                using (FileStream fileStream = new FileStream(result.Handle, FileAccess.Read))
                                {
                                    using System.Drawing.Image img = System.Drawing.Image.FromStream(fileStream,true,true);

                                    this._valid = img !=null && img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg);

                                    this.FileLoadMS = sw.ElapsedMilliseconds;

                                    if (!this._valid)
                                    {
                                        LastError = $"Error: Image file is not jpeg? LockMS={this.FileLockMS}ms, retries={this.FileLockErrRetryCnt} - ({img.RawFormat}): {this.image_path}";
                                        AITOOL.Log(LastError);
                                        break;
                                    }
                                    else
                                    {
                                        this.Width = img.Width;
                                        this.Height = img.Height;
                                        using MemoryStream ms = new MemoryStream();
                                        //fileStream.CopyTo(ms);
                                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                        this.ImageByteArray = ms.ToArray();
                                        this.FileLoadMS = sw.ElapsedMilliseconds;
                                        AITOOL.Log($"Debug: Image file is valid. LockMS={this.FileLockMS}ms, retries={this.FileLockErrRetryCnt}, size={Global.FormatBytes(ms.Length)}: {Path.GetFileName(this.image_path)}");
                                        break;
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                this._valid = false;
                                LastError = $"Error: Image is corrupt. LockMS={this.FileLockMS}ms, retries={this.FileLockErrRetryCnt}: {Global.ExMsg(ex)}";
                            }
                            finally 
                            {
                                this._loaded = true;

                                if (!result.Handle.IsClosed)
                                {
                                    result.Handle.Close();
                                    result.Handle.Dispose();
                                }
                            }

                        }
                        else
                        {
                            LastError = $"Error: Could not gain access to the image in {result.TimeMS}ms, retries={result.ErrRetryCnt}.";
                        }

                    } while ((!result.Success || !this._valid) && sw.ElapsedMilliseconds < 30000);

                }
                else
                {
                    //AITOOL.Log("Error: Tried to load the image too soon?");
                }
            }
            catch (Exception ex)
            {

                AITOOL.Log($"Error: {Global.ExMsg(ex)}");
            }
            finally
            {
                if (result.Handle != null && !result.Handle.IsInvalid && !result.Handle.IsClosed)
                {
                    result.Handle.Close();
                    result.Handle.Dispose();
                }
                if (!this._valid && !string.IsNullOrEmpty(LastError))
                    AITOOL.Log(LastError);
            }
        }


    }
}
