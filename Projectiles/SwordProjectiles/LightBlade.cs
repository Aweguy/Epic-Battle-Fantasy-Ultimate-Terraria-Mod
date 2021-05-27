#region

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace EpicBattleFantasyUltimate.Projectiles.SwordProjectiles
{
    public class LightBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.aiStyle = -1;

            projectile.friendly = true;
            projectile.penetrate = 100;
            projectile.melee = true;

            projectile.knockBack = 7f;
            projectile.light = 1f;
            drawOffsetX = -7;
            projectile.tileCollide = false;

            projectile.localNPCHitCooldown = -1;
            projectile.usesLocalNPCImmunity = true;
        }

        public override void Kill(int timeLeft)
        {
            if (Main.rand.Next(1) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.AncientLight);
                Main.dust[dust].velocity.X *= 0.8f;
            }
        }

        public override void AI()
        {
            //Change the 5 to determine how much dust will spawn. lower for more, higher for less
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.AncientLight);
                Main.dust[dust].velocity.X *= 0.4f;
            }

            float velXMult = 1f;
            projectile.velocity.X *= velXMult;
            projectile.alpha += 3;
            projectile.velocity *= 0.98f;

            if (projectile.alpha >= 255)
            {
                projectile.Kill();
            }

            float velRotation = projectile.velocity.ToRotation();
            projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;
        }
    }
}