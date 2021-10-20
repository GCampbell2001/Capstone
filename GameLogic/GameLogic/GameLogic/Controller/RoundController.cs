using GameLogic.Character;
using GameLogic.Character.Components;
using GameLogic.GameLogic;
using GameLogic.GameLogic.Controller;
using GameLogic.GameLogic.Interface;
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
            /*      TODO
             *  Create Method for enemy turn that get's called after player
             *  If player chooses to block or dodge then the controller for that class will handle enemy turn as well.
             * 
             */
            IActionHandler controller;
            switch(user.GetType().Name)
            {
                case "ThrillSeeker":
                    controller = new ThrillController();
                    break;
                default:
                    controller = new ThrillController();
                    break;
            }
            
            switch (userInput)
            {
                case UserInput.A:
                    //Attack
                    controller.Attack(user, room.Enemy);

                    break;
                case UserInput.S:
                    //Block
                    controller.Block(user, room.Enemy);


                    break;
                case UserInput.D:
                    //Dodge
                    controller.Dodge(user, room.Enemy);

                    break;
                case UserInput.Q:
                    //Tactical
                    controller.Tactical(user, room.Enemy);

                    break;
                case UserInput.W:
                    //Utility
                    controller.Utility(user, room.Enemy);

                    break;
                case UserInput.E:
                    //Ultimate
                    controller.Ultimate(user, room.Enemy);

                    break;
            }
        }


    }
}
