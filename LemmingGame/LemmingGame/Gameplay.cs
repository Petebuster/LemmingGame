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
        WorldObjects WorldObjects;
        MiniMap MiniMap;
        private List<Worm> Worms;
        //Private Variables
        private TimeSpan time;
        private Texture2D fallingTexture;
        private Texture2D idleTexture;
        private Texture2D walkingTexture;
        private Texture2D stoppingTexture;
        private Texture2D dyingTexture;
        private Texture2D breakingTexture;
        private Texture2D diggingTexture;
        private Texture2D weldingTexture;
        private Texture2D bouncingTexture;
        private Texture2D jumpingTexture;
        private Texture2D jetPackTexture;
        private Texture2D selectorTexture;
        private Texture2D miniMapTexture;
        private Texture2D backgoundTexture;
        private int wormCount = 6;
        private int i = 0;
        private static ContentManager content;
        public bool WeldingActive;
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
            idleTexture = content.Load<Texture2D>("wormIdle");
            walkingTexture = Content.Load<Texture2D>("wormWalking");
            stoppingTexture = Content.Load<Texture2D>("wormStopper");
            dyingTexture = Content.Load<Texture2D>("wormDying");
            breakingTexture = Content.Load<Texture2D>("Breakables");
            diggingTexture = Content.Load<Texture2D>("wormDigging");
            weldingTexture = Content.Load<Texture2D>("wormWelding");
            bouncingTexture = Content.Load<Texture2D>("wormBouncing");
            jumpingTexture = Content.Load<Texture2D>("wormJumping");
            jetPackTexture = Content.Load<Texture2D>("wormJetPack");
            selectorTexture = Content.Load<Texture2D>("selector1");
            miniMapTexture = Content.Load<Texture2D>("minimapB");
            backgoundTexture = Content.Load<Texture2D>("");
            Camera = new Camera();
            Map = new Map(); Map.Initialize();
            time = new TimeSpan();
            Worms = new List<Worm>();
            WorldObjects = new WorldObjects(Content.Load<Texture2D>("0"), Content.Load<Texture2D>("movePlattform"));
            MiniMap = new MiniMap(miniMapTexture);
        }

        public void LoadContent()
        {
            Tile.Content = Content;
            Timer.Content = Content;
            Map.GenerateMap(new int[,] {
                //1-2 Bricks
                //3-5 Floor
                //6-8 Ground
                //9-16 Slopes

                //{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                //{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                //{2,2,1,2,1,2,1,2,2,2,2,2,2,2,2,2},
                //{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,1,0,0,0,0,0,0,0,0,0,0,0,0,1,0},
                //{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                //{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2},
                //{2,2,1,2,1,2,1,2,2,2,2,2,2,2,2,2},
                //{2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},

                //{0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,0,0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,1,1,2,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,1,1,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{1,1,1,1,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{1,0,0,1,1,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,1,1,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0},
                //{0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,0,1,1,0,0},
                //{0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,1,1,0,1,0},
                //{0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,1,1,0,0,1,0},
                //{0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,1,1,0,0,1,0,0},
                //{0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,1,1,0,0,0,0,0,0},
                //{0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0},

                {9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {9,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {3,4,5,3,4,5,3,4,5,3,4,5,3,4,5,3,4,5,3,4,5,3,4 },
                {6,7,8,6,7,8,6,7,8,6,7,8,6,7,8,6,7,8,6,7,8,6,7 },
                {8,6,7,8,6,7,8,6,7,8,6,0,0,8,0,0,0,0,0,0,0,0,6 },
                {7,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,8 },
                {6,7,8,6,7,8,6,7,8,6,7,8,6,7,8,6,7,8,6,7,8,6,7 },
            }, 32);
        }

        public void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            time += gameTime.ElapsedGameTime;
            Camera.CameraInput();
            WorldObjects.Update(gameTime);
            GenerateWorms();
            
            foreach (var worm in Worms)
            {
                worm.Update(gameTime, Camera.CameraPos);
                worm.WorldObjectCollision(WorldObjects.donkeyRec);
                if (RectangleCollision.PixelIntersect(worm.WormRectangle, worm.textureData, WorldObjects.donkeyRec, WorldObjects.textureData))
                    Console.WriteLine("Touched");
                //Tilemap Collision
                foreach (var tile in Map.CollisionList)
                {
                    //if (worm.BreakingAnimation.IsDead)
                    //    tile.TileActive = false;

                    if (tile.tileActive)
                         worm.Collision(spriteBatch, gameTime, tile.TileRectangle, tile);
                   
                    //if (WeldingActive)
                    //{
                    //    tile.WeldTile();
                    //    WeldingActive = false;
                    //}
                }
                //Blocker Collision
                if (worm.state == Worm.wormState.Blocking)
                    foreach (var Blocker in Worms)
                        if (Blocker.state == Worm.wormState.Walking)
                            Blocker.BlockerCollision(worm.WormRectangle, worm.DirectionRight);

                //Bouncer Collision
                if (worm.state == Worm.wormState.Bouncing)
                    foreach (var Bouncer in Worms)
                        if (Bouncer.state == Worm.wormState.Walking)
                            Bouncer.BouncerCollision(worm.WormRectangle, worm.DirectionRight);
            }
        }
        public void BreakTile(Collision tile)
        {
            tile.tileActive = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            
            Map.Draw(spriteBatch, Camera.CameraPos);
            WorldObjects.Draw(spriteBatch, Camera.CameraPos);
            foreach (var worm in Worms)
                worm.Draw(spriteBatch, Camera.CameraPos);
            MiniMap.Draw(spriteBatch);
            foreach (var tile in Map.CollisionList)
                 MiniMap.DrawTile(spriteBatch, tile.tileTexture, tile.TileRectangle);
            
        }

        private void GenerateWorms()
            
        {
            if (i < wormCount)
                if (time.TotalSeconds > 2)
                {
                    Worms.Add(new Worm(fallingTexture, idleTexture, walkingTexture, stoppingTexture, dyingTexture, breakingTexture, diggingTexture, weldingTexture, bouncingTexture, jumpingTexture, jetPackTexture, selectorTexture, this));
                    time = new TimeSpan();
                    i++;
                }
        }

        public void Deselect()
        {
            foreach (var worm in Worms)
                worm.Selected = false;
        }
        public void WeldTile(Collision tile)
        {
            //tile.TileRectangle = new Rectangle();
        }
    }
}
