using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;

namespace EpicBattleFantasyUltimate.NPCs.Flybot
{
    public class RedCannon : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Cannon");
        }

        public override void SetDefaults()
        {
            projectile.width = 9;
            projectile.height = 9;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
        }




        public override void AI()
        {
            NPC npc = Main.npc[(int)projectile.ai[0]]; //Sets the npc that the projectile is spawned and will orbit
            Player player = Main.player[(int)projectile.ai[1]];

            if(npc.life <= 0)
            {
                projectile.Kill();
            }
            else
            {
                projectile.timeLeft = 100;
            }

            Vector2 velocity = projectile.DirectionTo(Main.player[npc.target].Center) * 10f;//sets the velocity of the projectile.

            //Projectile.NewProjectile(projectile.Center, velocity, ModContent.ProjectileType<LeafSplinter>(), 30, 3, Main.myPlayer, 0, 1);

            if(npc.spriteDirection == 1)
            {
                projectile.position = new Vector2(npc.Center.X + 5, npc.Center.Y);
            }
            else
            {
                projectile.position = new Vector2(npc.Center.X -15 , npc.Center.Y);

            }
            projectile.rotation = (projectile.Center - player.Center).ToRotation();


        }








    }
}
