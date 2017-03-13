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

    public class LevelComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game game;
        private Rectangle inputRectangle, oldInputRectangle;
        private List<Character> characters;
        private SpriteFont characterFont;

        public LevelComponent(Game game)
            : base(game)
        {
            this.game = game;
        }

        public override void Initialize()
        {
            this.inputRectangle = new Rectangle((int)(this.game.getViewportWidth() * 0.05f), (int)(this.game.getViewportHeight() * 0.55f),
                                (int)(this.game.getViewportWidth() * 0.9f), (int)(this.game.getViewportHeight() * 0.4f));

            this.oldInputRectangle = new Rectangle((int)(this.game.getViewportWidth() * 0.8f), (int)(this.game.getViewportHeight() * 0.2f),
                                (int)(this.game.getViewportWidth() * 0.15f), (int)(this.game.getViewportHeight() * 0.3f));


            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.characterFont = this.game.Content.Load<SpriteFont>(@"Fonts\characterFont");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (game.NewKeyboardKey(Keys.Escape))
            {
                this.game.SwitchWindows(this.game.menuWindow);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.game.spriteBatch.Begin();
                                
            RectangleSprite.DrawRectangle(this.game.spriteBatch, this.inputRectangle, Color.DodgerBlue, 5);
            RectangleSprite.DrawRectangle(this.game.spriteBatch, this.oldInputRectangle, Color.Azure, 3);

            Vector2 relativeCharacterSize = this.characterFont.MeasureString('Ž'.ToString());

            /*
            int currentLineWidth = 0;

            for (char c = 'A'; c <= 'Ž'; c++)
            {
                Vector2 size = this.characterFont.MeasureString(c.ToString());
                
            }
            */

            this.game.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
