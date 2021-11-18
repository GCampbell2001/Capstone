using GameLogic.Character.Components;
using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.GameLogic.GameController
{
    public class HealingFountain
    {
        /*
         * 
         * This Class will handle the player data for when they encounter the healing fountain.
         * 
         */

        public void HealingRoom(Biggie player) 
        {
            //this heals the character
            int missingHealth = player.baseHealth - player.health;
            player.raiseHealth(missingHealth);

            List<ModTool> items = player.GetItems();
            List<int> indexToRemove = new List<int>();
            //this will determine the index of all debuffs
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].Debuff)
                {
                    indexToRemove.Add(i);
                }
            }

            //this will remove all debuffs
            for(int i = 1; i <= indexToRemove.Count; i++)
            {
                items.RemoveAt(indexToRemove[indexToRemove.Count - i]);
            }

            //this will reset all the item's base components so they are connected again.
            CharacterComponent lastPiece = player;
            foreach(ModTool tool in items)
            {
                tool.changeBaseComponent(lastPiece);
                lastPiece = tool;
            }
            //this sets the main item so the proper order can be followed for stats in fights.
            player.SetMainItem(lastPiece);
        }
    }
}
