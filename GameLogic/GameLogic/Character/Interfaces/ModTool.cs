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

        public void changeBaseComponent(CharacterComponent baseComp)
        {
            this.baseComponent = baseComp;
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

        public void TacticalCooldownDecrement(int decrement)
        {
            this.baseComponent.TacticalCooldownDecrement(decrement);
        }
        public void UtilityCooldownDecrement(int decrement)
        {
            this.baseComponent.UtilityCooldownDecrement(decrement);
        }
        public void UltimateCooldownDecrement(int decrement)
        {
            this.baseComponent.UltimateCooldownDecrement(decrement);
        }

        public void LowerHealth(int damageTaken)
        {
            this.baseComponent.LowerHealth(damageTaken);
        }

        public string Format()
        {
            string name = this.GetType().Name;
            string[] peices = name.Split(".");
            return peices[peices.Length - 1];
        }
    }
}
