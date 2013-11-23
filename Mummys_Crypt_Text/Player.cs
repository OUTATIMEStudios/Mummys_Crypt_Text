using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mummys_Crypt_Text
{
    static class Player  // Only one player needed, so use static - no need to initialize; once the program is run this class is there.
    {
        private static int posX;
        private static int posY;

        private static int score = 0;
        private static List<Item> inventoryItems;
        private static List<Enemy> killedEnemies;
        private static int moves = 0;
        private static int weightCapacity = 6;
        private static int healthValue = 50;
        private static int attackValue = 10;
        public static bool Fountain = false;
        public static bool SpringTrap = false;


        #region properties

        public static int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        public static int PosY
        {
            get { return posY; }
            set { posY = value; }
        }


        public static int Score
        {
            get { return score; }
            set { score = value; }
        }

        public static int Moves
        {
            get { return moves; }
            set { moves = value; }
        }

        public static int WeightCapacity
        {
            get { return weightCapacity; }
            set { weightCapacity = value; }
        }

        public static int HealthValue
        {
            get { return healthValue; }
            set { healthValue = value; }
        }

        public static int AttackValue
        {
            get { return attackValue; }
            set { attackValue = value; }
        }

        public static int InventoryWeight
        {
            get             // This will be read only
            {
                int result = 0;
                foreach (Item item in inventoryItems)
                {
                    result += item.Weight;           // for every item in inventory, it adds the item's weight to the result
                }

                return result;                // returns result after counting all the weight
            }    
        }

        #endregion

        static Player()   // Constructor - this method gets called as soon as it is created.  Uses the exact same name as the class itself
        {
            inventoryItems = new List<Item>();  // when game is run, and automatically creates an inventory list
            killedEnemies = new List<Enemy>();  // when the game is run, automatically create a list to contain what enemies are killed
        }

        #region public methods

        public static void Move(string direction)
        {
            Room room = Player.GetCurrentRoom();

            if(!room.CanExit(direction))
            {
                Text.Add("You can't go that way!");
                return;  // returns out of the Move method after displaying above message
            }

            Player.moves++;

            switch (direction)
            {
                case Direction.North:  // "north"
                    posY--;
                    break;
                case Direction.South:
                    posY++;
                    break;
                case Direction.East:
                    posX++;
                    break;
                case Direction.West:
                    posX--;
                    break;
            }

            Player.GetCurrentRoom().Describe(); // After moving into a new room, describe the new current room.

        }

        public static void Attack(string enemyName) // For now, lets treat a person like an item
        {
            Room room = Player.GetCurrentRoom();
            Enemy enemy = room.GetEnemy(enemyName);

            if (enemy != null)
            {
                Text.Add("Someday, you will be able to attack."); // Combat();  // If enemy is in the room, then attack it.
            }
            else
                Text.Add("There is no " + enemyName +" here.");
        }

        public static void Look(string itemName)
        {
            Room room = Player.GetCurrentRoom();
            Item item = room.GetItem(itemName);
            Enemy enemy = room.GetEnemy(itemName);
            Item invItem = GetInventoryItem(itemName);  // Checks whether the player has the item in inventory

            if (item != null)
            {
                Text.Add(item.ItemDescription);  // If item is in the room, show its description.
            }
            else if (invItem != null)
            {
                Text.Add(invItem.InvDescription); // If item is in inventory, give separate description.
            }
            else if (enemy != null)
            {
                Text.Add(enemy.EnemyDescription);  // If enemy is in the room, show its description.
            }
            else
                Player.GetCurrentRoom().Describe();  // Otherwise show the room description.
        }

        public static void Drink(string itemName)
        {
            Room room = Player.GetCurrentRoom();
            Item item = room.GetItem(itemName);

            if ((itemName.ToLower() == "fountain" || itemName.ToLower() == "water") && room.Title == "Fountain Room")
            {
                Text.Add("The water is cool and refreshing - for a moment.  Then it begins to boil and burn within your mouth, scalding your throat and tongue.  You spit the steaming liquid out onto the floor.");
                HealthValue -= 5;
            }
            else
                Text.Add("You can't drink that.");
        }

        public static void Search(string itemName)
        {
            Room room = Player.GetCurrentRoom();
            Item item = room.GetItem(itemName);
            Item invItem = GetInventoryItem(itemName);  // Checks whether the player has the item in inventory

            if (item != null)
            {
                Text.Add(item.ItemDescription);  // If item is in the room, show its description.
            }
            else if (invItem != null)
            {
                Text.Add(invItem.InvDescription); // If item is in inventory, give separate description.
            }

            else if (itemName.ToLower() == "room" && room.Title == "Kennel Room")
            {
                Text.Add("You find an iron key, and a blue potion which the spirit warrior immediately quaffs. Fortunately potion heals your wounds somewhat.");
                Player.healthValue += 25;  //health potion adds to your health
            }
            else
                Player.GetCurrentRoom().SearchDescribe();  // Otherwise show the room search description.

        }


        public static void PickupItem(string itemName)
        {
            Room room = Player.GetCurrentRoom();
            Item item = room.GetItem(itemName);
            if (itemName == "dagger")
            {
                Text.Add("Yeah, okay.");
                Player.Score += 25;
            }
            else if (item != null)
            {
                if (Player.InventoryWeight + item.Weight > Player.weightCapacity)
                {
                    Text.Add("You must drop an item first.");
                    return;  // Returns out of the Pickup method
                }
                else if (itemName.ToLower() == "potion" && room.Title == "Kennel Room")
                {
                    //Text.Add("");
                    Player.healthValue += 25;  //health potion adds to your health
                }

                room.Items.Remove(item);      // Removes the item from the room...
                Player.inventoryItems.Add(item);  //...and adds it to the Player's inventory
                Player.Score += item.PointValue;  //add item's value to player score
                Text.Add(item.PickupText);
            }
            
            else if (room.Title == "Main Dungeon Room" && Player.GetInventoryItem("scroll") != null)
            {
                Text.Add("Ye doth suffer from memory loss. YE SCROLL is no more. Honestly." );
                Player.Score -= 1;
            }
            else if (room.Title == "South of the Main Dungeon Room" && Player.GetInventoryItem("trinket") != null)
            {
                Text.Add("Sigh. The trinket is in thou pouchel. Recallest thou?");
                Player.Score -= 1;
                Player.Fountain = true;
            }
            else
                Text.Add("You can't get that.");
        }

        public static void DropItem(string itemName)
        {
            Room room = Player.GetCurrentRoom();    
            Item item = GetInventoryItem(itemName);  // Checks whether the player has the item in inventory

            if (item != null)  //If item is not null, that means the item is in inventory
            {
                Player.inventoryItems.Remove(item);
                room.Items.Add(item);
                Text.Add("The " + item + " has been dropped.");
            }
            else
                Text.Add("You can't drop that.");
        }

        public static void Open(string itemName)
        {
            Room room = Player.GetCurrentRoom();
            Item item = room.GetItem(itemName);

            if (item != null && room.Title == "Dennis")
            {
                // Player.inventoryItems.Remove(item);
                //room.Items.Add(item);
                //Player.score += 127;
            }
            else
                Text.Add("There isn't a " + itemName + " to open.");
        }

        public static void DisplayInventory()
        {
            string message = "You carry:";
            string items = "";
            string underline = "";
            underline = underline.PadLeft(message.Length, '-');   // Puts the exact number of dashes to match "message"

            if (inventoryItems.Count > 0)
            {
                foreach (Item item in inventoryItems)
                {
                    items += "\n" + item.Title + " Weight: " + item.Weight.ToString();  // Displays item's name and weight
                }
            }
            else 
                items = "\nnothing";  // If inventory is empty, display this message

            items += "\n\nTotal Weight: " + Player.InventoryWeight + "/" + Player.weightCapacity;  // Display's InvWeight calculated above
            Text.Add(message + "\n" + items);  // message + "\n" + underline + items
        }

        public static void Die()
        {
            Player.score -= 100;
            Manager.EndGame("That wasn't very smart. Your score was: " + Player.score.ToString() + ".");
            //Program.Quit();
        }

        public static Room GetCurrentRoom()  // This returns a Room type
        {
            return Level.Rooms[posX, posY];  // Looks at 2D array coordinates
        }

        public static Item GetInventoryItem(string itemName)  // This returns an Item type
        {
            foreach (Item item in inventoryItems)
            {
                if (item.Title.ToLower() == itemName.ToLower())  // If item name matches one in inventoryItems, return item
                    return item;
            }
            return null;
        }

        public static Enemy GetKilledEnemies(string enemyName)  // This returns an Enemy type
        {
            foreach (Enemy enemy in killedEnemies)
            {
                if (enemy.Title.ToLower() == enemyName.ToLower())  // If enemy name matches one in killedEnemies, return enemy
                    return enemy;
            }
            return null;
        }

        #endregion

    }
}
