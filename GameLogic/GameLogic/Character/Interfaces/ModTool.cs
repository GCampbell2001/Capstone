using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Interfaces
{
    public abstract class ModTool : CharacterComponent
    {
        protected CharacterComponent baseComponent;
        public bool Debuff;

        public ModTool(CharacterComponent baseComponent)
        {
            this.baseComponent = baseComponent;
        }

        public virtual int Attack()
        {
            return this.baseComponent.Attack();
        }
        public virtual int Accuracy()
        {
            return this.baseComponent.Accuracy();
        }

        public virtual int Dodge()
        {
            return this.baseComponent.Dodge();
        }

        public virtual int AttemptDodge()
        {
            return this.baseComponent.Dodge();
        }

        public virtual int Block()
        {
            return this.baseComponent.Block();
        }

        public virtual int AttemptBlock()
        {
            return this.baseComponent.Block();
        }

        public virtual void CooldownRate(int currentCooldownLeft, int decrementCooldown)
        {
            this.baseComponent.CooldownRate(currentCooldownLeft, decrementCooldown);
        }

        public virtual void DurationRate(int currentDurationLeft, int decrementDuration)
        {
            this.baseComponent.DurationRate(currentDurationLeft, decrementDuration);
        }
    }
}
