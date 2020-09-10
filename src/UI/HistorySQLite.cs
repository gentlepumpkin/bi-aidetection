using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace AITool
{

    public class HistorySQLite
    {
        public string Filename { get; set; } = "";
        public SqliteConnection sqlite_conn { get; set; } = null;
        public bool ReadOnly { get; } = false;
        public HistorySQLite(string Filename, bool ReadOnly)
        {
            if (string.IsNullOrEmpty(Filename))
                throw new System.ArgumentException("Parameter cannot be empty", "Filename");

            this.Filename = Filename;

            if (!File.Exists(Filename) || new FileInfo(Filename).Length < 32)
            {
                //CreateDB();
            }

        }

        private SqliteConnection CreateConnection()
        {

            try
            {
                // Create a new database connection:
                // Connection pooling:  Pooling=True;Max Pool Size=100;
                // read-only Read Only=True;
                sqlite_conn = new SqliteConnection($"Data Source={this.Filename}; Version=3; New=True; Compress=True; ");

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

            }
            catch (Exception ex)
            {

                throw;
            }
            return sqlite_conn;
        }
        private void CreateTable(SqliteConnection conn)
        {

            SqliteCommand sqlite_cmd;
            string Createsql = "CREATE TABLE SampleTable(Col1 VARCHAR(20), Col2 INT)";
            string Createsql1 = "CREATE TABLE SampleTable1(Col1 VARCHAR(20), Col2 INT)";
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Createsql1;
            sqlite_cmd.ExecuteNonQuery();

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


    }
}

