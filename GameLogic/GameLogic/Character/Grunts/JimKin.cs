using GameLogic.Character.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Grunts
{
    public class JimKin : Characters
    {
        /*
         * Jim Kin is more of a Joke Character
         */



        //WaterGoblinHealth and BaseHealth are treated the same. WaterGoblinHealth is just character specific
        private static int JimKinHealth = 17;
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

        public JimKin()
            : base(JimKinHealth, baseDamage, baseDodge, baseBlock, baseAccuracy, false, false)
        {
            base.Level = 0;
        }

        public JimKin(int currentHealth, int currentDamage, int[] currentDodge, int currentBlock, int[] currentAccuracy, int currentLevel, int currentTactCooldown, int currentTactDuration, int currentUtilCooldown, int currentUtilDuration, int currentUltCooldown, int currentUltDuration, bool AttemptedToBlock, bool AttempedToDodge)
        : base(currentHealth, currentDamage, currentDodge, currentBlock, currentAccuracy, AttemptedToBlock, AttempedToDodge)
        {
            base.Level = currentLevel;

            this.tacticalDuration = currentTactDuration;
            base.TacticalCooldown = currentTactCooldown;
            //Don't really need these but might as well pass them
            //through since they are in the constructor
            base.UtilityCooldown = currentUtilCooldown;
            base.UltimateCooldown = currentUltCooldown;
            for (int i = 0; i < currentLevel; i++)
            {
                //This is to make sure the baseHealth variable matches with the current level.
                JimKinHealth += 25;
            }
            base.baseHealth = JimKinHealth;
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
            base.DurationRate(tact, util, ult);
        }

        public override void LevelUp()
        {
            base.LevelUp();
        }

        public override int Tactical()
        {
            return base.Tactical();
        }

        public override int Ultimate()
        {
            return base.Ultimate();
        }

        public override int Utility()
        {
            return base.Utility();
        }
    }
}
