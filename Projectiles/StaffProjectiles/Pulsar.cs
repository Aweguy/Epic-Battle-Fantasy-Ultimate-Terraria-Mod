/*using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EpicBattleFantasyUltimate.Projectiles.StaffProjectiles
{
    public class Pulsar : ModProjectile
    {
        private int timer2 = 0;
        private int timer = 1;
        private int timer3 = 5;
        private int DrainTimer = 60;
        private float SuckRange = 160f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pulsar");
            Main.projFrames[Projectile.type] = 24;
        }

        #region SetDefaults

        private int baseWidth;
        private int baseHeight;

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.magic = true;
            Projectile.knockBack = 1f;
            Projectile.timeLeft = 60 * 100000;
            Projectile.tileCollide = false;
            drawOffsetX = 1;
            drawOriginOffsetX = 1;
            drawOriginOffsetY = 3;
            baseWidth = Projectile.width;
            baseHeight = Projectile.height;
        }

        #endregion SetDefaults

        #region AI

        public override void AI()
        {
            Color drawColor = Color.Black;
            if (Main.rand.Next(2) == 0)
            {
                drawColor = Color.Red;
            }

            Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 0, drawColor, 1f);

            Vector2 oldSize = Projectile.Size;
            // In Multi Player (MP) This code only runs on the client of the Projectile's owner, this is because it relies on mouse position, which isn't the same across all clients.
            if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0f)
            {
                Player player = Main.player[Projectile.owner];

                var epicPlayer = EpicPlayer.ModPlayer(player);

                // If the player channels the weapon, do something. This check only works if item.channel is true for the weapon.
                if (player.channel && epicPlayer.LimitCurrent > 0)
                {
                    float maxDistance = 2.3f; // This also sets the maximun speed the Projectile can reach while following the cursor.
                    Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;
                    float distanceToCursor = vectorToCursor.Length();

                    // Here we can see that the speed of the Projectile depends on the distance to the cursor.
                    if (distanceToCursor > maxDistance)
                    {
                        distanceToCursor = maxDistance / distanceToCursor;
                        vectorToCursor *= distanceToCursor;
                    }

                    int velocityXBy1000 = (int)(vectorToCursor.X * 2.3f);
                    int oldVelocityXBy1000 = (int)(Projectile.velocity.X * 2.3f);
                    int velocityYBy1000 = (int)(vectorToCursor.Y * 2.3f);
                    int oldVelocityYBy1000 = (int)(Projectile.velocity.Y * 2.3f);

                    // This code checks if the precious velocity of the Projectile is different enough from its new velocity, and if it is, syncs it with the server and the other clients in MP.
                    // We previously multiplied the speed by 1000, then casted it to int, this is to reduce its precision and prevent the speed from being synced too much.
                    if (velocityXBy1000 != oldVelocityXBy1000 || velocityYBy1000 != oldVelocityYBy1000)
                    {
                        Projectile.netUpdate = true;
                    }

                    Projectile.velocity = vectorToCursor;

                    #region Sucking Target

                    Vector2 targetCenter = Projectile.position;

                    #endregion Sucking Target

                    DrainTimer--;

                    if (DrainTimer <= 0 && player.GetModPlayer<EpicPlayer>().LimitCurrent > 0)
                    {
                        player.GetModPlayer<EpicPlayer>().LimitCurrent--;

                        DrainTimer = 60;
                    }
                    else if (DrainTimer <= 0 && player.GetModPlayer<EpicPlayer>().LimitCurrent <= 0)
                    {
                        Projectile.Kill();

                        DrainTimer = 60;
                    }

                    timer--;

                    if (player.HasBuff(mod.BuffType("HasteBuff")))
                    {
                        if (Projectile.width <= 150)
                        {
                            Projectile.scale = Projectile.scale + 0.2f;
                            SuckRange = SuckRange + (0.5f * 16f);
                        }
                        else if (Projectile.width <= 325 && Projectile.width > 150)
                        {
                            Projectile.scale = Projectile.scale + 0.1f;
                            SuckRange = SuckRange + (0.25f * 16f);
                        }
                        else
                        {
                            Projectile.scale = Projectile.scale + 0.05f;
                            SuckRange = SuckRange + (0.125f * 16f);
                        }
                        timer = 1;
                        Projectile.width = (int)(baseWidth * Projectile.scale);
                        Projectile.height = (int)(baseHeight * Projectile.scale);
                        Projectile.position = Projectile.position - (Projectile.Size - oldSize) / 2f;
                    }
                    else
                    {
                        if (Projectile.width <= 150)
                        {
                            Projectile.scale = Projectile.scale + 0.1f;
                            SuckRange = SuckRange + (0.25f * 16f);
                        }
                        else if (Projectile.width <= 325 && Projectile.width > 150)
                        {
                            Projectile.scale = Projectile.scale + 0.05f;
                            SuckRange = SuckRange + (0.125f * 16f);
                        }
                        else
                        {
                            Projectile.scale = Projectile.scale + 0.025f;
                            SuckRange = SuckRange + (0.0625f * 16f);
                        }
                        timer = 1;
                        Projectile.width = (int)(baseWidth * Projectile.scale);
                        Projectile.height = (int)(baseHeight * Projectile.scale);
                        Projectile.position = Projectile.position - (Projectile.Size - oldSize) / 2f;
                    }

                    #region Sucking

                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC npc = Main.npc[i];

                        float between = Vector2.Distance(npc.Center, Projectile.Center);

                        bool inRange = between < SuckRange;

                        if (!(npc.boss || npc.friendly || npc.townNPC) && inRange)
                        {
                            npc.velocity = npc.DirectionTo(Projectile.Center) * 10f;
                        }
                    }

                    #endregion Sucking
                }
                // If the player stops channeling, do something else.
                else if (Projectile.ai[0] == 0f || epicPlayer.LimitCurrent <= 0)
                {
                    Projectile.timeLeft = 1;

                    if (timer2 == 0)
                    {
                        Projectile.tileCollide = false;
                        // Set to transparent. This Projectile technically lives as  transparent for about 3 frames
                        // change the hitbox size, centered about the original Projectile center. This makes the Projectile damage enemies during the explosion.
                        Projectile.position = Projectile.Center;
                        //Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
                        //Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
                        if (Projectile.width <= 150)
                        {
                            Projectile.width += 80;
                            Projectile.height += 80;
                            Projectile.damage = (80 + Projectile.width) * 3;
                        }
                        else if (Projectile.width <= 325 && Projectile.width > 150)
                        {
                            Projectile.width += 220;
                            Projectile.height += 220;
                            Projectile.damage = (220 + Projectile.width) * 4;
                        }
                        else
                        {
                            Projectile.width += 500;
                            Projectile.height += 500;
                            Projectile.damage = (700 + Projectile.width) * 5;
                        }
                        Projectile.Center = Projectile.position;
                        //Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
                        //Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
                        timer2 = 1;
                    }
                }
            }
            if (++Projectile.frameCounter >= 1)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 24)
                {
                    Projectile.frame = 0;
                }
            }
        }

        #endregion AI

        #region Kill

        public override void Kill(int timeLeft)
        {
            // Fire Dust spawn
            if (Projectile.width <= 150)
            {
                for (int i = 0; i < 50; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Red, 1f);
                    Main.dust[dustIndex].noGravity = true;
                    dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Black, 1.25f);
                }
            }
            else if (Projectile.width <= 325 && Projectile.width > 150)
            {
                for (int i = 0; i < 100; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Red, 1.5f);
                    Main.dust[dustIndex].noGravity = true;
                    dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Black, 2f);
                }
            }
            else
            {
                for (int i = 0; i < 400; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Red, 2f);
                    Main.dust[dustIndex].noGravity = true;
                    dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Black, 2.5f);
                }
            }

            // Large Smoke Gore spawn
            // reset size to normal width and height.
            Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
            Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
        }

        #endregion Kill

        #region PreDraw

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.ProjectileTexture[Projectile.type];

            spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * 24, 24, 24), Color.White, Projectile.rotation, new Vector2(12, 12), Projectile.scale, SpriteEffects.None, 0);

            return false;
        }

        #endregion PreDraw
    }
}*/