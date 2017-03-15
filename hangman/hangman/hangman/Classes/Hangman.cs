using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace hangman
{
    public class Hangman
    {
        private Rectangle rectangle;
        private List<Texture2D> sprites;
        private int defaultLives, lives;
        private WordGenerator wordGenerator;
        private string guessWord, secretWord;
        private Random random;

        public Hangman(Rectangle rectangle, int lives, string filename)
        {
            this.rectangle = rectangle;
            this.defaultLives = lives;
            this.lives = lives;
            this.sprites = new List<Texture2D>();
            this.wordGenerator = new WordGenerator(filename);
            this.guessWord = this.secretWord = "";
            this.random = new Random();
        }

        public void Reset()
        {
            this.lives = defaultLives;
            InitWord();
        }

        public void InitWord()
        {
            this.guessWord = "";

            this.secretWord = this.wordGenerator.GenerateWord();

            int maxShowChars = this.secretWord.Length / 2;
            int showedChars = 0;

            for (int i = 0; i < secretWord.Length; i++)
            {
                if (showedChars != maxShowChars && this.random.Next(2) == 1)
                {
                    this.guessWord += this.secretWord[i].ToString();
                    showedChars++;
                    continue;
                } 

                this.guessWord += "_";
            }
        }

        public void setSprites(List<Texture2D> sprites)
        {
            this.sprites = sprites;
        }

        public void DecrementLife()
        {
            this.lives--;
        }

        public void Draw(CoolFont spriteBatch)
        {
            if (this.sprites.Any() && this.lives > 0)
            {
                spriteBatch.Draw(this.sprites[this.lives - 1], this.rectangle, Color.White);
            }
        }

        public int getLives()
        {
            return this.lives;
        }

        public string getSecretWord()
        {
            return this.secretWord;
        }

        public string getGuessWord()
        {
            return this.guessWord;
        }

        public void setGuessWord(string guessWord)
        {
            this.guessWord = guessWord;
        }
    }
}
