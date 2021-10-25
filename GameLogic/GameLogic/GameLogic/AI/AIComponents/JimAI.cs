using GameLogic.Character.Components;
using GameLogic.GameLogic.AI.AIInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.AI.AIComponents
{
    public class JimAI : IAI
    { 
        /* JimKin's whole things is doubling in size. This however also doubles
         * his attack so he needs to handled quickly. I'll start by making the chances 
         * of being doubled 50%. If it's too OP then I'll change it to everything
         * having equal chance;
         */


        public UserInput MakeMove(Characters character)
        {
            Random generator = new Random();

            UserInput[] jimMoves = { UserInput.Q, UserInput.A, UserInput.Q, UserInput.S, UserInput.Q, UserInput.D };

            int moveChoice = generator.Next(jimMoves.Length);
            if(jimMoves[moveChoice] == UserInput.Q)
            {
                if(character.TacticalCooldown > 0)
                {
                    return jimMoves[moveChoice + 1];   
                } else if (character.TacticalCooldown <= 0)
                {
                    return jimMoves[moveChoice];
                } else
                {
                    //This is my failsafe incase something doesn't work
                    //Help prevent application crashes.
                    return UserInput.D;
                }
            }
            else
            {
                return jimMoves[moveChoice];
            }

        }
    }
}
