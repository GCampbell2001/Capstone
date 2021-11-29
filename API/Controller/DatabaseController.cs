using System;
using System.IO;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
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

        public void ResetMap(string file)
        {
            MongoClient client = new MongoClient("mongodb+srv://MatthewC:Vault159@cluster0.2ximt.mongodb.net/GameData?retryWrites=true&w=majority");
            IMongoDatabase database = client.GetDatabase("GameData");
            database.DropCollection("Map");
            //database.CreateCollection("Map");
            //IMongoCollection<BsonArray> collection = database.GetCollection<BsonArray>("Map");

            string text = System.IO.File.ReadAllText(file);
            BsonArray document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonArray>(text);
            var collection = database.GetCollection<BsonDocument>("Map");
            foreach(BsonDocument room in document)
            {
                collection.InsertOne(room);
            }
            

            //using (var streamReader = new StreamReader(file))
            //{
            //    string line;
            //    while ((line = streamReader.ReadLine()) != null)
            //    {
            //        using (var jsonReader = new JsonReader(line))
            //        {
            //            var context = BsonDeserializationContext.CreateRoot(jsonReader);
            //            try
            //            {
            //                var document = collection.DocumentSerializer.Deserialize(context);
            //                collection.InsertOne(document);
            //            } catch(Exception ex)
            //            {
            //                Console.WriteLine(ex.ToString());
            //            }
            //        }
            //    }
            //}
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