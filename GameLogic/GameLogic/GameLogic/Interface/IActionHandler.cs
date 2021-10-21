using GameLogic.Character.Components;
using GameLogic.GameLogic.ENUMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.Interface
{
    public abstract class IActionHandler
    {

        public abstract RoundResult Attack(Character.Components.Characters player, Character.Components.Characters enemy);
        public abstract RoundResult Block(Character.Components.Characters player, Character.Components.Characters enemy);
        public abstract RoundResult Dodge(Character.Components.Characters player, Character.Components.Characters enemy);
        public abstract RoundResult Tactical(Character.Components.Characters player, Character.Components.Characters enemy);
        public abstract RoundResult Utility(Character.Components.Characters player, Character.Components.Characters enemy);
        public abstract RoundResult Ultimate(Character.Components.Characters player, Character.Components.Characters enemy);

        public RoundResult CheckBlock(Character.Components.Characters enemy, int HitPoints)
        {
            int armor = enemy.Block();
            if (HitPoints > armor)
            {
                enemy.LowerHealth(HitPoints);
                return RoundResult.HIT;
            } else if (HitPoints < armor)
            {
                return RoundResult.BLOCKED;
            } else if (HitPoints == armor)
            {
                HitPoints = HitPoints / 2;
                enemy.LowerHealth(HitPoints);
                return RoundResult.BARELY;
            } else
            {
                Console.WriteLine("IActionHandler.cs Line 35 - Problem with CheckBlock Method");
                Console.WriteLine("No Conditional Met");
                Console.WriteLine("Enemy Block: " + armor);
                Console.WriteLine("HitPoints: " + HitPoints);
                return RoundResult.MISSED;
            }
        }

    }
}
