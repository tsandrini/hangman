using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hangman
{
    public class StringHelper
    {
        public static string ReplaceAt(string input, int index, char newChar)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
    }
}
