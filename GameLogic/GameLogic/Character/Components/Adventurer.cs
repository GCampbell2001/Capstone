using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Components
{
    public class Adventurer : CharacterComponent
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

        public Adventurer(int health, int damage, int[] dodge, int block, int[] accuracy)
        {
            this.health = health;
            this.damage = damage;
            this.dodge = dodge;
            this.block = block;
            this.accuracy = accuracy;
        }

        public override int Attack()
        {
            return this.damage;
        }

        public override int Block()
        {
            return this.block;
        }

        public override int Dodge()
        {
            Random rand = new Random();
            return rand.Next(dodge[0], dodge[1]);
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
            //This will be overrided by the ability class
        }
    }
}
