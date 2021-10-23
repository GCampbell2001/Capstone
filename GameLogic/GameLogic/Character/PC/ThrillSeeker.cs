using GameLogic.Character.Components;
using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character
{
    public class ThrillSeeker : Biggie
    {
        //This is the Thrill Seeker Character Class
        //They have high Dodge due to increased acrobatic ability.
        //Low Block from Light Armor
        //Low Attack but accurate. They are less likely to miss their target
        //Tactical: They throw a knife to do more damage than normal (very low chance to miss) (cooldown: 3 turns)
        //Utility: They use their hook to manuever around the field (massively increases Dodge) (Duration: 2 turns) (cooldown: 3 turns)
        //Ultimate: Channels their energy and creates 3 Astral Projections of his weapon. (massively lowers dodge during use. Garunteed hit) (triples attack) (Duration: 1 turn) (cooldown: 10 turns)


        public ThrillSeeker()
            : base()
        {
            SetBaseStats();
            base.Level = 0;
            setRates();
            useDefaultStats();

        }

        public ThrillSeeker(int currentHealth, int currentDamage, int[] currentDodge, int currentBlock, int[] currentAccuracy, int currentLevel, int currentTactCooldown, int currentTactDuration, int currentUtilCooldown, int currentUtilDuration, int currentUltCooldown, int currentUltDuration, bool AttemptedToBlock, bool AttempedToDodge)
            :base(currentHealth, currentDamage, currentDodge, currentBlock, currentAccuracy, AttemptedToBlock, AttempedToDodge)
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
        }

        public override int Tactical()
        {
            //keep the cooldown going for other abilities
            base.UtilityCooldown--;
            base.UltimateCooldown--;

            //cooldown is started
            base.TacticalCooldown = tacticalCooldownRate;
            tacticalDuration = tacticalStartingDuration;


            //Accuracy is increased so it is at 90%
            base.accuracy[0] = 90;
            base.accuracy[1] = 190;

            
            //Returns the Attack()
            //Dagger adds 2 damage for level 1 and adds 6 to that number per level after.
            //level 1 = +2
            //level 2 = +8
            //level 3 = +14
            //level 4 = +20
            //default shouldn't be reached. If it is it does not modify Attack()
            switch (Level)
            {
                case 0:
                    return Attack() + 2;
                case 1:
                    return Attack() + 8;
                case 2:
                    return Attack() + 14;
                case 3:
                    return Attack() + 20;
                default:
                    return Attack();
            }
        }

        public override int Ultimate()
        {
            //keep the cooldown going for other abilities
            base.UtilityCooldown--;
            base.TacticalCooldown--;

            //cooldown is started
            base.UltimateCooldown = ultimateCooldownRate;
            ultimateDuration = ultimateStartingDuration;

            //Lowers Dodge by 50 so almost any attack against them will hit
            base.dodge[0] -= 50;
            base.dodge[1] -= 50;

            //Since Classes are Coupled for Characters to allow more creative freedom on abilites accuracy is not affected
            //base.accuracy[0] += 25;
            //base.accuracy[1] += 25;

            //Damage is Tripled
            return Attack() * 3;
        }

        public override int Utility()
        {
            //keep the cooldown going for other abilities
            base.TacticalCooldown--;
            base.UltimateCooldown--;

            //cooldown is started
            base.UtilityCooldown = utilityCooldownRate;
            utilityDuration = utilityStartingDuration;

            //Increases Dodge by 20
            base.dodge[0] += 20;
            base.dodge[1] += 20;

            return Dodge();
        }

        public override void LevelUp()
        {
            //ThrillSeeker LevelUp Guideline
            //Health + 20
            //attack + 10
            //dodge + 10
            //block + 6
            //accuracy + 3
            Level++;
            baseHealth += 20;
            baseDamage += 10;
            baseDodge[0] += 10;
            baseDodge[1] += 10;
            baseBlock += 6;
            baseAccuracy[0] += 3;
            baseAccuracy[1] += 3;
        }

        private void checkAccuracy()
        {
            //Since Ultimate and Tactical change Accuracy This will check if those are currently in use for every attack
            //If not in use, reset accuracy
            if (tacticalDuration > 0 || ultimateDuration > 0)
            {
                // accuracy is not reset
            } else
            {
                base.accuracy = baseAccuracy;
            }
        }
        private void checkDodge()
        {
            //Utility and Ultimate affect Dodge so their duration is checked
            if (utilityDuration > 0 || ultimateDuration > 0)
            {
                // dodge is not reset
            }
            else
            {
                base.dodge = baseDodge;
            }
        }

        public override void SetBaseStats()
        {
            base.baseHealth = 100;
            base.baseDamage = 5;
            base.baseDodge[0] = 60;
            base.baseDodge[1] = 160;
            base.baseBlock = 1;
            base.baseAccuracy[0] = 80;
            base.baseAccuracy[1] = 180;
        }

        public override void setRates()
        {
            //These numbers are used to start the cooldown whenever abilites are activated.
            base.tacticalCooldownRate = 3;
            base.utilityCooldownRate = 4;
            base.ultimateCooldownRate = 10;

            //These numbers are used to start a duration countdown. Until These numbers reach 0 they will change certain stats.
            base.tacticalStartingDuration = 1; //This starts at 1 so accuracy can reset it's value after Tactical is called.
            base.utilityStartingDuration = 2;
            base.ultimateStartingDuration = 1;

            base.tacticalDuration = 0;
            base.utilityDuration = 0;
            base.ultimateDuration = 0;
        }
    }
}
