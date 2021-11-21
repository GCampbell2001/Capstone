using GameLogic.Character.Components;
using GameLogic.Character.Decorators;
using GameLogic.Character.Interfaces;
using GameLogic.GameLogic.Controller;
using GameLogic.GameLogic.ENUMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.CharacterController
{
    public class InfernalController : GeneralCharacterController
    {
        public override RoundResult BossTactical(ref Biggie player, ref Biggie boss, ref int importantData)
        {
            int fireDamage = boss.Tactical();
            int fireAccuracy = boss.Accuracy();

            int enemyDodgeAttempt = player.Dodge();

            if (fireAccuracy > (enemyDodgeAttempt + 40))
            {
                fireDamage = fireDamage * 2;
                CheckBlockWithoutItems(ref player, fireDamage, ref importantData);
                player.AddItem(new Burn(player));
                return RoundResult.CRITICAL;
            }
            else if (fireAccuracy >= enemyDodgeAttempt)
            {
                player.AddItem(new Burn(player));
                return CheckBlockWithItems(ref player, fireDamage, ref importantData);
            }
            else if (fireAccuracy < enemyDodgeAttempt)
            {
                return RoundResult.MISSED;
            }
            else
            {
                Console.WriteLine("InfernalController.cs - Line 58 \r\nNo Conditional Met");
                Console.WriteLine("Infernal Accuracy: " + fireAccuracy);
                Console.WriteLine("Player Dodge: " + enemyDodgeAttempt);
                return RoundResult.MISSED;
            }
        }

        

        public override RoundResult PCUtility(ref Biggie player, ref int importantData)
        {
            player.Utility();
            return RoundResult.BUFFED;
        }

        public override RoundResult BossUltimate(ref Biggie player, ref Biggie boss, ref int importantData)
        {
            boss.Ultimate();
            return RoundResult.BUFFED;
        }
    }
}
