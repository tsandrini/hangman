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

    public class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public CoolFont spriteBatch;
        public KeyboardState keys, lastKey;
        public MouseState mouse, lastMouse;
        public GameWindow loadingScreen, menuWindow, ingameWindow;
        public SpriteFont fontDistInking, characterFont, guessingWordFont;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // MENU ITEMS INIT
            MenuItemsComponent menuItems = new MenuItemsComponent(this, new Vector2(this.getViewportWidth() * 0.45f, this.getViewportHeight() * 0.75f), Color.Blue, Color.Yellow, 75);
            menuItems.AddItem("Hrát", "new-game");
            //menuItems.AddItem("Výsledky", "high-score");
            //menuItems.AddItem("Nastavení", "settings");
            menuItems.AddItem("Odejít", "exit");

            // COMPONENTS INIT
            ScrollingBackgroundComponent scrolling = new ScrollingBackgroundComponent(this);
            this.Components.Add(scrolling);
            MenuComponent menu = new MenuComponent(this, menuItems);
            this.Components.Add(menu);
            LevelComponent level = new LevelComponent(this);
            this.Components.Add(level);

            // GAMEWINDOWS INIT
            this.menuWindow = new GameWindow(this, menu, menuItems, scrolling);
            this.ingameWindow = new GameWindow(this, level, scrolling);

            // TURN OFF ALL COMPONENTS
            foreach (GameComponent component in this.Components)
            {
                this.SwitchComponent(component, false);
            }

            // BASE GRAPHICS STUFF
            this.IsMouseVisible = true;
            //this.graphics.IsFullScreen = true;
            this.graphics.PreferredBackBufferHeight = 1080;
            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.ApplyChanges();

            // INIT MENU WINDOW
            this.SwitchWindows(menuWindow);

            base.Initialize();
        }

        private void SwitchComponent(GameComponent component, bool state)
        {
            component.Enabled = state;
            if (component is DrawableGameComponent)
            {
                ((DrawableGameComponent)component).Visible = state;
            }
        }

        public bool NewKeyboardKey(Keys key)
        {
            return keys.IsKeyDown(key) && lastKey.IsKeyUp(key);
        }

        public Vector2 getMousePosChange()
        {
            return new Vector2(mouse.X - lastMouse.X, mouse.Y - lastMouse.Y);
        }

        public bool IsMouseClicked()
        {
            return lastMouse.LeftButton == ButtonState.Released && mouse.LeftButton == ButtonState.Pressed;
        }

        public void SwitchWindows(GameWindow window)
        {
            GameComponent[] granted = window.ReturnComponents();
            foreach (GameComponent component in Components)
            {
                bool enabled = granted.Contains(component);
                this.SwitchComponent(component, enabled);
            }

            lastKey = keys;
        }

        protected override void LoadContent()
        {
            // FONTS
            this.spriteBatch = new CoolFont(GraphicsDevice);
            this.fontDistInking = Content.Load<SpriteFont>(@"Fonts\menuFont");
            this.characterFont = Content.Load<SpriteFont>(@"Fonts\characterFont");
            this.guessingWordFont = Content.Load<SpriteFont>(@"Fonts\guessingWordFont");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime GameTime)
        {
            lastMouse = mouse;
            lastKey = keys;
            keys = Keyboard.GetState();
            mouse = Mouse.GetState();

            base.Update(GameTime);
        }

        protected override void Draw(GameTime GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            base.Draw(GameTime);
        }

        public int getViewportWidth()
        {
            return this.graphics.GraphicsDevice.Viewport.Width;
        }

        public int getViewportHeight()
        {
            return this.graphics.GraphicsDevice.Viewport.Height;
        }
    }
}
