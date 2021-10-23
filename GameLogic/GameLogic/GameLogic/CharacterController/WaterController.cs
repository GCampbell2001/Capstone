using GameLogic.Character.Components;
using GameLogic.GameLogic.ENUMS;
using GameLogic.GameLogic.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.Controller
{
    public class WaterController : GeneralCharacterController
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
            importantData = player.Tactical();
            return RoundResult.HEALED;
        }

        public override RoundResult Ultimate(Characters player, Characters enemy, int importantData)
        {
            throw new NotImplementedException();
        }

        public override RoundResult Utility(Characters player, Characters enemy, int importantData)
        {
            //Nothing to Implement here
            throw new NotImplementedException();
        }
    }
}
