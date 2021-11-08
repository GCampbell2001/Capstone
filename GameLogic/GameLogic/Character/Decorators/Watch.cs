using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Character.Decorators
{
    public class Watch : ModTool
    {
        /*
         *-------------------DEBUFF ITEM------------------------------
         * 
         *  Lowers Ultimate Cooldown by 1 tick each turn
         *  
         */

        public Watch(CharacterComponent baseComponent)
            : base(baseComponent)
        {
            base.Debuff = false;
            UltimateCooldownDecrement(1);
        }


    }
}
