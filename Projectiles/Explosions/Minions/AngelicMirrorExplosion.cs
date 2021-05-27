using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.Explosions.Minions
{
    public class AngelicMirrorExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Angelic Explosion");
            Main.projFrames[projectile.type] = 9;
        }

        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.ranged = true;
            projectile.knockBack = 1f;
            projectile.tileCollide = false;
            projectile.alpha = 64;
        }

        public override void AI()
        {
            projectile.alpha += 10;

            if (++projectile.frameCounter >= 2)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 8)
                {
                    projectile.Kill();
                }
            }
        }
    }
}