using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Decorators
{
    public class Amulet : ModTool
    {
        /*
         *-------------------DEBUFF ITEM------------------------------
         * 
         *  Raises Block by 40
         */

        public Amulet(CharacterComponent baseComponent)
            : base(baseComponent)
        {
            base.Debuff = false;
        }

        public override int Block()
        {
            return this.baseComponent.Block() + 40;
        }
    }
}
