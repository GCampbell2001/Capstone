using GameLogic.Character;
using GameLogic.Character.Components;
using GameLogic.Character.Interfaces;
using GameLogic.GameLogic.ENUMS;
using GameLogic.GameLogic.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.Controller
{
    public class ThrillController : GeneralCharacterController
    {  
        public override RoundResult UserTactical(ref Biggie player, ref ICharacter enemy, ref int importantData)
        {
            int daggerDamage = player.Tactical();
            int daggerAccuracy = player.Accuracy();

            int enemyDodgeAttempt = enemy.Dodge();

            if(daggerAccuracy > (enemyDodgeAttempt + 40))
            {
                daggerDamage = daggerDamage * 2;
                CheckBlockWithoutItems(ref enemy, daggerDamage, ref importantData);
                return RoundResult.CRITICAL;
            } else if(daggerAccuracy >= enemyDodgeAttempt)
            {
                return CheckBlockWithoutItems(ref enemy, daggerDamage, ref importantData);

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
        public override RoundResult UserUltimate(ref Biggie player, ICharacter enemy, ref int importantData)
        {
            int ghostDamage = player.Ultimate();
            return CheckBlockWithoutItems(ref enemy, ghostDamage, ref importantData);
        }

        public override RoundResult PCUtility(ref Biggie player, ref int importantData)
        {
            player.Utility();
            return RoundResult.CRITICAL;
        }
    }
}
