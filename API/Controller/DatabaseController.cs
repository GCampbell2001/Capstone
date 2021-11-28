using System;
using MySqlConnector;

namespace GameServer
{
    public class DatabaseController
    { 
    
        public void GetAudioFile(string fileName)
        {
            string cs = @"server=localhost;userid=rasp;password=Yoru^558;database=audio";
            using var con = new MySqlConnection(cs);
            con.Open();

            //Console.WriteLine($"MySQL version : {con.ServerVersion}");
        }
    }
}