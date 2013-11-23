using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mummys_Crypt_Text
{
    static class Level   //We only need one level, so it's "static class Level"
    {
        private static Room[,] rooms;   // a 2D array, since the class is static all of these need to be static as well

        #region properties

        public static Room[,] Rooms
        {
            get { return rooms; }
        }

        #endregion

        public static void Initialize()  
        {
            BuildLevel();  // Calls the BuildLevel method when the level is initialized.
        }

        private static void BuildLevel()
        {
            rooms = new Room[3,4];    //  [x,y] type in the actual numbers: 3 columns, 4 rows  - Capable of holding room objects

            Room room;
            Item item;
            Enemy enemy;

            //Create the Main Dungeon room //////////////////////////////////////////////////////////////-- Room 1
            room = new Room();  // Creates a new Room class for the Main Dungeon room.

            // Assign this room to 0,3
            rooms[0, 3] = room;          //Assign this room a location (coordinates) in the rooms grid [x, y].

            // Set the room properties
            room.Title = "Main Dungeon Room";
            room.Description = "You look around you with your spirit warrior eyes, and find yourself in what is obviously a dungeon.  Impliments of torture hang from the walls, and there is a pile of bones in the corner of the room.";
            room.SearchDescription = "The torture devices on the wall both compel you and fill you with a sense of revulsion.  Other than that there is nothing of note in this room. ";
            room.AddExit(Direction.North);
            room.AddExit(Direction.East);


            //Create the Goblin room /////////////////////////////////////////////////////////////////////////////// -- Room 2
            room = new Room();

            // Assign this room to 1,3
            rooms[1, 3] = room;         // Places the room just east the Main duneon room in the rooms Grid

            // Setup the room
            room.Title = "Goblin Room";
            room.Description = "A red eyed goblin jumps out from the shadows near the east wall.  He gnashes his sharp little teeth in anger and anticipation.  He bangs his axe against his small shield, issuing a challenge.";
            room.SearchDescription = "A goblin is here waiting to fight.  You cannot search the room at this time.";
            room.AddExit(Direction.West);
            room.AddExit(Direction.East);


            //Create an enemy
            enemy = new Enemy();

            //Set up enemy
            enemy.Title = "goblin";
            enemy.EnemyDescription = "A diminutive goblin scowles at you, anticipating a fight.";
            enemy.HealthValue = 8;
            enemy.AttackValue = 6;
            enemy.ArmorValue = 6;
            enemy.PointValue = 15;  // XP points

            //Add goblin to the current room
            room.Enemies.Add(enemy);


            //Create the Treasure Chest room  ////////////////////////////////////////////////////////////////////////////////
            room = new Room();

            // Assign this room to 2,3
            rooms[2, 3] = room;

            // Setup the room
            room.Title = "Treasure Chest Room";
            room.Description = "In the center of this room you see a large treasure chest.  There are numerous bones scattered about the floor.";
            room.SearchDescription = "Save for the treasure chest, you find nothing else of value.";
            room.AddExit(Direction.North);
            room.AddExit(Direction.West);

            //Create an item
            item = new Item();
            
            //Set up item
            item.Title = "chest";
            item.PickupText = "You can't pick up the treasure chest.  It is too heavy.";
            item.ItemDescription = "It is a large treasure chest.";  // Inside the treasure chest is a wand of missiles.
            item.CanPickUp = false;
            item.PointValue = 2;

            //Add treasure chest to current room
            room.Items.Add(item);


            // Create the fountain room ////////////////////////////////////////////////////////////////////////////
            room = new Room();

            // Assign this room to 0,2
            rooms[0, 2] = room;

            // Setup the room
            room.Title = "Fountain Room";
            room.Description = "Sparkling clear water cascades down from a sculpted fountain that dominates this room.";
            room.SearchDescription = "The water is crystal clear and it is easy to see there is nothing in the fountain itself.  The rest of the room is virtually bare.";
            room.AddExit(Direction.North);
            room.AddExit(Direction.South);
            room.AddExit(Direction.East);

            // Add interactions for fountain.
            item = new Item();

            //Set up item
            item.Title = "fountain";
            item.PickupText = "You cannot take the fountain.  It is a permanent fixture in the room.";
            item.ItemDescription = "The water is crystal clear and it is easy to see there is nothing in the fountain itself.";
            item.CanPickUp = false;
            item.PointValue = 2;

            //Add fountain to current room
            room.Items.Add(item);

            // Add new item - "water" and "fountain" will be the same thing...just in case a user types one or the other
            item = new Item();

            //Set up item
            item.Title = "water";
            item.PickupText = "You cannot take the fountain.  It is a permanent fixture in the room.";
            item.ItemDescription = "The water is crystal clear and it is easy to see there is nothing in the fountain itself.";
            item.CanPickUp = false;
            item.PointValue = 2;

            //Add fountain to current room
            room.Items.Add(item);


            // Create the torture room ////////////////////////////////////////////////////////////////////////////
            room = new Room();

            // Assign this room to 1,2
            rooms[1, 2] = room;

            // Setup the room
            room.Title = "Torture Room";
            room.Description = "Other than the manacles on the wall and the few gnawed bones in the corner, there does not appear to be anything remarkable about this room of the dungeon.";
            room.SearchDescription = "Stuffed inside a hollow bone in the corner is a scroll inscribed with an unfamiliar incantation and a picture of a gibberling.";
            room.AddExit(Direction.North);
            room.AddExit(Direction.East);
            room.AddExit(Direction.West);

            // Add interactions for searching the room.
            // Add scroll to room.
            item = new Item();

            //Set up item
            item.Title = "scroll";
            item.PickupText = "You decide to save the scroll for later, and place it inside your pack.";
            item.ItemDescription = "Stuffed inside a hollow bone in the corner is a scroll inscribed with an unfamiliar incantation and a picture of a gibberling.";
            item.InvDescription = "A parchment scroll with strange text and a picture of a gibberling.";
            item.PointValue = 2;

            //Add scroll to current room
            room.Items.Add(item);

            // Create the Kennel room ////////////////////////////////////////////////////////////////////////////
            room = new Room();

            // Assign this room to 2,2
            rooms[2, 2] = room;

            // Setup the room
            room.Title = "Kennel Room";
            room.Description = "As you step inside this room a growling war dog leaps out from the shadows.";
            room.SearchDescription = "A fearsome war dog threatens you.  This is no time to search the room.";
            room.AddExit(Direction.South);
            room.AddExit(Direction.West);

            //Create an enemy
            enemy = new Enemy();

            //Set up enemy
            enemy.Title = "dog";
            enemy.EnemyDescription = "A war dog foams at the mouth as it eyes you hungrily.";
            enemy.HealthValue = 10;
            enemy.AttackValue = 8;
            enemy.ArmorValue = 6;
            enemy.PointValue = 65;

            //Add skeleton to the current room
            room.Enemies.Add(enemy);


            // Create the Furnished room ////////////////////////////////////////////////////////////////////////////
            room = new Room();

            // Assign this room to 0,1
            rooms[0, 1] = room;

            // Setup the room
            room.Title = "Furnished Room";
            room.Description = "As you enter this well furnished room, a spectral figure in rotting rags materializes near the far wall.  It points an emaciated, crooked finger in your direction and glides towards you.";
            room.SearchDescription = "The ghost's presence in the room prevents you from searching this room.";
            room.AddExit(Direction.South);
            room.AddExit(Direction.East);

            // Add interactions for searching the room.

            //Set up item
            item = new Item();

            // Inside the chest is a magical helm.
            item.Title = "chest";
            item.PickupText = "There is no need to carry the small chest.";
            item.ItemDescription = "The chest is small and unassuming.  A perfect place to store valuables.";
            item.CanPickUp = false;

            //Add small chest to current room
            room.Items.Add(item);


            //Create an enemy
            enemy = new Enemy();

            //Set up enemy
            enemy.Title = "ghost";
            enemy.EnemyDescription = "A ghastly looking spirit floats ominously in the room.";
            enemy.HealthValue = 15;
            enemy.AttackValue = 10;
            enemy.ArmorValue = 8;
            enemy.PointValue = 7000;

            //Add ghost to the current room
            room.Enemies.Add(enemy);

            // Create the Plain room ////////////////////////////////////////////////////////////////////////////
            room = new Room();

            // Assign this room to 1,1
            rooms[1, 1] = room;

            // Setup the room
            room.Title = "Plain Room";
            room.Description = "There is little to distinguish this room from any of the others.  There are exits on the east, west, and south walls.";
            room.SearchDescription = "There is nothing worth keeping in this room.";
            room.AddExit(Direction.South);
            room.AddExit(Direction.East);
            room.AddExit(Direction.West);

            // Add interactions for searching the room.

            // Create the Desk room ////////////////////////////////////////////////////////////////////////////
            room = new Room();

            // Assign this room to 2,1
            rooms[2, 1] = room;

            // Setup the room
            room.Title = "Desk Room";
            room.Description = "This room is empty, save for a small desk of fine oak in the corner.  There is nothing on top of the desk, but there is a single small drawer which draws your attention.";
            room.SearchDescription = "Apart from the desk itself, there is nothing of interest in this room.";
            room.AddExit(Direction.North);
            room.AddExit(Direction.West);


            // Add desk to room.
            item = new Item();

            //Set up item
            item.Title = "desk";  //Inside the desk are a pair of bracers
            item.PickupText = "You cannot take the desk with you.";
            item.ItemDescription = "A desk made from fine oak.  It looks out of place in this room.";
            item.CanPickUp = false;
            item.PointValue = 2;

            //Add drawer to current room
            room.Items.Add(item);

            // Add drawer to room.
            item = new Item();

            //Set up item
            item.Title = "drawer";  //Inside the desk are a pair of bracers
            item.PickupText = "The drawer cannot be removed from the desk.";
            item.ItemDescription = "An oak drawer with a wrought iron handle.";
            item.CanPickUp = false;
            item.PointValue = 2;

            //Add drawer to current room
            room.Items.Add(item);

            // Create the Crypt room ////////////////////////////////////////////////////////////////////////////
            room = new Room();

            // Assign this room to 0,0
            rooms[0, 0] = room;

            // Setup the room
            room.Title = "Mummy's Crypt";
            room.Description = "The smell of ancient death permeates this room.  Standing upright against the far wall is a stone crypt with an archway leading into darkness.  Carved over the archway is a hand with a single, unblinking eye gazing out from the palm - the symbol of Helm. \n\nFrom within the darkness of the crypt comes the sound of slowly shuffling feet.  The sight of a desiccated corpse wrapped tightly in bandages emerging from the crypt comes as no great surprise.  You sense this is the final confrontation for the spirit warrior, and realize retreat is not an option.";
            room.SearchDescription = "A mummy threatens you with its presence.  There is no time to search this room!";

            // Add interactions for searching the room. 
            //Create an enemy
            enemy = new Enemy();

            //Set up enemy
            enemy.Title = "mummy";
            enemy.EnemyDescription = "The rotting mummy stands silently, awaiting your attack.";
            enemy.HealthValue = 20;
            enemy.AttackValue = 12;
            enemy.ArmorValue = 2;
            enemy.PointValue = 8000;

            //Add mummy to the current room
            room.Enemies.Add(enemy);

            // Create the Archway room ////////////////////////////////////////////////////////////////////////////
            room = new Room();

            // Assign this room to 1,0
            rooms[1, 0] = room;

            // Setup the room
            room.Title = "Archway Room";
            room.Description = "You sense on ominous presence beyond the archway to the west, and an iron chest stands against the north wall.";
            room.SearchDescription = "You carefully sift through the thick layer of dust on the floor, but apart from the chest and the ornate door, there is nothing else in the room.";
            room.AddExit(Direction.East);
            room.AddExit(Direction.West);

            // Add interactions for searching the room.
            //Set up item
            item = new Item();

            item.Title = "chest";
            item.PickupText = "The iron chest is too heavy for the spirit warrior to lift.";
            item.ItemDescription = "The iron construction makes the chest impossible to break into, and the lid is locked.  Without a key, there is no hope of opening this chest.";
            item.CanPickUp = false;

            //Add iron chest to current room
            room.Items.Add(item);

            // Create the Gibberling room ////////////////////////////////////////////////////////////////////////////
            room = new Room();

            // Assign this room to 2,0
            rooms[2, 0] = room;

            // Setup the room
            room.Title = "Gibberling Room";  //There are no exits until the gibberling is dead
            room.Description = "A rabid gibberling rushes from the shadows to attack you, its small body a mere blur of fur and teeth.  It moves with such furious speed that retreat is not an option.";
            room.SearchDescription = "The gibberling's fast movements prevent you from searching this room.";
            //room.AddExit(Direction.South);
            //room.AddExit(Direction.West);

            // Add interactions for searching the room.
            enemy = new Enemy();
            //Set up enemy
            enemy.Title = "gibberling";
            enemy.EnemyDescription = "The hunchbacked gibberling studies you with a manaiacal gleam in its eyes.";
            enemy.HealthValue = 15;
            enemy.AttackValue = 8;
            enemy.ArmorValue = 10;
            enemy.PointValue = 35;

            //Add gibberling to the current room
            room.Enemies.Add(enemy);


            // After building the level, place the player in the starting room  
            Player.PosX = 0;
            Player.PosY = 3;
        }
    }
}
