﻿using GameLogic.Character.Components;
using GameLogic.Character.Interfaces;
using GameLogic.GameLogic.Controller;
using GameLogic.GameLogic.ENUMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.CharacterController
{
    public class BrawlerController : GeneralCharacterController
    {

        public override RoundResult UserTactical(Biggie player, ICharacter enemy, int importantData)
        {
            int attackDamage = player.Tactical();
            int attackAccuracy = player.Accuracy();

            int enemyDodgeAttempt = enemy.Dodge();

            if (attackAccuracy > (enemyDodgeAttempt + 40))
            {
                attackDamage = attackDamage * 2;
                CheckBlockWithoutItems(enemy, attackDamage, importantData);
                player.raiseHealth(importantData);
                return RoundResult.CRITICAL;
            }
            else if (attackAccuracy >= enemyDodgeAttempt)
            {
                RoundResult result = CheckBlockWithoutItems(enemy, attackDamage, importantData);
                player.raiseHealth(importantData);
                return result;
            }
            else if (attackAccuracy < enemyDodgeAttempt)
            {
                return RoundResult.MISSED;

            }
            else
            {
                Console.WriteLine("BrawlerController.cs - Line 58 \r\nNo Conditional Met");
                Console.WriteLine("Brawler Accuracy: " + attackAccuracy);
                Console.WriteLine("Enemy Dodge: " + enemyDodgeAttempt);
                return RoundResult.MISSED;
            }
        }
        public override RoundResult PCUtility(Biggie player, int importantData)
        {
            player.Utility();
            return RoundResult.BUFFED;
        }
        public override RoundResult UserUltimate(Biggie player, ICharacter enemy, int importantData)
        {
            player.Ultimate();
            return RoundResult.BUFFED;
        }
    }
}
