using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Flame
{
    public class HellfireExplosion : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Explosion");
            Main.projFrames[projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = -1;
            projectile.friendly = true;

            projectile.penetrate = -1;
            projectile.knockBack = 1f;
            projectile.tileCollide = false;
            projectile.alpha = 1;

            projectile.localNPCHitCooldown = -1;
            projectile.usesLocalNPCImmunity = true;

        }

        public override void AI()
        {

            if (++projectile.frameCounter >= 1)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 7)
                {
                    projectile.Kill();
                }
            }

        }



    }
}
