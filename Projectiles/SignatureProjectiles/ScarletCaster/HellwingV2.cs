using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.ScarletCaster
{
    public class HellwingV2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellwing");
            Main.projFrames[projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bat);
            projectile.penetrate = 1;
            projectile.timeLeft = 1000;
            drawOffsetX = -5;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 240);
        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 5)
                {
                    projectile.frame = 0;
                }
            }

            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            Dust dust;

            Vector2 position = projectile.position;

            dust = Dust.NewDustDirect(position, projectile.width, projectile.height, 55, 0.2631578f, -2.368421f, 0, Color.PaleVioletRed, 0.5f);
        }
    }
}