using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Idols.IceIdol
{
    public class IceSpike : ModProjectile
    {
        private bool Frame = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wraith Icicle");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 5;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.timeLeft = 360;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (!Frame)
            {
                projectile.frame = Main.rand.Next(0, 3);

                Frame = true;
            }

            float velRotation = projectile.velocity.ToRotation();
            projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;
        }
    }
}