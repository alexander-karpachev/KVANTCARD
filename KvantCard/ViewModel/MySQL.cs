using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KvantCard
{
   public class MySQL
    {
        static protected MySqlConnection connection;

        static MySQL()// инициализация строки соединения
        {
            MySqlConnectionStringBuilder stringBuilder
                = new MySqlConnectionStringBuilder();
            stringBuilder.Server = "localhost";
            stringBuilder.Database = "KvantCard";
            stringBuilder.UserID = "adm";
            stringBuilder.Password = "adm";
            stringBuilder.CharacterSet = "utf8";
            connection = new MySqlConnection(
                stringBuilder.ToString());
        }

        public static bool OpenConnect()
        {
            if ( connection.State.ToString() != "Open" )
            try
            {
                connection.Open();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }
        public static void CloseConnect()
        {
            try
            {
                connection.Close();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }
        public static void Query(string query)
        {
            if (OpenConnect())
            {
                using (MySqlCommand mc = new MySqlCommand(
                    query, connection))
                    mc.ExecuteNonQuery();
                CloseConnect();
            }
        }
    }
}
