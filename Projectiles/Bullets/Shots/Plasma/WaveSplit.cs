using EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Plasma;
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
            Main.projFrames[Projectile.type] = 3;
        }

        private int ArmTimer = 20;
        private int DeathTimer = 4;
        private bool Death = false;
        private int death;

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.knockBack = 1f;
        }

        public override void AI()
        {
            float velRotation = Projectile.velocity.ToRotation();
            Projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            Projectile.spriteDirection = Projectile.direction;

            if (++Projectile.frameCounter >= 1)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 3)
                {
                    Projectile.frame = 0;
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
                    Projectile.Kill();
                }
                DeathTimer = 4;
            }
        }

        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

            int a = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<PlasmaExplosion>(), Projectile.damage, 0, Projectile.owner);
        }
    }
}