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
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.damage = 0;
            Projectile.knockBack = 1f;
            Projectile.timeLeft = 140;
            Projectile.tileCollide = false;
            Projectile.alpha = 255;

            Projectile.localNPCHitCooldown = -1;
            Projectile.usesLocalNPCImmunity = true;

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
                int a = Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Center.X + X, Projectile.Center.Y + Y, 0f, 0f, ModContent.ProjectileType<FlameBurstExplosion>(), 70, 0, Projectile.owner);

                timer = 14;
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