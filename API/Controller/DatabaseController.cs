using System;
using MySqlConnector;

namespace GameServer
{
    public class DatabaseController
    { 
    
        public void GetAudioFile(string fileName)
        {
            string cs = @"server=localhost;userid=rasp;password=Yoru^558;database=audio";
            using var connection = new MySqlConnection(cs);
            try
            {
                
                connection.Open();

                string sql = "SELECT location FROM audio.files WHERE filename LIKE '" + fileName + "'";
                MySqlCommand sqlCommand = new MySqlCommand(sql, connection);
                MySqlDataReader dataReader = sqlCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    Console.WriteLine(dataReader[0]);
                }

            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                // Returns Audio File that says there was an error with Getting audio from Database
            }
            finally
            {
                connection.Close();
            }



            //Console.WriteLine($"MySQL version : {con.ServerVersion}");
        }
    }
}