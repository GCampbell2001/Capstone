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
        public override RoundResult BossTactical(Biggie player, Biggie boss, int importantData)
        {
            int fireDamage = boss.Tactical();
            int fireAccuracy = boss.Accuracy();

            int enemyDodgeAttempt = player.Dodge();

            if (fireAccuracy > (enemyDodgeAttempt + 40))
            {
                fireDamage = fireDamage * 2;
                CheckBlockWithoutItems(player, fireDamage, importantData);
                player.AddItem(new Burn(player));
                return RoundResult.CRITICAL;
            }
            else if (fireAccuracy >= enemyDodgeAttempt)
            {
                player.AddItem(new Burn(player));
                return CheckBlockWithoutItems(player, fireDamage, importantData);
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

        public override RoundResult PCUtility(Biggie player, int importantData)
        {
            player.Utility();
            return RoundResult.BUFFED;
        }

        public override RoundResult BossUltimate(Biggie player, Biggie boss, int importantData)
        {
            boss.Ultimate();
            return RoundResult.BUFFED;
        }
    }
}
