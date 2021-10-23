using GameLogic.Character.Components;
using GameLogic.GameLogic.ENUMS;
using GameLogic.GameLogic.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.Controller
{
    public class ThrillController : GeneralCharacterController
    {
        public override RoundResult Attack(Characters player, Characters enemy, int importantData)
        {
            return base.Attack(player, enemy, importantData);
        }

        public override RoundResult Block(Characters player, Characters enemy)
        {
            return base.Block(player, enemy);
        }

        public override RoundResult Dodge(Characters player, Characters enemy)
        {
            return base.Dodge(player, enemy);
        }

        public override RoundResult Tactical(Characters player, Characters enemy, int importantData)
        {
            int daggerDamage = player.Tactical();
            int daggerAccuracy = player.Accuracy();

            int enemyDodgeAttempt = enemy.Dodge();

            if(daggerAccuracy > (enemyDodgeAttempt + 40))
            {
                daggerDamage = daggerDamage * 2;
                CheckBlock(enemy, daggerDamage, importantData);
                return RoundResult.CRITICAL;
            } else if(daggerAccuracy >= enemyDodgeAttempt)
            {
                return CheckBlock(enemy, daggerDamage, importantData);

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

        public override RoundResult Ultimate(Characters player, Characters enemy, int importantData)
        {
            //No need to check dodge and accuracy since this attack is a guaranteed hit
            int ghostDamage = player.Ultimate();
            return CheckBlock(enemy, ghostDamage, importantData);

        }

        public override RoundResult Utility(Characters player, Characters enemy, int importantData)
        {
            player.Utility();
            return RoundResult.CRITICAL;
        }
    }
}
