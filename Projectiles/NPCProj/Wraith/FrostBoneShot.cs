using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            AIType = ProjectileID.Bullet;
            Projectile.timeLeft = 120;
            DrawOffsetX = -2;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = false;
            Projectile.alpha = 100;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 3 * 60);
        }

        public override void AI()
        {
            Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.FrostHydra, 0f, 0f, 0, new Color(0, 255, 142), 0.4605263f);

            float velRotation = Projectile.velocity.ToRotation();
            Projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            Projectile.spriteDirection = Projectile.direction;
        }
    }
}