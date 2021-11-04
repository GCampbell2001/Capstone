using System;
using Newtonsoft.Json.Linq;

namespace GameLogic.GameLogic
{
    public class DataManipulation
    {

        /*
         * TODO
         * Method to convert the data that is sent back and forth into useable variables.
         * Method to pull from MongoDatabase based off of room and level.
         */

        public void rawInfo(string gameData)
        {
            var dataFormatted = JObject.Parse(gameData);
            JToken token;
            int level;
            if (dataFormatted.TryGetValue("level", out token))
                level = (int) token;
            
        }
    }
}
