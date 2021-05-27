using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Dark
{
    public class AntimatterExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Antimatter Explosion");
            Main.projFrames[projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;

            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.ranged = true;

            projectile.tileCollide = false;
            projectile.alpha = 1;

            projectile.localNPCHitCooldown = -1;
            projectile.usesLocalNPCImmunity = true;
        }

        public override void AI()
        {
            if (++projectile.frameCounter >= 2)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame == 7)
                {
                    projectile.Kill();
                }
            }
        }
    }
}