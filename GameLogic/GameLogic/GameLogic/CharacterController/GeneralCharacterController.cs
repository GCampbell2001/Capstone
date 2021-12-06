using GameLogic.Character.Components;
using GameLogic.Character.Decorators;
using GameLogic.Character.Interfaces;
using GameLogic.GameLogic.ENUMS;
using GameLogic.GameLogic.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.Controller
{
    public abstract class GeneralCharacterController : IActionHandler
    {
        public RoundResult Attack(ICharacter player, ICharacter enemy, ref int importantData)
        {
            /*
             * Determine if the player has items. Then choose which attack method to use from that
             */
            if(player.GetItems().Count == 0)
            {
                return this.AttackWithoutItems(ref player, ref enemy, ref importantData);
            } else
            {
                return this.AttackWithItems(ref player.GetMainItem(), ref enemy, ref importantData);
            }

        }
        public virtual RoundResult AttackWithItems(ref ModTool player, ref ICharacter enemy, ref int importantData)
        {
            int damage = player.Attack();
            int accuracy = player.Accuracy();

            int enemyDodgeAttempt = enemy.Dodge();

            if (accuracy > (enemyDodgeAttempt + 40))
            {
                damage = damage * 2;
                CheckBlockWithItems(ref enemy, damage, ref importantData);
                return RoundResult.CRITICAL;
            }
            else if (accuracy >= enemyDodgeAttempt)
            {
                return CheckBlockWithItems(ref enemy, damage, ref importantData);

            }
            else if (accuracy < enemyDodgeAttempt)
            {
                return RoundResult.MISSED;

            }
            else
            {
                Console.WriteLine("GeneralCharacterController.cs - Line 38 \r\nNo Conditional Met");
                Console.WriteLine("Player Accuracy: " + accuracy);
                Console.WriteLine("Enemy Dodge: " + enemyDodgeAttempt);
                return RoundResult.MISSED;
            }


            throw new NotImplementedException();
        }
        public virtual RoundResult AttackWithoutItems(ref ICharacter player, ref ICharacter enemy, ref int importantData)
        {
            int damage = player.Attack();
            int accuracy = player.Accuracy();

            int enemyDodgeAttempt = enemy.Dodge();

            if (accuracy > (enemyDodgeAttempt + 40))
            {
                damage = damage * 2;
                CheckBlockWithoutItems(ref enemy, damage, ref importantData);
                return RoundResult.CRITICAL;
            }
            else if (accuracy >= enemyDodgeAttempt)
            {
                return CheckBlockWithoutItems(ref enemy, damage, ref importantData);

            }
            else if (accuracy < enemyDodgeAttempt)
            {
                return RoundResult.MISSED;

            }
            else
            {
                Console.WriteLine("GeneralCharacterController.cs - Line 38 \r\nNo Conditional Met");
                Console.WriteLine("Player Accuracy: " + accuracy);
                Console.WriteLine("Enemy Dodge: " + enemyDodgeAttempt);
                return RoundResult.MISSED;
            }
        }

        public RoundResult Block(ICharacter player, ICharacter enemy)
        {
            player.AttemptBlock();
            return RoundResult.TEMPBLOCK;
        }

        public RoundResult Dodge(ICharacter player, ICharacter enemy)
        {
            player.AttemptDodge();
            return RoundResult.TEMPDODGE;
        }

        public RoundResult CheckBlockWithoutItems(ref ICharacter enemy, int HitPoints, ref int importantData)
        {
            int armor = enemy.Block();
            if (HitPoints > armor)
            {
                enemy.LowerHealth(HitPoints);
                importantData = HitPoints;
                return RoundResult.HIT;
            }
            else if (HitPoints < armor)
            {
                return RoundResult.BLOCKED;
            }
            else if (HitPoints == armor)
            {
                HitPoints = HitPoints / 2;
                enemy.LowerHealth(HitPoints);
                importantData = HitPoints;
                return RoundResult.HIT;
            }
            else
            {
                Console.WriteLine("IActionHandler.cs Line 35 - Problem with CheckBlock Method");
                Console.WriteLine("No Conditional Met");
                Console.WriteLine("Enemy Block: " + armor);
                Console.WriteLine("HitPoints: " + HitPoints);
                return RoundResult.MISSED;
            }
        }
        public RoundResult CheckBlockWithoutItems(ref Biggie enemy, int HitPoints, ref int importantData)
        {
            int armor = enemy.Block();
            if (HitPoints > armor)
            {
                enemy.LowerHealth(HitPoints);
                importantData = HitPoints;
                return RoundResult.HIT;
            }
            else if (HitPoints < armor)
            {
                return RoundResult.BLOCKED;
            }
            else if (HitPoints == armor)
            {
                HitPoints = HitPoints / 2;
                enemy.LowerHealth(HitPoints);
                importantData = HitPoints;
                return RoundResult.HIT;
            }
            else
            {
                Console.WriteLine("IActionHandler.cs Line 35 - Problem with CheckBlock Method");
                Console.WriteLine("No Conditional Met");
                Console.WriteLine("Enemy Block: " + armor);
                Console.WriteLine("HitPoints: " + HitPoints);
                return RoundResult.MISSED;
            }
        }
        public RoundResult CheckBlockWithItems(ref ICharacter enemy, int HitPoints, ref int importantData)
        {
            int armor = enemy.Block();
            if (HitPoints > armor)
            {
                enemy.LowerHealth(HitPoints);
                importantData = HitPoints;
                return RoundResult.HIT;
            }
            else if (HitPoints < armor)
            {
                return RoundResult.BLOCKED;
            }
            else if (HitPoints == armor)
            {
                HitPoints = HitPoints / 2;
                enemy.LowerHealth(HitPoints);
                importantData = HitPoints;
                return RoundResult.HIT;
            }
            else
            {
                Console.WriteLine("IActionHandler.cs Line 35 - Problem with CheckBlock Method");
                Console.WriteLine("No Conditional Met");
                Console.WriteLine("Enemy Block: " + armor);
                Console.WriteLine("HitPoints: " + HitPoints);
                return RoundResult.MISSED;
            }
        }
        public RoundResult CheckBlockWithItems(ref Biggie enemy, int HitPoints, ref int importantData)
        {
            int armor = enemy.Block();
            if (HitPoints > armor)
            {
                enemy.LowerHealth(HitPoints);
                importantData = HitPoints;
                return RoundResult.HIT;
            }
            else if (HitPoints < armor)
            {
                return RoundResult.BLOCKED;
            }
            else if (HitPoints == armor)
            {
                HitPoints = HitPoints / 2;
                enemy.LowerHealth(HitPoints);
                importantData = HitPoints;
                return RoundResult.HIT;
            }
            else
            {
                Console.WriteLine("IActionHandler.cs Line 35 - Problem with CheckBlock Method");
                Console.WriteLine("No Conditional Met");
                Console.WriteLine("Enemy Block: " + armor);
                Console.WriteLine("HitPoints: " + HitPoints);
                return RoundResult.MISSED;
            }
        }

        public virtual RoundResult UserTactical(ref Biggie player, ref ICharacter enemy, ref int importantData)
        {
            // User Tactical
            // Override
            throw new NotImplementedException();
        }
        public virtual RoundResult UserUltimate(ref Biggie player, ICharacter enemy, ref int importantData)
        {
            // this is meant to be overriden by only the user controller class
            throw new NotImplementedException();
        }

        public virtual RoundResult PCUtility(ref Biggie player, ref int importantData)
        {
            // Utility only ever affect the user so I can make one for bosses and users.
            // this is also meant to be overriden
            throw new NotImplementedException();
        }

        public virtual RoundResult BossTactical(ref Biggie player, ref Biggie boss, ref int importantData)
        {
            // This is the tactical for a boss
            // this is meant to be overriden
            throw new NotImplementedException();
        }

        public virtual RoundResult BossUltimate(ref Biggie player, ref Biggie boss, ref int importantData)
        {
            // This is the ultimate for a boss
            // this is meant to be overriden
            throw new NotImplementedException();
        }

        public virtual RoundResult GruntTactical(ref Grunt grunt, ref Biggie player, ref int importantData)
        {
            // This is the tactical for grunts
            // override
            throw new NotImplementedException();
        }

    }
}
