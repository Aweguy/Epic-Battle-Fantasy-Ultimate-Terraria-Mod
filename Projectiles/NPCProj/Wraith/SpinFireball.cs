using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Authentication.ExtendedProtection;
using Steamworks;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
    public class SpinFireball : ModProjectile
    {
        int OrbitTimer;//How many ticks it will orbit the npc
        double distance = 90;//The distance of the projectile from the npc that is spawned
        bool shoot = false;//The bool that makes it not follow the player after launched. Sets its velocity to the last player's position.
        bool Orbit = false;//Decides how many ticks each fireball will orbit the wraith.



        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spinning Fireball");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
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
            target.AddBuff(BuffID.OnFire, 2 * 60);
        }




        public override void AI()
        {

            NPC npc = Main.npc[(int)projectile.ai[0]]; //Sets the npc that the projectile is spawned and will orbit

            if(Orbit == false)
            {
                OrbitTimer = Main.rand.Next(60 * 8, 60 * 13);

                Orbit = true;
            }


            OrbitTimer--;

            if(OrbitTimer >= 0)
            {
                EpicBattleFantasyUltimate.instance.ProjEngine.DoProjectile_OrbitPosition(this, npc.Center, distance, 4);//Makes it orbit.
            }
            else
            {
                if (!shoot)
                {
                    projectile.velocity = projectile.DirectionTo(Main.player[npc.target].Center) * 10f;//sets the velocity of the projectile.

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




        }



        #region PreDraw
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];

            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle(0, projectile.frame * 48, 48, 48), Color.White, projectile.rotation, new Vector2(24, 24), projectile.scale, SpriteEffects.None, 0);

            return false;
        }
        #endregion








    }
}