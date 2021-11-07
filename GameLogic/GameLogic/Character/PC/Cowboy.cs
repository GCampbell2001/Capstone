using GameLogic.Character.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.PC
{
    public class Cowboy : Biggie
    {
        /*
         *  This is the Cowboy's class (final Boss)
         *  Right now his name is Clay but that's not set in stone
         *  He loves his Dog (second boss) and nothing else (husband already died years ago)
         *  He's as tough as they get and isn't afraid of a tussle.
         *  He's very stereotypical. 
         *  
         *  
         *  High Dodge
         *  No Block
         *  High Health
         *  High damage
         *  Average Accuracy
         *  
         *  Tactical: He reloads his weapon while saying some sort of quip (Decreases His Cooldowns) (Cooldown: 4)
         *  Utility: Massively increases accuracy for next turn (Duration: 1) (Cooldown: 5)
         *  Ultimate: He ropes in his enemy and shoots then with a sawed off shotgun (massive damage) (Cooldown: 12)
         */

        public Cowboy()
            : base()
        {
            SetBaseStats();
            base.Level = 0;
            setRates();
            useDefaultStats();
        }

        public Cowboy(int currentLevel)
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

        public Cowboy(int currentHealth, int currentDamage, int[] currentDodge, int currentBlock, int[] currentAccuracy, int currentLevel, int currentTactCooldown, int currentTactDuration, int currentUtilCooldown, int currentUtilDuration, int currentUltCooldown, int currentUltDuration, bool AttemptedToBlock, bool AttempedToDodge)
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
             * His ability is lowering the cooldown on his other abilites. So it'll call a specially made cooldown method that only affects Utility and Ultimate
             */
            CowboyCooldown();
            CowboyCooldown();
            return 0;
        }

        public override int Ultimate()
        {
            //keep the cooldown going for other abilities
            Cooldown();

            //cooldown is started
            base.UltimateCooldown = ultimateCooldownRate;
            ultimateDuration = ultimateStartingDuration;

            /*  
             *  Right now the game is set up for 3 levels. At that level the cowboy does about 64 base damage.
             *   Since his Utility is supposed to help achieve doing double damage his attack will be determined by an equation.
             *   The equation is double his base damage minus half of his base damage
             */
            int ultDamage = (baseDamage * 2) - (baseDamage / 2);

            return ultDamage;
        }

        public override int Utility()
        {
            //keep the cooldown going for other abilities
            Cooldown();

            //cooldown is started
            base.UtilityCooldown = utilityCooldownRate;
            utilityDuration = utilityStartingDuration;

            /*
            *   Tremendously increases accuracy
            */

            base.accuracy[0] += 50;
            base.accuracy[1] += 50;

            //return nothing
            return 0;
        }

        public override void LevelUp()
        {
            /*
             * Cowboy LevelUp Guidline
             * Health + 40
             * Attack + 16
             * Dodge + 17
             * Block + 2
             * Accuracy + 10
             */
            Level++;
            baseHealth += 40;
            baseDamage += 16;
            baseDodge[0] += 17;
            baseDodge[1] += 17;
            baseBlock += 2;
            baseAccuracy[0] += 10;
            baseAccuracy[1] += 10;
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
                base.accuracy[0] -= 50;
                base.accuracy[1] -= 50;             
            }
        }

        public override void SetBaseStats()
        {
            /*
             * High Dodge
             * no Block
             * High Health
             * High Attack
             */
            base.baseHealth = 140;
            base.baseDamage = 16;
            base.baseDodge[0] = 65;
            base.baseDodge[1] = 165;
            base.baseBlock = 3;
            base.baseAccuracy[0] = 0;
            base.baseAccuracy[1] = 100;
        }

        public override void setRates()
        {
            //These numbers are used to start the cooldown whenever abilites are activated.
            base.tacticalCooldownRate = 4;
            base.utilityCooldownRate = 5;
            base.ultimateCooldownRate = 12;

            //These numbers are used to start a duration countdown. Until These numbers reach 0 they will change certain stats.
            base.tacticalStartingDuration = 0;
            base.utilityStartingDuration = 1;
            base.ultimateStartingDuration = 0;

            base.tacticalDuration = 0;
            base.utilityDuration = 0;
            base.ultimateDuration = 0;
        }

        private void CowboyCooldown()
        {
            UtilityCooldown--;
            UltimateCooldown--;
        }
    }
}
