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
                    return GetCharacterName(character) + "Tactical";
                case UserInput.W:
                    return GetCharacterName(character) + "Utility";
                case UserInput.E:
                    return GetCharacterName(character) + "Ultimate";
                case UserInput.A:
                    return GetCharacterName(character) + "Attack";
                case UserInput.S:
                    return GetCharacterName(character) + "Block";
                case UserInput.D:
                    return GetCharacterName(character) + "Dodge";
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
                    return GetCharacterName(enemy) + "Hit";
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
                    finalString = GetCharacterName(attacker) + "Tactical";
                    break;
                case UserInput.W:
                    finalString = GetCharacterName(attacker) + "Utility";
                    break;
                case UserInput.E:
                    finalString = GetCharacterName(attacker) + "Ultimate";
                    break;
                default:
                    return "Error@AudioFilePrepare78 -  " + ability;
            }

            switch(result)
            {
                case RoundResult.HIT:
                    finalString += "|" + GetCharacterName(enemy) + "";
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
                //case RoundResult.HEALED:
                //    finalString += "|Healed";
                //    break;
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

        public void PlayerUpdate(Biggie player, List<string> update)
        {
            string name = GetCharacterName(player);
            string health = name + "CurrentHealth";
            string healthNumber = player.health.ToString();
            string tactTitle = name + "TactUpdate";
            string tactNumber = player.TacticalCooldown.ToString();
            string utilTitle = name + "UtilUpdate";
            string utilNumber = player.UtilityCooldown.ToString();
            string ultTitle = name + "UltUpdate";
            string ultNumber = player.UltimateCooldown.ToString();

            update.Add(health);
            update.Add(healthNumber);
            update.Add(tactTitle);
            update.Add(tactNumber);
            update.Add(utilTitle);
            update.Add(utilNumber);
            update.Add(ultTitle);
            update.Add(ultNumber);
        }

        public void BossUpdate(Biggie boss, List<string> update)
        {
            string name = GetCharacterName(boss);
            string health = name + "CurrentHealth";
            string healthNumber = boss.health.ToString();

            update.Add(health);
            update.Add(healthNumber);
        }

        public void GruntUpdate(Grunt grunt, List<string> update)
        {
            string name = GetCharacterName(grunt);
            string health = name + "CurrentHealth";
            string healthNumber = grunt.health.ToString();

            update.Add(health);
            update.Add(healthNumber);
        }


        public string GetCharacterName(ICharacter character)
        {
            string[] namePieces = character.GetType().Name.Split(".");
            //this line gets the last string of the array because GetType().Name returns a string that includes folder names from the location of the class
            return namePieces[namePieces.Length - 1];
        }
    }
}
