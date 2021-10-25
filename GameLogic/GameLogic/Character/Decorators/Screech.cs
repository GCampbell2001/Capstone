using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Decorators
{
    public class Screech : ModTool
    {
        /*
         *-------------------DEBUFF ITEM------------------------------
         * 
         *  Screech lowers block by 2
         */

        public Screech(CharacterComponent baseComponent)
            : base(baseComponent)
        {
            base.Debuff = true;
        }

        public override int Block()
        {
            return this.baseComponent.Block() - 2;
        }
    }
}
