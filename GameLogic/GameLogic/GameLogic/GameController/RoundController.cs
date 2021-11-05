using GameLogic.Character;
using GameLogic.Character.Components;
using GameLogic.Character.Interfaces;
using GameLogic.GameLogic;
using GameLogic.GameLogic.AI;
using GameLogic.GameLogic.AI.AIComponents;
using GameLogic.GameLogic.AI.AIInterface;
using GameLogic.GameLogic.CharacterController;
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

        public string RunFightCommand(UserInput userInput, Room room, Biggie user)
        {
            /*
             * This method handles what happens when the user is in a fight
             */

            RoundResult results;
            GeneralCharacterController controller;
            string audioFileNames = "";
            switch(user.GetType().Name)
            {
                case "ThrillSeeker":
                    controller = new ThrillController();
                    break;
                case "Tank":
                    controller = new TankController();
                    break;
                case "Brawler":
                    controller = new BrawlerController();
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
                    results = controller.UserTactical(user, room.Enemy, hitPointData);
                    return prepare.AbilityFileName(user, userInput, room.Enemy, results, hitPointData);
                case UserInput.W:
                    //Utility
                    results = controller.PCUtility(user, hitPointData);
                    return prepare.AbilityFileName(user, userInput, room.Enemy, results, hitPointData);
                case UserInput.E:
                    //Ultimate
                    results = controller.UserUltimate(user, room.Enemy, hitPointData);
                    return prepare.AbilityFileName(user, userInput, room.Enemy, results, hitPointData);
                default:
                    return "ERROR.wav - RoundController Problem with userInput - " + userInput;
            }
        }

        public string GruntTurn(Grunt grunt, Biggie user)
        {
            /*
             * This method handles enemy's turns in fights
             */

            RoundResult results;
            GeneralCharacterController controller;
            EnemyAI ai;
            int hitPointData = 0;
            string audioFileNames = "";
            switch (grunt.GetType().Name)
            {
                case "WaterGoblin":
                    controller = new WaterController();
                    ai = new WaterGoblinAI();
                    break;
                case "Sqwaubler":
                    controller = new SqwaublerController();
                    ai = new SqwaublerAI();
                    break;
                case "GigaWatt":
                    controller = new WattController();
                    ai = new GigAI();
                    break;
                case "JimKin":
                    controller = new JimController();
                    ai = new JimAI();
                    break;
                default:
                    controller = new WaterController();
                    ai = new WaterGoblinAI();
                    break;
            }
            UserInput enemyMove = ai.MakeMove(grunt);
            switch (enemyMove)
            {
                case UserInput.A:
                    //Attack
                    results = controller.Attack(grunt, user, hitPointData);
                    audioFileNames = prepare.AttackFileStyle(enemyMove, grunt, user);
                    audioFileNames += "|" + prepare.ResultFileStyle(results, user, grunt);
                    audioFileNames += "|" + prepare.HitpointFileStyle(hitPointData);
                    return audioFileNames;
                case UserInput.S:
                    //Block
                    results = controller.Block(grunt, user);
                    return prepare.AttackFileStyle(enemyMove, grunt, user);
                case UserInput.D:
                    //Dodge
                    results = controller.Dodge(grunt, user);
                    return prepare.AttackFileStyle(enemyMove, grunt, user);
                case UserInput.Q:
                    //Tactical
                    results = controller.GruntTactical(grunt, user, hitPointData);
                    return prepare.AbilityFileName(grunt, enemyMove, user, results, hitPointData);
                default:
                    return "ERROR.wav - RoundController Problem with enemyMove - " + enemyMove;
            }

        }

        private string ResultFileStyle(RoundResult result, ICharacter enemy)
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
