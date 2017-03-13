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
        public Vector2 pos;
        public Vector2 size;
        public float scale;

        public Character(Vector2 pos, char character)
        {
            this.pos = pos;
            this.character = character;
            this.scale = 0.6f;
        }

        public Character(Vector2 pos, char character, float scale)
        {
            this.pos = pos;
            this.character = character;
            this.scale = scale;
        }
    }
}
