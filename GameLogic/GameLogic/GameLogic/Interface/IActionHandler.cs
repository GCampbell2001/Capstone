using GameLogic.Character.Components;
using GameLogic.Character.Interfaces;
using GameLogic.GameLogic.ENUMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.Interface
{
    public interface IActionHandler
    {
        public abstract RoundResult Attack(ICharacter player, ICharacter enemy, ref int importantData);
        public abstract RoundResult AttackWithItems(ref ModTool player, ref ICharacter grunt, ref int importantData);
        public abstract RoundResult AttackWithoutItems(ref ICharacter player, ref ICharacter enemy, ref int importantData);
        public abstract RoundResult Block(ICharacter player, ICharacter enemy);
        public abstract RoundResult Dodge(ICharacter player, ICharacter enemy);

    }
}
