using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Components
{
    public abstract class Grunt : ICharacter
    {
        /*
         * This class is the template for PCs and Bosses
         * It will have all basic moves done.
         * So any character specific classes will only handle abilities but all the variables will be created. This should help prevent repeated code.
         * It also adds the Utility and Ultimate methods. Those 2 and tactical will be left empty to be overriden by the character specific classes.
         */

        public List<ModTool> items;

        public int baseHealth { get; set; }
        protected int health;
        protected int damage;
        protected int[] dodge;
        protected int block;
        protected int[] accuracy;

        protected int baseDamage = 2;
        protected int[] baseDodge = { 60, 160 };
        protected int baseBlock = 1;
        protected int[] baseAccuracy = { 80, 180 };


        public int AbilityUse;
        public int TacticalCooldown;

        public bool AttemptedToBlock;
        public bool AttemptedToDodge;

        protected int Level = 0;

        //These numbers are used to start the cooldown whenever abilites are activated.
        protected int tacticalCooldownRate;

        //These numbers are used to start a duration countdown. Until These numbers reach 0 they will change certain stats.
        protected int tacticalStartingDuration;

        protected int tacticalDuration;

        public Grunt() { }

        public Grunt(int health, int damage, int[] dodge, int block, int[] accuracy, bool AttemptedToBlock, bool AttemptedToDodge)
        {
            this.health = health;
            this.damage = damage;
            this.dodge = dodge;
            this.block = block;
            this.accuracy = accuracy;
            this.AttemptedToBlock = AttemptedToBlock;
            this.AttemptedToDodge = AttemptedToDodge;
        }

        public int Attack()
        {
            Cooldown();
            return this.damage;
        }

        public int Accuracy()
        {
            Random generator = new Random();
            return generator.Next(accuracy[1] - accuracy[0]) + accuracy[0];
        }

        public int AttemptBlock()
        {
            Cooldown();
            return this.block + 10;
        }

        public int AttemptDodge()
        {
            Cooldown();
            Random rand = new Random();
            return rand.Next(dodge[0] + 10, dodge[1] + 10);
        }

        public int Block()
        {
            return this.block;
        }

        public int Dodge()
        {
            Random rand = new Random();
            return rand.Next(dodge[0], dodge[1]);
        }
        public void CooldownRate(int currentCooldownLeft, int decrementCooldown)
        {
            currentCooldownLeft -= decrementCooldown;
        }

        public void DurationRate(int currentDurationLeft, int decrementDuration)
        {
            currentDurationLeft -= decrementDuration;
        }

        public void Cooldown()
        {
            TacticalCooldown--;
        }
        public void ResetCharacter()
        {
            if (AttemptedToBlock)
            {
                AttemptedToBlock = false;
                block -= 10;
            }
            if (AttemptedToDodge)
            {
                AttemptedToDodge = false;
                dodge[0] -= 10;
                dodge[1] -= 10;
            }
        }
        public void LowerHealth(int damageTaken)
        {
            health -= damageTaken;
        }
        public void useDefaultStats()
        {
            this.damage = this.baseDamage;
            this.block = this.baseBlock;
            this.dodge = this.baseDodge;
            this.accuracy = this.baseAccuracy;

        }
        public void matchLevel(int level)
        {
            for (int i = 0; i < level; i++)
            {
                LevelUp();
            }
        }

        public List<ModTool> GetItems()
        {
            return items;
        }

        public void AddItem(ModTool item)
        {
            items.Add(item);
        }

        //These methods are all suppsoed to be overwritten.
        public abstract void SetBaseStats();
        public abstract void LevelUp();
        public abstract int Tactical();

        public abstract void setRates();
    }
}
