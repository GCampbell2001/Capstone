using GameLogic.Character.Components;
using GameLogic.GameLogic.ENUMS;
using GameLogic.GameLogic.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.Controller
{
    public class ThrillController : IActionHandler
    {
        public override RoundResult Attack(Characters player, Characters enemy)
        {
            int hookDamage = player.Attack();
            int hookAccuracy = player.Accuracy();

            int enemyDodgeAttempt = enemy.Dodge();

            if (hookAccuracy > (enemyDodgeAttempt + 40))
            {
                hookDamage = hookDamage * 2;
                CheckBlock(enemy, hookDamage);
                return RoundResult.CRITICAL;
            }
            else if (hookAccuracy >= enemyDodgeAttempt)
            {
                return CheckBlock(enemy, hookDamage);

            }
            else if (hookAccuracy < enemyDodgeAttempt)
            {
                return RoundResult.MISSED;

            }
            else
            {
                Console.WriteLine("ThrillController.cs - Line 38 \r\nNo Conditional Met");
                Console.WriteLine("ThrillSeeker Accuracy: " + hookAccuracy);
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

        public override RoundResult Tactical(Characters player, Characters enemy)
        {
            int daggerDamage = player.Tactical();
            int daggerAccuracy = player.Accuracy();

            int enemyDodgeAttempt = enemy.Dodge();

            if(daggerAccuracy > (enemyDodgeAttempt + 40))
            {
                daggerDamage = daggerDamage * 2;
                CheckBlock(enemy, daggerDamage);
                return RoundResult.CRITICAL;
            } else if(daggerAccuracy >= enemyDodgeAttempt)
            {
                return CheckBlock(enemy, daggerDamage);

            } else if(daggerAccuracy < enemyDodgeAttempt)
            {
                return RoundResult.MISSED;

            }  else
            {
                Console.WriteLine("ThrillController.cs - Line 58 \r\nNo Conditional Met");
                Console.WriteLine("ThrillSeeker Accuracy: " + daggerAccuracy);
                Console.WriteLine("Enemy Dodge: " + enemyDodgeAttempt);
                return RoundResult.MISSED;
            }

            
        }

        public override RoundResult Ultimate(Characters player, Characters enemy)
        {
            //No need to check dodge and accuracy since this attack is a guaranteed hit
            int ghostDamage = player.Ultimate();
            return CheckBlock(enemy, ghostDamage);

        }

        public override RoundResult Utility(Characters player, Characters enemy)
        {
            player.Utility();
            return RoundResult.CRITICAL;
        }
    }
}
