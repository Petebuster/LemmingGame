using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemmingGame
{
    class Animation
    {//animation Height: 21 px 
        private int frameCounter, switchFrame;
        private Vector2 position, amountOfFrames, currentFrame;
        private Texture2D image;
        private Rectangle sourceRect;
        private bool active;

        public bool Active
        {
            set { active = value; }
        }

        public Vector2 CurrentFrame
        {
            get { return currentFrame; }
            set { currentFrame = value; }
        }
        public int FrameWidth
        {
            get { return image.Width / (int)amountOfFrames.X; }
        }

        public int FrameHeight
        {
            get { return image.Height / (int)amountOfFrames.Y; }
        }

        public void Initialize(Vector2 _position, Vector2 _Frames, int _switchFrame, Texture2D _spriteSheet)
        {
            active = false;
            switchFrame = _switchFrame;
            position = _position;
            amountOfFrames = _Frames;
            image = _spriteSheet;
        }

        public void Update(GameTime gameTime)
        {if (active)
                frameCounter += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            else
                frameCounter = 0;

            if (frameCounter >= switchFrame)
            {
                frameCounter = 0;
                currentFrame.X += FrameWidth;
                if (currentFrame.X >= image.Width)
                    currentFrame.X = 0;
            }
            sourceRect = new Rectangle((int)currentFrame.X, (int)currentFrame.Y, FrameWidth, FrameHeight);
        }
        
        public void Draw(SpriteBatch spriteBatch, Vector2 _position, bool flip = false)
        {
            SpriteEffects appliedEffect = SpriteEffects.None;
            if (flip)
                appliedEffect = SpriteEffects.FlipHorizontally;

            spriteBatch.Begin();
            spriteBatch.Draw(image, _position, sourceRect, Color.White, 0, Vector2.Zero, 1, appliedEffect, 1);
            spriteBatch.End();
        }
    }
}
