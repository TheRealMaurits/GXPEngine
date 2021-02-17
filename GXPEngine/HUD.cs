﻿using System;                                    // System contains a lot of default C# libraries 
using GXPEngine;                                // GXPEngine contains the engine
using System.Drawing;
using System.Drawing.Text;

public class HUD : Canvas
{
    Healthbar_Player _healthbar_player;
    Healthbar_Boss _healthbar_boss;

    public HUD() : base(1440, 1080)
    {
        Spawn();
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        Spawn()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void Spawn()
    {
        _healthbar_player = new Healthbar_Player();
        AddChild(_healthbar_player);

        _healthbar_player.x = 10;
        _healthbar_player.y = 10;

        _healthbar_boss = new Healthbar_Boss();
        AddChild(_healthbar_boss);

        _healthbar_boss.x = 1020;
        _healthbar_boss.y = 10;
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        LoadFontFile()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    private Font LoadFontFile(string path, int size)
    {
        PrivateFontCollection fontCollection = new System.Drawing.Text.PrivateFontCollection();

        fontCollection.AddFontFile(path);

        return new System.Drawing.Font(fontCollection.Families[0], size);
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    //                                                                                                                        Update()
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        Font font = LoadFontFile(@"Font_Pixeled.ttf", 15);

        graphics.Clear(Color.Empty);
        graphics.DrawString("FPS: " + Globals.FPS_Game, font, Brushes.White, 10, 80);
    }
}
