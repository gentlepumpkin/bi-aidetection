using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Data.Sqlite;

namespace AITool
{

	public class HistorySQLite : IDisposable
	{
        private bool disposedValue;

        public string Filename { get; set; } = "";
		public SqliteConnection sqlite_conn { get; set; } = null;
		public bool ReadOnly { get; } = false;
		public HistorySQLite(string Filename, bool ReadOnly)
		{
			if (string.IsNullOrEmpty(Filename))
				throw new System.ArgumentException("Parameter cannot be empty", "Filename");

			this.Filename = Filename;
			this.ReadOnly = ReadOnly;

			if (!this.CreateConnection())
				throw new System.Exception("Could not connect to SQLite database");

		}

		private bool IsSQLiteConnectionVerified()
        {
			bool ret = false;
			try
            {
                if (this.sqlite_conn != null && !string.IsNullOrEmpty(this.sqlite_conn.DataSource))
                {
                    if (this.sqlite_conn. State == System.Data.ConnectionState.Open)
                    {
						ret = true;
                    }
                    else
                    {
						Global.Log($"Error: sqlite_conn.state = {this.sqlite_conn.State}");
					}
				}
                else
                {
					Global.Log("Error: sqlite_conn is null?");

				}
			}
            catch (Exception ex)
            {

				Global.Log("Error: " + Global.ExMsg(ex));
			}
			return ret;
        }

		private bool CreateConnection()
		{
			bool ret = false;

			try
			{

				// Create a new database connection:
				// Connection pooling:  Pooling=True;Max Pool Size=100;
				// read-only Read Only=True;
				//https://www.connectionstrings.com/sqlite/

				string connectstr = $"Data Source={this.Filename}; Version=3; New=True; Compress=True; ";

				if (this.ReadOnly)
					connectstr += " Read Only=True;";

				Stopwatch sw = Stopwatch.StartNew();

				//If the database file doesn't exist, the default behaviour is to create a new file
				sqlite_conn = new SqliteConnection(connectstr);

				//set up events so we know anything weird is happening with db
				sqlite_conn.StateChange += (sender, e) =>
				{
					Global.Log($"SQLConnection state changed from '{e.OriginalState}' to '{e.CurrentState}'");
				};
				sqlite_conn.Disposed += (sender, e) =>
				{
					Global.Log($"SQLConnection DISPOSED.");
				};

				// Open the connection:
				sqlite_conn.Open();

				// Enable write-ahead logging
				//https://www.sqlite.org/wal.html
				SqliteCommand cmd = sqlite_conn.CreateCommand();
				cmd.CommandText = @"PRAGMA journal_mode = 'WAL'";
				cmd.ExecuteNonQuery();

				//https://www.sqlite.org/pragma.html#pragma_busy_timeout
				cmd.CommandText = @"PRAGMA busy_timeout = 30000";   //set high to 30 seconds to be sure no failures when sharing database
				cmd.ExecuteNonQuery();

				//Create our table if it doesnt exist

				if (this.CreateTable())
					ret = true;

				sw.Stop();

				Global.Log($"Created connection to SQLite db v{sqlite_conn.ServerVersion} in {sw.ElapsedMilliseconds}ms - {connectstr}");

			}
			catch (Exception ex)
			{

				Global.Log("Error: " + Global.ExMsg(ex));
			}

			return ret;
		}
		private bool CreateTable()
		{
			bool ret = false;

			try
			{
				Stopwatch sw = Stopwatch.StartNew();

				SqliteCommand sqlite_cmd;

				//string Createsql = @"CREATE TABLE IF NOT EXISTS HISTORY (
				//                                                        id integer PRIMARY KEY,
				//                                                        name text NOT NULL,
				   //                                                     priority integer,
				//                                                        project_id integer NOT NULL,
				   //                                                     status_id integer NOT NULL,
				//                                                        begin_date text NOT NULL,
				   //                                                     end_date text NOT NULL,
				//                                                        FOREIGN KEY(project_id) REFERENCES projects(id)
				//                                                      )";
				
				string Createsql = @"CREATE TABLE IF NOT EXISTS HISTORY (
																		id integer PRIMARY KEY,
																		name text NOT NULL,
																		priority integer,
																		project_id integer NOT NULL,
																		status_id integer NOT NULL,
																		begin_date text NOT NULL,
																		end_date text NOT NULL,
																		FOREIGN KEY(project_id) REFERENCES projects(id)
																	  )";

				sqlite_cmd = this.sqlite_conn.CreateCommand();
				sqlite_cmd.CommandText = Createsql;
				int updated = sqlite_cmd.ExecuteNonQuery();

				sw.Stop();

				Global.Log($"Created or verified HISTORY table (updated {updated}) in {sw.ElapsedMilliseconds}ms");

				ret = true;
			}
			catch (Exception ex)
			{

				Global.Log("Error: " + Global.ExMsg(ex));
			}

			return ret;

		}

		private void InsertData(SqliteConnection conn)
		{
			SqliteCommand sqlite_cmd;
			sqlite_cmd = conn.CreateCommand();
			sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test Text ', 1); ";
			sqlite_cmd.ExecuteNonQuery();
			sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test1 Text1 ', 2); ";
			sqlite_cmd.ExecuteNonQuery();
			sqlite_cmd.CommandText = "INSERT INTO SampleTable(Col1, Col2) VALUES('Test2 Text2 ', 3); ";
			sqlite_cmd.ExecuteNonQuery();
			sqlite_cmd.CommandText = "INSERT INTO SampleTable1(Col1, Col2) VALUES('Test3 Text3 ', 3); ";
			sqlite_cmd.ExecuteNonQuery();

		}

		static void ReadData(SqliteConnection conn)
		{
			SqliteDataReader sqlite_datareader;
			SqliteCommand sqlite_cmd;
			sqlite_cmd = conn.CreateCommand();
			sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

			sqlite_datareader = sqlite_cmd.ExecuteReader();
			while (sqlite_datareader.Read())
			{
				string myreader = sqlite_datareader.GetString(0);
				Console.WriteLine(myreader);
			}
			conn.Close();
		}

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (this.sqlite_conn != null)
                    {
                        if (this.sqlite_conn.State != System.Data.ConnectionState.Closed)
                        {
							this.sqlite_conn.Close();
						}
						this.sqlite_conn.Dispose();
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

