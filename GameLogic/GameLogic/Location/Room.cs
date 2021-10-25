using GameLogic.Character.Components;
using GameLogic.Character.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Location
{
    class Room
    {
        //TODO: Add Character Class

        public ICharacter Enemy { get; set; }

        //these booleans will be checked before Clear since Clear is only for enemies. 
        //This allows me to just skip any room set up for player commands.
        //This should help speed up the program since most of the rooms have enemies.
        public bool Grunt { get; set; }
        public bool Boss { get; set; }
        public bool Tool { get; set; }
        public bool Potion { get; set; }

        //Cleared lets the computer whether or not to ignore the enemy variable
        public bool Cleared { get; set; }
        public Room North { get; set; }
        public Room South { get; set; }
        public Room East { get; set; }
        public Room West { get; set; }

    }
}
