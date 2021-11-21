using GameLogic.Character.Components;
using GameLogic.Character.Grunts;
using GameLogic.Character.Interfaces;
using GameLogic.GameLogic.AI.AIInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.AI
{
    public class WaterGoblinAI : GruntAI
    {
        /* WaterGoblin's only ability is to heal himself. 
         * Because of this he only heal's if he is below half health.
         * WaterGoblin is more likely to try and dodge an attack than anything else.
         * So there is 50% chance the goblin will attempt a dodge and 25% chance to do anything else.
         * if he is below half health he is 40% likely to try and heal 20% likely to any of the other options.
         */

        public UserInput MakeMove(ref Grunt character)
        {
            Random generator = new Random();

            UserInput[] highChances = { UserInput.D, UserInput.S, UserInput.D, UserInput.A };
            UserInput[] lowChances = { UserInput.Q, UserInput.D, UserInput.A, UserInput.Q, UserInput.S };

            int moveChoice;

            if (character.GetHealth() < character.baseHealth/2)
            {
                moveChoice = generator.Next(lowChances.Length);
                if(lowChances[moveChoice] == UserInput.Q && character.TacticalCooldown <= 0)
                {
                    moveChoice = generator.Next(highChances.Length);
                    return highChances[moveChoice];
                } 
                return lowChances[moveChoice];

            } else
            {
                moveChoice = generator.Next(highChances.Length);
                return highChances[moveChoice];
            }
        }
    }
}
