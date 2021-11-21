using GameLogic.Character.Components;
using GameLogic.Character.Interfaces;
using GameLogic.GameLogic.AI.AIInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.AI.AIComponents
{
    public class GigAI : GruntAI
    {
        /*
         * GigaWatt doesn't move much but has a high block. He also has nodge
         * so for his move set he's not going to have a chance of dodging. 
         * To make the fight more fair I'll make the chances of him choosing
         * to block lower than using his tactical or attacking.
         */
        public UserInput MakeMove(ref Grunt character)
        {
            Random generator = new Random();

            UserInput[] moveSet = { UserInput.A, UserInput.Q, UserInput.S, UserInput.Q, UserInput.A };

            int moveChoice = generator.Next(moveSet.Length);

            if(moveSet[moveChoice] == UserInput.Q)
            {
                if(character.TacticalCooldown > 0)
                {
                    return moveSet[moveChoice - 1];
                } else
                {
                    return UserInput.Q;
                }
            } else
            {
                return moveSet[moveChoice];
            }
        }
    }
}
