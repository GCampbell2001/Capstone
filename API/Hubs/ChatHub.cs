using Microsoft.AspNetCore.SignalR;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
       

            await Clients.All.SendAsync("ReceiveMessage", user, message);
            await Clients.All.SendAsync("ReceiveMessage", "Server", "Hello");

           
            await Clients.All.SendAsync("ReceiveMessage", "Server", "World");
        }

        public async Task SendAudio()
        {
            string wavFile = "C:\\Users\\Matthew\\Documents\\Capstone\\API\\fuseyboi.wav";

            byte[] bitArray = WavToBitArray(wavFile);


            await Clients.All.SendAsync("RecieveAudio", bitArray);

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

        private  byte[] CreateHeadersArray(byte[] bitArray, int StartingIndexOfDataChunk)
        {
            byte[] onlyHeaders = new byte[StartingIndexOfDataChunk];
            Array.Copy(bitArray, 0, onlyHeaders, 0, StartingIndexOfDataChunk);
            return onlyHeaders;
        }

        private  byte[] CreateAudioArray(byte[] bitArray, int StartingIndexOfDataChunk)
        {
            byte[] onlyAudio = new byte[bitArray.Length - StartingIndexOfDataChunk];
            Array.Copy(bitArray, StartingIndexOfDataChunk, onlyAudio, 0, bitArray.Length - StartingIndexOfDataChunk);
            return onlyAudio;
        }
    
        
    }
}