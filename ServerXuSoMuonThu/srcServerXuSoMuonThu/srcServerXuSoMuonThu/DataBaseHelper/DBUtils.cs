using ExitGames.Logging;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace srcServerXuSoMuonThu.DataBaseHelper
{
    public class DBUtils
    {
        private static readonly ILogger Log = LogManager.GetCurrentClassLogger();
        public static MySqlConnection GetDBConnetion()
        {
            string host = "127.0.0.1";
            int port = 3306;
            string database = "xusomuonthu";
            string username = "root";
            string password = "";

            return GetDBConnetion(host, (uint)port, database, username, password);
        }

        private static MySqlConnection GetDBConnetion(string host, uint port, string database, string username, string password)
        {
            /*
            MySqlConnectionStringBuilder connString = new MySqlConnectionStringBuilder();
            connString.Server = host;
            connString.Database = database;
            connString.Port = (uint)port;
            connString.UserID = username;
            connString.Password = password;
            */

            string connString = $"Server = {host}; Database = {database}; " +
                $"Port = {port}; UID = {username}; Password = {password}; ";

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }

        public static int getAutoIncrement(string table)
        {
            int rs = -100;
            var conn = GetDBConnetion();
            conn.Open();
            string sqlS = "Select auto_increment from information_schema.TABLES where TABLE_SCHEMA = 'xusomuonthu' and table_name = @tb;";
            var cmd = new MySqlCommand(sqlS, conn);
            cmd.Parameters.Add("@tb", MySqlDbType.String).Value = table;

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    rs = reader.GetInt32("auto_increment");
                }
            }

            cmd.Cancel();
            cmd.Dispose();
            conn.Close();
            conn.Dispose();

            return rs;
        }
    }
}
