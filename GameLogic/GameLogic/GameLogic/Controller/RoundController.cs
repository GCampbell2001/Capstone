using GameLogic.Character.Components;
using GameLogic.GameLogic;
using GameLogic.Location;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    class RoundController
    {
        /* TODO
         * Method to call CharacterMoves methods based on input
         * 
         * 
         * Method to handle Attack
         * Method to handle Block
         * Method to handle Dodge
         * Method to handle Tactical
         * Method to handle Utility
         * Method to handle Ultimate
         * 
         * 
         */

        public void RunFightCommand(UserInput userInput, Room room, Characters user)
        {
            switch (userInput)
            {
                case UserInput.A:
                    //Attack

                    break;
                case UserInput.S:
                    //Block

                    break;
                case UserInput.D:
                    //Dodge

                    break;
                case UserInput.Q:
                    //Tactical

                    break;
                case UserInput.W:
                    //Utility

                    break;
                case UserInput.E:
                    //Ultimate

                    break;
            }
        }


    }
}
