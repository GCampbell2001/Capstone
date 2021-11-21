using GameLogic.Character.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Grunts
{
    public class GigaWatt : Grunt
    {
        /*
         * This is the Giga Watt class
         * They are metal suit inhabited by a creature of pure electrical energy.
         * The being is a trickster. They struggle moving their body so it's harder to hit enemies, even with their tactical.
         * They love magnitizing people. So their tactical is just a debuff that lowers an enemies dodge because they cause small pieces of metal to weigh them down.
         *
         * They have low health but a high block. They'll be hard to bring down but can be brought down the fastest
         * low health
         * nodge
         * high block
         * medium attack
         *
         * Tactical: magnitize foe (applies a debuff [item] that lowers the enemies dodge) (Cooldown: 3 turns)
         * Utility: None because Grunt
         * Ultimate: None because Grunt
         * 
         */

        public GigaWatt()
            : base()
        {
            SetBaseStats();
            base.Level = 0;
            setRates();
            useDefaultStats();
        }

        public GigaWatt(int currentLevel) 
            : base()
        {
            SetBaseStats();
            base.Level = 0;
            setRates();
            useDefaultStats();
            for(int i = 0; i < currentLevel; i ++)
            {
                LevelUp();
            }
        }

        public GigaWatt(int currentHealth, int currentDamage, int[] currentDodge, int currentBlock, int[] currentAccuracy, int currentLevel, int currentTactCooldown, int currentTactDuration, bool AttemptedToBlock, bool AttempedToDodge)
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
            //Giga Watt LevelUp Guideline
            //Health + 10
            //attack + 2
            //dodge + 4
            //block + 9
            //accuracy + 7
            Level++;
            baseHealth += 10;
            baseDamage += 2;
            baseDodge[0] += 4;
            baseDodge[1] += 4;
            baseBlock += 9;
            baseAccuracy[0] += 7;
            baseAccuracy[1] += 7;
        }



        public override int Tactical()
        {
            //cooldown is started
            base.TacticalCooldown = tacticalCooldownRate;
            tacticalDuration = tacticalStartingDuration;

            /*
             * I'm return 1/2 base damage because this attack isn't meant to hurt the enemy as much as it just applies a debuff
             */

            return this.damage / 2;
        }

        public override void SetBaseStats()
        {
            base.baseHealth = 18;
            this.health = base.baseHealth;
            base.baseDamage = 6;
            base.baseDodge[0] = 10;
            base.baseDodge[1] = 110;
            base.baseBlock = 12;
            base.baseAccuracy[0] = 60;
            base.baseAccuracy[1] = 160;
        }

        public override void setRates()
        {

            //These numbers are used to start the cooldown whenever abilites are activated.
            base.tacticalCooldownRate = 3;

            //These numbers are used to start a duration countdown. Until These numbers reach 0 they will change certain stats.
            base.tacticalStartingDuration = 0; //no stats are changed back so no starting duration

            base.tacticalDuration = 0;
        }
    }
}
