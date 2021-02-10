﻿using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class Player : AnimationSprite
    {
        int animationDrawsBetweenFrames;
        int step;

        int speed;

        bool wIsPressed;
        bool sIsPressed;
        bool dIsPressed;
        bool aIsPressed;

        Healthbar _healthbar;

        public Player() : base("player_tile.png", 6, 1)
        {
            Spawn();

            animationDrawsBetweenFrames = 5;
            speed = 5;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Spawn()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Spawn()
        {
            SetFrame(0);

            SetXY(game.width / 2, game.height / 2);
            SetOrigin(width / 2, height / 2);

            _healthbar = new Healthbar();
            AddChild(_healthbar);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAnimation()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleAnimation()
        {
            if (Input.GetKeyDown(Key.W))
            {
                wIsPressed = true;
            }
            if (Input.GetKeyUp(Key.W))
            {
                wIsPressed = false;
                SetFrame(0);
            }

            if (Input.GetKeyDown(Key.S))
            {
                sIsPressed = true;
            }
            if (Input.GetKeyUp(Key.S))
            {
                sIsPressed = false;
                SetFrame(0);
            }

            if (Input.GetKeyDown(Key.D))
            {
                dIsPressed = true;
            }
            if (Input.GetKeyUp(Key.D))
            {
                dIsPressed = false;
                SetFrame(0);
            }

            if (Input.GetKeyDown(Key.A))
            {
                aIsPressed = true;
            }
            if (Input.GetKeyUp(Key.A))
            {
                aIsPressed = false;
                SetFrame(0);
            }

            if (wIsPressed == true || sIsPressed == true || dIsPressed == true || aIsPressed == true)
            {
                step = step + 1;

                if (step > animationDrawsBetweenFrames)
                {
                    NextFrame();
                    step = 0;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleMovement()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleMovement()
        {
            if (Input.GetKey(Key.W))
            {
                Move(0, -speed);
            }

            if (Input.GetKey(Key.S))
            {
                Move(0, speed);
            }

            if (Input.GetKey(Key.D))
            {
                Move(speed, 0);
            }

            if (Input.GetKey(Key.A))
            {
                Move(-speed, 0);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleBorders()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleBorders()
        {
            x = Mathf.Clamp(x, (0 + (width / 6) + 12), (1440 - (width / 6)));
            y = Mathf.Clamp(y, (0 + (height / 2) - 18), (1080 - (height / 2)));
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        OnCollision()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void OnCollision(GameObject other)
        {
            if (other is Enemy)
            {
                Globals.health = Globals.health - 1;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {
            HandleAnimation();
            HandleMovement();
            HandleBorders();
        }
    }
}
