using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mummys_Crypt_Text
{
    static class Commands
    {
        //This will contain all the verb noun stuff
        public static void ProcessCommand(string line)
        {
            string command = TextUtils.ExtractCommand(line.Trim()).Trim().ToLower();
            string arguments = TextUtils.ExtractArguments(line.Trim()).Trim().ToLower();

            if (Direction.IsValidDirection(command))
            {
                Player.Move(command);  // moves the player if just "north" or "east" is typed
            }
            else
            {
                switch (command)
                {
                    case "exit":
                    case "quit":
                        Program.quit = true;
                        return;
                    case "help":
                        ShowHelp();
                        break;
                    case "move":
                    case "go":
                        Player.Move(arguments);
                        break;
                    case "look":
                        // Player.GetCurrentRoom().Describe();
                        Player.Look(arguments);
                        break;
                    case "search":
                        Player.Search(arguments);
                        break;
                    case "open":
                        Player.Open(arguments);
                        break;
                    case "get":
                    case "take":
                    case "pickup":
                        Player.PickupItem(arguments);
                        break;
                    case "get dagger":
                        Text.Add("Yeah, okay.");
                        break;
                    case "drop":
                        Player.DropItem(arguments);
                        break;
                    case "inventory":
                    case "inv":
                        Player.DisplayInventory();
                        break;
                    case "whereami":
                        Player.GetCurrentRoom().ShowTitle();
                        break;
                    case "attack":
                    case "fight":
                    case "kill":
                        Player.Attack(arguments);
                        break;
                    case "drink":
                        Player.Drink(arguments);
                        break;
                    case "die":
                        Player.Die();
                        break;
                    case "status":
                        Text.Add("HP = " + Player.HealthValue);
                        break;
                    default:   // if there is anything else typed besides the cases above.
                        Text.Add("I don't understand. Type HELP if you need it.");
                        break;
                }
            }
            
            Manager.ApplyRules();  // This will check for conditions (such as winning the game) based on what you typed above.
            Text.Display();        // Displays any messages built up in the text buffer
        }

        public static void ShowHelp()
        {
           // Text.Add("Available Commands:");
           // Text.Add("Go North - go to the north room");
           // Text.Add("Dance - makes you dance");

            Player.GetCurrentRoom().Describe();   // Thy Dungeonman only shows the room description for HELP
        }
    }
}
