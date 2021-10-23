using GameLogic.Character.Components;
using GameLogic.GameLogic.ENUMS;
using GameLogic.GameLogic.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.Controller
{
    public class GeneralCharacterController : IActionHandler
    {
        public override RoundResult Attack(Characters player, Characters enemy, int importantData)
        {
            int damage = player.Attack();
            int accuracy = player.Accuracy();

            int enemyDodgeAttempt = enemy.Dodge();

            if (accuracy > (enemyDodgeAttempt + 40))
            {
                damage = damage * 2;
                CheckBlock(enemy, damage, importantData);
                return RoundResult.CRITICAL;
            }
            else if (accuracy >= enemyDodgeAttempt)
            {
                return CheckBlock(enemy, damage, importantData);

            }
            else if (accuracy < enemyDodgeAttempt)
            {
                return RoundResult.MISSED;

            }
            else
            {
                Console.WriteLine("GeneralCharacterController.cs - Line 38 \r\nNo Conditional Met");
                Console.WriteLine("Player Accuracy: " + accuracy);
                Console.WriteLine("Enemy Dodge: " + enemyDodgeAttempt);
                return RoundResult.MISSED;
            }
        }

        public override RoundResult Block(Characters player, Characters enemy)
        {
            player.AttemptBlock();
            return RoundResult.TEMPBLOCK;
        }

        public override RoundResult Dodge(Characters player, Characters enemy)
        {
            player.AttemptDodge();
            return RoundResult.TEMPDODGE;
        }

        public override RoundResult Tactical(Characters player, Characters enemy, int importantData)
        {
            //This is overriden by Character Specific Controller
            throw new NotImplementedException();
        }

        public override RoundResult Ultimate(Characters player, Characters enemy, int importantData)
        {
            //This is overriden by Character Specific Controller
            throw new NotImplementedException();
        }

        public override RoundResult Utility(Characters player, Characters enemy, int importantData)
        {
            //This is overriden by Character Specific Controller
            throw new NotImplementedException();
        }
    }
}
