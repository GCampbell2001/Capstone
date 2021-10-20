using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Interfaces
{
    public abstract class CharacterComponent
    {
        public abstract int Attack();
        public abstract int Accuracy();
        public abstract int Dodge();
        public abstract int AttemptDodge();
        public abstract int Block();
        public abstract int AttemptBlock();
        public abstract int Tactical();
        public abstract int Utility();
        public abstract int Ultimate();
        public abstract void CooldownRate(int tact, int util, int ult);
        public abstract void DurationRate(int tact, int util, int ult);
    }
}
