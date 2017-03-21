﻿using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using ASTROMARINES.Properties;

namespace ASTROMARINES
{
    public partial class Player : IPlayer
    {
        int HP;
        int HPMax;
        Texture playerTexture;
        Sprite playerSprite;
        Vector2f dimensions;
        Vector2f speedVector;
        MousePointer mousePointer;
        Weapon weapon;
        PlayerLevel playerLevel;
        HPBar hpBar;
        public List<Bullet> Bullets { get; private set; }
        public Vector2f Position { get => playerSprite.Position; }

        public Player()
        {
            playerTexture = new Texture(Resources.Player);

            playerSprite = new Sprite(playerTexture);
            playerSprite.Scale = new Vector2f(0.25f * WindowProperties.ScaleX,
                                              0.25f * WindowProperties.ScaleY);
            playerSprite.Origin = new Vector2f(256 / 2, 256 / 2);
            dimensions.X = 256 * 0.25f * WindowProperties.ScaleX;
            dimensions.Y = 256 * 0.25f * WindowProperties.ScaleY;
            playerSprite.Position = new Vector2f(WindowProperties.WindowWidth / 2,
                                                 WindowProperties.WindowHeight - dimensions.Y);
            playerLevel = new PlayerLevel();

            mousePointer = new MousePointer();
            weapon = new Weapon();
            hpBar = new HPBar(dimensions);
            Bullets = new List<Bullet>();

            HPMax = 100;
            HP = HPMax;
        }


        public bool ShouldBeDeleted { get => HP <= 0; }

        public FloatRect BoundingBox { get => playerSprite.GetGlobalBounds(); }

        public void Damaged()
        {
            HP--;
            hpBar.UpdateHPBarSize(HP, HPMax);
        }

        public void DrawPlayer(RenderWindow window)
        {
            window.Draw(playerSprite);
            mousePointer.Draw(window);
            weapon.Draw(window, playerLevel);
            foreach (var bullet in Bullets)
                bullet.Draw(window);
            hpBar.Draw(window);
        }

        public void LevelUp()
        {
            switch (playerLevel)
            {
                case PlayerLevel.Level1:
                    playerLevel = PlayerLevel.Level2;
                    break;
                case PlayerLevel.Level2:
                    playerLevel = PlayerLevel.Level3;
                    break;
                case PlayerLevel.Level3:
                    playerLevel = PlayerLevel.Level4;
                    break;
                case PlayerLevel.Level4:
                    break;
                default:
                    break;
            }
        }

        public void Move()
        {
            foreach (var bullet in Bullets)
                bullet.Move();

            //TO DO
            //PROPER MOVEMENT AND BOUNCING ON WALLS
            playerSprite.Position += speedVector;

            hpBar.SetHPBarPositon(Position, dimensions);
        }



        public void Shoot(RenderWindow window)
        {
            Bullets = weapon.Shoot(playerLevel, Position, dimensions, window);
        }
    }
}
