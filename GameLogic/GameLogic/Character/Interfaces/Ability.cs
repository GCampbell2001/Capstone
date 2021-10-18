using GameLogic.Character.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Interfaces
{
    public class Ability : Characters
    {
        public Ability(int health, int damage, int[] dodge, int block, int[] accuracy)
            : base(health, damage, dodge, block, accuracy)
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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override int Tactical()
        {
            return base.Tactical();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override int Ultimate()
        {
            return base.Ultimate();
        }

        public override int Utility()
        {
            return base.Utility();
        }

        public override void LevelUp()
        {
            //This will be overriden by character specific class
        }
    }
}
