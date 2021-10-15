using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Interfaces
{
    public abstract class Mod : CharacterComponent
    {
        private CharacterComponent baseComponent;

        public Mod(CharacterComponent baseComponent)
        {
            this.baseComponent = baseComponent;
        }

        public override int Attack()
        {
            return this.baseComponent.Attack();
        }

        public override int Dodge()
        {
            return this.baseComponent.Dodge();
        }

        public override int Block()
        {
            return this.baseComponent.Block();
        }

        public override int Tactical()
        {
            return this.baseComponent.Tactical();
        }

        public override int Ultimate()
        {
            return this.baseComponent.Ultimate();
        }

        public override int Utility()
        {
            return this.baseComponent.Utility();
        }
    }
}
