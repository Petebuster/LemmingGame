﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LemmingGame
{
    class Worm
    {
        public enum wormState { Falling, Walking, Blocking, Dying}
        public wormState state = new wormState();
        MouseState mouseState;

        private Texture2D wormWalking;
        private Texture2D wormStopping;
        private Texture2D selectorTexture;
        private Vector2 position = new Vector2();
        private float Velocity = 1;
        private int speed = 1;
        private Rectangle wormRectangle;
        private bool OnGround;
        public bool DirectionRight = true;
        private bool Selected;
        private bool Working;
        private KeyboardState key;

        public Rectangle WormRectangle
        {
            get { return wormRectangle; }
        }

        

        Animation WalkingAnimation = new Animation();
        Animation StopperAnimation = new Animation();
        Selector Selector = new Selector();


        //Initialize

        public Worm(Texture2D _wormWalking, Texture2D _wormStopping, Texture2D _selectorTexture)
        {
            wormWalking = _wormWalking;
            wormStopping = _wormStopping;
            selectorTexture = _selectorTexture;
            WalkingAnimation.Initialize(position, new Vector2(13, 1), 60, wormWalking);
            StopperAnimation.Initialize(position, new Vector2(4, 1), 200, wormStopping);
        }

        public void Update(GameTime gameTime, Vector2 _cameraPos)
        {
            // Console.WriteLine("cam" + _cameraPos);
            int camPointerX = mouseState.X + (int)_cameraPos.X;
            int camPointerY = mouseState.Y + (int)_cameraPos.Y;
            key = Keyboard.GetState();
            mouseState = Mouse.GetState();
           // Console.WriteLine(mouseState);

            position.Y += Velocity;
            CheckState();
            CheckDirection();
            OnGround = false;
            wormRectangle = new Rectangle((int)position.X, (int)position.Y, WalkingAnimation.FrameWidth, WalkingAnimation.FrameHeight);
            if (wormRectangle.Contains(camPointerX, camPointerY) && mouseState.LeftButton == ButtonState.Pressed)
                Selected = true;

                switch (state)
            {
               case wormState.Falling:
                    WalkingAnimation.Active = false;
                    WalkingAnimation.Update(gameTime);
                    Velocity = 1; break;
                case wormState.Walking:
                    WalkingAnimation.Active = true;
                    WalkingAnimation.Update(gameTime);
                    position.X += 1 * speed; break;
                case wormState.Blocking:
                    StopperAnimation.Active = true;
                    StopperAnimation.Update(gameTime);
                    speed = 0; break;
                case wormState.Dying: break;
            }
            
        }

        private void CheckState()
        {          
            if (Selected && key.IsKeyDown(Keys.B))
                {
                    state = wormState.Blocking;
                    Working = true;
                    Selected = false;
                }

            if(!Working)
            { 
            if (OnGround)
                    state = wormState.Walking;
                else state = wormState.Falling;
            }    
        }

        public void CheckDirection()
        {
            if (speed > 0)
                DirectionRight = true;
            else if(speed < 0)
                DirectionRight = false;
        }

        public void Collision(Rectangle _collisionRect)
        {
            if (wormRectangle.TouchTop(_collisionRect))
            {
                Velocity = 0;
                OnGround = true;

            }
            if (wormRectangle.TouchRight(_collisionRect) && !DirectionRight)
            {
                
               Velocity = 0;
               OnGround = true;
               speed = 1;
               //DirectionRight = true;
            }

              if (wormRectangle.TouchLeft(_collisionRect) && DirectionRight)
            {
                Velocity = 0;
                OnGround = true;
                speed = -1;
                //DirectionRight = false;
            }       
        }

        public void BlockerCollision(Rectangle _blockerRec, bool _blockToRight)
        {

            if (wormRectangle.Intersects(_blockerRec))
                if (_blockToRight)
                    speed = -1;
                else speed = 1;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 _cameraPos)
        {
            //Console.WriteLine(mouseState.X);
            switch (state)
            {
                case wormState.Falling:
                    WalkingAnimation.Draw(spriteBatch, position - _cameraPos);
                    break;
                case wormState.Walking:
                    if (DirectionRight)
                        WalkingAnimation.Draw(spriteBatch, position - _cameraPos);
                    else
                        WalkingAnimation.Draw(spriteBatch, position - _cameraPos, true);
                    break;
                case wormState.Blocking:
                    if (DirectionRight)
                        StopperAnimation.Draw(spriteBatch, position - _cameraPos, true);
                    else
                        StopperAnimation.Draw(spriteBatch, position - _cameraPos);
                    break;
                case wormState.Dying:
                    break;
            }


            //        if (DirectionRight)
            //    WalkingAnimation.Draw(spriteBatch, position - _cameraPos);
            //else
            //    WalkingAnimation.Draw(spriteBatch, position - _cameraPos, true);
            if (Selected)
                Selector.Draw(spriteBatch, wormRectangle, selectorTexture, _cameraPos);
        }


         
    }
}