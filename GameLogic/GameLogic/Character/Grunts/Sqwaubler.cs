using GameLogic.Character.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Grunts
{
    public class Sqwaubler : Grunt
    {
        /*
         * This is the Sqwaubler Grunt Class
         * They are small lithe reptilian like creatures.
         * They are not known for being able to take a hit nor can the really hit others.
         * They are extremely annoying being able to lower their foes blocking capabilities.
         * They open their mouths folding 4 flaps open and letting out a loud screech hurting anyone's eardrums within a miles reach. (distance might be a little exaggerated)
         * 
         * 
         *
         * medium health
         * high dodge
         * no block
         * low attack
         *
         * Tactical: Unleash a blood curdling screech (applies a debuff [item] that lowers the enemies block.) (Cooldown: 3)
         * Utility: None because Grunt
         * Ultimate: None because Grunt
         * 
         */

        public Sqwaubler()
            : base()
        {
            SetBaseStats();
            base.Level = 0;
            setRates();
            useDefaultStats();
        }
        public Sqwaubler(int currentLevel)
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

        public Sqwaubler(int currentHealth, int currentDamage, int[] currentDodge, int currentBlock, int[] currentAccuracy, int currentLevel, int currentTactCooldown, int currentTactDuration, bool AttemptedToBlock, bool AttempedToDodge)
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
            //Sqwaubler LevelUp Guideline
            //Health + 15
            //attack + 6
            //dodge + 15
            //block + 4
            //accuracy + 10
            Level++;
            baseHealth += 15;
            baseDamage += 6;
            baseDodge[0] += 15;
            baseDodge[1] += 15;
            baseBlock += 4;
            baseAccuracy[0] += 10;
            baseAccuracy[1] += 10;
        }



        public override int Tactical()
        {
            //cooldown is started
            base.TacticalCooldown = tacticalCooldownRate;
            tacticalDuration = tacticalStartingDuration;

            /*
             * I'm return 1/2 base damage because this attack isn't meant to hurt the enemy as much as it just applies a debuff
             */

            return this.damage / 2 ;
        }

        public override void SetBaseStats()
        {
            base.baseHealth = 23;
            base.baseDamage = 7;
            base.baseDodge[0] = 70;
            base.baseDodge[1] = 170;
            base.baseBlock = 4;
            base.baseAccuracy[0] = 65;
            base.baseAccuracy[1] = 165;
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
