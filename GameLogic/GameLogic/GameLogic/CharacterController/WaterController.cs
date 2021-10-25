﻿using GameLogic.Character.Components;
using GameLogic.Character.Interfaces;
using GameLogic.GameLogic.ENUMS;
using GameLogic.GameLogic.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.Controller
{
    public class WaterController : GeneralCharacterController
    {

        public override RoundResult GruntTactical(Grunt grunt, Biggie player, int importantData)
        {
            importantData = grunt.Tactical();
            return RoundResult.HEALED;
        }
    }
}
