using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace hangman
{
    public class Character
    {
        public char character;
        public Rectangle rectangle;
        public float scale;
        public Color color;

        public Character(Rectangle rectangle, char character, Color color)
        {
            this.character = character;
            this.rectangle = rectangle;
            this.scale = 0.6f;
            this.color = color;
        }

        public Character(Rectangle rectangle, char character, float scale, Color color)
        {
            this.rectangle = rectangle;
            this.character = character;
            this.scale = scale;
            this.color = color;
        }

        public void Draw(CoolFont spriteBatch, SpriteFont spriteFont)
        {
            spriteBatch.MuchCoolerFont(spriteFont,
                this.character.ToString(),
                new Vector2(this.rectangle.X + this.rectangle.Width * 0.35f, this.rectangle.Y + this.rectangle.Height * 0.3f),
                color, 0.6f);
        }

    }
}
