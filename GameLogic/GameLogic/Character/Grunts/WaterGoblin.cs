using GameLogic.Character.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Grunts
{
    public class WaterGoblin : Grunt
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

        public WaterGoblin()
            : base()
        {
            SetBaseStats();
            base.health = baseHealth;
            base.Level = 0;
            setRates();
            useDefaultStats();
        }
        public WaterGoblin(int currentLevel)
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

        public WaterGoblin(int currentHealth, int currentDamage, int[] currentDodge, int currentBlock, int[] currentAccuracy, int currentLevel, int currentTactCooldown, int currentTactDuration, bool AttemptedToBlock, bool AttempedToDodge)
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

        public override void SetBaseStats()
        {
            //WaterGoblinHealth and BaseHealth are treated the same. WaterGoblinHealth is just character specific
            base.baseHealth = 27;
            base.baseDamage = 3;
            base.baseDodge[0] = 80;
            base.baseDodge[1] = 180;
            base.baseBlock = 1;
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
