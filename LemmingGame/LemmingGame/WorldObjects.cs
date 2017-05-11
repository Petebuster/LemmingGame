using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemmingGame
{
    class WorldObjects
    {//Donkey
        Texture2D donkeyTexture;
        Vector2 position = new Vector2(260,65);
        public Rectangle donkeyRec;
        public Color[] textureData;
        //movablePlattform
        Texture2D platformTexture;
        Vector2 plattformPosition;
        Vector2 plattformOrigin = new Vector2(50, 30);
        float travelDistance = 30;
        Rectangle plattformRec;
        int speed = 1;

        public WorldObjects(Texture2D _donkeyTexture, Texture2D _moveablePlattform)
        {
            donkeyTexture = _donkeyTexture;
            donkeyRec = new Rectangle((int)position.X,(int) position.Y, donkeyTexture.Width, donkeyTexture.Height);
            textureData = new Color[donkeyTexture.Width * donkeyTexture.Height];
            //donkeyTexture.GetData(0,new Rectangle((int)position.X, (int)position.Y, donkeyTexture.Width, donkeyTexture.Height), textureData, 0, textureData.Length);
            donkeyTexture.GetData(textureData);

            platformTexture = _moveablePlattform;
            plattformPosition = plattformOrigin;

        }

        public void Update(GameTime gameTime)
        {
            plattformRec = new Rectangle((int)plattformPosition.X, (int)plattformPosition.Y, platformTexture.Width, platformTexture.Height);
            plattformPosition.X += 1 * speed;

            if (plattformPosition.X >= plattformOrigin.X + travelDistance)
                speed = -1;
            if (plattformPosition.X <= plattformOrigin.X)
                speed = 1;

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 _camPos)
        {
            Matrix m = Matrix.CreateScale(0.25f);
            //m = Matrix.CreateTranslation(new Vector3())
            spriteBatch.Begin();
            spriteBatch.Draw(donkeyTexture, position - _camPos, Color.White);
            spriteBatch.Draw(platformTexture, plattformPosition - _camPos, Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateScale(Game1.scale));
            spriteBatch.Draw(donkeyTexture, position + new Vector2(2400, 1460), Color.White);
            spriteBatch.Draw(platformTexture, plattformPosition + new Vector2(2400, 1460), Color.White);
            spriteBatch.End();
        }
    }
}
