using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.SpearProjectiles
{
	public class GashSpike : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gash Spike");
		}

		private int pentimer = 30;

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			DrawOffsetX = -3;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.knockBack = 1f;
		}

		public override void AI()
		{
			Projectile.velocity.Y += 0.1f;

			pentimer--;

			if (pentimer <= 0)
			{
				Projectile.penetrate = 1;
			}

			float velRotation = Projectile.velocity.ToRotation();
			Projectile.rotation = velRotation + MathHelper.ToRadians(90f);
			Projectile.spriteDirection = Projectile.direction;
		}
	}
}