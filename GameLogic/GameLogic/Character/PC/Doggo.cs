using GameLogic.Character.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.PC
{
    public class Doggo : Biggie
    {
        /*
         *  This is the Doggo Character Class (Character Name in Development
         *  Doggo is the Second Boss players must face
         *  It is a three headed dog similar to cerberus
         *  
         *  
         *  High Dodge
         *  Low Block
         *  High Health
         *  Lower Medium damage
         *  Slightly-Higher Than Average Accuracy
         *  
         *  Tactical: Doggo barks at the enemy with all their heads applying a Fear debuff (Cooldown: 2)
         *  Utility: All heads focus on the enemy tripling accuracy for one turn (Duration: 1) (Cooldown: 3)
         *  Ultimate: Doggo tries to eat the enemy doing massive damage. (triple damage)(Cooldown: 8)
         */

        public Doggo()
            : base()
        {
            SetBaseStats();
            base.Level = 0;
            setRates();
            useDefaultStats();
        }

        public Doggo(int currentLevel)
            : base()
        {
            SetBaseStats();
            base.Level = 0;
            setRates();
            useDefaultStats();
            for (int i = 0; i < currentLevel; i++)
            {
                LevelUp();
            }
        }

        public Doggo(int currentHealth, int currentDamage, int[] currentDodge, int currentBlock, int[] currentAccuracy, int currentLevel, int currentTactCooldown, int currentTactDuration, int currentUtilCooldown, int currentUtilDuration, int currentUltCooldown, int currentUltDuration, bool AttemptedToBlock, bool AttempedToDodge)
            : base(currentHealth, currentDamage, currentDodge, currentBlock, currentAccuracy, AttemptedToBlock, AttempedToDodge)
        {
            SetBaseStats();
            setRates();
            base.Level = currentLevel;
            this.tacticalDuration = currentTactDuration;
            base.TacticalCooldown = currentTactCooldown;
            this.utilityDuration = currentUtilDuration;
            base.UtilityCooldown = currentUtilCooldown;
            this.ultimateDuration = currentUltDuration;
            base.UltimateCooldown = currentUltCooldown;
            matchLevel(base.Level);
            checkUtilityState();
        }

        public override int Tactical()
        {
            //keep the cooldown going for other abilities
            Cooldown();

            //cooldown is started
            base.TacticalCooldown = tacticalCooldownRate;
            tacticalDuration = tacticalStartingDuration;

            /*
             * Just applies a debuff
             */
            return 0;
        }

        public override int Ultimate()
        {
            //keep the cooldown going for other abilities
            Cooldown();

            //cooldown is started
            base.UltimateCooldown = ultimateCooldownRate;
            ultimateDuration = ultimateStartingDuration;

            //They try to do an attack that does triple damage.

            return Attack() * 3;
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
                    base.accuracy[0] += 30;
                    base.accuracy[1] += 30;
                    break;
                case 1:
                    base.accuracy[0] += 45;
                    base.accuracy[1] += 45;
                    break;
                case 2:
                    base.accuracy[0] += 60;
                    base.accuracy[1] += 60;
                    break;
                case 3:
                    base.accuracy[0] += 75;
                    base.accuracy[1] += 75;
                    break;
                default:
                    base.accuracy[0] += 30;
                    base.accuracy[1] += 30;
                    break;
            }
            //return nothing
            return 0;
        }

        public override void LevelUp()
        {
            /*
             * Doggo LevelUp Guidline
             * Health + 35
             * Attack + 13
             * Dodge + 19
             * Block + 3
             * Accuracy + 8
             */
            Level++;
            baseHealth += 35;
            baseDamage += 13;
            baseDodge[0] += 19;
            baseDodge[1] += 19;
            baseBlock += 3;
            baseAccuracy[0] += 8;
            baseAccuracy[1] += 8;
        }

        private void checkUtilityState()
        {
            //Utility affects Accuracy so its duration is checked
            if (utilityDuration > 0)
            {
                // UltState is not reset
            }
            else
            {
                switch (Level)
                {
                    case 0:
                        base.accuracy[0] += 30;
                        base.accuracy[1] += 30;
                        break;
                    case 1:
                        base.accuracy[0] += 45;
                        base.accuracy[1] += 45;
                        break;
                    case 2:
                        base.accuracy[0] += 60;
                        base.accuracy[1] += 60;
                        break;
                    case 3:
                        base.accuracy[0] += 75;
                        base.accuracy[1] += 75;
                        break;
                    default:
                        base.accuracy[0] += 30;
                        base.accuracy[1] += 30;
                        break;
                }
            }
        }

        public override void SetBaseStats()
        {
            /*
             * High Dodge
             * Low Block
             * High Health
             * Low-Medium Attack
             */
            base.baseHealth = 120;
            base.baseDamage = 14;
            base.baseDodge[0] = 50;
            base.baseDodge[1] = 150;
            base.baseBlock = 7;
            base.baseAccuracy[0] = 15;
            base.baseAccuracy[1] = 115;
        }

        public override void setRates()
        {
            //These numbers are used to start the cooldown whenever abilites are activated.
            base.tacticalCooldownRate = 2;
            base.utilityCooldownRate = 3;
            base.ultimateCooldownRate = 8;

            //These numbers are used to start a duration countdown. Until These numbers reach 0 they will change certain stats.
            base.tacticalStartingDuration = 0;
            base.utilityStartingDuration = 1;
            base.ultimateStartingDuration = 0;

            base.tacticalDuration = 0;
            base.utilityDuration = 0;
            base.ultimateDuration = 0;
        }
    }
}
