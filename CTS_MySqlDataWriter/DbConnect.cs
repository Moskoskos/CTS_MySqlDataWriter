using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data;

namespace ConsoleApplication1
{
    public partial class DbConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;


        //Source:
        //http://www.codeproject.com/Articles/43438/Connect-C-to-MySQL
        //The server settings for logging on to the MySql Database
        public DbConnect()
        {
            server = "127.0.0.1";
            database = "cts";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            
        }
        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Close();
                    connection.Open();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Could not connect to database");
                        break;
                    case 1045:
                        Console.WriteLine("Wrong password / username");
                        break;
                }
                return false;
            }
        }
        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;

            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }
        public bool PopulateHIstorian(double valueIn, int days)
        {
            //
            //Source:
            //http://stackoverflow.com/questions/19527554/inserting-values-into-mysql-database-from-c-sharp-application-text-box
            {
                
                try
                {
                    string query = "INSERT INTO historian(datetime_recorded,value)VALUES(@datetime,@value);";
                    //Checks if connection is open
                    if (this.OpenConnection() == true)
                    {
                        //uses the connection string and the query created above.
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            // The paramteres mentioned in VALUES is here given a value
                            cmd.Parameters.AddWithValue("@datetime", DateTime.Now.AddDays(days));
                            cmd.Parameters.AddWithValue("@value", valueIn);
                            // Execute the query
                            cmd.ExecuteNonQuery();
                        }
                    }
                    return true;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message + "\r\r\n" + ex.GetType());
                    return false;
                }
            }
        }
        public double Temperature(double days)
        {
            double temp = 0.0;
            double x = days;
            temp = (20 * Math.Cos(((2 * Math.PI * x) / 365) - ((80 * Math.PI) / 73) + 13));
            temp = Math.Round(temp, 1);
            return temp;
        }
        public bool DeleteRecordsInTable()
        {
            try
            {
                //ALTER TABLE @tablename AUTO_INCREMENT=0

                string query = "TRUNCATE TABLE historian";
                if (this.OpenConnection() == true)
                {
                    using (MySqlCommand cmdTruncate = new MySqlCommand(query, connection))
                    {
                        cmdTruncate.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\r\n");
            }
            return false;
        }
        public bool ActivateTimeStamp()
        {
            try
            {
                string query = "ALTER TABLE historian (datetime_recorded DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP)";
                if (this.OpenConnection() == true)
                {
                    using (MySqlCommand cmdActivateTimestamp = new MySqlCommand(query,connection))
                    {
                        cmdActivateTimestamp.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        public bool DisableTimeStamp()
        {
            try
            {
                string query = "ALTER TABLE historian (datetime_recorded DATETIME NOT NULL DEFAULT )";
                if (this.OpenConnection() == true)
                {
                    using (MySqlCommand cmdActivateTimestamp = new MySqlCommand(query, connection))
                    {
                        cmdActivateTimestamp.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
