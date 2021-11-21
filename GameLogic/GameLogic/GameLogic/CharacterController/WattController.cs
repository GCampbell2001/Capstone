using GameLogic.Character.Components;
using GameLogic.Character.Decorators;
using GameLogic.GameLogic.Controller;
using GameLogic.GameLogic.ENUMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.CharacterController
{
    public class WattController : GeneralCharacterController
    {
        /*
         * GigaWatt's Tactical applies a debuff to the opponent.
         */

        public override RoundResult GruntTactical(ref Grunt grunt, ref Biggie player, ref int importantData)
        {
            grunt.Tactical();
            player.AddItem(new Magnetize(player));
            return RoundResult.DEBUFFED;
        }
    }
}
