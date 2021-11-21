using GameLogic.Character.Components;
using GameLogic.GameLogic.AI.AIInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic.GameLogic.AI.AIComponents
{
    public class CowboyAI : BossAI
    {
        /*
         * Cowboy will never attempt to block an attack. Instead he tries to dodge to make up for it. Beyond that he has equal chance to Attack, use his Tactical,
         * or use his Utility. If he uses his utiltiy he will immeditaly try to attack by either doing the general attack or his ultimate.
         * he won't use his ultimate until he loses at least a quarter health. It's not a lot of health loss but he won't start the engagement of with it (if that's even possible)
         * If tries to use his ultimate but can't then he will try to use his tactical instead. If he can't use his tactical then he will use his base attack. However, if his
         * Utility is active then he'll go straight for his general attack instead of his tactical.
         */
        public UserInput MakeMove(ref Biggie character)
        {
            List<UserInput> normalMoveSet = new List<UserInput>() { UserInput.Q, UserInput.W, UserInput.A, UserInput.D, UserInput.D };
            List<UserInput> threeQuartersHealthMoveSet = new List<UserInput> { UserInput.Q, UserInput.W, UserInput.E, UserInput.A, UserInput.D, UserInput.D };
            List<UserInput> utilityMoveSet = new List<UserInput> { UserInput.A, UserInput.E };
            UserInput normalMoveChoice = normalMoveSet.OrderBy(m => new Random().Next()).ElementAt(0);
            UserInput threeQuartersHealthMoveChoice = threeQuartersHealthMoveSet.OrderBy(m => new Random().Next()).ElementAt(0);
            UserInput utilityMoveChoice = utilityMoveSet.OrderBy(m => new Random().Next()).ElementAt(0);
            if(character.utilityDuration > 0)
            {
                if(character.health <= (character.health - (character.health / 4)))
                {
                    return tryUtilityMove(character, utilityMoveChoice);
                } else
                {
                    return UserInput.A;
                }
                
            } else if (character.health <= (character.health - (character.health / 4)))
            {
                return tryBasicMove(ref character, threeQuartersHealthMoveChoice);
            }
            else
            {
                return tryBasicMove(ref character, normalMoveChoice);
            }
        }

        private UserInput tryUtilityMove(Biggie character, UserInput utilityMoveChoice)
        {
            if(utilityMoveChoice == UserInput.E && character.UltimateCooldown > 0)
            {
                return UserInput.A;
            } else
            {
                return utilityMoveChoice;
            }
        }

        private UserInput tryBasicMove(ref Biggie character, UserInput normalMoveChoice)
        {
            if (normalMoveChoice == UserInput.Q && character.TacticalCooldown > 0)
            {
                return UserInput.A;
            }
            else if (normalMoveChoice == UserInput.W && character.UtilityCooldown > 0)
            {
                return UserInput.D;
            }
            else if (normalMoveChoice == UserInput.E && character.UltimateCooldown > 0)
            {
                return tryBasicMove(ref character, UserInput.Q);
            }
            else
            {
                return normalMoveChoice;
            }
        }
    }
}