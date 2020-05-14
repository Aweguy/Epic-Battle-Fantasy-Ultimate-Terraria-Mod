using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
    public class FrostBoneShot : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Bone Shot");
        }

        public override void SetDefaults()
        {
            projectile.width = 9;
            projectile.height = 9;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.ranged = true;
            aiType = ProjectileID.Bullet;
            projectile.scale = 0.5f;
            projectile.timeLeft = 120;
            drawOffsetX = -2;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.alpha = 100;

        }


        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 3 * 60);
        }




        public override void AI()
        {

            Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 185, 0f, 0f, 0, new Color(0, 255, 142), 0.4605263f);

            float velRotation = projectile.velocity.ToRotation();
            projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;

        }


















    }
}
