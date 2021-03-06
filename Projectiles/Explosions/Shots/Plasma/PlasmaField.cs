﻿using EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Plasma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Plasma
{
    public class PlasmaField : ModProjectile
    {
        private float rotation = 0; //The rotation of the wave to the right
        private int WaveTimer = 0; //The interval between the waves
        private int NumberOfBullets = 50;//The number of bullets each weave will spawn.

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasma Field");
            Main.projFrames[projectile.type] = 13;
        }

        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.ranged = true;
            projectile.damage = 100;
            projectile.knockBack = 1f;
            projectile.tileCollide = false;
            projectile.scale = 5f;
        }

        public override void AI()
        {
            Shooting(projectile);

            #region Frame Changing

            projectile.frameCounter += 1;
            if (projectile.frameCounter > 2)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame >= 9 && projectile.ai[0] < 3)
                {
                    projectile.frame = 2;
                    projectile.ai[0]++;
                    if (projectile.ai[0] >= 3)
                    {
                        projectile.frame = 10;
                    }
                }
                if (projectile.frame >= 12)
                {
                    projectile.Kill();
                }
            }

            #endregion Frame Changing
        }

        private void Shooting(Projectile projectile)
        {
            rotation = 0;
            WaveTimer--;

            if (WaveTimer <= 0)
            {
                for (int i = 0; i <= NumberOfBullets; i++)
                {
                    Vector2 velocity = Vector2.One.RotatedBy(rotation) * 6;

                    Vector2 SpawnPos = projectile.Center + Vector2.One.RotatedBy(rotation) * 100;

                    Projectile.NewProjectile(SpawnPos, velocity, ModContent.ProjectileType<FieldWave>(), projectile.damage, 0, Main.myPlayer);

                    rotation += 360 / NumberOfBullets;
                }



                WaveTimer = 30;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            Texture2D texture = Main.projectileTexture[projectile.type];

            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle(0, projectile.frame * 64, 64, 64), Color.White, projectile.rotation, new Vector2(32, 32), projectile.scale, SpriteEffects.None, 0);

            return false;

            //return true;
        }
    }
}