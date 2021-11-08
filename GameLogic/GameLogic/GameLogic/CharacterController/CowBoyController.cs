﻿using GameLogic.Character.Components;
using GameLogic.GameLogic.Controller;
using GameLogic.GameLogic.ENUMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.CharacterController
{
    class CowBoyController : GeneralCharacterController
    {
        public override RoundResult BossTactical(Biggie player, Biggie boss, int importantData)
        {
            boss.Tactical();
            return RoundResult.BUFFED;
        }

        public override RoundResult PCUtility(Biggie player, int importantData)
        {
            player.Utility();
            return RoundResult.BUFFED;
        }

        public override RoundResult BossUltimate(Biggie player, Biggie boss, int importantData)
        {
            int bossDamage = boss.Ultimate();
            player.LowerHealth(bossDamage);
            return RoundResult.HIT;
        }
    }
}