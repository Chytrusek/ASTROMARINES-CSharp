﻿using ASTROMARINES.Properties;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace ASTROMARINES.Characters.Enemies
{
    public interface IEnemyFactory: IDisposable
    {
        IEnemy CreateEnemy(EnemyTypes enemyType);
        IEnemy CreateRandomEnemy();
        bool IsNewEnemyAvalible();
        bool IsPowerUpAvalible();
    }

    public class EnemyFactory : IEnemyFactory
    {
        private List<Texture> enemyTextures;
        private Clock enemyReloadClock;
        private Clock powerupReloadClock;

        public EnemyFactory()
        {
            enemyTextures = new List<Texture>();
            enemyTextures.Add(new Texture(Resources.Enemy1));
            enemyTextures.Add(new Texture(Resources.Enemy2));
            enemyTextures.Add(new Texture(Resources.Enemy3));
            enemyTextures.Add(new Texture(Resources.Enemy4));

            enemyReloadClock = new Clock();
            powerupReloadClock = new Clock();
        }

        public IEnemy CreateEnemy(EnemyTypes enemyType)
        {
            switch(enemyType)
            {
                case EnemyTypes.PowerUp:
                    powerupReloadClock.Restart();
                    return new Enemy1(enemyTextures);
                case EnemyTypes.Enemy2:
                    enemyReloadClock.Restart();
                    return new Enemy2(enemyTextures);
                case EnemyTypes.Enemy3:
                    enemyReloadClock.Restart();
                    return new Enemy3(enemyTextures);
                case EnemyTypes.Enemy4:
                    enemyReloadClock.Restart();
                    return new Enemy4(enemyTextures);
                default:
                    throw new Exception("You tried to create non-existing enemy");
            }
        }

        public IEnemy CreateRandomEnemy()
        {
            var random = new Random();
            var EnemyValues = Enum.GetValues(typeof(EnemyTypes));
            var randomEnemy = (EnemyTypes)EnemyValues.GetValue(random.Next(1,EnemyValues.Length));

            return CreateEnemy(randomEnemy);
        }

        public void Dispose()
        {
            foreach (var enemyTexture in enemyTextures)
                enemyTexture.Dispose();
            enemyReloadClock.Dispose();
            powerupReloadClock.Dispose();
        }

        public bool IsNewEnemyAvalible()
        {
            return enemyReloadClock.ElapsedTime.AsSeconds() > 3;
        }

        public bool IsPowerUpAvalible()
        {
            return powerupReloadClock.ElapsedTime.AsSeconds() > 15;
        }
    }
}
