using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Flame
{
    public class BurstFissure : ModProjectile
    {
        private int timer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Burst Fissure");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.magic = true;
            projectile.damage = 0;
            projectile.knockBack = 1f;
            projectile.timeLeft = 140;
            projectile.tileCollide = false;
            projectile.alpha = 255;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat() < 0.4f)
            {
                target.AddBuff(BuffID.OnFire, 300, false);
            }
        }

        public override void AI()
        {
            if (Main.rand.Next(3) == 0)
            {
            }

            timer--;
            if (timer <= 0)
            {
                float X = Main.rand.NextFloat(-100f, 100f);
                float Y = Main.rand.NextFloat(-100f, 100f);
                int a = Projectile.NewProjectile(projectile.Center.X + X, projectile.Center.Y + Y, 0f, 0f, mod.ProjectileType("FlameBurstExplosion"), 70, 0, projectile.owner);

                timer = 14;
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