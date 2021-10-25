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
        public void CooldownRate(int currentCooldownLeft, int decrementCooldown);
        public void DurationRate(int currentDurationLeft, int decrementDuration);
        public abstract void LowerHealth(int damageTaken);
    }
}
