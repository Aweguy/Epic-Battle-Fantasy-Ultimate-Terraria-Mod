using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;


namespace EpicBattleFantasyUltimate.Projectiles.Thrown
{
    public class GlassShardProjectile : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Glass Shard");
        }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.penetrate = 8;
            projectile.damage = 7;
            projectile.knockBack = 5f;
            projectile.ranged = true;
            projectile.aiStyle = 2;
            aiType = ProjectileID.ThrowingKnife;
            projectile.friendly = true;
            projectile.scale = 0.5f;
            drawOffsetX = -16;
            drawOriginOffsetX = 8;
            drawOriginOffsetY = -8;
        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("RampantBleed"), 60 * 10);
        }





        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            

            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);
            
            projectile.Kill();
            return false;

        }


        public override void Kill(int timeLeft)
        {
            Dust dust;


            for (int i = 0; i <= 10; i++)
            {

                dust = Main.dust[Terraria.Dust.NewDust(projectile.Center, projectile.width, projectile.height, 13, 0f, 0f, 0, new Color(255, 255, 255), 0.6578947f)];
            }
        }







    }
}
