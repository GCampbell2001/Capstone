using GameLogic.Character.Components;
using GameLogic.Character.Interfaces;
using GameLogic.GameLogic.ENUMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic
{
    public class AudioFilePrepare
    {
        /* 
         * Each method takes in the enemy and attacker just in case they are needed 
         * 
         * No file name has .wav added at the end. This way I can have more than one of an audio type with a number at the end.
         * Hopefully to help prevent the game from becoming stale too quickly
         */

        // As of 10/22/2021 12:06 P.M. -
        // I plan on audio stuff for when the player wants an update not in here.

        public string AttackFileStyle(UserInput action, ICharacter character, ICharacter enemy)
        {
            switch(action)
            {
                case UserInput.Q:
                    return character.GetType().Name + "Tactical";
                case UserInput.W:
                    return character.GetType().Name + "Utility";
                case UserInput.E:
                    return character.GetType().Name + "Ultimate";
                case UserInput.A:
                    return character.GetType().Name + "Attack";
                case UserInput.S:
                    return character.GetType().Name + "Block";
                case UserInput.D:
                    return character.GetType().Name + "Dodge";
                default:
                    return "Error";
            }
        }

        public string ResultFileStyle(RoundResult result, ICharacter enemy, ICharacter attacker)
        {
            // This method is to prepare the file name based on whether or not the attack had hit the target

            switch (result)
            {
                case RoundResult.HIT:
                    return enemy.GetType().Name + "Hit";
                case RoundResult.CRITICAL:
                    return "Critical";
                case RoundResult.BLOCKED:
                    return "Blocked";
                case RoundResult.MISSED:
                    return "Missed";
                default:
                    return "Error";
            }
        }

        public string HitpointFileStyle(int damage)
        {
            string hitpointNumber = damage.ToString();
            return hitpointNumber;
        }

        public string AbilityFileName(ICharacter attacker, UserInput ability, ICharacter enemy, RoundResult result, int hitpoints)
        {
            /*
             * This compiles the string for all the audio files as it goes down. First determining wich ability to call for, then if it has a result
             *      that would need to be played, and finally adding the number if there is one.
             */
            string finalString = "";
            switch (ability)
            {
                case UserInput.Q:
                    finalString = attacker.GetType().Name + "Tactical";
                    break;
                case UserInput.W:
                    finalString = attacker.GetType().Name + "Utility";
                    break;
                case UserInput.E:
                    finalString = attacker.GetType().Name + "Ultimate";
                    break;
                default:
                    return "Error@AudioFilePrepare78 -  " + ability;
            }

            switch(result)
            {
                case RoundResult.HIT:
                    finalString += "|" + enemy.GetType().Name + "";
                    break;
                case RoundResult.BUFFED:
                    finalString += "|";
                    break;
                case RoundResult.BLOCKED:
                    finalString += "|Blocked";
                    break;
                case RoundResult.MISSED:
                    finalString += "|Missed";
                    break;
            }
            if(hitpoints != 0)
            {
                finalString += "|" + hitpoints;
            } else
            {
                finalString += "|";
            }

            return finalString;
        }
    }
}
