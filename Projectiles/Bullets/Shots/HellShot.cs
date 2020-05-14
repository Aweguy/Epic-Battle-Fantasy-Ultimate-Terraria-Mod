using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Projectiles.Bullets.Shots
{
    public class HellShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bullet Hell");
            Main.projFrames[projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.damage = 10;
            projectile.knockBack = 1f;
            aiType = ProjectileID.Bullet;
            projectile.timeLeft = 20;
            drawOffsetX = -9;
        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 9 * 60);
        }



        public override void AI()
        {
        
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            
            for (int i = 0; i <= 3; i++)
            {
                 Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 0, new Color(255, 201, 0), 1f);
            }

            float velRotation = projectile.velocity.ToRotation();
            projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;


            if (++projectile.frameCounter >= 1)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 5)
                {
                    projectile.frame = 0;
                }
            }



        }







        public override void Kill(int timeLeft)
        {
            // This code and the similar code above in OnTileCollide spawn dust from the tiles collided with. SoundID.Item10 is the bounce sound you hear.
            Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
            Main.PlaySound(SoundID.Item10, projectile.position);

            
            for (int i = 0; i <= 90; i++)
            {
                Vector2 vel = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));
                if (vel.Length() < 3) vel = Vector2.Normalize(vel) * 3f;   //minimum speed
                {
                    Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("HellfireBullet"), 20, 0, projectile.owner, 0, 1);
                }
                
            }


     
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.

            for (int i = 0; i <= 13; i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 55, 0f, 0f, 0, new Color(255, 201, 0), 1f);
            }



        }
    }
}