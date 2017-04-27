using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemmingGame
{
    class Map
    {
        private List<Collision> collisionList;
        public List<Collision> CollisionList
        {
            get { return collisionList; }
        }

        public int MapWidth;
        public int MapHeight;
        public void Initialize()
        {
            collisionList = new List<Collision>();
        }
        public void GenerateMap(int [,] _map, int _tileSize)
        {
            for (int x=0; x<_map.GetLength(1); x++)
            {
                for (int y=0; y<_map.GetLength(0); y++)
                {
                    int number = _map[y, x];

                    if(number > 0)
                    collisionList.Add(new Collision(number, new Rectangle(x * _tileSize, y * _tileSize, _tileSize, _tileSize)));

                    MapWidth = (x + 1) * _tileSize;
                    MapHeight = (y + 1) * _tileSize;
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 _cameraPos)
        {
            foreach (var tile in collisionList)
                tile.Draw(spriteBatch, _cameraPos);
        }

    }
}
