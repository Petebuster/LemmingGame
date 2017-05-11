using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemmingGame
{
    class MiniMap
    {
        Texture2D wormTexture;
        Texture2D worldObjectTexture;
        Texture2D frameTexture;
        List<Collision> tileList;
        public MiniMap(Texture2D _frameTexture)
        {
            frameTexture = _frameTexture;

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(frameTexture, new Rectangle(600, 360, 200, 120), Color.White);
            spriteBatch.End();

            
            
        }
        public void DrawTile(SpriteBatch spriteBatch, Texture2D tileTexture, Rectangle tileRectangle)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateScale(0.25f));
            spriteBatch.Draw(tileTexture, new Rectangle((int)tileRectangle.X + 2400, (int)tileRectangle.Y + 1460, tileRectangle.Width, tileRectangle.Height), Color.White);                        
            spriteBatch.End();            
        }
    }
}
