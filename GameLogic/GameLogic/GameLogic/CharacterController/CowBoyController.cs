using GameLogic.Character.Components;
using GameLogic.GameLogic.Controller;
using GameLogic.GameLogic.ENUMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.CharacterController
{
    class CowBoyController : GeneralCharacterController
    {
        public override RoundResult BossTactical(ref Biggie player, ref Biggie boss, ref int importantData)
        {
            boss.Tactical();
            return RoundResult.BUFFED;
        }

        public override RoundResult PCUtility(ref Biggie player, ref int importantData)
        {
            player.Utility();
            return RoundResult.BUFFED;
        }

        public override RoundResult BossUltimate(ref Biggie player, ref Biggie boss, ref int importantData)
        {
            int bossDamage = boss.Ultimate();
            importantData = bossDamage;
            player.LowerHealth(bossDamage);
            return RoundResult.HIT;
        }
    }
}
