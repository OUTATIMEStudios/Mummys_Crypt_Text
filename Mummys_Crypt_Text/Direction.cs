using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mummys_Crypt_Text
{
    public struct Direction   // Struct is like a very lightweight class, 
    {
        public const string North = "north";       // These are constants and will not change.  North is North, etc.
        public const string South = "south";
        public const string East = "east";
        public const string West = "west";

        public static bool IsValidDirection(string direction)  // used to allow the user to just type "north" to go north
        {
            switch (direction)
            {
                case Direction.North:
                    return true;
                case Direction.South:
                    return true;
                case Direction.East:
                    return true;
                case Direction.West:
                    return true;
            }
            return false;

        }
    }
}
