using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemmingGame
{
    class Gameplay
    {
        // Classes
        Map Map;
        Camera Camera;
        //Private Variables
        private List<Worm> Worms;
        private TimeSpan time;
        private Texture2D walkingTexture;
        private Texture2D stoppingTexture;
        private Texture2D dyingTexture;
        private Texture2D selectorTexture;
        private int wormCount = 3;
        private int i = 0;
        private static ContentManager content;
        //Public Variables
        public static ContentManager Content
        {
            get { return content; }
            set { content = value; }
        }
        //Methods
        public void Initialize()
        {
            walkingTexture = Content.Load<Texture2D>("wormWalking");
            stoppingTexture = Content.Load<Texture2D>("wormStopper");
            dyingTexture = Content.Load<Texture2D>("wormDying");
            selectorTexture = Content.Load<Texture2D>("selector1");
            Camera = new Camera();
            Map = new Map(); Map.Initialize();
            time = new TimeSpan();
            Worms = new List<Worm>();
        }

        public void LoadContent()
        {
            Tile.Content = Content;
            Map.GenerateMap(new int[,] {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            }, 32);
        }

        public void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime;
            Camera.CameraInput();
           
            GenerateWorms();

            foreach (var worm in Worms)
            {
                worm.Update(gameTime, Camera.CameraPos);
                foreach (var tile in Map.CollisionList)
                    worm.Collision(tile.TileRectangle);


                if(worm.state == Worm.wormState.Blocking)
                foreach (var Blocker in Worms)
                    if (Blocker.state == Worm.wormState.Walking)
                         Blocker.BlockerCollision(worm.WormRectangle, worm.DirectionRight);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Map.Draw(spriteBatch, Camera.CameraPos);
            foreach (var worm in Worms)
                worm.Draw(spriteBatch, Camera.CameraPos);
        }

        private void GenerateWorms()
            
        {
            if (i < wormCount)
                if (time.TotalSeconds > 2)
                {
                    Worms.Add(new Worm(walkingTexture, stoppingTexture, dyingTexture, selectorTexture));
                    time = new TimeSpan();
                    i++;
                }
        }

        private void BlockerCollision()
        {

        }
    }
}
