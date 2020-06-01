using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Projectiles.SpearProjectiles
{
    public class GashSpike : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gash Spike");
        }

        int pentimer = 30;


        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            drawOffsetX = -3;
            projectile.melee = true;
            projectile.knockBack = 1f;
        }

        public override void AI()
        {
            projectile.velocity.Y += 0.1f;

            pentimer--;

            if(pentimer <= 0)
            {
                projectile.penetrate = 1;
            }


            float velRotation = projectile.velocity.ToRotation();
            projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;

        }





    }
}
