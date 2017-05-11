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

        protected int test;

        public Texture2D tileTexture;
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
            spriteBatch.Draw(tileTexture, new Rectangle((int)tileRectangle.X -(int) _cameraPos.X , (int)tileRectangle.Y - (int)_cameraPos.Y, tileRectangle.Width, tileRectangle.Height), Color.White);
            spriteBatch.End();

            //spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateScale(0.25f));
            //if (tileActive)
            //    spriteBatch.Draw(tileTexture, new Rectangle((int)tileRectangle.X + 2400, (int)tileRectangle.Y + 1460, tileRectangle.Width, tileRectangle.Height), Color.White);
            //spriteBatch.End();


        }

        public void WeldTile()
        {
            tileRectangle.Width -= test;
            test += 2;
        }
    }

    class Collision : Tile
    {

        public Collision(int number, Rectangle _collisionRect)
        {
            tileTexture = Content.Load<Texture2D>("Tile" + number);
           // TileRectangle = _collisionRect; // new Rectangle(_collisionRect.X, _collisionRect.Y, _collisionRect.Width - test, _collisionRect.Height);
            TileRectangle = new Rectangle(_collisionRect.X, _collisionRect.Y, _collisionRect.Width, _collisionRect.Height);
        }
        
    }
}
