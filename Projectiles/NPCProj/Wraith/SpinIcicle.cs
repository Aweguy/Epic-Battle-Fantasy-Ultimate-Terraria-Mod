using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
    public class SpinIcicle : ModProjectile
    {

        int OrbitTimer;//How many ticks it will orbit the player
        double distance = 240;//The distance of the projectile from the player target.
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




        public override void AI()
        {
            NPC npc = Main.npc[(int)projectile.ai[0]]; //Sets the npc that the projectile is spawned and will orbit

            Orbiting();
            FrameCheck();



        }

        private void Orbiting()
        {

            Color drawColor = Color.White;
            if (Main.rand.Next(2) == 0)
            {
                drawColor = Color.Cyan;
            }

            if(Main.rand.Next(10) == 0)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Ice, 0f, 0f, 0, drawColor, 0.8f);

            }



            NPC npc = Main.npc[(int)projectile.ai[0]]; //Sets the npc that the projectile is spawned and will orbit

            if (!Orbit)
            {
                OrbitTimer = Main.rand.Next(60 * 8, 60 * 13);

                Orbit = true;
            }


            OrbitTimer--;

            if (OrbitTimer >= 0)
            {
                EpicBattleFantasyUltimate.instance.ProjEngine.DoProjectile_OrbitPosition(this, Main.player[npc.target].Center, distance, 5);//Makes it orbit.

                projectile.rotation = (projectile.Center - Main.player[npc.target].Center).ToRotation();

            }
            else
            {
                if (!shoot)
                {
                    projectile.velocity = projectile.DirectionTo(Main.player[npc.target].Center) * 7f;//sets the velocity of the projectile.

                    projectile.rotation = (projectile.Center - Main.player[npc.target].Center).ToRotation();

                    shoot = true;
                }
            }


            





        }


        private void FrameCheck()
        {
            if (!Frame)
            {
                projectile.frame = Main.rand.Next(0, 3);

                Frame = true;
            }
        }





    }
}
