using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Decorators
{
    public class Goggles : ModTool
    {
        /*
         *-------------------DEBUFF ITEM------------------------------
         * 
         *  Raises Dodge by 35
         *  Raises Accuracy by 35
         */

        public Goggles(CharacterComponent baseComponent)
            : base(baseComponent)
        {
            base.Debuff = false;
        }

        public override int Dodge()
        {
            return this.baseComponent.Dodge() + 35;
        }

        public override int Accuracy()
        {
            return base.Accuracy() + 35;
        }
    }
}
