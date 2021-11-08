using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Interfaces
{
    public interface CharacterComponent
    {
        public int Attack();
        public int Accuracy();
        public int Dodge();
        public int AttemptDodge();
        public int Block();
        public int AttemptBlock();
        public void TacticalCooldownDecrement(int decrement);
        public void UtilityCooldownDecrement(int decrement);
        public void UltimateCooldownDecrement(int decrement);
        public abstract void LowerHealth(int damageTaken);
    }
}
