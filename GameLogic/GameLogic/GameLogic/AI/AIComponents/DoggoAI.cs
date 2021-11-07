using GameLogic.Character.Components;
using GameLogic.GameLogic.AI.AIInterface;
using GameLogic.Character.Components;
using GameLogic.GameLogic.AI.AIInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic.GameLogic.AI.AIComponents
{
    public class DoggoAI : BossAI
    {
        /*
         * Doggo naturally tries to dodge attacks and use his tactical (barking)
         * He has the same chance to block as he does to attack.  He doesn't try to use his ultimate
         * until he reaches half health. This is to help keep Doggo from being too OP
         */
        public UserInput MakeMove(Biggie character)
        {
            List<UserInput> normalMoveSet = new List<UserInput>() { UserInput.Q, UserInput.Q, UserInput.A, UserInput.S, UserInput.D, UserInput.W };
            List<UserInput> halfHealthMoveSet = new List<UserInput> { UserInput.Q, UserInput.W, UserInput.E, UserInput.A, UserInput.S, UserInput.D };
            UserInput normalMoveChoice = normalMoveSet.OrderBy(m => new Random().Next()).ElementAt(0);
            UserInput halfHealthMoveChoice = halfHealthMoveSet.OrderBy(m => new Random().Next()).ElementAt(0);
            if(character.health <= character.health / 2)
            {
                return tryBasicMove(character, halfHealthMoveChoice);
            } else
            {
                return tryBasicMove(character, normalMoveChoice);
            }
        }

        private UserInput tryBasicMove(Biggie character, UserInput normalMoveChoice)
        {
            if (normalMoveChoice == UserInput.Q && character.TacticalCooldown > 0)
            {
                return UserInput.D;
            }
            else if (normalMoveChoice == UserInput.W && character.UtilityCooldown > 0)
            {
                return UserInput.D;
            }
            else if (normalMoveChoice == UserInput.E && character.UltimateCooldown > 0)
            {
                return UserInput.A;
            } else
            {
                return normalMoveChoice;
            }
        }
    }
}
