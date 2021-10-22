using GameLogic.Character;
using GameLogic.Character.Components;
using GameLogic.GameLogic;
using GameLogic.GameLogic.AI;
using GameLogic.GameLogic.AI.AIInterface;
using GameLogic.GameLogic.Controller;
using GameLogic.GameLogic.ENUMS;
using GameLogic.GameLogic.Interface;
using GameLogic.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    class RoundController
    {
        /* TODO
         * Need to return some data to say where audio files are.
         *     IDEA
         *  data is put in some sort of json format and passed through class that splits in and query's database to get specific file location
         *  
         *     WHAT IS NEEDED
         *  Need to know how data is stored, file name wise
         */

        public void RunFightCommand(UserInput userInput, Room room, Characters user)
        {
            /*      TODO
             *  Create Method for enemy turn that get's called after player
             *  If player chooses to block or dodge then the controller for that class will handle enemy turn as well.
             * 
             */
            RoundResult results;
            IActionHandler controller;
            string CharacterString = "";
            string audioFileNames = "";
            switch(user.GetType().Name)
            {
                case "ThrillSeeker":
                    controller = new ThrillController();
                    CharacterString = "ThrillSeeker";
                    break;
                default:
                    controller = new ThrillController();
                    break;
            }
            int hitPointData = 0;
            user.ResetCharacter();
            switch (userInput)
            {
                //      IDEA
                // Put all the audio files together in one string to return
                // seperate each by | and then the server can split each one or whatever class to get the individual file.
                case UserInput.A:
                    //Attack
                    results = controller.Attack(user, room.Enemy, hitPointData);
                    audioFileNames = ResultFileStyle(results, room.Enemy);
                    break;
                case UserInput.S:
                    //Block
                    results = controller.Block(user, room.Enemy);


                    break;
                case UserInput.D:
                    //Dodge
                    results = controller.Dodge(user, room.Enemy);

                    break;
                case UserInput.Q:
                    //Tactical
                    results = controller.Tactical(user, room.Enemy, hitPointData);

                    break;
                case UserInput.W:
                    //Utility
                    results = controller.Utility(user, room.Enemy, hitPointData);

                    break;
                case UserInput.E:
                    //Ultimate
                    results = controller.Ultimate(user, room.Enemy, hitPointData);

                    break;
            }
        }

        public void EnemyTurn(Room room, Characters user)
        {
            RoundResult results;
            IActionHandler controller;
            IAI ai;
            int hitPointData = 0;
            switch (room.Enemy.GetType().Name)
            {
                case "WaterGoblin":
                    controller = new WaterController();
                    ai = new WaterGoblinAI();
                    break;
                default:
                    controller = new WaterController();
                    ai = new WaterGoblinAI();
                    break;
            }
            UserInput enemyMove = ai.MakeMove(room.Enemy);
            switch (enemyMove)
            {
                case UserInput.A:
                    //Attack
                    results = controller.Attack(room.Enemy, user, hitPointData);

                    break;
                case UserInput.S:
                    //Block
                    results = controller.Block(room.Enemy, user);


                    break;
                case UserInput.D:
                    //Dodge
                    results = controller.Dodge(room.Enemy, user);

                    break;
                case UserInput.Q:
                    //Tactical
                    results = controller.Tactical(room.Enemy, user, hitPointData);

                    break;
                case UserInput.W:
                    //Utility
                    results = controller.Utility(room.Enemy, user, hitPointData);

                    break;
                case UserInput.E:
                    //Ultimate
                    results = controller.Ultimate(room.Enemy, user, hitPointData);

                    break;
            }

        }

        private string ResultFileStyle(RoundResult result, Characters enemy)
        {
            switch (result)
            {
                case RoundResult.HIT:
                    return enemy.GetType().Name + "Hit.wav";
                case RoundResult.CRITICAL:
                    return "Critical.wav";
                case RoundResult.BLOCKED:
                    return "Blocked.wav";
                case RoundResult.MISSED:
                    return "Missed.wav";
                default:
                    return "Error.wav";
            }

        }
    }
}
