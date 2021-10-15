using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Components
{
    class Adventurer : CharacterComponent
    {
        private int BaseDamage;
        private int[] BaseDodge;
        private int BaseBlock;
        private int BaseAccuracy;
        private int 

        public Adventurer(int damage, int[] dodge, int block, int accuracy)
        {
            this.BaseDamage = damage;
            this.BaseDodge = dodge;
            this.BaseBlock = block;
            this.BaseAccuracy = accuracy;
        }


        public override int Attack()
        {
            throw new NotImplementedException();
        }

        public override int Block()
        {
            throw new NotImplementedException();
        }

        public override int Dodge()
        {
            throw new NotImplementedException();
        }

        public override int Tactical()
        {
            throw new NotImplementedException();
        }

        public override int Ultimate()
        {
            throw new NotImplementedException();
        }

        public override int Utility()
        {
            throw new NotImplementedException();
        }
    }
}
