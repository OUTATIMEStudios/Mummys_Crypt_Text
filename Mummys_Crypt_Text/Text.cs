using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mummys_Crypt_Text
{
    static class Text
    {
        private static string outputBuffer;

        public static void Add(String text)
        {
            outputBuffer += text + "\n";  // adds new text to text already there and waiting to be displayed.
        }

        public static void Display()
        {
            // Displays message along with a prompt
            Console.Clear();
            Console.Write(TextUtils.WordWrap(outputBuffer, Console.WindowWidth));  //This will word wrap regardless of the window size.
            Console.WriteLine("\nWhat will you do?");
            Console.Write("> ");                                // SInce this is Write and not WriteLine, the cursor remains on the same line

            outputBuffer = "";  // After displaying prompt, this will clear out the data in the outputBuffer so new text can be added
        }
    }
}
