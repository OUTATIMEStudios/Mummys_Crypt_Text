using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mummys_Crypt_Text
{
    static class Manager  //We only need one game manager, not multiple instances, so this is STATIC class Manager
    {
        //Public Methods
        public static void ShowTitleScreen()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("                                           \n" +
                              "       _.-._        ************************  \n" +
                              "     _| | | |       *  The Mummy's Crypt   *                       \n" +
                              "    | | | | |       *   A Baldur's Gate    *                      \n" +
                              "    | | | | |       *   Text Adventure     *                   \n" +
                              "    |   --  | _     *                      *         \n" +
                              "    ;       /`/     *         by           *       \n" +
                              "    |        /      *    Jason Chadwell    *            \n" +
                              "     \\      /       *                      *              \n" +
                              "      |    |        ************************           \n" +
                              "                                                \n" +
                             "           Press any key to begin            ");
            Console.ReadKey();
            
        }

        public static void StartGame()
        {
            Console.Clear();
            Console.WriteLine("THY DUNGEONMAN\n");
            Console.WriteLine("YOU ARE THY DUNGEONMAN!\n");
            Player.GetCurrentRoom().Describe();               // This section 
            Text.Display();
        }

        public static void EndGame(string endingText)
        {
            

            Console.Clear();

            Console.WriteLine(TextUtils.WordWrap(endingText, Console.WindowWidth));
            Console.Write("\nThanks for playing. ");
            Console.CursorVisible = false;

            //while (true)
            //    Console.ReadKey(true);
            while (true)
            //char input = (string)Console.ReadKey();
            //if (input.ToLower() == "n")
            //{
            //    Program.quit = true;
            //}
            //else if (input.ToLower() == "y")
            //{
            //    Level.Initialize();
            //    Manager.StartGame();
            //}
            Console.ReadKey(true);
        }

        public static void ApplyRules()
        {
            Room room = Player.GetCurrentRoom();
            // You can keep track of how many times someone typed the wrong command and then display a specific message for that.

            if (Player.GetKilledEnemies("goblin") != null)  // Checks to see if the goblin has been killed before changing the room description.
            {
                Level.Rooms[1, 3].Description = "The corpse of a goblin lies in the middle of the floor, his axe and shield broken on the floor beside him.";
            }

            if (Player.GetKilledEnemies("skeleton") != null)
            {
                Level.Rooms[2, 3].Description = "Looking at the shattered bones of the defeated skeleton on the floor of this room, you are certain the guardian will not rise again.  You may approach the treasure chest without fear.";
            }

            if (Player.GetKilledEnemies("dog") != null)
            {
                Level.Rooms[2, 2].Description = "You step over the blood-soaked corpse of the canine who attacked you earlier.  The room smells of wet dog.";
            }

            //if (Player.GetCurrentRoom() == Level.Rooms[0, 1] && Level.Rooms[0, 1].GetEnemy("ghost") != null && Player.Move(string Direction))
            //{
            //    Text.Add("You turn and flee in terror, but not before the ghost brushes you with its bony finger, numbing your body with the chill touch of the grave.");
            //    Player.ge
            //}

            if (Player.GetInventoryItem("scroll") != null)  // If you picked up the scroll, then we need to change the search description.
            {
                Level.Rooms[1, 2].SearchDescription = "You search the room and find nothing of value.";
            }

            if (Player.GetKilledEnemies("ghost") != null)
            {
                Level.Rooms[0, 1].Description = "With the ghost vanquished, you notice the many furnishings of this chamber.  A table, desk, bookcases and several chairs fill the room.  There is a small chest in the corner.";
                Level.Rooms[0, 1].SearchDescription = "You examine each piece of furniture in detail, paying particular attention to the books on the shelf.  Your search proves fruitless.";
            }

            if (Player.GetKilledEnemies("gibberling") != null)
            {
                Level.Rooms[2, 0].Description = "With the gibberling disposed of, there is little else of note in this room.";
            }

            if (Player.GetKilledEnemies("mummy") != null)  // Killing the Mummy wins the game.
            {
                EndGame("As the mummy falls, the symbol of Helm erupts with a blinding light.  You feel the body of the spirit warrior dissolve, and your life force rushes back to your true body. \nThanks for playing!  Your score was: " + Player.Score.ToString() + ".");
            }

            if (Player.HealthValue <= 0)  //If the player's health reaches zero, it's game over.
            {
                Player.Score -= 1;
                EndGame("The spirit warrior collapses from its wounds. \nGAME OVER. Your score was: " + Player.Score + ". ");
            }
        }
    }
}
