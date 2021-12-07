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
    public class RoundController
    {
        AudioFilePrepare prepare = new AudioFilePrepare();

        public string PlayerTurn(UserInput userInput, ICharacter enemy, Biggie user)
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
                    results = controller.Attack(user, enemy, ref hitPointData);
                    audioFileNames = prepare.AttackFileStyle(userInput, user, enemy);
                    audioFileNames += "|" + prepare.ResultFileStyle(results, enemy, user);
                    audioFileNames += "|" + prepare.HitpointFileStyle(hitPointData);
                    return audioFileNames;
                case UserInput.S:
                    //Block
                    results = controller.Block(user, enemy);
                    return prepare.AttackFileStyle(userInput, user, enemy);
                case UserInput.D:
                    //Dodge
                    results = controller.Dodge(user, enemy);
                    return prepare.AttackFileStyle(userInput, user, enemy);
                case UserInput.Q:
                    //Tactical
                    results = controller.UserTactical(ref user, ref enemy, ref hitPointData);
                    return prepare.AbilityFileName(user, userInput, enemy, results, hitPointData);
                case UserInput.W:
                    //Utility
                    results = controller.PCUtility(ref user, ref hitPointData);
                    return prepare.AbilityFileName(user, userInput, enemy, results, hitPointData);
                case UserInput.E:
                    //Ultimate
                    results = controller.UserUltimate(ref user, enemy, ref hitPointData);
                    return prepare.AbilityFileName(user, userInput, enemy, results, hitPointData);
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
            UserInput gruntMove = ai.MakeMove(ref grunt);
            switch (gruntMove)
            {
                case UserInput.A:
                    //Attack
                    results = controller.Attack(grunt, user, ref hitPointData);
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
                    results = controller.GruntTactical(ref grunt, ref user, ref hitPointData);
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
                case "InfernalWish":
                    controller = new InfernalController();
                    ai = new InfernalAI();
                    break;
                case "Doggo":
                    controller = new DoggoController();
                    ai = new DoggoAI();
                    break;
                case "Cowboy":
                    controller = new CowBoyController();
                    ai = new CowboyAI();
                    break;
                default:
                    controller = new InfernalController();
                    ai = new InfernalAI();
                    break;
            }
            UserInput bossMove = ai.MakeMove(ref boss);
            switch (bossMove)
            {
                case UserInput.A:
                    //Attack
                    results = controller.Attack(boss, player, ref hitPointData);
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
                    results = controller.BossTactical(ref player, ref boss, ref hitPointData);
                    return prepare.AbilityFileName(boss, bossMove, player, results, hitPointData);
                case UserInput.W:
                    //Utility
                    results = controller.PCUtility(ref boss, ref hitPointData);
                    return prepare.AbilityFileName(boss, bossMove, player, results, hitPointData);
                case UserInput.E:
                    //Ultimate
                    results = controller.BossUltimate(ref player, ref boss, ref hitPointData);
                    return prepare.AbilityFileName(boss, bossMove, player, results, hitPointData);
                default:
                    return "ERROR.wav - RoundController Problem with bossMove - " + bossMove;
            }

        }

        public void Update(Biggie user, Biggie boss, Grunt grunt, List<string> audioFiles)
        {
            /*
             * Taking in all possible characters and determining which is null here so I utilize class specific methods for enemies.   
             * player first and then enemy 
             */
            prepare.PlayerUpdate(user, audioFiles);
            if(grunt == null)
            {
                prepare.BossUpdate(boss, audioFiles);
            } else
            {
                prepare.GruntUpdate(grunt, audioFiles);
            }

        }


        //Commented out this method. Going to test it later. If it turns out I'm good to delete this I will

        //private string ResultFileStyle(RoundResult result, ICharacter enemy)
        //{
        //    switch (result)
        //    {
        //        case RoundResult.HIT:
        //            return prepare.GetCharacterName(enemy) + "Hit.wav";
        //        case RoundResult.CRITICAL:
        //            return "Critical.wav";
        //        case RoundResult.BLOCKED:
        //            return "Blocked.wav";
        //        case RoundResult.MISSED:
        //            return "Missed.wav";
        //        default:
        //            return "Error.wav";
        //    }

        //}

    }
}
