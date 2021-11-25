using GameServer;
using Microsoft.AspNetCore.SignalR;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;

namespace SignalRChat.Hubs
{
    public class GameHub : Hub
    {
        InputHandler controller = new InputHandler();

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
                //TODO
                //make client method that takes in a string and spits back an array of numbers
            }


        }

        public async Task ChangeRooms(string userInput)
        {
            string roomAudio = controller.ChangeRoom(userInput);

            //Run client method that handles audio strings


        }

        public async Task Fight(string userInput)
        {
            List<string> userAudio = new List<string>();
            List<string> enemyAudio = new List<string>();
            bool gameOver = controller.FightRound(userInput, userAudio, enemyAudio);
            foreach(string file in userAudio)
            {
                //client method
            }
            foreach(string file in userAudio)
            {
                //client method
            }


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