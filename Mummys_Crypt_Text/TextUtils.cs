using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mummys_Crypt_Text
{
    static class TextUtils
    {
        public static string ExtractCommand(string line)
        {
            int index = line.IndexOf(' ');   // Goes through user's line of text to find a space ' ')
            if (index == -1)      // If there is no space character, the IndexOf will return -1
                return line;      // So just return the line, which is obviously a command (such as "help" "look", etc.)
            else
                return line.Substring(0, index);  // Else if there is a space, we need to just return the text before that space as a command
        }

        public static string ExtractArguments(string line)
        {
            int index = line.IndexOf(' ');
            if (index == -1)
                return "";          //  THis is the opposite of above, if there is no space, then return a null string, there is no arguement
            else
                return line.Substring(index + 1, line.Length - index - 1);  // index + 1 to grab from the next character after space
                                                                            // line.Length-index-1 to get the correct arguement length
        }

        // This allows the words to wrap on the screen.
        public static string WordWrap(String text, int bufferWidth)  // Shoud I use "String" or "string"  ??
        {
            String result = "";
            String[] lines = text.Split('\n');

            foreach (String line in lines)
            {
                int lineLength = 0;
                String[] words = line.Split(' ');

                foreach (String word in words)
                {
                    // If the word is longer than the screen...
                    if (word.Length + lineLength >= bufferWidth - 1)   // Use -1 to pad the screen by one character
                    {
                        result += "\n";  //Inserts a new line to carry the word to the next line
                        lineLength = 0;
                    }
                    result += word + " "; // The Split(' ') above removes the space, so this adds the space back.
                    lineLength += word.Length + 1;  //add to the word counter
                }
                result += "\n";  // Adds a new line to the end of the line of text.
            }
            return result;
        }
    }
}
