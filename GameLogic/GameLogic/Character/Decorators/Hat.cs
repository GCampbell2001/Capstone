using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Decorators
{
    public class Hat : ModTool
    {
        /*
         *-------------------DEBUFF ITEM------------------------------
         * 
         *  Raises Attack by 40
         *  
         */

        public Hat(CharacterComponent baseComponent)
            : base(baseComponent)
        {
            base.Debuff = false;
        }

        public override int Attack()
        {
            return this.baseComponent.Attack() + 40;
        }
    }
}
