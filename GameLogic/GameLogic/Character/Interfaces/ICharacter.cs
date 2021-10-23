using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Interfaces
{
    public interface ICharacter : CharacterComponent
    {
        /* ICharacter has all the methods CharacterComponent has but this interface will add a Tactical
         * This is because all characters have a Tactical
         * They also have a LevelUp method();
         */

        public abstract int Tactical();

        public abstract void LevelUp();
        public abstract void SetBaseStats();
        public abstract void setRates();
        public abstract void matchLevel(int level);
        public abstract void useDefaultStats();


    }
}
