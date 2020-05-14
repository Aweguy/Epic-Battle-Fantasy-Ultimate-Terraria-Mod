﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;



namespace EpicBattleFantasyUltimate.Projectiles.StaffProjectiles
{
    
    public class Pulsar : ModProjectile
    {
        int timer2 = 0;
        int timer = 1;
        int timer3 = 5;



        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pulsar");
            Main.projFrames[projectile.type] = 24;
        }

        #region SetDefaults
        int baseWidth;
        int baseHeight;
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.magic = true;
            projectile.knockBack = 1f;
            projectile.timeLeft = 60 * 100000;
            projectile.tileCollide = false;
            drawOffsetX = 1;
            drawOriginOffsetX = 1;
            drawOriginOffsetY = 3;
            baseWidth = projectile.width;
            baseHeight = projectile.height;

        }
        #endregion



        #region OnHitNPC
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.velocity = target.DirectionTo(projectile.Center) * 20;

            target.immune[projectile.owner] = 0;
        }
        #endregion

        #region AI

        public override void AI()
        {

            
            // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
            Vector2 position = Main.LocalPlayer.Center;

            Color drawColor = Color.Black;
            if (Main.rand.Next(2) == 0)
            {
                drawColor = Color.Red;
            }


            Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 0, drawColor, 1f);



            Vector2 oldSize = projectile.Size;
            // In Multi Player (MP) This code only runs on the client of the projectile's owner, this is because it relies on mouse position, which isn't the same across all clients.
            if (Main.myPlayer == projectile.owner && projectile.ai[0] == 0f)
            {

                Player player = Main.player[projectile.owner];
                // If the player channels the weapon, do something. This check only works if item.channel is true for the weapon.
                if (player.channel)
                {
                    float maxDistance = 2.3f; // This also sets the maximun speed the projectile can reach while following the cursor.
                    Vector2 vectorToCursor = Main.MouseWorld - projectile.Center;
                    float distanceToCursor = vectorToCursor.Length();

                    // Here we can see that the speed of the projectile depends on the distance to the cursor.
                    if (distanceToCursor > maxDistance)
                    {
                        distanceToCursor = maxDistance / distanceToCursor;
                        vectorToCursor *= distanceToCursor;
                    }

                    int velocityXBy1000 = (int)(vectorToCursor.X * 2.3f);
                    int oldVelocityXBy1000 = (int)(projectile.velocity.X * 2.3f);
                    int velocityYBy1000 = (int)(vectorToCursor.Y * 2.3f);
                    int oldVelocityYBy1000 = (int)(projectile.velocity.Y * 2.3f);

                    // This code checks if the precious velocity of the projectile is different enough from its new velocity, and if it is, syncs it with the server and the other clients in MP.
                    // We previously multiplied the speed by 1000, then casted it to int, this is to reduce its precision and prevent the speed from being synced too much.
                    if (velocityXBy1000 != oldVelocityXBy1000 || velocityYBy1000 != oldVelocityYBy1000)
                    {
                        projectile.netUpdate = true;
                    }

                    projectile.velocity = vectorToCursor;

                    timer--;
                    timer3--;

                    if(timer3 == 0)
                    {
                        player.manaRegen = 0;
                        player.statMana -= 2;

                        timer3 = 5;
                    }
                       


                    if(player.statMana <= 0)
                    {
                        projectile.timeLeft = 0;
                    }





                    if (player.HasBuff(mod.BuffType("HasteBuff")))
                    {
                        if (timer == 0)
                        {
                            if (projectile.width <= 150)
                            {
                                projectile.scale = projectile.scale + 0.2f;
                            }
                            else if (projectile.width <= 325 && projectile.width > 150)
                            {
                                projectile.scale = projectile.scale + 0.1f;
                            }
                            else
                            {
                                projectile.scale = projectile.scale + 0.05f;
                            }
                            timer = 1;
                            projectile.width = (int)(baseWidth * projectile.scale);
                            projectile.height = (int)(baseHeight * projectile.scale);
                            projectile.position = projectile.position - (projectile.Size - oldSize) / 2f;
                        }
                    }
                    else
                    {
                        if (timer == 0)
                        {
                            if (projectile.width <= 150)
                            {
                                projectile.scale = projectile.scale + 0.1f;
                            }
                            else if (projectile.width <= 325 && projectile.width > 150)
                            {
                                projectile.scale = projectile.scale + 0.05f;
                            }
                            else
                            {
                                projectile.scale = projectile.scale + 0.025f;
                            }
                            timer = 1;
                            projectile.width = (int)(baseWidth * projectile.scale);
                            projectile.height = (int)(baseHeight * projectile.scale);
                            projectile.position = projectile.position - (projectile.Size - oldSize) / 2f;
                        }
                    }



                }
                // If the player stops channeling, do something else.
                else if (projectile.ai[0] == 0f)
                {
                    projectile.timeLeft = 1;   
                    
                    if(timer2 == 0)
                    {
                        projectile.tileCollide = false;
                        // Set to transparent. This projectile technically lives as  transparent for about 3 frames
                        // change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
                        projectile.position = projectile.Center;
                        //projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
                        //projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
                        if (projectile.width <= 150)
                        {
                            projectile.width += 80;
                            projectile.height += 80;
                            projectile.damage = 80 + projectile.width * 3;
                        }
                        else if (projectile.width <= 325 && projectile.width > 150)
                        {
                            projectile.width += 220;
                            projectile.height += 220;
                            projectile.damage = 220 + projectile.width * 4;
                        }
                        else
                        {
                            projectile.width += 500;
                            projectile.height += 500;
                            projectile.damage = 700 + projectile.width * 5;
                        }                      
                        projectile.Center = projectile.position;
                        //projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
                        //projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
                        timer2 = 1;
                    }
                }
            }
            if (++projectile.frameCounter >= 1)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 24)
                {
                    projectile.frame = 0;
                }
            }

        }
        #endregion


        #region Kill
        public override void Kill(int timeLeft)
        {
            // Fire Dust spawn
            if (projectile.width <= 150)
            {
                for (int i = 0; i < 50; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 1f, 1f, 0, Color.Red, 1f);
                    Main.dust[dustIndex].noGravity = true;
                    dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 1f, 1f, 0, Color.Black, 1.25f);
                }
            }
            else if (projectile.width <= 325 && projectile.width > 150)
            {
                for (int i = 0; i < 100; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 1f, 1f, 0, Color.Red, 1.5f);
                    Main.dust[dustIndex].noGravity = true;
                    dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 1f, 1f, 0, Color.Black, 2f);
                }
            }
            else
            {
                for (int i = 0; i < 400; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 1f, 1f, 0, Color.Red, 2f);
                    Main.dust[dustIndex].noGravity = true;
                    dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 1f, 1f, 0, Color.Black, 2.5f);
                }
            }
               
            // Large Smoke Gore spawn
            for (int g = 0; g < 2; g++)
            {
                int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
                goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
                goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
            }
            // reset size to normal width and height.
            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = 10;
            projectile.height = 10;
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
            
            

          
        }
        #endregion



        #region PreDraw
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];

            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle(0, projectile.frame * 24, 24, 24), Color.White, projectile.rotation, new Vector2(12, 12), projectile.scale, SpriteEffects.None, 0);

            return false;
        }
        #endregion
    }
}