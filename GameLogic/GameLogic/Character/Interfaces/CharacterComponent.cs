using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Interfaces
{
    public abstract class CharacterComponent
    {
        public abstract int Attack();
        public abstract int Dodge();
        public abstract int Block();
        public abstract int Tactical();
        public abstract int Utility();
        public abstract int Ultimate();
    }
}
