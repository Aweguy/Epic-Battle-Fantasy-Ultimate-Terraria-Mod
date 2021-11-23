using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Dark
{
    public class SurgeFissure : ModProjectile
    {
        private int timer = 0;
        private int choose = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Surge Fissure");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.damage = 0;
            projectile.knockBack = 1f;
            projectile.timeLeft = 35;
            projectile.tileCollide = false;
            projectile.alpha = 255;
        }

        public override void AI()
        {
            timer--;
            if (timer <= 0)
            {
                float X = Main.rand.NextFloat(-75f, 75f);
                float Y = Main.rand.NextFloat(-50f, 50f);

                choose = Main.rand.Next(2);

                if (choose == 0)
                {
                    Projectile.NewProjectile(projectile.Center.X + X, projectile.Center.Y + Y, 0f, 0f, ModContent.ProjectileType<SurgeExplosion>(), 70, 0, projectile.owner);
                }
                else if (choose == 1)
                {
                    Projectile.NewProjectile(projectile.Center.X + X, projectile.Center.Y + Y, 0f, 0f, ModContent.ProjectileType<SurgeExplosion2>(), 70, 0, projectile.owner);
                }

                timer = 5;
            }

            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
    }
}