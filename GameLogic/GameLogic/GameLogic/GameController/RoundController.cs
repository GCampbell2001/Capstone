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
        AudioFilePrepare prepare = new AudioFilePrepare();

        public string RunFightCommand(UserInput userInput, Room room, Characters user)
        {
            /*
             * This method handles what happens when the user is in a fight
             */

            RoundResult results;
            IActionHandler controller;
            string audioFileNames = "";
            switch(user.GetType().Name)
            {
                case "ThrillSeeker":
                    controller = new ThrillController();
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
                    audioFileNames = prepare.AttackFileStyle(userInput, user, room.Enemy);
                    audioFileNames += "|" + prepare.ResultFileStyle(results, room.Enemy, user);
                    audioFileNames += "|" + prepare.HitpointFileStyle(hitPointData);
                    return audioFileNames;
                case UserInput.S:
                    //Block
                    results = controller.Block(user, room.Enemy);
                    return prepare.AttackFileStyle(userInput, user, room.Enemy);
                case UserInput.D:
                    //Dodge
                    results = controller.Dodge(user, room.Enemy);
                    return prepare.AttackFileStyle(userInput, user, room.Enemy);
                case UserInput.Q:
                    //Tactical
                    results = controller.Tactical(user, room.Enemy, hitPointData);
                    return prepare.AbilityFileName(user, userInput, room.Enemy, results, hitPointData);
                case UserInput.W:
                    //Utility
                    results = controller.Utility(user, room.Enemy, hitPointData);
                    return prepare.AbilityFileName(user, userInput, room.Enemy, results, hitPointData);
                case UserInput.E:
                    //Ultimate
                    results = controller.Ultimate(user, room.Enemy, hitPointData);
                    return prepare.AbilityFileName(user, userInput, room.Enemy, results, hitPointData);
                default:
                    return "ERROR.wav - RoundController Problem with userInput - " + userInput;
            }
        }

        public string EnemyTurn(Room room, Characters user)
        {
            /*
             * This method handles enemy's turns in fights
             */

            RoundResult results;
            IActionHandler controller;
            IAI ai;
            int hitPointData = 0;
            string audioFileNames = "";
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
                    audioFileNames = prepare.AttackFileStyle(enemyMove, room.Enemy, user);
                    audioFileNames += "|" + prepare.ResultFileStyle(results, user, room.Enemy);
                    audioFileNames += "|" + prepare.HitpointFileStyle(hitPointData);
                    return audioFileNames;
                case UserInput.S:
                    //Block
                    results = controller.Block(room.Enemy, user);
                    return prepare.AttackFileStyle(enemyMove, room.Enemy, user);
                case UserInput.D:
                    //Dodge
                    results = controller.Dodge(room.Enemy, user);
                    return prepare.AttackFileStyle(enemyMove, room.Enemy, user);
                case UserInput.Q:
                    //Tactical
                    results = controller.Tactical(room.Enemy, user, hitPointData);
                    return prepare.AbilityFileName(room.Enemy, enemyMove, user, results, hitPointData);
                case UserInput.W:
                    //Utility
                    results = controller.Utility(room.Enemy, user, hitPointData);
                    return prepare.AbilityFileName(room.Enemy, enemyMove, user, results, hitPointData);
                case UserInput.E:
                    //Ultimate
                    results = controller.Ultimate(room.Enemy, user, hitPointData);
                    return prepare.AbilityFileName(room.Enemy, enemyMove, user, results, hitPointData);
                default:
                    return "ERROR.wav - RoundController Problem with enemyMove - " + enemyMove;
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
