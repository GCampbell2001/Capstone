using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Decorators
{
    class Fear : ModTool
    {
        /*
         *-------------------DEBUFF ITEM------------------------------
         * 
         *  Fear lowers dodge by 6
         */

        public Fear(CharacterComponent baseComponent)
            : base(baseComponent)
        {
            base.Debuff = true;
        }

        public override int Dodge()
        {
            return this.baseComponent.Dodge() - 2;
        }
    }
}
