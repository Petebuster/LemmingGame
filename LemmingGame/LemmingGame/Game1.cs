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

namespace LemmingGame
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private enum screenState { Unspecified, Splash, Menu, Game}
        screenState state = new screenState();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Gameplay Gameplay;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            state = screenState.Game;
        
            switch (state)
            {
                case screenState.Splash: break;
                case screenState.Menu: break;
                case screenState.Game:
                    Gameplay = new Gameplay();
                    Gameplay.Content = Content;
                    Gameplay.Initialize(); 
                    break;
            }
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Gameplay.LoadContent();
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            switch (state)
            {
                case screenState.Splash: break;
                case screenState.Menu: break;
                case screenState.Game: Gameplay.Update(gameTime, spriteBatch); break;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            switch (state)
            {
                case screenState.Splash: break;
                case screenState.Menu: break;
                case screenState.Game: Gameplay.Draw(spriteBatch); break;
            }

            base.Draw(gameTime);
        }
    }
}
