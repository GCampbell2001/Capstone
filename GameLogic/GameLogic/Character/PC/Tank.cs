using GameLogic.Character.Components;
using GameLogic.Character.Decorators;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.PC
{
    public class Tank : Biggie
    {
        /*
         *  This is the Tank Character Class
         *  Nodge is a playable character.
         *  He's a really big guy with an even bigger gut that he takes immense pride from. 
         *      No attack can possible get past his gut (at least that's what he thinks)
         *  
         *  No dodge (Why bother?)
         *  High Block
         *  High Health
         *  Medium damage
         *  
         *  Tactical: His Warhammer draws blood damaging him but buffing his attack (Cooldown: 2)
         *  Utility: Juts out his guts and laughs at his foes. (raises his block, somehow lowers his dodge further) (Duration: 1) (Cooldown: 2)
         *  Ultimate: Nodge Instantly Full Heals (Cooldown: 10)
         */


        public Tank()
            : base()
        {
            
            base.player = true;
            SetBaseStats();
            base.Level = 0;
            setRates();
            useDefaultStats();

            //this next bit is purely for testing
            //Goggles goggles = new Goggles(this);
            //Hat hat = new Hat(goggles);
            //AddItem(goggles);
            //AddItem(hat);
            //SetMainItem(hat);
        }

        public Tank(int currentHealth, int currentDamage, int[] currentDodge, int currentBlock, int[] currentAccuracy, int currentLevel, int currentTactCooldown, int currentTactDuration, int currentUtilCooldown, int currentUtilDuration, int currentUltCooldown, int currentUltDuration, bool AttemptedToBlock, bool AttempedToDodge)
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
            checkBlock();   
        }

        public override void LevelUp()
        {
            /*
             * Nodge LevelUp Guidline
             * Health + 65
             * Attack + 15
             * Dodge + 1
             * Block + 12
             * Accuracy + 5
             */
            Level++;
            baseHealth += 65;
            baseDamage += 15;
            baseDodge[0] += 1;
            baseDodge[1] += 1;
            baseBlock += 12;
            baseAccuracy[0] += 5;
            baseAccuracy[1] += 5;
        }

        public override void SetBaseStats()
        {
            /*
             * No dodge
             * High Block
             * High Health
             * Medium Attack
             */
            base.baseHealth = 150;
            this.health = base.baseHealth;
            base.baseDamage = 11;
            base.baseDodge[0] = 0;
            base.baseDodge[1] = 100;
            base.baseBlock = 20;
            base.baseAccuracy[0] = 60;
            base.baseAccuracy[1] = 160;
        }

        public override void setRates()
        {
            //These numbers are used to start the cooldown whenever abilites are activated.
            base.tacticalCooldownRate = 2;
            base.utilityCooldownRate = 2;
            base.ultimateCooldownRate = 10;

            //These numbers are used to start a duration countdown. Until These numbers reach 0 they will change certain stats.
            base.tacticalStartingDuration = 0; 
            base.utilityStartingDuration = 1; //This number is 1 so that block stays high for the 1 turn it's used
            base.ultimateStartingDuration = 0;

            base.tacticalDuration = 0;
            base.utilityDuration = 0;
            base.ultimateDuration = 0;
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


            //Returns the Attack()
            //Removes 10% of health
            //Tactical adds 10 damage for level 1 and adds 10 to that number per level after.
            //level 1 = + 10
            //level 2 = + 20
            //level 3 = + 30
            //level 4 = + 40
            //default shouldn't be reached. If it is it only adds 10. Don't want to hurt the user for no reason.
            base.health -= base.baseHealth / 10;
            switch (Level)
            {
                case 0:
                    return Attack() + 10;
                case 1:
                    return Attack() + 20;
                case 2:
                    return Attack() + 30;
                case 3:
                    return Attack() + 40;
                default:
                    return Attack() + 10;
            }
        }

        public override int Ultimate()
        {
            //keep the cooldown going for other abilities
            Cooldown();

            //cooldown is started
            base.UltimateCooldown = ultimateCooldownRate;
            ultimateDuration = ultimateStartingDuration;

            int healthHealedFor = base.baseHealth - base.health;
            base.health = base.baseHealth;
            return healthHealedFor;
        }

        public override int Utility()
        {
            //keep the cooldown going for other abilities
            Cooldown();

            //cooldown is started
            base.UtilityCooldown = utilityCooldownRate;
            utilityDuration = utilityStartingDuration;

            //How much block increases is dependent on level
            int blockIncrease = 0;
            switch (this.Level)
            {
                case 0:
                    blockIncrease = 10;
                    break;
                case 1:
                    blockIncrease = 15;
                    break;
                case 2:
                    blockIncrease = 20;
                    break;
                case 3:
                    blockIncrease = 25;
                    break;
                case 4:
                    blockIncrease = 30;
                    break;
                default:
                    blockIncrease = 15;
                    break;
            }
            base.block += blockIncrease;
            return AttemptBlock();
        }

        private void checkBlock()
        {
            if (utilityDuration > 0 || ultimateDuration > 0)
            {
                // block is not reset
            }
            else
            {
                base.block = baseBlock;
            }
        }
    }
}
