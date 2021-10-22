using GameLogic.Character.Components;
using GameLogic.GameLogic.ENUMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.Interface
{
    public abstract class IActionHandler
    {

        public abstract RoundResult Attack(Characters player, Characters enemy, int importantData);
        public abstract RoundResult Block(Characters player, Characters enemy);
        public abstract RoundResult Dodge(Characters player, Characters enemy);
        public abstract RoundResult Tactical(Characters player, Characters enemy, int importantData);
        public abstract RoundResult Utility(Characters player, Characters enemy, int importantData);
        public abstract RoundResult Ultimate(Characters player, Characters enemy, int importantData);

        public RoundResult CheckBlock(Characters enemy, int HitPoints, int importantData)
        {
            int armor = enemy.Block();
            if (HitPoints > armor)
            {
                enemy.LowerHealth(HitPoints);
                importantData = HitPoints;
                return RoundResult.HIT;
            } else if (HitPoints < armor)
            {
                return RoundResult.BLOCKED;
            } else if (HitPoints == armor)
            {
                HitPoints = HitPoints / 2;
                enemy.LowerHealth(HitPoints);
                importantData = HitPoints;
                return RoundResult.HIT;
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
