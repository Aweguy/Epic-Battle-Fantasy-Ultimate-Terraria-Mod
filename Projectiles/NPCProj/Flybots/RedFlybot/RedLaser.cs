using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Flybots.RedFlybot
{
    public class RedLaser : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Laser");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.timeLeft = 240;
            projectile.hostile = true;
            projectile.friendly = false;

        }





        public override void AI()
        {

            Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 6, 0.2631578f, -2.368421f, 0, Color.Orange, 0.5f);

            float velRotation = projectile.velocity.ToRotation();
            projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;

        }







    }
}
