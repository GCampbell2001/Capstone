using GameLogic.Character.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.PC
{
    public class Brawler : Biggie
    {
        /*
         * This is the Brawler Character Class
         * Low Dodge
         * Low Block
         * Medium Health
         * High Attack
         * 
         * Tactical: Attack and return all damage as health (Cooldown: 3)
         * Utility: Increase damage and lower dodge slightly (Duration: 2) (Cooldown:3)
         * Ultimate: Massivly increase damage, moderatly increase block, massively lower dodge (Duration: 6) (Cooldown: 20)
         */


        

        public Brawler()
            : base()
        {
            base.player = true;
            SetBaseStats();
            base.Level = 0;
            setRates();
            useDefaultStats();

        }

        public Brawler(int currentHealth, int currentDamage, int[] currentDodge, int currentBlock, int[] currentAccuracy, int currentLevel, int currentTactCooldown, int currentTactDuration, int currentUtilCooldown, int currentUtilDuration, int currentUltCooldown, int currentUltDuration, bool AttemptedToBlock, bool AttempedToDodge)
            : base(currentHealth, currentDamage, currentDodge, currentBlock, currentAccuracy, AttemptedToBlock, AttempedToDodge)
        {
            base.player = true;
            SetBaseStats();
            setRates();
            base.Level = currentLevel;
            this.tacticalDuration = currentTactDuration;
            base.TacticalCooldown = currentTactCooldown;
            this.utilityDuration = currentUtilDuration;
            base.UtilityCooldown = currentUtilCooldown;
            this.ultimateDuration = currentUltDuration;
            base.UltimateCooldown = currentUltCooldown;
            //matchLevel(base.Level);
            checkAccuracy();
            checkUltState();
            checkUtilityState();
        }
       
        public override int Tactical()
        {
            //keep the cooldown going for other abilities
            Cooldown();

            //cooldown is started
            base.TacticalCooldown = tacticalCooldownRate;
            tacticalDuration = tacticalStartingDuration;


            //Accuracy is increased so it is at 90%
            base.accuracy[0] = 90;
            base.accuracy[1] = 190;

            /*
             * Returns Attack()
             * Coupled with Controller for healing incase she gets a critical
             */
            return base.Attack();
        }

        public override int Ultimate()
        {
            //keep the cooldown going for other abilities
            Cooldown();

            //cooldown is started
            base.UltimateCooldown = ultimateCooldownRate;
            ultimateDuration = ultimateStartingDuration;

            //Lowers Dodge slightly
            base.dodge[0] -= 15;
            base.dodge[1] -= 15;


            /*
             * buffs are determined based on level
             */
            switch (Level)
            {
                case 0:
                    base.block += 15;
                    base.damage += 25;
                    break;
                case 1:
                    base.block += 30;
                    base.damage += 35;
                    break;
                case 2:
                    base.block += 45;
                    base.damage += 45;
                    break;
                case 3:
                    base.block += 60;
                    base.damage += 55;
                    break;
                default:
                    base.block += 15;
                    base.damage += 25;
                    break;
            }

            
            return Attack();
        }

        public override int Utility()
        {
            //keep the cooldown going for other abilities
            Cooldown();

            //cooldown is started
            base.UtilityCooldown = utilityCooldownRate;
            utilityDuration = utilityStartingDuration;

            /*
               * buffs and debuffs are determined based on level
               */
            switch (Level)
            {
                case 0:
                    base.dodge[0] -= 5;
                    base.dodge[1] -= 5;
                    base.damage += 13;
                    break;
                case 1:
                    base.dodge[0] -= 7;
                    base.dodge[1] -= 7;
                    base.damage += 26;
                    break;
                case 2:
                    base.dodge[0] -= 9;
                    base.dodge[1] -= 9;
                    base.damage += 39;
                    break;
                case 3:
                    base.dodge[0] -= 11;
                    base.dodge[1] -= 11;
                    base.damage += 52;
                    break;
                default:
                    base.dodge[0] -= 5;
                    base.dodge[1] -= 5;
                    base.damage += 13;
                    break;
            }
            //return nothing
            return 0;
        }

        public override void LevelUp()
        {
            /*
             * Brawler LevelUp Guidline
             * Health + 25
             * Attack + 20
             * Dodge + 4
             * Block + 6
             * Accuracy + 5
             */
            Level++;
            baseHealth += 25;
            baseDamage += 20;
            baseDodge[0] += 4;
            baseDodge[1] += 4;
            baseBlock += 6;
            baseAccuracy[0] += 5;
            baseAccuracy[1] += 5;
        }

        private void checkAccuracy()
        {
            if (tacticalDuration > 0)
            {
                // accuracy is not reset
            }
            else
            {
                base.accuracy = baseAccuracy;
            }
        }
        private void checkUltState()
        {
            //Utility and Ultimate affect Dodge so their duration is checked
            if (ultimateDuration > 0)
            {
                // UltState is not reset
            }
            else
            {
                base.dodge[0] += 15;
                base.dodge[1] += 15;
                switch (Level)
                {
                    case 0:
                        base.block -= 15;
                        base.damage -= 25;
                        break;
                    case 1:
                        base.block -= 30;
                        base.damage -= 35;
                        break;
                    case 2:
                        base.block -= 45;
                        base.damage -= 45;
                        break;
                    case 3:
                        base.block -= 60;
                        base.damage -= 55;
                        break;
                    default:
                        base.block -= 15;
                        base.damage -= 25;
                        break;
                }
            }
        }
        private void checkUtilityState()
        {
            //Utility and Ultimate affect Dodge so their duration is checked
            if (utilityDuration > 0)
            {
                // UltState is not reset
            }
            else
            {
                switch (Level)
                {
                    case 0:
                        base.dodge[0] += 5;
                        base.dodge[1] += 5;
                        base.damage -= 13;
                        break;
                    case 1:
                        base.dodge[0] += 7;
                        base.dodge[1] += 7;
                        base.damage -= 26;
                        break;
                    case 2:
                        base.dodge[0] += 9;
                        base.dodge[1] += 9;
                        base.damage -= 39;
                        break;
                    case 3:
                        base.dodge[0] += 11;
                        base.dodge[1] += 11;
                        base.damage -= 52;
                        break;
                    default:
                        base.dodge[0] += 5;
                        base.dodge[1] += 5;
                        base.damage -= 13;
                        break;
                }
            }
        }

        public override void SetBaseStats()
        {
            /*
             * Low dodge
             * Low Block
             * Low Health
             * High Attack
             */
            base.baseHealth = 80;
            this.health = base.baseHealth;
            base.baseDamage = 17;
            base.baseDodge[0] = 40;
            base.baseDodge[1] = 140;
            base.baseBlock = 11;
            base.baseAccuracy[0] = 75;
            base.baseAccuracy[1] = 175;
        }

        public override void setRates()
        {
            //These numbers are used to start the cooldown whenever abilites are activated.
            base.tacticalCooldownRate = 3;
            base.utilityCooldownRate = 3;
            base.ultimateCooldownRate = 20;

            //These numbers are used to start a duration countdown. Until These numbers reach 0 they will change certain stats.
            base.tacticalStartingDuration = 1; 
            base.utilityStartingDuration = 2;
            base.ultimateStartingDuration = 6;

            base.tacticalDuration = 0;
            base.utilityDuration = 0;
            base.ultimateDuration = 0;
        }
    }
}