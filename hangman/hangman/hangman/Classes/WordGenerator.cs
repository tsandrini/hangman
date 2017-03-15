using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace hangman
{
    public class WordGenerator
    {
        private string filename;
        private int linesCount;
        private Random random;

        public WordGenerator(string filename)
        {
            if (File.Exists(filename))
            {
                this.filename = filename;
                this.linesCount = File.ReadLines(filename).Count();
                this.random = new Random();
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        public string GenerateWord()
        {
            int randomLine = random.Next(this.linesCount);

            int counter = 0;
            string line = "";

            StreamReader sr = new StreamReader(this.filename);

            while ((line = sr.ReadLine()) != null)
            {
                counter++;
                if (counter == randomLine)
                {
                    break;
                }
            }

            return line;
        }
    }
}
