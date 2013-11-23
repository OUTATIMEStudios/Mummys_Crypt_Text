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
        private static int weightCapacity = 8;
        private static int healthValue = 50;
        private static int attackValue = 10;
        private static int multiplierValue = 1;
        private static int armorValue = 7;
        public static bool IronKey = false;
        public static bool Skeleton = false;
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

        public static int ArmorValue
        {
            get { return armorValue; }
            set { armorValue = value; }
        }

        public static int MultiplierValue
        {
            get { return multiplierValue; }
            set { multiplierValue = value; }
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
            //Enemy enemy = room.Enemies.Max();  //gets the highest index in the list, which will be the enemy in the room.

            if(!room.CanExit(direction))
            {
                Text.Add("You can't go that way!");
                return;  // returns out of the Move method after displaying above message
            }

            if (room.Enemies.Count != 0 && room.Title == "Furnished Room")  //If there is an enemy in the room then the Count will be greater than 0
            {
                Text.Add("You turn and flee in terror, but not before the ghost brushes you with its boney finger, numbing your body with the chill touch of the grave.\n");
                Player.HealthValue -= 7;
            }
            else if (room.Enemies.Count != 0 && room.Title == "Goblin Room")
            {
                Text.Add("As you flee in terror, the goblin gets a free swing at your unprotected back.\n");
                Player.HealthValue -= 2;
            }
            else if (room.Enemies.Count != 0 && room.Title == "Treasure Chest Room")
            {
                Text.Add("As you flee, the skeleton gets a free swing at your unprotected back with his rusty scimitar.\n");
                Player.HealthValue -= 5;
                // room.Enemies.Clear();   // Removes the skeleton to essentially "reset" the room.
            }
            else if (room.Enemies.Count != 0 && room.Title == "Kennel Room")
            {
                Text.Add("The war dog gets a free shot at you as you turn tail and run.\n");
                Player.HealthValue -= 5;
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
                //Text.Add("You attack the " + enemyName + "." + Combat.PlayerAttack(enemyName)); // Combat();  // If enemy is in the room, then attack it.
                Combat.PlayerAttack(enemyName);  
                if (enemy.HealthValue <= 0)
                {
                    Player.Score += enemy.PointValue; //Add to your XP
                    Text.Add("The " + enemyName + " has been vanquished.");  // let player know that the enemy is dead
                    room.Enemies.Remove(enemy);  // Remove enemy from the room
                    Player.killedEnemies.Add(enemy);
                }

                Combat.EnemyAttack(enemyName);  //Check to see if enemy is alive before attacking.
            }
            else
                Text.Add("There is no " + enemyName + " here.");
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

            else if (Player.GetKilledEnemies("dog") != null && IronKey == false && room.Title == "Kennel Room")
            {
                    Text.Add("You find an iron key, and a blue potion which the spirit warrior immediately quaffs. Fortunately potion heals your wounds somewhat.  You tuck the iron key into your belt.");
                    Player.healthValue += 25;  //health potion adds to your health

                    // Add key to room.
                    item = new Item();

                    //Set up item
                    item.Title = "key";
                    item.PickupText = "You tuck the iron key into your belt.";
                    item.ItemDescription = "A sturdy key made of iron.";
                    item.InvDescription = "A sturdy key made of iron.  Perhaps it will come in handy later.";
                    item.PointValue = 25;

                    //Add key to player inventory
                    Player.inventoryItems.Add(item);

                    IronKey = true;
            }
            else
                Player.GetCurrentRoom().SearchDescribe();  // Otherwise show the room search description.

        }


        public static void PickupItem(string itemName)
        {
            Room room = Player.GetCurrentRoom();
            Item item = room.GetItem(itemName);
            if (item != null && item.CanPickUp == false)
            {
                Text.Add(item.PickupText);
            }
            else if (item != null)
            {
                if (Player.InventoryWeight + item.Weight > Player.weightCapacity)
                {
                    Text.Add("You must drop an item first.");
                    return;  // Returns out of the Pickup method
                }
                //else if (itemName.ToLower() == "potion" && room.Title == "Archway Room")
                //{
                //    //Text.Add("");
                //    Player.healthValue += 25;  //health potion adds to your health
                //}

                room.Items.Remove(item);      // Removes the item from the room...
                Player.inventoryItems.Add(item);  //...and adds it to the Player's inventory
                Player.Score += item.PointValue;  //add item's value to player XP score
                Player.ArmorValue += item.ArmorValue;  //add item's armor modifier to player's armor class.
                //Player.MultiplierValue += item.AttackValue; // add item's "attack value" to player's multiplier
                Player.AttackValue += item.AttackValue; // add item's attack modifier to player's attack.
                Text.Add(item.PickupText);
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

        public static void Use(string itemName)
        {
            Room room = Player.GetCurrentRoom();
            Item item = Player.GetInventoryItem(itemName);
            Enemy enemy = room.Enemies.Max();  // This grabs the highest value in the list of enemies.  To get an enemy based on the current room (instead of referencing by name)

            if (item != null)
            {
                if (room.Title == "Gibberling Room" && itemName == "scroll")
                {
                    Text.Add("You pull out the scroll from your pack.  Its mysterious text glows and suddenly becomes readable.  As you read the words, the gibberling emits a loud shriek and vanishes in a puff of smoke.  The scroll then fades away in your hands.");
                    room.Enemies.Remove(enemy);  // remove the gibberling from the room...
                    Player.killedEnemies.Add(enemy);  //  ...and add it to the tally of killed enemies
                    Player.Score += 100;
                    Player.inventoryItems.Remove(item);
                }
                else if (itemName == "potion")
                {
                    Text.Add("The spirit warrior drinks down the blue potion, causing your wounds to heal greatly.");
                    Player.HealthValue += 25;
                }
                else if (itemName == "wand")
                {
                    Text.Add("You point the wand of missiles out in front of you.  A brilliant flash of light shoots forth from the tip of the wand and strikes the " + enemy.Title + "!  After the fireworks dissipate, the wand crumbles into dust.");
                    enemy.HealthValue -= 10;
                    Player.inventoryItems.Remove(item);
                    Player.Score += 25;
                }
                else
                    Text.Add("You brandish the " + itemName + " in the air, but nothing happens.");
            }
            else
                Text.Add("The spirit warrior has no " + itemName + " to use.");
        }

        // Open ////////////////////////////////////////////////////////////////////////////////////////////////
        public static void Open(string itemName)
        {
            Room room = Player.GetCurrentRoom();
            Item item = room.GetItem(itemName);

            if (item != null && room.Title == "Treasure Chest Room")
            {
                // Player.inventoryItems.Remove(item);
                //room.Items.Add(item);
                //Player.score += 127;
                if (Player.Skeleton == false && room.Enemies.Count == 0)
                {
                    //Create an enemy
                    Enemy enemy = new Enemy();
                    //Level.Rooms[2, 3].Enemies.Add(enemy);   //enemy = new Enemy();

                    //Set up enemy
                    enemy.Title = "skeleton";
                    enemy.EnemyDescription = "A large skeleton guards the treasure chest against your advances.";
                    enemy.HealthValue = 15;
                    enemy.AttackValue = 6;
                    enemy.ArmorValue = 8;
                    enemy.PointValue = 65;

                    //Add skeleton to the current room
                    room.Enemies.Add(enemy);

                    Text.Add("You cautiously approach the chest.  With a swirl of centuries-old dust, the bones on the floor spring to life and assemble themselves into a large skeleton.  The undead creature advances.");
                }
                else if (room.Enemies.Count == 1)
                {
                    Text.Add("The skeleton is guarding the treasure chest from your advances.");
                }
                else if (Player.GetKilledEnemies("skeleton") != null)
                {
                    if (Player.GetInventoryItem("wand") != null)
                    {
                        Text.Add("The chest is empty");
                    }
                    else
                    {
                        item = new Item();

                        item.Title = "wand";
                        item.PickupText = "You pick up the wand and stick it inside the belt of your spirit warrior form.";
                        item.ItemDescription = "It is a wand of missiles";
                        item.InvDescription = "The wand of missiles almost feels alive with power.";
                        item.PointValue = 2;

                        //Add wand to current room
                        room.Items.Add(item);

                        Text.Add("Inside the chest is a wand of missiles.");
                    }
                }
            }
            else if (item != null && room.Title == "Furnished Room")
            {
                if (room.Enemies.Count == 1)
                {
                    Text.Add("A ghost threatens you with with its presence.  You cannot deal with the chest at this time.");
                }
                else if (Player.GetKilledEnemies("ghost") != null)
                {
                    if (Player.GetInventoryItem("helm") != null)
                    {
                        Text.Add("The chest is empty");
                    }
                    else
                    {
                        item = new Item();

                        item.Title = "helm";
                        item.PickupText = "You place the helm on the head of your spirit warrior body, knowing the extra protection will come in handy.";
                        item.ItemDescription = "A helmet imbued with magical properties.";
                        item.InvDescription = "You feel more hardy while wearing the magical helm.";
                        item.PointValue = 2;
                        item.ArmorValue = 1;

                        //Add helm to current room
                        room.Items.Add(item);

                        Text.Add("Inside the chest is a magical helm.");
                    }
                }
            }
            else if (item != null && room.Title == "Desk Room")
            {
                if (Player.GetInventoryItem("bracers") != null)
                {
                    Text.Add("The desk drawer is empty");
                }
                else if (Player.SpringTrap == false)
                {
                    item = new Item();

                    item.Title = "bracers";
                    item.PickupText = "You strap on the pair of bracers and feel an instant increase in the combat skills of your spirit warrior body.";
                    item.ItemDescription = "A pair of bracers that appear to be a perfect fit for your spirit warrior.";
                    item.InvDescription = "A pair of sturdy bracers.  You feel more powerful while wearing them.";
                    item.PointValue = 2;
                    item.AttackValue = 3;
                    item.ArmorValue = 1;

                    //Add bracers to current room
                    room.Items.Add(item);

                    Random random = new Random();
                    int rollPoison = random.Next(20);
                    if (rollPoison % 3 == 0)
                    {
                        Text.Add("The spirit warrior approaches the desk and opens the drawer.\nThe desk is coated with a powerful contact poison.  The spirit warrior takes a great deal of damage due to the effects of the deadly toxin.\nInside the desk are a pair of bracers.");
                        Player.healthValue -= 10;
                    }
                    else
                    {
                        Text.Add("The spirit warrior approaches the desk and opens the drawer.\nThe desk is coated with a powerful contact poison, Fortunately, the hardy constitution of the spirit warrior's body allows you to shrug off the effects of the deadly toxin.\nInside the desk are a pair of bracers.");
                    }

                    Player.SpringTrap = true;
                }
                else
                    Text.Add("Inside the desk are a pair of bracers.");
            }
            else if (item != null && IronKey == true && room.Title == "Archway Room")
            {
                if (Player.GetInventoryItem("potion") != null)
                {
                    Text.Add("The iron chest is empty");
                }
                else
                {
                    item = new Item();

                    item.Title = "potion";
                    item.PickupText = "You sieze the potion and put it in your pack.";
                    item.ItemDescription = "A blue potion that can restore your health greatly.";
                    item.InvDescription = "A blue potion that can restore your health greatly.";
                    item.PointValue = 20;

                    //Add potion to current room
                    room.Items.Add(item);

                    Text.Add("Using the iron key you found earlier, you unlock the lid of the chest.  Inside is a blue potion.");
                }
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
            Manager.EndGame("That wasn't very smart. Your acquired experience points: " + Player.score.ToString() + ".");
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
