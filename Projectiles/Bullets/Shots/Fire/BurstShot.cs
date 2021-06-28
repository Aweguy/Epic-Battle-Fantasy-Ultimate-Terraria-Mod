using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Fire
{
    public class BurstShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Burst Shot");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.damage = 10;
            projectile.knockBack = 1f;
            aiType = ProjectileID.Bullet;
            drawOffsetX = -6;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 9 * 60);
        }

        public override void AI()
        {
            Dust dust;
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            dust = Dust.NewDustDirect(projectile.position, 10, 10, DustID.Pixie, 0f, 0f, 0, new Color(255, 176, 0), 1f);

            float velRotation = projectile.velocity.ToRotation();
            projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;
        }

        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);

            int a = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("BurstFissure"), 0, 0, projectile.owner);

            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.

            for (int i = 0; i <= 13; i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Pixie, 0f, 0f, 0, new Color(255, 201, 0), 1f);
            }
        }
    }
}