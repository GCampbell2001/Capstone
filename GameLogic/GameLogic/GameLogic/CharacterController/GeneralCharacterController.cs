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
        public RoundResult Attack(ICharacter player, ICharacter enemy, int importantData)
        {
            /*
             * This specific method is supposed to be overriden
             * The Controller class will determine if the character has items and if it does generate's the stack so that it can be passed around.
             */

            throw new NotImplementedException();
        }
        public virtual RoundResult AttackWithItems(CharacterComponent player, CharacterComponent enemy, int importantData)
        {


            throw new NotImplementedException();
        }
        public virtual RoundResult AttackWithoutItems(ICharacter player, ICharacter enemy, int importantData)
        {
            int damage = player.Attack();
            int accuracy = player.Accuracy();

            int enemyDodgeAttempt = enemy.Dodge();

            if (accuracy > (enemyDodgeAttempt + 40))
            {
                damage = damage * 2;
                CheckBlockWithoutItems(enemy, damage, importantData);
                return RoundResult.CRITICAL;
            }
            else if (accuracy >= enemyDodgeAttempt)
            {
                return CheckBlockWithoutItems(enemy, damage, importantData);

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

        public RoundResult CheckBlockWithoutItems(ICharacter enemy, int HitPoints, int importantData)
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

        public virtual RoundResult UserTactical(Biggie player, ICharacter enemy, int importantData)
        {
            // User Tactical
            // Override
            throw new NotImplementedException();
        }
        public virtual RoundResult UserUltimate(Biggie player, ICharacter enemy, int importantData)
        {
            // this is meant to be overriden by only the user controller class
            throw new NotImplementedException();
        }

        public virtual RoundResult PCUtility(Biggie player, int importantData)
        {
            // Utility only ever affect the user so I can make one for bosses and users.
            // this is also meant to be overriden
            throw new NotImplementedException();
        }

        public virtual RoundResult BossTactical(Biggie player, Biggie boss, int importantData)
        {
            // This is the tactical for a boss
            // this is meant to be overriden
            throw new NotImplementedException();
        }

        public virtual RoundResult BossUltimate(Biggie player, Biggie boss, int importantData)
        {
            // This is the ultimate for a boss
            // this is meant to be overriden
            throw new NotImplementedException();
        }

        public virtual RoundResult GruntTactical(Grunt grunt, Biggie player, int importantData)
        {
            // This is the tactical for grunts
            // override
            throw new NotImplementedException();
        }

    }
}
