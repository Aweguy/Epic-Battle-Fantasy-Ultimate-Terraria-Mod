using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Plasma
{
    public class WaveSplit : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wave Split");
            Main.projFrames[projectile.type] = 3;
        }

        private int ArmTimer = 20;
        private int DeathTimer = 4;
        private bool Death = false;
        private int death;

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.knockBack = 1f;
        }

        public override void AI()
        {
            float velRotation = projectile.velocity.ToRotation();
            projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;

            if (++projectile.frameCounter >= 1)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 3)
                {
                    projectile.frame = 0;
                }
            }

            if (Death == false)
            {
                ArmTimer--;
            }

            if (ArmTimer <= 0)
            {
                Death = true;
            }

            if (Death)
            {
                DeathTimer--;
            }

            if (DeathTimer <= 0)
            {
                death = Main.rand.Next(3);
                if (death == 0)
                {
                    projectile.Kill();
                }
                DeathTimer = 4;
            }
        }

        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);

            int a = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("PlasmaExplosion"), projectile.damage, 0, projectile.owner);
        }
    }
}