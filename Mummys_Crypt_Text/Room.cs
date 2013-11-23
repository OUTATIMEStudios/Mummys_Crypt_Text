using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mummys_Crypt_Text
{
    class Room
    {
        private string title;
        private string description;
        private string searchDescription;

        private List<string> exits;
        private List<Item> items;
        private List<Enemy> enemies;

        

        #region properties
        
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string SearchDescription
        {
            get { return searchDescription; }
            set { searchDescription = value; }
        }

        public List<Item> Items
        {
            get { return items; }
            set { items = value; }
        }

        public List<Enemy> Enemies
        {
            get { return enemies; }
            set { enemies = value; }
        }

        #endregion

        // Public Methods

        public Room()  // Constructor - automatically created when a room is created (since it's not static)
        {
            exits = new List<string>();  // Creates a list of exits when game starts
            items = new List<Item>();    // Creates a list of items when game starts
            enemies = new List<Enemy>(); // Creates a list of enemies when game starts
        }

        #region public methods

        public void Describe()
        {
            Text.Add(this.description);
            //Text.Add(this.GetItemList());
            //Text.Add(this.GetExitList());
        }

        public void SearchDescribe()
        {
            Text.Add(this.searchDescription);
        }

        public void ShowTitle()
        {
            Text.Add(this.title);  // Adds the title for this room to the text buffer.
        }

        public Item GetItem(String itemName)   // The return type is Item, so this is why this says "public Item" instead of "public void"
        {
            foreach (Item item in this.items)
            {
                if (item.Title.ToLower() == itemName.ToLower())  //if itemName matches an item in the list, return item
                    return item;
            }
            return null;  // did not find any item in room matching item typed in, so return "nothing"
        }

        public Enemy GetEnemy(String enemyName)   // The return type is Enemy, so this is why this says "public Enemy" instead of "public void"
        {
            foreach (Enemy enemy in this.enemies)
            {
                if (enemy.Title.ToLower() == enemyName.ToLower())  //if enemyName matches an item in the list, return item
                    return enemy;
            }
            return null;  // did not find any enemy in room matching enemy typed in, so return "nothing"
        }

        public void AddExit(string direction)  // This will add exits to room  "add north" or "add south"
        {
            if (this.exits.IndexOf(direction) == -1)  // if -1 this means that the exit isn't currently there...
                this.exits.Add(direction);            // ...so this line will add the exit (prevents duplicate entries)
        }

        public void RemoveExit(string direction)  // This will remove exits from room
        {
            if (this.exits.IndexOf(direction) != -1)   // if this is anything but -1 that means there is an exit...
                this.exits.Remove(direction);          // ...so this line will remove the exit
        }

        public bool CanExit(string direction)  // returns true or false if the direction is valid for room
        {
            foreach (string validExit in this.exits)  
            {
                if (direction == validExit)
                    return true;                // If there is a valid exit in the list, return true
            }
            return false;                // there is no exit in the specified direction
        }

        #endregion

        // Private Methods

        #region private methods

        private string GetItemList()
        {
            string itemString = "";
            string message = "Items in room:";
            string underline = "";
            underline = underline.PadLeft(message.Length, '-');   // automatically fills underline string with dashes to the length of the message

            if (this.items.Count > 0)
            {
                foreach (Item item in this.items)
                {
                    itemString += "\n[" + item.Title + "]";
                }
            }
            else
            {
                itemString = "\n<none>";
            }
            return "\n" + message + "\n" + underline + itemString;
        }

        private string GetExitList()
        {
            string exitString = "";
            string message = "Possible Exits:";
            string underline = "";
            underline = underline.PadLeft(message.Length, '-');   // automatically fills underline string with dashes to the length of the message

            if (this.exits.Count > 0)
            {
                foreach (string exitDirection in this.exits)
                {
                    exitString += "\n[" + exitDirection + "]";
                }
            }
            else
            {
                exitString = "\n<none>";
            }
            return "\n" + message + "\n" + underline + exitString;
        }

        private string GetCoordinates()
        {
            for (int y = 0; y < Level.Rooms.GetLength(1); y++)   // 1 represents the second item in an array (which is x) [x,y]
            {
                for (int x = 0; x < Level.Rooms.GetLength(0); x++)  // 0 represents the first item in an array (which is y) [x,y]
                {
                    if (this == Level.Rooms[x, y])
                        return "[" + x.ToString() + "," + y.ToString() + "]";
                }
            }
            return "This room is not within the Rooms Grid.";
        }

        #endregion

    }
}
