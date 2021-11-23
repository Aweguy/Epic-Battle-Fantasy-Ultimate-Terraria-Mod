#region Using

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#endregion Using

namespace EpicBattleFantasyUltimate.Projectiles.SwordProjectiles
{
    public class LightningShardCyclone : ModProjectile
    {
        #region Variables

        private bool stay;
        private bool check;
        private int timer = 60;
        private float num601;
        private float returnVel;
        private float returnDistance = 500f;

        #endregion Variables

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Shard");
        }

        public override void SetDefaults()
        {
            projectile.Size = new Vector2(104, 116);
            projectile.friendly = true;

            projectile.melee = true;
            projectile.maxPenetrate = -1;

            projectile.tileCollide = false;
            projectile.penetrate = -1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            stay = true;
        }

        public override void AI()
        {
            float between = Vector2.Distance(Main.player[projectile.owner].Center, projectile.Center);

            bool Return = between > returnDistance;

            if (Return)
            {
                check = true;
            }

            if (stay && timer > 0 && !Return)
            {
                projectile.velocity = Vector2.Zero;
                timer--;
                projectile.ai[1] += 1f;
            }
            // JUST THROWN
            else if (projectile.ai[0] == 0f)
            {
                // INCREASE COUNTER
                projectile.ai[1] += 1f;
                projectile.velocity *= 0.96f;
                // CHANGE DIRECTION BASED ON VELOCITY
                if (projectile.velocity.X > 0f)
                {
                    projectile.spriteDirection = 1;
                }
                else
                {
                    projectile.spriteDirection = -1;
                }
                // AFTER THE COUNTER HITS 50 START RETURN
                if (projectile.ai[1] >= 50f)
                {
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                }
            }
            else if (check) // RETURNING CODE
            {
                // SOMETHING TO DO WITH SPEED?

                // POSITION STUFF
                Vector2 vector63 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float ownerX = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - vector63.X;
                float ownerY = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - vector63.Y;
                float ownerRoot = (float)Math.Sqrt(ownerX * ownerX + ownerY * ownerY);
                if (ownerRoot > 3000f)
                {
                    projectile.Kill();
                }
                num601 += 0.20f;
                returnVel += 0.048f;
                // SPEED STUFF
                ownerRoot = num601 / ownerRoot;
                ownerX *= ownerRoot;
                ownerY *= ownerRoot;

                // HANDLE RETURN VELOCITY
                // X
                if (projectile.velocity.X < ownerX)
                {
                    projectile.velocity.X = projectile.velocity.X + returnVel;
                    if (projectile.velocity.X < 0f && ownerX > 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X + returnVel;
                    }
                }
                else if (projectile.velocity.X > ownerX)
                {
                    projectile.velocity.X = projectile.velocity.X - returnVel;
                    if (projectile.velocity.X > 0f && ownerX < 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X - returnVel;
                    }
                }
                // Y
                if (projectile.velocity.Y < ownerY)
                {
                    projectile.velocity.Y = projectile.velocity.Y + returnVel;
                    if (projectile.velocity.Y < 0f && ownerY > 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + returnVel;
                    }
                }
                else if (projectile.velocity.Y > ownerY)
                {
                    projectile.velocity.Y = projectile.velocity.Y - returnVel;
                    if (projectile.velocity.Y > 0f && ownerY < 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - returnVel;
                    }
                }
                // CHECK IF COLLIDE WITH OWNER
                if (Main.myPlayer == projectile.owner)
                {
                    Rectangle rect = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
                    Rectangle value12 = new Rectangle((int)Main.player[projectile.owner].position.X, (int)Main.player[projectile.owner].position.Y, Main.player[projectile.owner].width, Main.player[projectile.owner].height);
                    if (rect.Intersects(value12))
                    {
                        projectile.Kill();
                    }
                }
            }
            // ROTATION CODE
            projectile.rotation += 0.4f;
        }
    }
}