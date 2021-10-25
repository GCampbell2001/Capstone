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
        public abstract RoundResult Attack(ICharacter player, ICharacter enemy, int importantData);
        public abstract RoundResult AttackWithItems(CharacterComponent player, CharacterComponent grunt, int importantData);
        public abstract RoundResult AttackWithoutItems(ICharacter player, ICharacter enemy, int importantData);
        public abstract RoundResult Block(ICharacter player, ICharacter enemy);
        public abstract RoundResult Dodge(ICharacter player, ICharacter enemy);

    }
}
