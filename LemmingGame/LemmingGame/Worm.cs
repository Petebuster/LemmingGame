using Microsoft.Xna.Framework;
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
        public enum wormState { Falling, Walking, Blocking, Digging, Breaking, Welding, Bouncing, Jumping, Dying}
        public wormState state = new wormState();
        MouseState mouseState;

        private Texture2D wormFalling;
        private Texture2D wormWalking;
        private Texture2D wormStopping;
        private Texture2D wormDying;
        private Texture2D wormDigging;
        private Texture2D breakingTile;
        private Texture2D wormWelding;
        private Texture2D wormBouncing;
        private Texture2D wormJumping;
        private Texture2D selectorTexture;
        private Vector2 position = new Vector2(20,0);
        private Rectangle breakingRect = new Rectangle();
        private bool breakingActive;
        private float Velocity = 1;
        private int speed = 1;
        private Rectangle wormRectangle;
        private bool OnGround;
        public bool DirectionRight = true;
        public bool Selected;
        private bool Working;
        private KeyboardState key;

        public Rectangle WormRectangle
        {
            get { return wormRectangle; }
        }



        Animation FallingAnimation = new Animation();
        Animation WalkingAnimation = new Animation();
        Animation StopperAnimation = new Animation();
        Animation DyingAnimation = new Animation();
        Animation WeldingAnimation = new Animation();
        public Animation BreakingAnimation = new Animation();
        Animation DiggingAnimation = new Animation();
        Animation BouncingAnimation = new Animation();
        Animation JumpingAnimation = new Animation();

        Selector Selector = new Selector();

        Gameplay Gameplay;

        //Initialize

        public Worm(Texture2D _wormFalling, Texture2D _wormWalking, Texture2D _wormStopping, Texture2D _wormDying, Texture2D _tileBreaking, Texture2D _wormDigging, Texture2D _wormWelding, Texture2D _wormBouncing, Texture2D _wormJumping, Texture2D _selectorTexture, Gameplay _gameplay)
        {
            Gameplay = _gameplay;
            wormFalling = _wormFalling;
            wormWalking = _wormWalking;
            wormStopping = _wormStopping;
            wormDying = _wormDying;
            wormDigging = _wormDigging;
            wormWelding = _wormWelding;
            wormBouncing = _wormBouncing;
            wormJumping = _wormJumping;
            breakingTile = _tileBreaking;
            selectorTexture = _selectorTexture;
            FallingAnimation.Initialize(position, new Vector2(10,1), 100, wormFalling);
            WalkingAnimation.Initialize(position, new Vector2(13, 1), 60, wormWalking);
            StopperAnimation.Initialize(position, new Vector2(4, 1), 200, wormStopping);
            DyingAnimation.Initialize(position, new Vector2(14, 1), 100, wormDying);
            DiggingAnimation.Initialize(position, new Vector2(4, 1), 100, wormDigging);
            BreakingAnimation.Initialize(position, new Vector2(10, 1), 700, breakingTile);
            WeldingAnimation.Initialize(position, new Vector2(4,1), 100, wormWelding);
            BouncingAnimation.Initialize(position, new Vector2(6,1), 100, wormBouncing);
            JumpingAnimation.Initialize(position, new Vector2(7,1), 100, wormJumping);
        }

        public void Update(GameTime gameTime, Vector2 _cameraPos)
        {

            if (!DyingAnimation.IsDead)
            {
                int camPointerX = mouseState.X + (int)_cameraPos.X;
                int camPointerY = mouseState.Y + (int)_cameraPos.Y;
                key = Keyboard.GetState();
                mouseState = Mouse.GetState();

                position.Y += Velocity;
                CheckState();
                CheckDirection();
                OnGround = false;
                wormRectangle = new Rectangle((int)position.X, (int)position.Y, FallingAnimation.FrameWidth, FallingAnimation.FrameHeight);
                if (wormRectangle.Contains(camPointerX, camPointerY) && mouseState.LeftButton == ButtonState.Pressed)
                {
                    Gameplay.Deselect();
                    Selected = true;
                }

                //ToDo: kill AnimationUpdate when animation.IsDead
                switch (state)
                {
                    case wormState.Falling:
                        FallingAnimation.Active = true;
                        FallingAnimation.Update(gameTime);
                        Velocity = 1; break;
                    case wormState.Walking:
                        WalkingAnimation.Active = true;
                        WalkingAnimation.Update(gameTime);
                        position.X += 1 * speed; break;
                    case wormState.Blocking:
                        StopperAnimation.Active = true;
                        StopperAnimation.Update(gameTime);
                        speed = 0; break;
                    case wormState.Digging:
                        DiggingAnimation.Active = true;
                        DiggingAnimation.Update(gameTime);
                        speed = 0;
                        Velocity = 1;
                        break;
                    case wormState.Welding:
                        WeldingAnimation.Active = true;
                        WeldingAnimation.Update(gameTime);
                        position.X += 0.1f;
                        if(!Gameplay.WeldingActive)
                            Velocity = 1;
                        break;
                    case wormState.Bouncing:
                        BouncingAnimation.Active = true;
                        BouncingAnimation.Update(gameTime);
                        break;
                    case wormState.Jumping:
                        JumpingAnimation.Active = true;
                        JumpingAnimation.IsLoop = false;
                        JumpingAnimation.Update(gameTime);
                        if (!JumpingAnimation.IsDead)
                        {
                            if (DirectionRight)
                            {
                                position.X += 1;
                                position.Y -= 2 * speed;
                            }
                            else
                            {
                                position.X -= 1;
                                position.Y += 2 * speed;
                            }
                        }
                        else { state = wormState.Falling;
                            JumpingAnimation.Reset();
                        }
                        break;
                    case wormState.Dying:
                        DyingAnimation.Active = true;
                        DyingAnimation.IsLoop = false;
                        DyingAnimation.Update(gameTime);
                        if (DyingAnimation.IsDead)
                            Selected = false;
                         break;
                }
            }
        }

        private void CheckState()
        {//This should be changed // remove Jumping if schleife
            if (state != wormState.Jumping)
            {
                if (Selected && key.IsKeyDown(Keys.D))
                    state = wormState.Dying;
                else if (state != wormState.Dying)
                {
                    if (Selected && key.IsKeyDown(Keys.B))
                    {
                        state = wormState.Blocking;
                        Working = true;
                        Selected = false;
                    }

                    if (Selected && key.IsKeyDown(Keys.W))
                    {
                        state = wormState.Welding;
                        Working = true;
                        Selected = false;
                    }

                    if (Selected && key.IsKeyDown(Keys.J))
                    {
                        state = wormState.Bouncing;
                        Working = true;
                        Selected = false;
                    }

                    if (Selected && key.IsKeyDown(Keys.R))
                    {
                        state = wormState.Digging;
                        Working = true;
                        Selected = false;
                    }

                    if (!Working)
                    {
                        if (OnGround)
                            state = wormState.Walking;
                        else state = wormState.Falling;
                    }

                    // else if (!OnGround) state = wormState.Falling;

                }

            }
        }

        public void CheckDirection()
        {
            if (speed > 0)
                DirectionRight = true;
            else if(speed < 0)
                DirectionRight = false;
        }

        public void Collision(SpriteBatch spriteBatch, GameTime gameTime, Rectangle _collisionRect, Collision tile)
        {

            if (wormRectangle.TouchTop(/*_collisionRect*/tile.TileRectangle))
            {
                Velocity = 0;
                OnGround = true;
                if (state == wormState.Digging)
                {                   
                    BreakingAnimation.Active = true;
                    BreakingAnimation.IsLoop = false;
                    BreakingAnimation.Update(gameTime);
                    breakingRect = /*_collisionRect*/tile.TileRectangle;
                    
                    if (BreakingAnimation.IsDead)
                    {
                        Gameplay.BreakTile(tile);
                        BreakingAnimation.Reset();
                    }
                    breakingActive = tile.tileActive;
                }
               
            }
            if (wormRectangle.TouchRight(/*_collisionRect*/tile.TileRectangle) && !DirectionRight)
            {
                //if (state == wormState.Digging)
                    //tileActive = false; 
                
               Velocity = 0;
               OnGround = true;
               speed = 1;

                if (state == wormState.Welding /*&& Gameplay.WeldingActive*/)
                {
                    Gameplay.BreakTile(tile);
                    Gameplay.WeldingActive = false;
                   // Velocity = 1;
                }
                //DirectionRight = true;
            }

              if (wormRectangle.TouchLeft(/*_collisionRect*/tile.TileRectangle) && DirectionRight)
            {
                Velocity = 0;
                OnGround = true;
                speed = -1;

                if (state == wormState.Welding)
                {
                    Gameplay.WeldingActive = true;
                    Velocity = 0;
                }

                //Gameplay.BreakTile(tile);
                //DirectionRight = false;
            }

            if (wormRectangle.TouchBottom(tile.TileRectangle))
                state = wormState.Falling;
        }

        public void BlockerCollision(Rectangle _blockerRec, bool _blockToRight)
        {

            if (wormRectangle.Intersects(_blockerRec))
                if (_blockToRight)
                    speed = -1;
                else speed = 1;
        }

        public void BouncerCollision(Rectangle _bouncerRec, bool _bounceToRight)
        {
            if (wormRectangle.Intersects(_bouncerRec))
                state = wormState.Jumping;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 _cameraPos)
        {
            //Console.WriteLine(mouseState.X);
            switch (state)
            {
                case wormState.Falling:
                    FallingAnimation.Draw(spriteBatch, position - _cameraPos);
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
                case wormState.Digging:
                    DiggingAnimation.Draw(spriteBatch, position - _cameraPos);
                    if(/*!BreakingAnimation.IsDead*/breakingActive)
                    BreakingAnimation.Draw(spriteBatch, new Vector2(breakingRect.X - _cameraPos.X, breakingRect.Y - _cameraPos.Y));
                    break;
                case wormState.Welding:
                    WeldingAnimation.Draw(spriteBatch, position - _cameraPos);
                    break;
                case wormState.Bouncing:
                    BouncingAnimation.Draw(spriteBatch, position - _cameraPos);
                    break;
                case wormState.Jumping:
                    JumpingAnimation.Draw(spriteBatch, position - _cameraPos);
                    break;
                case wormState.Dying:
                    if (DirectionRight)
                        DyingAnimation.Draw(spriteBatch, position - _cameraPos);
                    else
                        DyingAnimation.Draw(spriteBatch, position - _cameraPos, true);
                    break;
            }
            if (Selected && state != wormState.Dying)
                Selector.Draw(spriteBatch, wormRectangle, selectorTexture, _cameraPos);
        }
        public void DrawBreakable(SpriteBatch spriteBatch, Vector2 _cameraPos) { }
         
        public bool isBroken()
        {
            return BreakingAnimation.IsDead;
        }
                    
                    


         
    }
}
