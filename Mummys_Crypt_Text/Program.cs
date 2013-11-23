using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mummys_Crypt_Text
{
    class Program
    {
        public static bool quit = false;

        static void Main(string[] args)
        {
            Manager.ShowTitleScreen();
            Level.Initialize();        // Initialize the Level (and calls BuildLevel method)
            Manager.StartGame();

            while (!quit)  // Main Game Loop - !quit means "not quitting" because it reverses boolean values (like quit == false)
            {
                Commands.ProcessCommand(Console.ReadLine());   // Reads user input using Console.ReadLine and passes it to ProcessCommand
            }                                                  // The Text class will handle displaying text.

        }

        public static void Quit()
        {
            Text.Add("Play again? [Y/N] ");
            Text.Display();   // Not quite right.
            Console.ReadLine();
            
        }
    }
}
