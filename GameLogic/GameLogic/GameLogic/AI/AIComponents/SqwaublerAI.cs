using GameLogic.Character.Components;
using GameLogic.Character.Interfaces;
using GameLogic.GameLogic.AI.AIInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.AI.AIComponents
{
    public class SqwaublerAI : GruntAI
    {
        /*
         * Squabler is meant to be annoying. Their tactical helps achieve that
         * but it can be very hindering if  used constantly so until they hit
         * 25% health they have as much chance to use anything else instead.
         * Squabler never blocks because it is prefers to dodge everything.
         * Therefore, there is no chance of it trying to block anything but
         * there
         */

        public UserInput MakeMove(Grunt character)
        {
            Random generator = new Random();

            UserInput[] normalSet = { UserInput.A, UserInput.D, UserInput.D, UserInput.Q };
            //Quarter set doesn't include tactical cause it's only used
            //when tactical is on cooldown
            UserInput[] quarterSet = { UserInput.A, UserInput.D, UserInput.D };

            int moveChoice;

            if(character.GetHealth() <= character.baseHealth / 4)
            {
                moveChoice = generator.Next(quarterSet.Length);
                if(character.TacticalCooldown <= 0)
                {
                    return UserInput.Q;
                } else if(character.TacticalCooldown > 0)
                {
                    return quarterSet[moveChoice];
                }
                else
                {
                    //This is a failsafe incase something didn't work
                    return UserInput.D;
                }
            } else
            {
                moveChoice = generator.Next(normalSet.Length);
                if(normalSet[moveChoice] == UserInput.Q)
                {
                    if(character.TacticalCooldown <= 0)
                    {
                        return normalSet[moveChoice];
                    } else if(character.TacticalCooldown > 0)
                    {
                        //if squabler tries to use it's tactical but can't
                        //it'll just dodge
                        return UserInput.D;
                    }
                } 
                else
                {
                    return normalSet[moveChoice];
                }
            }
            //I need to return something here so the default action will be dodge
            return UserInput.D;
        }
    }
}
