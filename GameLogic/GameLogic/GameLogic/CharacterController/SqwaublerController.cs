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
    public class SqwaublerController : GeneralCharacterController
    {
        /*
         * The Squabler Tactical applies a debuff to the opponent.
         */

        public override RoundResult GruntTactical(ref Grunt grunt, ref Biggie player, ref int importantData)
        {
            grunt.Tactical();
            player.AddItem(new Screech(player));
            return RoundResult.DEBUFFED;
        }
    }
}
