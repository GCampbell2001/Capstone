using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic.Location
{
    class Room
    {
        //TODO: Add Character Class

        public bool Cleared { get; set; }
        public Room North { get; set; }
        public Room South { get; set; }
        public Room East { get; set; }
        public Room West { get; set; }

    }
}
