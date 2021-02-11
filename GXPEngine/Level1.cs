﻿using System;									// System contains a lot of default C# libraries 
using GXPEngine;								// GXPEngine contains the engine

namespace GXPEngine
{
    public class Level1 : GameObject
    {
        Sprite background_level1;

        Level1_Background background;
        Block_Jump block_jump;
        Player player;
        Enemy enemy1;

        public Level1() : base()
        {
            StartLevel();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        StartLevel()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void StartLevel()
        {
            background_level1 = new Sprite("background_level1.png");
            AddChild(background_level1);

            background = new Level1_Background();
            AddChild(background);

            block_jump = new Block_Jump();
            //AddChild(block_jump);

            enemy1 = new Enemy();
            //AddChild(enemy1);

            player = new Player();
            AddChild(player);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {

        }
    }
}
