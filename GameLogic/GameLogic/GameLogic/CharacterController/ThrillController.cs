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
        public RoundResult Attack(ICharacter player, ICharacter enemy, int importantData)
        {
            /* 
             * This method determines if the characters have any items.
             * If they do It creates the stack and calls the appropreite attack method.
             */

            List<ModTool> playerItems = player.GetItems();
            if(playerItems.Count <= 0)
            {
                return this.AttackWithoutItems(player, enemy, importantData);
            } else
            {

            }

            return this.Attack(player, enemy, importantData);
        }
        

        public override RoundResult UserTactical(Biggie player, ICharacter enemy, int importantData)
        {
            int daggerDamage = player.Tactical();
            int daggerAccuracy = player.Accuracy();

            int enemyDodgeAttempt = enemy.Dodge();

            if(daggerAccuracy > (enemyDodgeAttempt + 40))
            {
                daggerDamage = daggerDamage * 2;
                CheckBlockWithoutItems(enemy, daggerDamage, importantData);
                return RoundResult.CRITICAL;
            } else if(daggerAccuracy >= enemyDodgeAttempt)
            {
                return CheckBlockWithoutItems(enemy, daggerDamage, importantData);

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
        public override RoundResult UserUltimate(Biggie player, ICharacter enemy, int importantData)
        {
            int ghostDamage = player.Ultimate();
            return CheckBlockWithoutItems(enemy, ghostDamage, importantData);
        }

        public override RoundResult PCUtility(Biggie player, int importantData)
        {
            player.Utility();
            return RoundResult.CRITICAL;
        }
    }
}
