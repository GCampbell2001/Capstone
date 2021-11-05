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
                case "GameLogic.Character.PC.ThrillSeeker":
                    controller = new ThrillController();
                    break;
                case "GameLogic.Character.PC.Tank":
                    controller = new TankController();
                    break;
                case "GameLogic.Character.PC.Brawler":
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
            GruntAI ai;
            int hitPointData = 0;
            string audioFileNames = "";
            switch (grunt.GetType().Name)
            {
                case "GameLogic.Character.Grunts.WaterGoblin":
                    controller = new WaterController();
                    ai = new WaterGoblinAI();
                    break;
                case "GameLogic.Character.Grunts.Sqwaubler":
                    controller = new SqwaublerController();
                    ai = new SqwaublerAI();
                    break;
                case "GameLogic.Character.Grunts.GigaWatt":
                    controller = new WattController();
                    ai = new GigAI();
                    break;
                case "GameLogic.Character.Grunts.JimKin":
                    controller = new JimController();
                    ai = new JimAI();
                    break;
                default:
                    controller = new WaterController();
                    ai = new WaterGoblinAI();
                    break;
            }
            UserInput gruntMove = ai.MakeMove(grunt);
            switch (gruntMove)
            {
                case UserInput.A:
                    //Attack
                    results = controller.Attack(grunt, user, hitPointData);
                    audioFileNames = prepare.AttackFileStyle(gruntMove, grunt, user);
                    audioFileNames += "|" + prepare.ResultFileStyle(results, user, grunt);
                    audioFileNames += "|" + prepare.HitpointFileStyle(hitPointData);
                    return audioFileNames;
                case UserInput.S:
                    //Block
                    results = controller.Block(grunt, user);
                    return prepare.AttackFileStyle(gruntMove, grunt, user);
                case UserInput.D:
                    //Dodge
                    results = controller.Dodge(grunt, user);
                    return prepare.AttackFileStyle(gruntMove, grunt, user);
                case UserInput.Q:
                    //Tactical
                    results = controller.GruntTactical(grunt, user, hitPointData);
                    return prepare.AbilityFileName(grunt, gruntMove, user, results, hitPointData);
                default:
                    return "ERROR.wav - RoundController Problem with enemyMove - " + gruntMove;
            }

        }

        public string BossTurn(Biggie boss, Biggie player) 
        {
            /*
             * This method handles the Boss's turn in a fight
             */

            RoundResult results;
            GeneralCharacterController controller;
            BossAI ai;
            int hitPointData = 0;
            string audioFileNames = "";
            switch (boss.GetType().Name)
            {
                case "GameLogic.Character.PC.InfernalWish":
                    controller = new InfernalController();
                    ai = new InfernalAI();
                    break;
                default:
                    controller = new InfernalController();
                    ai = new InfernalAI();
                    break;
            }
            UserInput bossMove = ai.MakeMove(boss);
            switch (bossMove)
            {
                case UserInput.A:
                    //Attack
                    results = controller.Attack(boss, player, hitPointData);
                    audioFileNames = prepare.AttackFileStyle(bossMove, boss, player);
                    audioFileNames += "|" + prepare.ResultFileStyle(results, player, boss);
                    audioFileNames += "|" + prepare.HitpointFileStyle(hitPointData);
                    return audioFileNames;
                case UserInput.S:
                    //Block
                    results = controller.Block(boss, player);
                    return prepare.AttackFileStyle(bossMove, boss, player);
                case UserInput.D:
                    //Dodge
                    results = controller.Dodge(boss, player);
                    return prepare.AttackFileStyle(bossMove, boss, player);
                case UserInput.Q:
                    //Tactical
                    results = controller.BossTactical(player, boss, hitPointData);
                    return prepare.AbilityFileName(boss, bossMove, player, results, hitPointData);
                case UserInput.W:
                    //Utility
                    results = controller.PCUtility(boss, hitPointData);
                    return prepare.AbilityFileName(boss, bossMove, player, results, hitPointData);
                default:
                    return "ERROR.wav - RoundController Problem with bossMove - " + bossMove;
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
