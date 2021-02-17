﻿using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public enum BossState
    {
        None,
        Idle,
        AttackGrenade,
        AttackShotgun,
        AttackSpawnEnemies
    }

    public class Enemy_Boss : AnimationSprite
    {
        BossState currentState = BossState.None;

        float stepIdle;
        float animationDrawsBetweenFramesIdle;
        float countFramesIdle;

        float bossX;
        float bossY;

        Hitbox_Boss _hitbox_boss;
        Hitbox_Player _hitbox_player;

        public Enemy_Boss(float bossX, float bossY) : base("enemy_boss_tile.png", 7, 1)
        {
            Spawn();

            x = bossX;
            y = bossY;

            animationDrawsBetweenFramesIdle = 10;

            SetState(BossState.Idle);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        SetState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void SetState(BossState newState)
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
        void HandleStateTransition(BossState oldState, BossState newState)
        {
            if (newState == BossState.Idle)
            {
                SetFrame(0);
            }

            if (newState == BossState.AttackGrenade)
            {
                SetFrame(7);
            }

            if (newState == BossState.AttackShotgun)
            {
                SetFrame(14);
            }

            if (newState == BossState.AttackSpawnEnemies)
            {
                SetFrame(21);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleState()
        {
            if (currentState == BossState.Idle)
            {
                HandleIdleState();
            }
            if (currentState == BossState.AttackGrenade)
            {
                HandleAttackGrenadeState();
            }
            if (currentState == BossState.AttackShotgun)
            {
                HandleAttackShotgunState();
            }
            if (currentState == BossState.AttackSpawnEnemies)
            {
                HandleAttackSpawnEnemieState();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleIdleState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleIdleState()
        {
            stepIdle = stepIdle + 1;

            if (stepIdle > animationDrawsBetweenFramesIdle)
            {
                NextFrame();
                stepIdle = 0;
                countFramesIdle = countFramesIdle + 1;
            }

            if (countFramesIdle >= 7)
            {
                SetFrame(0);
                countFramesIdle = 0;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAttackGrenadeState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleAttackGrenadeState()
        {

        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAttackShotgunState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleAttackShotgunState()
        {

        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAttackSpawnEnemieState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleAttackSpawnEnemieState()
        {

        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Spawn()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Spawn()
        {
            SetFrame(0);

            SetXY(bossX, bossY);
            SetOrigin(width, 0);

            _hitbox_boss = new Hitbox_Boss();
            AddChild(_hitbox_boss);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleDeath()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleDeath()
        {
            if (Globals.bossIsDead == true)
            {
                LateDestroy();
                LateRemove();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {
            HandleState();
            HandleDeath();
        }
    }
}
