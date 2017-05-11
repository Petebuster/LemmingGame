using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemmingGame
{
    class Camera
    {
        private MouseState mouseState;
        private Vector2 cameraPos;
        public Vector2 CameraPos
        {
            get { return cameraPos; }
        }

        private bool rightMouseBuffer()
        {
            return (mouseState.X > 700);
        }
        private bool bottomMouseBuffer()
        {
            return (mouseState.Y > 380);
        }
        private bool leftMouseBuffer()
        {
            return (mouseState.X < 100);
        }
        private bool topMouseBuffer()
        {
            return (mouseState.Y < 100);
        }

        public void CameraInput()
        {
            mouseState = Mouse.GetState();
          //  Console.WriteLine(mouseState.Y);
            if (mouseState.LeftButton == ButtonState.Pressed && rightMouseBuffer())
                cameraPos.X += 2;
            if (mouseState.LeftButton == ButtonState.Pressed && bottomMouseBuffer())
                cameraPos.Y += 2;
            if (mouseState.LeftButton == ButtonState.Pressed && leftMouseBuffer())
                cameraPos.X -= 2;
            if (mouseState.LeftButton == ButtonState.Pressed && topMouseBuffer())
                cameraPos.Y -= 2;

            //Keyboard Input
            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //    cameraPos.X += 2;
            //if (Keyboard.GetState().IsKeyDown(Keys.Up))
            //    cameraPos.Y -= 2;
            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //    cameraPos.X -= 2;
            //if (Keyboard.GetState().IsKeyDown(Keys.Down))
            //    cameraPos.Y += 2;
        }
    }
}
