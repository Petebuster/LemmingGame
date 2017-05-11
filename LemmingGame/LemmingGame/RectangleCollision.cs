using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemmingGame
{
    static class RectangleCollision
    {
        public static bool PixelIntersect(Rectangle r1, Color[] data1, Rectangle r2, Color[] data2)
        {
            int top = Math.Max(r1.Top, r2.Top);
            int bottom = Math.Min(r1.Bottom, r2.Bottom);
            int left = Math.Max(r1.Left, r2.Right);
            int right = Math.Min(r1.Right, r2.Left);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color color1 = data1[(x - r1.Left) + (y - r1.Top) * r1.Width];

                    Color color2 = data2[(x - r2.Left) + (y - r2.Top) * r2.Width];

                    if (color1.A != 0 && color2.A != 0)
                        return true;
                }
            }

            return false;
        }

        public static bool TouchTop(this Rectangle r1, Rectangle r2)
        {
            return (r1.Bottom >= r2.Top + 1 &&
                r1.Bottom <= r2.Top + (r2.Height / 2) &&
                r1.Right >= r2.Left + (r2.Width / 5) &&
                r1.Left <= r2.Right - (r2.Width / 5));
        }

        public static bool TouchBottom(this Rectangle r1, Rectangle r2)
        {
            return (r1.Top <= r2.Bottom + (r2.Height / 5) &&
                r1.Top >= r2.Bottom - 1 &&
                r1.Right >= r2.Left + (r2.Width / 5) &&
                r1.Left <= r2.Right - (r2.Width / 5));
        }

        public static bool TouchLeft(this Rectangle r1, Rectangle r2)
        {
            return (r1.Right <= r2.Right &&
                r1.Right >= r2.Left - 5 &&
                r1.Top <= r2.Bottom - (r2.Width / 4) &&
                r1.Bottom >= r2.Top + (r2.Width / 4));
        }

        public static bool TouchRight(this Rectangle r1, Rectangle r2)
        {
            return (r1.Left >= r2.Left &&
                r1.Left <= r2.Right + 5 &&
                r1.Top <= r2.Bottom - (r2.Width / 4) &&
                r1.Bottom >= r2.Top + (r2.Width / 4));
        }
    }
}
