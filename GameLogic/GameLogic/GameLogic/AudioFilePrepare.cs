using GameLogic.Character.Components;
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
         */

        // As of 10/22/2021 12:06 P.M. -
        // I plan on audio stuff for when the player wants an update not in here.

        public string AttackFileStyle(UserInput action, Characters character, Characters enemy)
        {
            switch(action)
            {
                case UserInput.Q:
                    return character.GetType().Name + "Tactical.wav";
                case UserInput.W:
                    return character.GetType().Name + "Utility.wav";
                case UserInput.E:
                    return character.GetType().Name + "Ultimate.wav";
                case UserInput.A:
                    return character.GetType().Name + "Attack.wav";
                case UserInput.S:
                    return character.GetType().Name + "Block.wav";
                case UserInput.D:
                    return character.GetType().Name + "Dodge.wav";
                default:
                    return "Error.wav";
            }
        }

        public string ResultFileStyle(RoundResult result, Characters enemy, Characters attacker)
        {
            // This method is to prepare the file name based on whether or not the attack had hit the target

            switch (result)
            {
                case RoundResult.HIT:
                    return enemy.GetType().Name + "Hit.wav";
                case RoundResult.CRITICAL:
                    return "Critical.wav";
                case RoundResult.BLOCKED:
                    return "Blocked.wav";
                case RoundResult.MISSED:
                    return "Missed.wav";
                default:
                    return "Error.wav";
            }
        }

        public string HitpointFileStyle(int damage)
        {
            string hitpointNumber = damage.ToString();
            return hitpointNumber + ".wav";
        }

        public string AbilityFileName(Characters attacker, UserInput ability, Characters enemy, RoundResult result, int hitpoints)
        {
            /*
             * This compiles the string for all the audio files as it goes down. First determining wich ability to call for, then if it has a result
             *      that would need to be played, and finally adding the number if there is one.
             */
            string finalString = "";
            switch (ability)
            {
                case UserInput.Q:
                    finalString = attacker.GetType().Name + "Tactical.wav";
                    break;
                case UserInput.W:
                    finalString = attacker.GetType().Name + "Utility.wav";
                    break;
                case UserInput.E:
                    finalString = attacker.GetType().Name + "Ultimate.wav";
                    break;
                default:
                    return "Error@AudioFilePrepare78.wav -  " + ability;
            }

            switch(result)
            {
                case RoundResult.HIT:
                    finalString += "|" + enemy.GetType().Name + ".wav";
                    break;
                case RoundResult.BUFFED:
                    finalString += "|";
                    break;
                case RoundResult.BLOCKED:
                    finalString += "|Blocked.wav";
                    break;
                case RoundResult.MISSED:
                    finalString += "|Missed.wav";
                    break;
            }
            if(hitpoints != 0)
            {
                finalString += "|" + hitpoints + ".wav";
            } else
            {
                finalString += "|";
            }

            return finalString;
        }
    }
}
