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
            Main.projFrames[Projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 64;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;

            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = false;

            Projectile.alpha = 1;


            Projectile.localNPCHitCooldown = -1;
            Projectile.usesLocalNPCImmunity = true;

        }

        public override bool PreAI()
        {
            if (++Projectile.frameCounter >= 2)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 7)
                {
                    Projectile.Kill();
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
