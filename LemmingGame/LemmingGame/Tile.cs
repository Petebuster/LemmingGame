using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemmingGame
{
     class Tile
    {
        Animation breakingAnimation = new Animation();

        public bool tileActive = true;


        protected Texture2D tileTexture;
        private Rectangle tileRectangle;
        public Rectangle TileRectangle
        {
            get { return tileRectangle; }
            set { tileRectangle = value; }
        }

        private static ContentManager content;
        public static ContentManager Content
        {
            protected get { return content; }
            set { content = value; }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 _cameraPos)
        {
            spriteBatch.Begin();
            if(tileActive)
            spriteBatch.Draw(tileTexture, new Rectangle((int)tileRectangle.X -(int) _cameraPos.X, (int)tileRectangle.Y - (int)_cameraPos.Y, tileRectangle.Width, tileRectangle.Height), Color.White);
            spriteBatch.End();
        }
    }

    class Collision : Tile
    {

        public Collision(int number, Rectangle _collisionRect)
        {
            tileTexture = Content.Load<Texture2D>("Tile" + number);
            TileRectangle = _collisionRect;
        }
    }
}
