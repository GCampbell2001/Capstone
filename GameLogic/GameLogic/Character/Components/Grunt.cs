using GameLogic.Character.Interfaces;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
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

        private List<ModTool> items = new List<ModTool>();
        private ModTool currentItem = null;
        public int baseHealth { get; set; }
        public int health;
        protected int damage;
        protected int[] dodge;
        protected int block;
        protected int[] accuracy;

        protected int baseDamage = 2;
        protected int[] baseDodge = { 60, 160 };
        protected int baseBlock = 1;
        protected int[] baseAccuracy = { 80, 180 };


        public int AbilityUse;

        internal int GetHealth()
        {
            return this.health;
        }

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
            Duration();
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
            Duration();
            return this.block + 10;
        }

        public int AttemptDodge()
        {
            Cooldown();
            Duration();
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
        public void TacticalCooldownDecrement(int decrement)
        {
            TacticalCooldown -= decrement;
        }
        public void UtilityCooldownDecrement(int decrement)
        {
            //this method is for Biggies
        }
        public void UltimateCooldownDecrement(int decrement)
        {
            //this method is for Biggies
        }

        public void Cooldown()
        {
            TacticalCooldown--;
        }
        public void Duration()
        {
            tacticalDuration -= 1;
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

        public ModTool GetMainItem()
        {
            return currentItem;
        }

        public List<ModTool> GetItems()
        {
            return items;
        }

        public void SetMainItem(CharacterComponent item)
        {
            this.currentItem = (ModTool)item;
        }

        public void AddItem(ModTool item)
        {
            items.Add(item);
        }

        public BsonDocument ToBson()
        {
            //included the true grunt variable for pulling data from database
            string final = "{ grunt : true, class : \"" + this.GetType().ToString() + "\", level : "
                + this.Level + ", health : " + this.health + ", damage : " + this.damage
                + ", dodge : [" + this.dodge[0] + ", " + this.dodge[1] + "], block : " +
                this.block + ", accuracy : [" + this.accuracy[0] + ", " + this.accuracy[1]
                + "], tactCooldown : " + this.TacticalCooldown + ", attemptDodge : \"" + AttemptedToDodge
                + "\", attemptBlock : \"" + AttemptedToBlock + "\"}";
            return BsonDocument.Parse(final);
        }

        public bool isDead()
        {
            if(this.health <= 0)
            {
                return true;
            } else
            {
                return false;
            }
        }

        //These methods are all suppsoed to be overwritten.
        public abstract void SetBaseStats();
        public abstract void LevelUp();
        public abstract int Tactical();

        public abstract void setRates();

        
    }
}
