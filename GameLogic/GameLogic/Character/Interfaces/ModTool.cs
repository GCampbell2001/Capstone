using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Interfaces
{
    public abstract class ModTool : CharacterComponent
    {
        private CharacterComponent baseComponent;

        public ModTool(CharacterComponent baseComponent)
        {
            this.baseComponent = baseComponent;
        }

        public override int Attack()
        {
            return this.baseComponent.Attack();
        }
        public override int Accuracy()
        {
            return this.baseComponent.Accuracy();
        }

        public override int Dodge()
        {
            return this.baseComponent.Dodge();
        }

        public override int AttemptDodge()
        {
            return this.baseComponent.Dodge();
        }

        public override int Block()
        {
            return this.baseComponent.Block();
        }

        public override int AttemptBlock()
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
        public override void CooldownRate(int tact, int util, int ult)
        {
            this.baseComponent.CooldownRate(tact, util, ult);
        }
        public override void DurationRate(int tact, int util, int ult)
        {
            this.baseComponent.DurationRate(tact, util, ult);
        }
    }
}
