using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Authentication.ExtendedProtection;
using Steamworks;
using EpicBattleFantasyUltimate.HelperClasses;
using System.IO;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
    public class SpinFireball : ModProjectile
    {
        // How many ticks it will orbit the npc.
        private int OrbitTimer;

        // The distance of the projectile from the npc that is spawned.
        private float Distance = 90;

        // The bool that makes it not follow the player after launched. Sets its velocity to the last player's position.
        private bool shoot = false;

        // Decides how many ticks each fireball will orbit the wraith.
        private bool Orbit = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spinning Fireball");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = projectile.height = 48;

            projectile.penetrate = 1;
            projectile.timeLeft = 60 * 25;

            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;

            Orbit = false;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 2 * 60);
        }

        public override bool PreAI()
        {




            Color drawColor = Color.Orange;
            if (Main.rand.Next(2) == 0)
            {
                drawColor = Color.Red;
            }
            Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire, 0f, 0f, 0, drawColor, 0.8f);

            NPC npc = Main.npc[(int)projectile.ai[0]]; //Sets the npc that the projectile is spawned and will orbit


            if(!npc.active)
            {
                projectile.Kill();
            }

            if(npc.life <= 0)
            {
                projectile.Kill();
            }


            if (Orbit == false)
            {
                // Again, networking compatibility.
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    projectile.netUpdate = true;
                    OrbitTimer = Main.rand.Next(60 * 8, 60 * 13);
                }

                Orbit = true;
            }

            if (--OrbitTimer >= 0)
            {
                projectile.DoProjectile_OrbitPosition(npc.Center, Distance, MathHelper.PiOver2);
            }
            else
            {
                if (!shoot)
                {
                    projectile.velocity = projectile.DirectionTo(Main.player[npc.target].Center) * 10f;//sets the velocity of the projectile.
                    projectile.netUpdate = true; // Eldrazi: Multiplayer compatibility.
                    shoot = true;
                }
            }

            if (++projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame == 2)
                {
                    projectile.frame = 0;
                }
            }

            return (false);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];

            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle(0, projectile.frame * 48, 48, 48), Color.White, projectile.rotation, new Vector2(24, 24), projectile.scale, SpriteEffects.None, 0);

            return false;
        }

        #region Networking

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(OrbitTimer);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            OrbitTimer = reader.ReadInt32();
        }

        #endregion






    }
}