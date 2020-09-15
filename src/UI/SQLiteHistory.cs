using System;
using System.Diagnostics;
using System.IO;
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

		private async void InsertHistory(History hist)
		{
			try
			{

				int ret = await this.sqlite_conn.InsertAsync(hist);

			}
			catch (Exception ex)
			{

				Global.Log("Error: " + Global.ExMsg(ex));
			}

		}
		private async void DeleteHistory(string Filename)
		{
			try
			{

				int ret = await this.sqlite_conn.DeleteAsync(Filename);

			}
			catch (Exception ex)
			{

				Global.Log("Error: " + Global.ExMsg(ex));
			}

		}

		static void ReadData(SQLiteAsyncConnection conn)
		{


		}

        protected virtual async void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (this.sqlite_conn != null)
                    {
						await this.sqlite_conn.CloseAsync();
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

