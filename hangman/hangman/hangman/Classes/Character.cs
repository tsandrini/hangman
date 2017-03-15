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
        public bool enabled;

        public Character(Rectangle rectangle, char character, Color color)
        {
            this.character = character;
            this.rectangle = rectangle;
            this.scale = 0.6f;
            this.color = color;
            this.enabled = true;
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
            if (this.enabled)
            {
                spriteBatch.MuchCoolerFont(spriteFont,
                    this.character.ToString(),
                    new Vector2(this.rectangle.X + this.rectangle.Width * 0.35f, this.rectangle.Y + this.rectangle.Height * 0.3f),
                    color, 0.6f);
            }
        }

    }
}
