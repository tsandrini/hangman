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
        private static string avaibleCharacters = "a·bcËdÔeÈÏfghiÌjklmnÚoÛpqr¯sötùu˙˘vwxy˝zû";
        private static string word = "test";

        private Game game;
        private Rectangle inputRectangle, hangmanRectangle;
        private List<Character> characters;
        private SpriteFont characterFont;
        private Character selectedItem;
        private Color selectedCharacterColor;
        private Color unselectedCharacterColor;
        private Vector2 relativeRectangleSize, guessWordPos;
        private string guessWord;

        public LevelComponent(Game game)
            : base(game)
        {
            this.game = game;
            this.characters = new List<Character>();
            this.selectedCharacterColor = Color.Magenta;
            this.unselectedCharacterColor = Color.Blue;
            this.guessWord = "";
        }

        public override void Initialize()
        {
            for (int i = 0; i < word.Length; i++)
            {
                this.guessWord += "_";
            }

            this.inputRectangle = new Rectangle(
                (int)(this.game.getViewportWidth() * 0.15f),
                (int)(this.game.getViewportHeight() * 0.65f),
                (int)(this.game.getViewportWidth() * 0.7f),
                (int)(this.game.getViewportHeight() * 0.32f)
            );

            this.hangmanRectangle = new Rectangle(
                (int)(this.inputRectangle.X),
                (int)((this.inputRectangle.Y) * 0.30f),
                (int)((this.inputRectangle.Width * 0.5f) * 0.7f),
                (int)((this.inputRectangle.Y) * 0.55f)
            );

            this.guessWordPos = new Vector2(
                (this.game.getViewportWidth() - this.hangmanRectangle.X + this.hangmanRectangle.Width) * 0.6f,
                (this.hangmanRectangle.Y + this.hangmanRectangle.Height) * 0.65f
            );

            // TODO: ??????
            Vector2 relativeCharacterSize = new Vector2(35, 65);


            this.relativeRectangleSize = new Vector2(relativeCharacterSize.X + this.game.getViewportWidth() * 0.015f,
                    relativeCharacterSize.Y + this.game.getViewportHeight() * 0.015f);

            int onCurrentLineCharCount, currentLine;
            onCurrentLineCharCount = currentLine = 0;
            float spaceBetweenChars = this.game.getViewportWidth() * 0.015f;

            foreach (char c in avaibleCharacters)
            {
                // If another character would exceed the line go to new line
                if (((onCurrentLineCharCount + 1) * (this.relativeRectangleSize.X + spaceBetweenChars)) >= inputRectangle.Width)
                {
                    currentLine++;
                    onCurrentLineCharCount = 0;
                }

                Rectangle rect = new Rectangle(
                    (int)(inputRectangle.X +  (onCurrentLineCharCount * (this.relativeRectangleSize.X + spaceBetweenChars) + spaceBetweenChars)),
                    (int)(inputRectangle.Y + ( currentLine * ((this.relativeRectangleSize.Y + spaceBetweenChars))) + spaceBetweenChars),
                    (int)this.relativeRectangleSize.X,
                    (int)this.relativeRectangleSize.Y
                );

                // CHARACTER SETUP
                Character character = new Character(rect, c, Color.Yellow);
                this.characters.Add(character);

                onCurrentLineCharCount++;
            }

            base.Initialize();
        }

        public void CharacterClicked(Character character)
        {
            char clickedCharacter = character.character;

            for (int i = word.IndexOf(clickedCharacter); i > -1; i = word.IndexOf(clickedCharacter, i + 1))
            {
                guessWord = StringHelper.ReplaceAt(guessWord, i, clickedCharacter);
            }

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

            if (this.game.getMousePosChange().X > 0 || this.game.getMousePosChange().Y > 0)
            {
                if (inputRectangle.Contains(game.mouse.X, game.mouse.Y))
                {
                    bool somethingSelected = false;

                    foreach (Character character in this.characters)
                    {
                        if (character.rectangle.Contains(this.game.mouse.X, this.game.mouse.Y))
                        {
                            this.selectedItem = character;
                            somethingSelected = true;
                            break;
                        }
                    }

                    if (!somethingSelected)
                    {
                        this.selectedItem = null;
                    }
                }
            }

            if (game.IsMouseClicked())
            {
                if (this.inputRectangle.Contains(this.game.mouse.X, this.game.mouse.Y))
                {
                    foreach (Character character in this.characters)
                    {
                        if (character.rectangle.Contains(this.game.mouse.X, this.game.mouse.Y))
                        {
                            this.CharacterClicked(character);
                        }
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.game.spriteBatch.Begin();
            
            // INPUT RECTANGLE
            RectangleSprite.DrawMuchCoolerRectangle(this.game.spriteBatch, this.inputRectangle, Color.DarkBlue, 6);

            Texture2D texture = new Texture2D(this.game.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            texture.SetData<Color>(new Color[] { Color.White });

            this.game.spriteBatch.Draw(texture, this.hangmanRectangle, Color.GhostWhite);
            RectangleSprite.DrawMuchCoolerRectangle(this.game.spriteBatch, this.hangmanRectangle, Color.BlueViolet, 5);

            foreach (Character character in this.characters)
            {
                this.game.spriteBatch.Draw(texture, character.rectangle, (character == this.selectedItem) ? this.selectedCharacterColor : this.unselectedCharacterColor);
                RectangleSprite.DrawRectangle(this.game.spriteBatch, character.rectangle, Color.Black, 1);
                character.Draw(this.game.spriteBatch, this.game.characterFont);
            }

            this.game.spriteBatch.MuchCoolerFont(this.game.guessingWordFont, this.guessWord, guessWordPos, Color.Blue, 0.6f);

            this.game.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
