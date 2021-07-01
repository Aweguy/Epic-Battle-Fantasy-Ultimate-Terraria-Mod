using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.StaffProjectiles.JudgementLaser
{
    class LightExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            projectile.width = projectile.height = 64;

            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;

            projectile.ranged = true;
            projectile.tileCollide = false;

            projectile.alpha = 1;


            projectile.localNPCHitCooldown = -1;
            projectile.usesLocalNPCImmunity = true;

        }

        public override bool PreAI()
        {
            if (++projectile.frameCounter >= 2)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 7)
                {
                    projectile.Kill();
                }
            }

            return false;
        }

        /*public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
        }*/
    }
}
