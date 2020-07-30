using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Dark
{
    public class SurgeShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Surge Shot");
            Main.projFrames[projectile.type] = 10;
        }

        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.damage = 10;
            projectile.knockBack = 1f;
            aiType = ProjectileID.Bullet;
            //drawOffsetX = -9;
        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 9 * 60);
        }



        public override void AI()
        {
            if (++projectile.frameCounter >= 7)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 10)
                {
                    projectile.frame = 0;
                }
            }



            float velRotation = projectile.velocity.ToRotation();
            projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;


        }







        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);


            
                    
            int a = Projectile.NewProjectile(projectile.Center.X , projectile.Center.Y , 0f, 0f, mod.ProjectileType("SurgeFissure"), 70, 0, projectile.owner);
                


        }
    }
}