using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Projectiles.SwordProjectiles
{
    public class GaiaAxeProj : ModProjectile
    {
        int timer = 0;
        int direction;
        bool directionB = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gaia's Axe");
        }

        public override void SetDefaults()
        {
            projectile.width = 23;
            projectile.height = 23;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            drawOffsetX = -5;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if(!directionB)
            {
                direction = player.direction;
                directionB = true;
            }









            projectile.rotation -= 2 * direction;

            timer++;


            if(timer >= 20)
            {
                projectile.velocity.Y += 0.2f;

                if(projectile.velocity.X != 0)
                {
                    projectile.velocity.X *= 0.99f;
                }
            }


        }








    }
}
