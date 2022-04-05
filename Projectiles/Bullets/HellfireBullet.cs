#region Using

using EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Flame;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#endregion Using

namespace EpicBattleFantasyUltimate.Projectiles.Bullets
{
    public class HellfireBullet : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.aiStyle = -1;

            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.DamageType = DamageClass.Ranged;

            Projectile.knockBack = 1f;
            Projectile.scale = 0.5f;
            Projectile.timeLeft = 60;
            DrawOffsetX = -6;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 3 * 60);
        }

        public override void AI()
        {
            Dust dust;

            dust = Dust.NewDustPerfect(Projectile.position, 55, new Vector2(0f, 0f), 0, new Color(255, 226, 0), 0.5921053f);
            dust.noGravity = true;
            dust.fadeIn = 0.85f;

            float velRotation = Projectile.velocity.ToRotation();
            Projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            Projectile.spriteDirection = Projectile.direction;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
            return true;
        }

        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

            int a = Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<HellfireExplosion>(), 70, 0, Projectile.owner);
        }
    }
}