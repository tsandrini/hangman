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

    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Game game;
        private MenuItemsComponent menuItems;
        private SoundEffect menuConfirm;

        public MenuComponent(Game game, MenuItemsComponent menuItems)
            : base(game)
        {
            this.game = game;
            this.menuItems = menuItems;
        }

        public override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.menuConfirm = this.game.Content.Load<SoundEffect>(@"SFX\menu_confirm");

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
        
            if (game.NewKeyboardKey(Keys.Enter))
            {
                this.ItemSubmitted();
            }

            if (this.game.IsMouseClicked())
            {
                foreach (MenuItem item in this.menuItems.items)
                {
                    Rectangle bounds = new Rectangle((int)item.pos.X, (int)item.pos.Y, (int)item.size.X, (int)item.size.Y);

                    if (bounds.Contains(game.mouse.X, game.mouse.Y))
                    {
                        this.ItemSubmitted();
                    }
                }
            }


            base.Update(gameTime);
        }

        private void ItemSubmitted()
        {
            switch (this.menuItems.selectedItem.identifier)
            {
                case "exit":
                    {
                        this.game.Exit();
                        break;
                    }
                case "new-game":
                    {
                        this.game.SwitchWindows(this.game.ingameWindow);
                        break;
                    }

            }

            this.menuConfirm.Play();
        }
    }
}
