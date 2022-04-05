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
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.damage = 0;
            Projectile.knockBack = 1f;
            Projectile.timeLeft = 35;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;
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
                    Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(),Projectile.Center.X + X, Projectile.Center.Y + Y, 0f, 0f, ModContent.ProjectileType<SurgeExplosion>(), 70, 0, Projectile.owner);
                }
                else if (choose == 1)
                {
                    Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(),Projectile.Center.X + X, Projectile.Center.Y + Y, 0f, 0f, ModContent.ProjectileType<SurgeExplosion2>(), 70, 0, Projectile.owner);
                }

                timer = 5;
            }

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 4)
                {
                    Projectile.frame = 0;
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            return true;
        }
    }
}