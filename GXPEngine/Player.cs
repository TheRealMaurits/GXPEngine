﻿using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public enum PlayerState
    {
        None,
        Walk,
        Jump,
        Attack
    }

    public class Player : AnimationSprite
    {
        PlayerState currentState = PlayerState.None;

        int animationDrawsBetweenFramesWalk;
        int stepWalk;

        int animationDrawsBetweenFramesAttack;
        int stepAttack;

        float speedX;
        float speedY;

        bool isLanded;

        int countFramesWalk;

        Hitbox_Player _hitbox_player;
        Hitbox_Fist _hitbox_fist;

        Sound _attackSound;       

        public Player() : base("player_tile.png", 8, 1)
        {
            _attackSound = new Sound("Attack_sound_player.wav", false, false);            

            Globals.MCfacingRight = true;
            Globals.MCfacingLeft = false;

            Spawn();

            animationDrawsBetweenFramesWalk = 5;
            animationDrawsBetweenFramesAttack = 5;

            speedX = 5;
            speedY = 0;

            SetState(PlayerState.Walk);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        SetState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void SetState(PlayerState newState)
        {
            if (currentState != newState)
            {
                HandleStateTransition(currentState, newState);
                currentState = newState;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleStateTransition()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleStateTransition(PlayerState oldState, PlayerState newState)
        {
            if (newState == PlayerState.Jump)
            {
                speedY = -32;
                isLanded = false;
                SetFrame(7);
            }

            if (newState == PlayerState.Walk)
            {
                SetFrame(0);
            }

            if (newState == PlayerState.Attack)
            {
                SetFrame(4);
                Globals.countFramesAttackPlayer = 4;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleState()
        {
            if (currentState == PlayerState.Walk)
            {
                HandleWalkState();
            }
            if (currentState == PlayerState.Jump)
            {
                HandleJumpState();
            }
            if (currentState == PlayerState.Attack)
            {
                HandleAttackState();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleWalkState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void HandleWalkState()
        {
            HandleHorizontalControls();

            if (Input.GetKey(Key.D) || Input.GetKey(Key.A))
            {
                stepWalk = stepWalk + 1;

                if (stepWalk > animationDrawsBetweenFramesWalk)
                {
                    NextFrame();
                    stepWalk = 0;
                    countFramesWalk = countFramesWalk + 1;
                }

                if (countFramesWalk >= 4)
                {
                    SetFrame(0);
                    countFramesWalk = 0;
                }
            }

            if (Input.GetKey(Key.W))
            {
                SetState(PlayerState.Jump);
            }

            if (Input.GetKey(Key.SPACE))
            {
                _attackSound.Play();                
                SetState(PlayerState.Attack);
                Globals.aIsPressed = false;
                Globals.dIsPressed = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleJumpState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void HandleJumpState()
        {
            HandleHorizontalControls();

            if (isLanded)
            {
                SetState(PlayerState.Walk);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAttackState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void HandleAttackState()
        {
            stepAttack = stepAttack + 1;

            if (stepAttack > animationDrawsBetweenFramesAttack)
            {
                NextFrame();
                stepAttack = 0;
                Globals.countFramesAttackPlayer = Globals.countFramesAttackPlayer + 1;
                Globals.playerIsAttacking = true;
            }

            if (Globals.countFramesAttackPlayer >= 7)
            {
                Globals.playerIsAttacking = false;
                SetState(PlayerState.Walk);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Spawn()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Spawn()
        {
            SetFrame(0);

            SetXY(game.width / 2 + 50, game.height / 2);
            SetOrigin(width / 2, height);

            _hitbox_player = new Hitbox_Player();
            AddChild(_hitbox_player);

            _hitbox_fist = new Hitbox_Fist();
            AddChild(_hitbox_fist);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleHorizontalControls()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleHorizontalControls()
        {
            if (Input.GetKey(Key.A))
            {
                speedX -= 5f;
                Globals.MCfacingRight = false;
                Globals.MCfacingLeft = true;
            }

            if (Input.GetKey(Key.D))
            {
                speedX += 5f;
                Globals.MCfacingRight = true;
                Globals.MCfacingLeft = false;
            }

            if (Input.GetKeyDown(Key.D))
            {
                Mirror(false, false);
                Globals.dIsPressed = true;
            }

            if (Input.GetKeyDown(Key.A))
            {
                Mirror(true, false);
                Globals.aIsPressed = true;
            }

            if (Input.GetKeyUp(Key.D))
            {
                Globals.dIsPressed = false;
            }

            if (Input.GetKeyUp(Key.A))
            {
                Globals.aIsPressed = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleHorizontalMovement()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleHorizontalMovement()
        {
            MoveWithCollision(speedX, 0f);
            speedX *= 0.6f;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleVerticalMovement()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleVerticalMovement()
        {
            speedY = speedY + 1.5f;
            if (MoveWithCollision(0f, speedY) == false)
            {
                speedY = 0f;
                isLanded = true;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleBorders()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleBorders()
        {
            MyGame mygame = game as MyGame;

            if (mygame.level1IsActive == true || mygame.level2IsActive == true)
            {
                x = Mathf.Clamp(x, 700, 13700);
            }

            if (Globals.levelBossIsActive == true)
            {
                x = Mathf.Clamp(x, (0 + width / 2), (1440 - width / 2));
            }

            y = Mathf.Clamp(y, (0), (830));

            if (y >= (830))
            {
                isLanded = true;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        MoveWithCollision()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        bool MoveWithCollision(float moveX, float moveY)
        {
            float previousX = x;
            float previousY = y;

            x += moveX;
            y += moveY;

            bool hasCollided = false;

            foreach (GameObject other in GetCollisions())
            {
                if (other is Block_Jump)
                {
                    hasCollided = true;
                }
                if (other is Block_Jump2)
                {
                    hasCollided = true;
                }
            }

            if (hasCollided == true)
            {
                x = previousX;
                y = previousY;
            }

            return (hasCollided == false);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {
            HandleState();

            HandleHorizontalMovement();
            HandleVerticalMovement();

            HandleBorders();

            Globals.playerX = x;
        }
    }
}
