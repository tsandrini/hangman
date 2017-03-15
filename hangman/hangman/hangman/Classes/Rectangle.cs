using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace hangman
{
    public class RectangleSprite
    {
        static Texture2D _pointTexture;
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            if (_pointTexture == null)
            {
                _pointTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _pointTexture.SetData<Color>(new Color[] { Color.White });
            }

            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + lineWidth, lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(_pointTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + lineWidth, lineWidth), color);
        }

        public static void DrawMuchCoolerRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            DrawRectangle(spriteBatch, rectangle, color, lineWidth);
            DrawRectangle(spriteBatch, new Rectangle(rectangle.X + 2, rectangle.Y + 2, rectangle.Width, rectangle.Height), Color.Black, 1);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 end, Color color, int width = 1)
        {
            Vector2 edge = end - start;

            float angle = (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(texture,
                new Rectangle(
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(),
                    width),
                null,
                color,
                angle,   
                new Vector2(0, 0), 
                SpriteEffects.None,
                0);
        }
    }
}
