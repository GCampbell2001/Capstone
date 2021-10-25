﻿using GameLogic.Character.Components;
using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.AI.AIInterface
{
    public interface IAI
    {
        public UserInput MakeMove(ICharacter character);


    }
}
