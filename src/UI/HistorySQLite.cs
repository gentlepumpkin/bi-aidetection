using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace AITool
{
    public class History
    {
        //filename|date and time|camera|detections|positions of detections|success
        //D:\BlueIrisStorage\AIInput\AIFOSCAMDRIVEWAY\AIFOSCAMDRIVEWAY.20200901_073513579.jpg|01.09.20, 07:35:15|AIFOSCAMDRIVEWAY|false alert||false
        //D:\BlueIrisStorage\AIInput\AILOREXBACK\AILOREXBACK.20200901_073516231.jpg|
        //      01.09.20, 07:35:17|
        //      AILOREXBACK|
        //      1x not in confidence range; 6x irrelevant : fire hydrant (47%); dog (42%); umbrella (77%); chair (98%); chair (86%); chair (60%); chair (59%); |
        //      1541,870,1690,1097;1316,1521,1583,1731;226,214,1231,637;451,1028,689,1321;989,769,1225,1145;882,616,1089,938;565,647,701,814;|
        //      false
        public string Filename { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.MinValue;
        public string Camera { get; set; } = "";
        public string Detections { get; set; } = "";   //TODO: should this be a list in the db?
        public string Positions { get; set; } = "";
        public bool Success { get; set; } = false;


    }
    public class HistorySQLite
    {
        public string Filename { get; set; } = "";

        public HistorySQLite(string Filename)
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

            SqliteConnection sqlite_conn;

            try
            {
                // Create a new database connection:
                sqlite_conn = new SqliteConnection($"Data Source={this.Filename}; Version=3; New=True; Compress=True; ");

                // Open the connection:
                
                sqlite_conn.Open();

                // Enable write-ahead logging
                //https://www.sqlite.org/wal.html
                SqliteCommand walCommand = sqlite_conn.CreateCommand();
                walCommand.CommandText = @"PRAGMA journal_mode = 'wal'";
                walCommand.ExecuteNonQuery();

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

