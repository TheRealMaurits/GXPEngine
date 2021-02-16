﻿using System;
using GXPEngine;

namespace GXPEngine
{
    class Hitbox_Player : Sprite
    {
        Sound _damageSound;

        bool MCDamagetake;
        float Damagecounter;

        public Hitbox_Player() : base("hitbox_player.png")
        {
            Spawn();

            _damageSound = new Sound("damage_sound_player.wav", false, false);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Spawn()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Spawn()
        {
            SetOrigin(width / 2, height / 2);
            SetXY(0, 0);
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        OnCollision()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void OnCollision(GameObject other)
        {
            if (MCDamagetake == false)
            {
                Damagecounter = Damagecounter + 1;
            }

            if (Damagecounter == 200)
            {
                MCDamagetake = true;
                Damagecounter = 0;
            }

            if (other is Hitbox_Enemy && Globals.countFramesAttackEnemy == 6 && MCDamagetake == true)
            {
                Globals.health = Globals.health - 1;
                _damageSound.Play();
                MCDamagetake = false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //                                                                                                                        Update()
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        void Update()
        {

        }
    }
}
