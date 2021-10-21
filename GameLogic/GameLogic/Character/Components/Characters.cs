using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Components
{
    public class Characters : CharacterComponent
    {
        private CharacterComponent baseComponent;

        protected int health;
        protected int damage;
        protected int[] dodge;
        protected int block;
        protected int[] accuracy;

        public int AbilityUse;
        public int TacticalCooldown;
        public int UtilityCooldown;
        public int UltimateCooldown;

        public bool AttemptedToBlock;
        public bool AttemptedToDodge;

        public Characters(int health, int damage, int[] dodge, int block, int[] accuracy, bool AttemptedToBlock, bool AttemptedToDodge)
        {
            this.health = health;
            this.damage = damage;
            this.dodge = dodge;
            this.block = block;
            this.accuracy = accuracy;
            this.AttemptedToBlock = AttemptedToBlock;
            this.AttemptedToDodge = AttemptedToDodge;
        }

        public override int Attack()
        {
            Cooldown();
            return this.damage;
        }

        public override int Accuracy()
        {
            Random generator = new Random();
            return generator.Next(accuracy[1] - accuracy[0]) + accuracy[0];
        }

        public override int Block()
        {
            return this.block;
        }

        public override int AttemptBlock()
        {
            Cooldown();
            return this.block + 10;
        }

        public override int Dodge()
        {
            Random rand = new Random();
            return rand.Next(dodge[0], dodge[1]);
        }

        public override int AttemptDodge()
        {
            Cooldown();
            Random rand = new Random();
            return rand.Next(dodge[0] + 10, dodge[1] + 10);
        }

        public void LowerHealth(int damageTaken)
        {
            health -= damageTaken;
        }

        public override int Tactical()
        {
            //this will be overrided by the ability and thrillSeeker classes
            throw new NotImplementedException();
        }

        public override int Ultimate()
        {
            //this will be overrided by the ability class
            throw new NotImplementedException();
        }

        public override int Utility()
        {
            //this will be overrided by the ability class
            throw new NotImplementedException();
        }

        public virtual void LevelUp()
        {
            //This will be overriden by Child classes
        }
        public override void CooldownRate(int tact, int util, int ult)
        {
            this.baseComponent.CooldownRate(tact, util, ult);
        }
        public override void DurationRate(int tact, int util, int ult)
        {
            this.baseComponent.DurationRate(tact, util, ult);
        }

        public void Cooldown()
        {
            TacticalCooldown--;
            UtilityCooldown--;
            UltimateCooldown--;
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
    }
}
