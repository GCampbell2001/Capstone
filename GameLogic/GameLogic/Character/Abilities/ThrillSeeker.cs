using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Abilities
{
    public class ThrillSeeker : Ability
    {
        //This is the Thrill Seeker Character Class
        //They have high Dodge due to increased acrobatic ability.
        //Low Block from Light Armor
        //Low Attack but accurate. They are less likely to miss their target
        //Tactical: They throw a knife to do more damage than normal (very low chance to miss) (cooldown: 3 turns)
        //Utility: They use their hook to manuever around the field (massively increases Dodge) (Duration: 1 turns) (cooldown: 3 turns)
        //Ultimate: Channels their energy and creates 3 Astral Projections of his weapon. (massively lowers dodge during use and increases damage taken. Garunteed hit) (triples attack) (Duration: 1 turn) (cooldown: 10 turns)

        private static int baseHealth = 100;
        private static int baseDamage = 2;
        private static int[] baseDodge = { 50, 150 };
        private static int baseBlock = 1;
        private static int[] baseAccuracy = { 80, 180 };

        //These numbers are used to start the cooldown whenever abilites are activated.
        private int tacticalCooldownRate = 3;
        private int utilityCooldownRate = 3;
        private int ultimateCooldownRate = 10;

        //These numbers are used to start a duration countdown. Until These numbers reach 0 they will change certain stats.
        private int tacticalDuration = 0;
        private int utilityDuration = 1;
        private int ultimateDuration = 1;

        public ThrillSeeker()
            : base(baseHealth, baseDamage, baseDodge, baseBlock, baseAccuracy)
        {

        }

        public override int Attack()
        {
            return base.Attack();
        }

        public override int Block()
        {
            return base.Block();
        }

        public override int Dodge()
        {
            return base.Dodge();
        }

        public override int Tactical()
        {
            //Returns the attack
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

        private void checkAccuracy()
        {
            //Since Ultimate and Tactical change Accuracy This will check if those are currently in use for every attack
            //If not in use reset accuracy
        }
    }
}
