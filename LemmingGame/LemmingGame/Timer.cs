using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemmingGame
{
    class Timer
    {
        public TimeSpan time = new TimeSpan();
        private SpriteFont font;
        public int totalCountDown = 5;
        public int i;
        public static ContentManager Content;
        private string counter;
        private Vector2 position;
        private Vector2 camPos;

        public Timer()
        {
            i = totalCountDown;
            font = Content.Load<SpriteFont>("SpriteFont");
        }

        public void Update(GameTime gameTime, Vector2 _position, Vector2 _camPos)
        {
            Console.WriteLine(time.TotalSeconds);
            time += gameTime.ElapsedGameTime;
            counter = "" + i;
            if (time.TotalSeconds > 1)
            {
                i--;
                Reset();
            }
            camPos = _camPos;
            position = _position + new Vector2(0, -30) - _camPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, counter, position, Color.Red);
            spriteBatch.End();
        }

        private void Reset()
        {
            time = new TimeSpan();
        }
    }
}
