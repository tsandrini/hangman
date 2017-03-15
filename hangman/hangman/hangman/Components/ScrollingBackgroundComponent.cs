using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace hangman
{
 
    public class ScrollingBackgroundComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game game;
        private List<ScrollingBackground> backgrounds;

        public ScrollingBackgroundComponent(Game game)
            : base(game)
        {
            this.game = game;
            this.backgrounds = new List<ScrollingBackground>();
        }

        public void AddBackground(ScrollingBackground background)
        {
            if (!this.backgrounds.Contains(background))
            {
                this.backgrounds.Add(background);
            }
        }

        public void Reset()
        {
            this.backgrounds.Clear();
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.AddBackground(new ScrollingBackground(this.game.Content.Load<Texture2D>(@"Graphics\Backgrounds\image_part_001"),
                new Rectangle(0, 0, this.game.getViewportWidth(), this.game.getViewportHeight())));

            this.AddBackground(new ScrollingBackground(this.game.Content.Load<Texture2D>(@"Graphics\Backgrounds\image_part_002"),
                new Rectangle(this.game.getViewportWidth(), 0, this.game.getViewportWidth(), this.game.getViewportHeight())));

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < backgrounds.Count; i++)
            { 
                if (backgrounds[i].rectangle.X + backgrounds[i].rectangle.Width <= 0)
                {
                    if (backgrounds[i] == this.backgrounds.Last())
                    {
                        backgrounds[i].rectangle.X = backgrounds.First().rectangle.X + backgrounds.First().rectangle.Width;
                    } 
                    else
                    {
                        backgrounds[i].rectangle.X = backgrounds[i + 1].rectangle.X + backgrounds[i + 1].rectangle.Width;
                    }
                }

                backgrounds[i].Update();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.game.spriteBatch.Begin();

            foreach (ScrollingBackground background in this.backgrounds)
            {
                background.Draw(this.game.spriteBatch);
            }

            this.game.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
