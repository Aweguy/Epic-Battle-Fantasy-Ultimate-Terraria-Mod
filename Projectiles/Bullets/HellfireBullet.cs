#region Using

using EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Flame;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion Using

namespace EpicBattleFantasyUltimate.Projectiles.Bullets
{
    public class HellfireBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = -1;

            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.ranged = true;

            projectile.knockBack = 1f;
            projectile.scale = 0.5f;
            projectile.timeLeft = 60;
            drawOffsetX = -6;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 3 * 60);
        }

        public override void AI()
        {
            Dust dust;

            dust = Dust.NewDustPerfect(projectile.position, 55, new Vector2(0f, 0f), 0, new Color(255, 226, 0), 0.5921053f);
            dust.noGravity = true;
            dust.fadeIn = 0.85f;

            float velRotation = projectile.velocity.ToRotation();
            projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            return true;
        }

        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);

            int a = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<HellfireExplosion>(), 70, 0, projectile.owner);
        }
    }
}