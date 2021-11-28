using System;
using System.IO;
using MySqlConnector;

namespace GameServer
{
    public class DatabaseController
    { 
    
        public byte[] GetAudioFile(string fileName)
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
                    byte[] audioFile = WavToBitArray(dataReader[0].ToString());
                    return audioFile;
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

            return null;



            //Console.WriteLine($"MySQL version : {con.ServerVersion}");
        }

        private byte[] WavToBitArray(string wavFilePath)
        {
            byte[] wavBitArray;
            using (FileStream wavFileStream = new FileStream(wavFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                wavBitArray = new byte[wavFileStream.Length];
                wavFileStream.Read(wavBitArray, 0, (int)wavFileStream.Length);
            }
            return wavBitArray;
        }
    }
}