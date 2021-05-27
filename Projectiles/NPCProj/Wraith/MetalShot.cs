using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
    public class MetalShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metal Shot");
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
            projectile.timeLeft = 120;
            drawOffsetX = -5;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.alpha = 50;
            drawOffsetX = -5;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("RampantBleed"), 60 * 10);
        }

        public override void AI()
        {
            Dust dust;
            dust = Dust.NewDustPerfect(projectile.position, 11, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
            dust.noGravity = true;
            dust.fadeIn = 0.1578947f;

            float velRotation = projectile.velocity.ToRotation();
            projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;
        }
    }
}