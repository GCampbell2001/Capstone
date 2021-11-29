using GameServer;
using Microsoft.AspNetCore.SignalR;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;

namespace SignalRChat.Hubs
{
    public class GameHub : Hub
    {
        InputHandler controller = new InputHandler();
        DatabaseController databaseController = new DatabaseController();

        public async Task SendFile(byte[] soundFile)
        {
            await Clients.All.SendAsync("ReceiveFile", soundFile);
        }

        public async Task StartGame(string characterClass)
        {
            controller.StartGame(characterClass);
            await Clients.All.SendAsync("ReceiveMessage", "done");
        }

        public async Task Update(string userInput)
        {
            List<string> audioFiles = new List<string>();
            controller.Update(audioFiles);

            foreach(string file in audioFiles)
            {
                Console.WriteLine(file);
                try
                {
                    byte[] audioFile = databaseController.GetAudioFile(file);
                    await Clients.All.SendAsync("ReceiveFile", audioFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }


        }

        public async Task ChangeRooms(string userInput)
        {
            Console.WriteLine(userInput);
            string roomAudio = controller.ChangeRoom(userInput);
            byte[] audioFile = databaseController.GetAudioFile(roomAudio);
            await Clients.All.SendAsync("ReceiveFile", audioFile);

            //await Clients.All.SendAsync("ReceiveRequest", roomAudio);

            //string wavFile = "C:\\Users\\Matthew\\Documents\\Capstone\\API\\fuseyboi.wav";

            //byte[] bitArray = WavToBitArray(wavFile);


            //await Clients.All.SendAsync("RecieveAudio", bitArray);
            //Run client method that handles audio strings


        }

        public async Task Fight(string userInput)
        {
            Console.WriteLine(userInput);
            List<string> userAudio = new List<string>();
            List<string> enemyAudio = new List<string>();
            bool gameOver = controller.FightRound(userInput, userAudio, enemyAudio);
            foreach(string file in userAudio)
            {
                Console.WriteLine(file);
                try
                {
                    byte[] audioFile = databaseController.GetAudioFile(file);
                    await Clients.All.SendAsync("ReceiveFile", audioFile);
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                //client method
            }
            foreach(string file in enemyAudio)
            {
                Console.WriteLine(file);
                try
                {
                    byte[] audioFile = databaseController.GetAudioFile(file);
                    await Clients.All.SendAsync("ReceiveFile", audioFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                //client method
            }


        }

        public async void ResetGame()
        {
            databaseController.ResetMap("Map\\map.json");
        }

        public async Task SendMessage(string user, string message)
        {
       

            await Clients.All.SendAsync("ReceiveMessage", user, message);
            await Clients.All.SendAsync("ReceiveMessage", "Server", "Hello");

           
            await Clients.All.SendAsync("ReceiveMessage", "Server", "World");
        }

        public async Task SendTest(string message)
        {
            await Clients.All.SendAsync("ReceiveTest", "THE AUDIO WAS RECIEVED");
        }

        public async Task SendAudio()
        {
            string wavFile = "C:\\Users\\Matthew\\Documents\\Capstone\\API\\fuseyboi.wav";

            byte[] bitArray = WavToBitArray(wavFile);


            await Clients.All.SendAsync("RecieveAudio", bitArray);

        }


        /*
         * TODO
         * Make Python Application to talk to this hub that Sends the Wav file Already turned to Array
         */
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