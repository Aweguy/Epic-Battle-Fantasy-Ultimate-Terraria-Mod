using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.HelperClasses;
using System.IO;


namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
    public class SpinIcicle : ModProjectile
    {

        int OrbitTimer;//How many ticks it will orbit the player
        float Distance = 240;//The distance of the projectile from the player target.
        bool shoot = false;//The bool that makes it not follow the player after launched. Sets its velocity to the last player's position.
        bool Orbit = false;//Decides how many ticks each icicle will orbit the player.
        bool Frame = false;//The bool that determines the texture of the icicle




        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icicle");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 5;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.timeLeft = 60 * 25;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;

        }


        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 2 * 60);
        }

        public override bool PreAI()
        {




            Color drawColor = Color.Orange;
            if (Main.rand.Next(2) == 0)
            {
                drawColor = Color.Red;
            }

            NPC npc = Main.npc[(int)projectile.ai[0]]; //Sets the npc that the projectile is spawned and will orbit


            if (!npc.active)
            {
                projectile.Kill();
            }

            if (npc.life <= 0)
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
                projectile.DoProjectile_OrbitPosition(Main.player[npc.target].Center, Distance, MathHelper.PiOver2);
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


            if(Frame == false)
            {
                FrameCheck();
                Frame = true;
            }



            return (false);
        }


        private void FrameCheck()
        {
            if (!Frame)
            {
                projectile.frame = Main.rand.Next(0, 3);

                Frame = true;
            }
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
