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
        AttackSpawnEnemies,
        Vulnerable
    }

    public class Enemy_Boss : AnimationSprite
    {
        BossState currentState = BossState.None;

        Grenade _grenade;
        Bullet _bullet;

        float stepIdle;
        float animationDrawsBetweenFramesIdle;
        float countFramesIdle;

        float stepAG;
        float animationDrawsBetweenFramesAG;
        float countFramesAG;

        float stepAS;
        float animationDrawsBetweenFramesAS;
        float countFramesAS;

        float stepSP;
        float animationDrawsBetweenFramesSP;
        float countFramesSP;

        float stepV;
        float animationDrawsBetweenFramesV;
        float countFramesV;

        float bossX;
        float bossY;

        float AllFrames;

        Hitbox_Boss _hitbox_boss;

        Enemy _enemy1;
        Enemy _enemy2;
        Enemy _enemy3;

        public Enemy_Boss(float bossX, float bossY) : base("enemy_boss_tile.png", 7, 6)
        {
            Spawn();

            x = bossX;
            y = bossY;

            animationDrawsBetweenFramesIdle = 10;
            animationDrawsBetweenFramesAG = 10;
            animationDrawsBetweenFramesAS = 10;
            animationDrawsBetweenFramesSP = 10;
            animationDrawsBetweenFramesV = 8;

            AllFrames = 0;
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
                countFramesIdle = 0;
                SetFrame(0);
            }

            if (newState == BossState.AttackGrenade)
            {
                countFramesAG = 0;
                SetFrame(7);
            }

            if (newState == BossState.AttackShotgun)
            {
                countFramesAS = 0;
                SetFrame(14);
            }

            if (newState == BossState.AttackSpawnEnemies)
            {
                countFramesSP = 0;
                SetFrame(21);
            }

            if (newState == BossState.Vulnerable)
            {
                countFramesV = 0;
                SetFrame(28);
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
            if (currentState == BossState.Vulnerable)
            {
                HandleVulnerableState();
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleIdleState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleIdleState()
        {
            if (currentState == BossState.Idle)
            {
                stepIdle = stepIdle + 1;

                if (stepIdle > animationDrawsBetweenFramesIdle)
                {
                    NextFrame();
                    stepIdle = 0;

                    countFramesIdle = countFramesIdle + 1;

                    AllFrames = AllFrames + 1;
                }

                if (countFramesIdle == 7)
                {
                    countFramesIdle = 0;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAttackGrenadeState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleAttackGrenadeState()
        {
            if (currentState == BossState.AttackGrenade)
            {
                stepAG = stepAG + 1;

                if (stepAG > animationDrawsBetweenFramesAG)
                {
                    NextFrame();
                    stepAG = 0;

                    HandleGrenades();
                    countFramesAG = countFramesAG + 1;

                    AllFrames = AllFrames + 1;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAttackShotgunState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleAttackShotgunState()
        {
            if (currentState == BossState.AttackShotgun)
            {
                stepAS = stepAS + 1;

                if (stepAS > animationDrawsBetweenFramesAS)
                {
                    NextFrame();
                    stepAS = 0;

                    HandleBullets();
                    countFramesAS = countFramesAS + 1;

                    AllFrames = AllFrames + 1;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAttackSpawnEnemieState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleAttackSpawnEnemieState()
        {
            if (currentState == BossState.AttackSpawnEnemies)
            {
                stepSP = stepSP + 1;

                if (stepSP > animationDrawsBetweenFramesSP)
                {
                    NextFrame();
                    stepSP = 0;

                    HandleSpawnEnemies();
                    countFramesSP = countFramesSP + 1;

                    AllFrames = AllFrames + 1;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleVulnerableState()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleVulnerableState()
        {
            if (currentState == BossState.Vulnerable)
            {
                _enemy1.LateDestroy();
                _enemy1.LateRemove();

                _enemy2.LateDestroy();
                _enemy2.LateRemove();

                _enemy3.LateDestroy();
                _enemy3.LateRemove();

                stepV = stepV + 1;

                if (stepV > animationDrawsBetweenFramesV)
                {
                    NextFrame();
                    stepV = 0;

                    countFramesV = countFramesV + 1;

                    AllFrames = AllFrames + 1;
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleGrenades()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleGrenades()
        {
            if (countFramesAG == 6 && currentState == BossState.AttackGrenade)
            {
                _grenade = new Grenade(-1075, -100, 490);
                AddChild(_grenade);

                _grenade = new Grenade(-800, -100, 790);
                AddChild(_grenade);

                _grenade = new Grenade(-1300, -100, 790);
                AddChild(_grenade);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleBullets()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleBullets()
        {
            if (countFramesAS == 4 && currentState == BossState.AttackShotgun)
            {
                _bullet = new Bullet(-450, 650, -1460, 70);
                AddChild(_bullet);

                _bullet = new Bullet(-450, 650, -1460, 390);
                AddChild(_bullet);

                _bullet = new Bullet(-450, 650, -1460, 610);
                AddChild(_bullet);

                _bullet = new Bullet(-450, 650, -1460, 900);
                AddChild(_bullet);

                _bullet = new Bullet(-450, 650, -1460, 1150);
                AddChild(_bullet);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleSpawnEnemies()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleSpawnEnemies()
        {
            if (countFramesSP == 6 && currentState == BossState.AttackSpawnEnemies)
            {
                _enemy1 = new Enemy(-1075, 530, -1140, -1000);
                AddChild(_enemy1);

                _enemy2 = new Enemy(-1375, 830, -1440, -900);
                AddChild(_enemy2);

                _enemy3 = new Enemy(-200, 830, -900, -200);
                AddChild(_enemy3);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        ChechIfBossIsVulnerable()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void ChechIfBossIsVulnerable()
        {
            if (currentState == BossState.Vulnerable && countFramesV >= 7)
            {
                Globals.bossIsAttacking = false;
            }
            else
            {
                Globals.bossIsAttacking = true;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        HandleAttacks()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void HandleAttacks()
        {
            if (Globals.bossIsDead == false)
            {
                if (AllFrames == 0)
                {
                    SetState(BossState.Idle);
                    AllFrames = AllFrames + 1;
                }

                if (AllFrames == 7)
                {
                    SetState(BossState.AttackGrenade);
                }

                if (AllFrames == 14)
                {
                    SetState(BossState.Idle);
                }

                if (AllFrames == 21)
                {
                    SetState(BossState.AttackShotgun);
                }

                if (AllFrames == 28)
                {
                    SetState(BossState.Idle);
                }

                if (AllFrames == 35)
                {
                    SetState(BossState.AttackSpawnEnemies);
                }

                if (AllFrames == 42)
                {
                    SetState(BossState.Idle);
                }

                if (AllFrames == 49)
                {
                    SetState(BossState.Vulnerable);
                }

                if (AllFrames == 63)
                {
                    AllFrames = 0;
                }
            }
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

            ChechIfBossIsVulnerable();

            HandleAttacks();
        }
    }
}
