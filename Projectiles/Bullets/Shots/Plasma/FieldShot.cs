using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using EpicBattleFantasyUltimate.HelperClasses;
using Terraria.ModLoader;
using EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Plasma;
using Terraria.Audio;

namespace EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Plasma
{
    public class FieldShot : Terraria.ModLoader.ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Field Shot");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.knockBack = 1f;
        }

        public override void AI()
        {

            Player player = Main.player[Projectile.owner];

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
        }

        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center); ;

            int a = Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<PlasmaField>(), Projectile.damage, 0, Projectile.owner);
        }
    }
}