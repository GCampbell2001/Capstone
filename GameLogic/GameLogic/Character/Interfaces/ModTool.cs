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

        public int Attack()
        {
            return this.baseComponent.Attack();
        }
        public int Accuracy()
        {
            return this.baseComponent.Accuracy();
        }

        public int Dodge()
        {
            return this.baseComponent.Dodge();
        }

        public int AttemptDodge()
        {
            return this.baseComponent.Dodge();
        }

        public int Block()
        {
            return this.baseComponent.Block();
        }

        public int AttemptBlock()
        {
            return this.baseComponent.Block();
        }

        public void CooldownRate(int currentCooldownLeft, int decrementCooldown)
        {
            this.baseComponent.CooldownRate(currentCooldownLeft, decrementCooldown);
        }

        public void DurationRate(int currentDurationLeft, int decrementDuration)
        {
            this.baseComponent.DurationRate(currentDurationLeft, decrementDuration);
        }
    }
}
