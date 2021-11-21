using GameLogic.Character.Components;
using GameLogic.GameLogic.AI.AIInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLogic.GameLogic.AI.AIComponents
{
    public class InfernalAI : BossAI
    {
        /*
         * Infernal Wish has 3 abilites to choose from along with the basic moves.
         * He doesn't have a high block or a hogh dodge normally so he is going to rely on 
         * dealing damage to win. However his utility is largely focused on avoiding damage rather than anything else. 
         * Because of this, Infernal wish has a a 50% chance to choose to attack rather than dodge or block.
         * During his utility phase though, he has a 50% chance of choosing to dodge, rather than attack or block. 
         * However, he does want to use his tactical as often as he can so attack chance is split with that. 
         * He is just as likely to use his utility as he is to dodge or block.
         * When Infernal Wish reaches about a quarter health he will use his ultimate. In his ultimate state
         * he doesn't have any change in move chance.
         * 
         */
        public UserInput MakeMove(ref Biggie character)
        {
            List<UserInput> normalMoveSet = new List<UserInput>() { UserInput.Q, UserInput.Q, UserInput.A, UserInput.S, UserInput.D, UserInput.W };
            List<UserInput> utilityMoveSet = new List<UserInput> { UserInput.D, UserInput.Q, UserInput.D, UserInput.S, UserInput.D, UserInput.A };
            UserInput normalMoveChoice = normalMoveSet.OrderBy(m => new Random().Next()).ElementAt(0);
            UserInput utilityMoveChoice = utilityMoveSet.OrderBy(m => new Random().Next()).ElementAt(0);
            if (character.health <= character.health / 4)
            {
                if (character.UltimateCooldown <= 0)
                    return UserInput.E;
                else
                {
                    return tryBasicMove(ref character, normalMoveChoice);
                }
            } else if (character.utilityDuration > 0)
            {
                if (normalMoveChoice == UserInput.Q && character.TacticalCooldown > 0)
                {
                    return UserInput.A;
                }
                return utilityMoveChoice;
            } else
            {
                return tryBasicMove(ref character, normalMoveChoice);
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
            else
            {
                return normalMoveChoice;
            }
        }
    }
}
