using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Decorators
{
    public class Magnetize : ModTool
    {
        /*
         *-------------------DEBUFF ITEM------------------------------
         * 
         *  Magnetize lowers dodge by 2
         */

        public Magnetize(CharacterComponent baseComponent)
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
