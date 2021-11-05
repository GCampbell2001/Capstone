using GameLogic.Character.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.PC
{
    public class InfernalWish : Biggie
    {
        /*
         *  This is the Infernal Wish Character Class
         *  Infernal Wish is the first Boss players must face
         *  He is a demon satyr with fire abilities
         *  His mainly attacks with a chain he treats as a whip that has spikes all over it.
         *  
         *  Nodge
         *  No Block
         *  Medium Health
         *  High damage
         *  Average Accuracy
         *  
         *  Tactical: Infernal Wish uses a fire based attack that burns the enemy doing little damage but applying the debuff Burn (Cooldown: 3)
         *  Utility: He focuses his energy on dodging attacks but this lowers his attack in result (Duration: 3) (Cooldown: 5)
         *  Ultimate: Infernal Wish unleashes his inner rage and transforms into his demonic state. 
         *      Massively increases dodge, moderate increase in block, accuracy is slightly decreased, small heal, and damage is left untouched (Duration: 7) (Cooldown: 10)
         */




        public InfernalWish()
            : base()
        {
            SetBaseStats();
            base.Level = 0;
            setRates();
            useDefaultStats();
        }

        public InfernalWish(int currentLevel)
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

        public InfernalWish(int currentHealth, int currentDamage, int[] currentDodge, int currentBlock, int[] currentAccuracy, int currentLevel, int currentTactCooldown, int currentTactDuration, int currentUtilCooldown, int currentUtilDuration, int currentUltCooldown, int currentUltDuration, bool AttemptedToBlock, bool AttempedToDodge)
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

            /*
             * Returns Attack() / 4
             * Coupled with Controller for applying Debuff to enemy
             */
            return base.Attack() / 4;
        }

        public override int Ultimate()
        {
            //keep the cooldown going for other abilities
            Cooldown();

            //cooldown is started
            base.UltimateCooldown = ultimateCooldownRate;
            ultimateDuration = ultimateStartingDuration;


            /*
             * buffs are determined based on level
             */
            switch (Level)
            {
                case 0:
                    base.dodge[0] += 35;
                    base.dodge[1] += 35;
                    base.block += 15;
                    base.accuracy[0] -= 5;
                    base.accuracy[1] -= 5;
                    raiseHealth(10);
                    break;
                case 1:
                    base.dodge[0] += 45;
                    base.dodge[1] += 45;
                    base.block += 20;
                    base.accuracy[0] -= 10;
                    base.accuracy[1] -= 10;
                    raiseHealth(25);
                    break;
                case 2:
                    base.dodge[0] += 55;
                    base.dodge[1] += 55;
                    base.block += 25;
                    base.accuracy[0] -= 15;
                    base.accuracy[1] -= 15;
                    raiseHealth(40);
                    break;
                case 3:
                    base.dodge[0] += 65;
                    base.dodge[1] += 65;
                    base.block += 30;
                    base.accuracy[0] -= 20;
                    base.accuracy[1] -= 20;
                    raiseHealth(55);
                    break;
                default:
                    base.dodge[0] += 35;
                    base.dodge[1] += 35;
                    base.block += 15;
                    base.accuracy[0] -= 5;
                    base.accuracy[1] -= 5;
                    raiseHealth(10);
                    break;
            }


            return 0;
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
                    base.dodge[0] += 15;
                    base.dodge[1] += 15;
                    base.damage -= 10;
                    break;
                case 1:
                    base.dodge[0] += 25;
                    base.dodge[1] += 25;
                    base.damage -= 16;
                    break;
                case 2:
                    base.dodge[0] += 32;
                    base.dodge[1] += 32;
                    base.damage -= 24;
                    break;
                case 3:
                    base.dodge[0] += 40;
                    base.dodge[1] += 40;
                    base.damage -= 31;
                    break;
                default:
                    base.dodge[0] += 15;
                    base.dodge[1] += 15;
                    base.damage -= 12;
                    break;
            }
            //return nothing
            return 0;
        }

        public override void LevelUp()
        {
            /*
             * Infernal LevelUp Guidline
             * Health + 15
             * Attack + 15
             * Dodge + 5
             * Block + 5
             * Accuracy + 5
             */
            Level++;
            baseHealth += 15;
            baseDamage += 15;
            baseDodge[0] += 5;
            baseDodge[1] += 5;
            baseBlock += 5;
            baseAccuracy[0] += 5;
            baseAccuracy[1] += 5;
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
                switch (Level)
                {
                    case 0:
                        base.dodge[0] -= 35;
                        base.dodge[1] -= 35;
                        base.block -= 15;
                        base.accuracy[0] += 5;
                        base.accuracy[1] += 5;
                        break;
                    case 1:
                        base.dodge[0] -= 45;
                        base.dodge[1] -= 45;
                        base.block -= 20;
                        base.accuracy[0] += 10;
                        base.accuracy[1] += 10;
                        break;
                    case 2:
                        base.dodge[0] -= 55;
                        base.dodge[1] -= 55;
                        base.block -= 25;
                        base.accuracy[0] += 15;
                        base.accuracy[1] += 15;
                        break;
                    case 3:
                        base.dodge[0] -= 65;
                        base.dodge[1] -= 65;
                        base.block -= 30;
                        base.accuracy[0] += 20;
                        base.accuracy[1] += 20;
                        break;
                    default:
                        base.dodge[0] -= 35;
                        base.dodge[1] -= 35;
                        base.block -= 15;
                        base.accuracy[0] += 5;
                        base.accuracy[1] += 5;
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
                        base.dodge[0] -= 15;
                        base.dodge[1] -= 15;
                        base.damage += 10;
                        break;
                    case 1:
                        base.dodge[0] -= 25;
                        base.dodge[1] -= 25;
                        base.damage += 16;
                        break;
                    case 2:
                        base.dodge[0] -= 32;
                        base.dodge[1] -= 32;
                        base.damage += 24;
                        break;
                    case 3:
                        base.dodge[0] -= 40;
                        base.dodge[1] -= 40;
                        base.damage += 31;
                        break;
                    default:
                        base.dodge[0] -= 15;
                        base.dodge[1] -= 15;
                        base.damage += 12;
                        break;
                }
            }
        }

        public override void SetBaseStats()
        {
            /*
             * Nodge
             * No Block
             * Medium Health
             * High Attack
             */
            base.baseHealth = 80;
            base.baseDamage = 20;
            base.baseDodge[0] = 0;
            base.baseDodge[1] = 100;
            base.baseBlock = 8;
            base.baseAccuracy[0] = 0;
            base.baseAccuracy[1] = 100;
        }

        public override void setRates()
        {
            //These numbers are used to start the cooldown whenever abilites are activated.
            base.tacticalCooldownRate = 3;
            base.utilityCooldownRate = 5;
            base.ultimateCooldownRate = 10;

            //These numbers are used to start a duration countdown. Until These numbers reach 0 they will change certain stats.
            base.tacticalStartingDuration = 1;
            base.utilityStartingDuration = 3;
            base.ultimateStartingDuration = 7;

            base.tacticalDuration = 0;
            base.utilityDuration = 0;
            base.ultimateDuration = 0;
        }
    }
}
