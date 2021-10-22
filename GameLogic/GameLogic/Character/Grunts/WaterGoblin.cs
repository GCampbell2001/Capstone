using GameLogic.Character.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Grunts
{
    public class WaterGoblin : Characters
    {
        /*
         * This is the Water Goblin Grunt Class
         * The Water Goblin is supposed to be small and slippery.
         * They can create and manipulate water.
         * Because of their short stature they aren't very good at blocking damage, nor are they very strong
         * However, they are really good at dodging attacks thrown at them.
         * Being on the lower end of the medium health class they should still take a good time to beat.
         * 
         * Water Goblins can also drink the moisture from the atmosphere allowing them to heal through any pain they may feel.
         * 
         * Tactical: Consume moisture from the atmosphere (heal anywhere between 1/10th - 1/2 of their health) (cooldown: 3 turns)
         * Utility: None because Grunt
         * Ultimate: None because Grunt
         * 
         */

        //WaterGoblinHealth and BaseHealth are treated the same. WaterGoblinHealth is just character specific
        private static int WaterGoblinHealth = 17;
        private static int baseDamage = 3;
        private static int[] baseDodge = { 80, 180 };
        private static int baseBlock = 1;
        private static int[] baseAccuracy = { 60, 160 };

        //This will work in conjuction to abilities to determine how much numbers changed depending on ability
        

        //These numbers are used to start the cooldown whenever abilites are activated.
        private int tacticalCooldownRate = 3;
        private int utilityCooldownRate = 4;
        private int ultimateCooldownRate = 10;

        //These numbers are used to start a duration countdown. Until These numbers reach 0 they will change certain stats.
        private int tacticalStartingDuration = 1; //This starts at 1 so accuracy can reset it's value after Tactical is called.

        private int tacticalDuration = 0;

        public WaterGoblin()
            :base(WaterGoblinHealth, baseDamage, baseDodge, baseBlock, baseAccuracy, false, false)
        {
            base.Level = 0;
        }

        public WaterGoblin(int currentHealth, int currentDamage, int[] currentDodge, int currentBlock, int[] currentAccuracy, int currentLevel, int currentTactCooldown, int currentTactDuration, int currentUtilCooldown, int currentUtilDuration, int currentUltCooldown, int currentUltDuration, bool AttemptedToBlock, bool AttempedToDodge)
        : base(currentHealth, currentDamage, currentDodge, currentBlock, currentAccuracy, AttemptedToBlock, AttempedToDodge)
        {
            base.Level = currentLevel;
            
            this.tacticalDuration = currentTactDuration;
            base.TacticalCooldown = currentTactCooldown;
            //Don't really need these but might as well pass them
            //through since they are in the constructor
            base.UtilityCooldown = currentUtilCooldown;
            base.UltimateCooldown = currentUltCooldown;
            for(int i = 0; i < currentLevel; i++)
            {
                //This is to make sure the baseHealth variable matches with the current level.
                WaterGoblinHealth += 25;
            }
            base.baseHealth = WaterGoblinHealth;
        }

        public override int Accuracy()
        {
            return base.Accuracy();
        }

        public override int Attack()
        {
            return base.Attack();
        }

        public override int AttemptBlock()
        {
            return base.AttemptBlock();
        }

        public override int AttemptDodge()
        {
            return base.AttemptDodge();
        }

        public override int Block()
        {
            return base.Block();
        }

        public override void CooldownRate(int tact, int util, int ult)
        {
            base.CooldownRate(tact, util, ult);
        }

        public override int Dodge()
        {
            return base.Dodge();
        }

        public override void DurationRate(int tact, int util, int ult)
        {
            //1's will be passed in normally unless an item affects it.
            tacticalDuration -= tact;
        }

        public override void LevelUp()
        {
            //Water Goblin LevelUp Guideline
            //Health + 25
            //attack + 10
            //dodge + 7
            //block + 7
            //accuracy + 5
            Level++;
            baseHealth += 25;
            baseDamage += 10;
            baseDodge[0] += 7;
            baseDodge[1] += 7;
            baseBlock += 7;
            baseAccuracy[0] += 5;
            baseAccuracy[1] += 5;
        }

        public override int Tactical()
        {
            //keep the cooldown going for other abilities
            base.UtilityCooldown--;
            base.UltimateCooldown--;

            //cooldown is started
            base.TacticalCooldown = tacticalCooldownRate;
            tacticalDuration = tacticalStartingDuration;

            Random generator = new Random();
            int healBonus = generator.Next(baseHealth / 10, baseHealth / 2);

            base.health += healBonus;

            //Returns how much the Water Goblin was healed for to
            //inform the player

            return healBonus;
        }

        public override int Ultimate()
        {
            //No Ultimate
            return base.Ultimate();
        }

        public override int Utility()
        {
            //No Utility
            return base.Utility();
        }
    }
}
