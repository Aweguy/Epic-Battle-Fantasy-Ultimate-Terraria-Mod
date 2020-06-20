using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
    public class Icicle: ModProjectile
    {


        bool Frame = false;




        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wraith Icicle");
            Main.projFrames[projectile.type] = 5;
        }

        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 5;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.timeLeft = 120;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;

        }


        public override void AI()
        {
            if (!Frame)
            {
                projectile.frame = Main.rand.Next(0, 4);

                Frame = true;
            }

            float velRotation = projectile.velocity.ToRotation();
            projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;




        }






    }
}
