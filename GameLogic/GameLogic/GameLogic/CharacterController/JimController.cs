using GameLogic.Character.Components;
using GameLogic.Character.Interfaces;
using GameLogic.GameLogic.Controller;
using GameLogic.GameLogic.ENUMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.CharacterController
{
    public class JimController : GeneralCharacterController
    {
        public override RoundResult GruntTactical(Grunt grunt, Biggie player, int importantData)
        {
            grunt.Tactical();
            return RoundResult.BUFFED;
        }
    }
}
