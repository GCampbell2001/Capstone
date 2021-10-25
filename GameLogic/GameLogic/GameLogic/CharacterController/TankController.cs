using GameLogic.Character.Components;
using GameLogic.Character.Interfaces;
using GameLogic.GameLogic.Controller;
using GameLogic.GameLogic.ENUMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.CharacterController
{
    public class TankController : GeneralCharacterController
    {
        public override RoundResult UserTactical(Biggie player, ICharacter enemy, int importantData)
        {
            int hammerDamage = player.Tactical();
            int hammerAccuracy = player.Accuracy();

            int enemyDodgeAttempt = enemy.Dodge();

            if (hammerAccuracy > (enemyDodgeAttempt + 40))
            {
                hammerDamage = hammerDamage * 2;
                CheckBlockWithoutItems(enemy, hammerDamage, importantData);
                return RoundResult.CRITICAL;
            }
            else if (hammerAccuracy >= enemyDodgeAttempt)
            {
                return CheckBlockWithoutItems(enemy, hammerDamage, importantData);

            }
            else if (hammerAccuracy < enemyDodgeAttempt)
            {
                return RoundResult.MISSED;

            }
            else
            {
                Console.WriteLine("TankController.cs - Line 58 \r\nNo Conditional Met");
                Console.WriteLine("Tank Accuracy: " + hammerAccuracy);
                Console.WriteLine("Enemy Dodge: " + enemyDodgeAttempt);
                return RoundResult.MISSED;
            }
        }

        public override RoundResult UserUltimate(Biggie player, ICharacter enemy, int importantData)
        {
            importantData = player.Ultimate();
            return RoundResult.HEALED;
        }

        public override RoundResult PCUtility(Biggie player, int importantData)
        {
            player.Utility();
            return RoundResult.TEMPBLOCK;
        }
    }
}
