using GameLogic.Character.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Grunts
{
    public class JimKin : Grunt
    {
        /*
         * This is the Jim Kin Grunt Class
         * A Jim Kin is basically just a chicken
         * They are responsible for protecting Items kept in the Dungeon
         * To do so they harnes the ability to double in size.
         *
         * They have relatively low health so the trick for these creatures is to defeat them quickly
         * nodge
         * low block
         * low attack
         *
         * Tactical: double in size, double in damage (damage is doubled) (cooldown: 2 turns)
         * Utility: None because Grunt
         * Ultimate: None because Grunt
         * 
         */

        public JimKin()
            : base()
        {
            SetBaseStats();
            base.Level = 0;
            setRates();
            useDefaultStats();
        }
        public JimKin(int currentLevel)
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

        public JimKin(int currentHealth, int currentDamage, int[] currentDodge, int currentBlock, int[] currentAccuracy, int currentLevel, int currentTactCooldown, int currentTactDuration, bool AttemptedToBlock, bool AttempedToDodge)
        : base(currentHealth, currentDamage, currentDodge, currentBlock, currentAccuracy, AttemptedToBlock, AttempedToDodge)
        {
            SetBaseStats();
            setRates();
            base.Level = currentLevel;
            this.tacticalDuration = currentTactDuration;
            base.TacticalCooldown = currentTactCooldown;
            matchLevel(base.Level);
        }

        public override void LevelUp()
        {
            //Jim Kin LevelUp Guideline
            //Health + 10
            //attack + 1
            //dodge + 4
            //block + 4
            //accuracy + 5
            Level++;
            baseHealth += 10;
            baseDamage += 1;
            baseDodge[0] += 4;
            baseDodge[1] += 4;
            baseBlock += 4;
            baseAccuracy[0] += 5;
            baseAccuracy[1] += 5;
        }



        public override int Tactical()
        {
            //cooldown is started
            base.TacticalCooldown = tacticalCooldownRate;
            tacticalDuration = tacticalStartingDuration;

            this.damage = this.damage * 2;

            /*
             * I'm returning zero here because I don't want Jim Kin to attack
             * right after growing in size.
             */

            return 0;
        }

        public override void SetBaseStats()
        {
            base.baseHealth = 20;
            this.health = base.baseHealth;
            base.baseDamage = 1;
            base.baseDodge[0] = 30;
            base.baseDodge[1] = 130;
            base.baseBlock = 1;
            base.baseAccuracy[0] = 50;
            base.baseAccuracy[1] = 150;
        }

        public override void setRates()
        {

            //These numbers are used to start the cooldown whenever abilites are activated.
            base.tacticalCooldownRate = 2;

            //These numbers are used to start a duration countdown. Until These numbers reach 0 they will change certain stats.
            base.tacticalStartingDuration = 0; //no stats are changed back so no starting duration

            base.tacticalDuration = 0;
        }
    }
}
