using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemmingGame
{
    class Selector
    {
        public void Draw(SpriteBatch spriteBatch, Rectangle _wormRect, Texture2D _selectorTexture, Vector2 _cameraPos)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_selectorTexture, new Rectangle(_wormRect.X - (int)_cameraPos.X, _wormRect.Y - (int)_cameraPos.Y, _wormRect.Width, _wormRect.Height), Color.White);
            spriteBatch.End();
        }
    }
}
