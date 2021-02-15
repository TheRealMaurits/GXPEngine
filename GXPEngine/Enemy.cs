﻿using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class Enemy : AnimationSprite
    {
        int animationDrawsBetweenFrames;
        int step;

        int speed;

        bool goToLeft;
        bool goToRight;

        float countFrames;

        Hitbox_Enemy _hitbox_enemy;

        Sound _deadSound;

        public Enemy() : base("enemy_tile.png", 4, 2)
        {
            _deadSound = new Sound("Dead_sound_enemy.wav", false, false);

            Spawn();

            animationDrawsBetweenFrames = 10;
            speed = 5;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Spawn()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Spawn()
        {
            SetFrame(1);

            SetXY(1440 / 2 + 500, 700);
            SetOrigin(width / 2, height / 2);

            goToRight = true;

            _hitbox_enemy = new Hitbox_Enemy();
            AddChild(_hitbox_enemy);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAnimation()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleAnimation()
        {
            step = step + 1;

            if (step > animationDrawsBetweenFrames)
            {
                NextFrame();
                step = 0;

                countFrames = countFrames + 1;
            }

            if (countFrames >= 3)
            {
                SetFrame(0);
                countFrames = 0;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleMovement()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleMovement()
        {
            if (x >= 2880)
            {
                goToRight = false;
                goToLeft = true;
            }

            if (x <= 1440)
            {
                goToLeft = false;
                goToRight = true;
            }

            if (goToLeft == true)
            {
                x = x - speed;
                Mirror(false, false);
            }

            if (goToRight == true)
            {
                x = x + speed;
                Mirror(true, false);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Dead()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Dead()
        {
            if (Globals.EnemyIsDead == true)
            {
                _deadSound.Play();

                LateDestroy();
                LateRemove();

                Globals.EnemyIsDead = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {
            Dead();
            HandleAnimation();
            HandleMovement();
        }
    }
}
