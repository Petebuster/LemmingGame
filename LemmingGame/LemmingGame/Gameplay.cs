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
        private List<Worm> Worms;
        //Private Variables
        private TimeSpan time;
        private Texture2D fallingTexture;
        private Texture2D walkingTexture;
        private Texture2D stoppingTexture;
        private Texture2D dyingTexture;
        private Texture2D breakingTexture;
        private Texture2D diggingTexture;
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
            fallingTexture = Content.Load<Texture2D>("wormFalling");
            walkingTexture = Content.Load<Texture2D>("wormWalking");
            stoppingTexture = Content.Load<Texture2D>("wormStopper");
            dyingTexture = Content.Load<Texture2D>("wormDying");
            breakingTexture = Content.Load<Texture2D>("Breakables");
            diggingTexture = Content.Load<Texture2D>("wormDigging");
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
                {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                {2,2,1,2,1,2,1,2,2,2,2,2,2,2,2,2},
                {0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                {2,2,1,2,1,2,1,2,2,2,2,2,2,2,2,2},
                {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},

            }, 32);
        }

        public void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            time += gameTime.ElapsedGameTime;
            Camera.CameraInput();
           
            GenerateWorms();
            
            foreach (var worm in Worms)
            {
                worm.Update(gameTime, Camera.CameraPos);
                

                //Tilemap Collision
                foreach (var tile in Map.CollisionList)
                {
                    //if (worm.BreakingAnimation.IsDead)
                    //    tile.TileActive = false;
                    if (tile.tileActive)
                         worm.Collision(spriteBatch, gameTime, tile.TileRectangle, tile);
                    

                }
                //Blocker Collision
                if (worm.state == Worm.wormState.Blocking)
                    foreach (var Blocker in Worms)
                        if (Blocker.state == Worm.wormState.Walking)
                            Blocker.BlockerCollision(worm.WormRectangle, worm.DirectionRight);
            }
        }
        public void BreakTile(Collision tile)
        {
            tile.tileActive = false;
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
                    Worms.Add(new Worm(fallingTexture, walkingTexture, stoppingTexture, dyingTexture, breakingTexture, diggingTexture, selectorTexture, this));
                    time = new TimeSpan();
                    i++;
                }
        }

        
    }
}
