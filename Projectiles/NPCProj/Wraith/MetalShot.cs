using EpicBattleFantasyUltimate.Buffs.Debuffs;
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
			Projectile.width = 9;
			Projectile.height = 9;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			AIType = ProjectileID.Bullet;
			Projectile.timeLeft = 120;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.tileCollide = false;
			Projectile.alpha = 50;
			DrawOffsetX = -5;
			DrawOffsetX = -5;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(ModContent.BuffType<RampantBleed>(), 60 * 10);
		}

		public override void AI()
		{
			Dust dust;
			dust = Dust.NewDustPerfect(Projectile.position, 11, new Vector2(0f, 0f), 0, new Color(255, 255, 255), 1f);
			dust.noGravity = true;
			dust.fadeIn = 0.1578947f;

			float velRotation = Projectile.velocity.ToRotation();
			Projectile.rotation = velRotation + MathHelper.ToRadians(90f);
			Projectile.spriteDirection = Projectile.direction;
		}
	}
}