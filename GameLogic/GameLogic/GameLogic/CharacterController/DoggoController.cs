using GameLogic.Character.Components;
using GameLogic.Character.Decorators;
using GameLogic.GameLogic.Controller;
using GameLogic.GameLogic.ENUMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.CharacterController
{
    public class DoggoController : GeneralCharacterController
    {
        public override RoundResult BossTactical(ref Biggie player, ref Biggie boss, ref int importantData)
        {
            boss.Tactical();
            player.AddItem(new Fear(player));
            return RoundResult.DEBUFFED;
        }

        public override RoundResult PCUtility(ref Biggie player, ref int importantData)
        {
            player.Utility();
            return RoundResult.BUFFED;
        }

        public override RoundResult BossUltimate(ref Biggie player, ref Biggie boss, ref int importantData)
        {
            int bossDamage = boss.Ultimate();
            player.LowerHealth(bossDamage);
            return RoundResult.HIT;
        }
    }
}
