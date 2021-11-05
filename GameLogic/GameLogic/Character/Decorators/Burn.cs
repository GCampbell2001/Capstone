using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Decorators
{
    public class Burn : ModTool
    {
        /*
         *-------------------DEBUFF ITEM------------------------------
         * 
         *  Burn lowers block by 3
         */

        public Burn(CharacterComponent baseComponent)
            : base(baseComponent)
        {
            base.Debuff = true;
        }

        public override int Block()
        {
            return this.baseComponent.Block() - 3;
        }
    }
}
