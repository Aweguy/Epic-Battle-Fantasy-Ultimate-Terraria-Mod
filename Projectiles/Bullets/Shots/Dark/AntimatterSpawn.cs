using EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Dark;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Dark
{
    public class AntimatterSpawn : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Antimatter Spawn");
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.knockBack = 1f;
            Projectile.timeLeft = 25;
        }

        public override void AI()
        {
            float velRotation = Projectile.velocity.ToRotation();
            Projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            Projectile.spriteDirection = Projectile.direction;

            if (++Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 2)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

            int a = Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(),Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AntimatterExplosion>(), Projectile.damage / 4, 0, Projectile.owner);
        }
    }
}