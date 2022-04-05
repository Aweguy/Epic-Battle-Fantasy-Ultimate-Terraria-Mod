#region Using

using Terraria;
using Terraria.ModLoader;

#endregion Using

namespace EpicBattleFantasyUltimate.Projectiles.SwordProjectiles
{
	public class GaiaAxeProj : ModProjectile
	{
		private int timer = 0;
		private int direction;
		private bool directionB = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gaia's Axe");
		}

		public override void SetDefaults()
		{
			Projectile.width = 23;
			Projectile.height = 23;

			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.aiStyle = -1;

			Projectile.penetrate = -1;
			DrawOffsetX = -5;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (!directionB)
			{
				direction = player.direction;
				directionB = true;
			}

			Projectile.rotation -= 2 * direction;

			timer++;

			if (timer >= 20)
			{
				Projectile.velocity.Y += 0.2f;

				if (Projectile.velocity.X != 0)
				{
					Projectile.velocity.X *= 0.99f;
				}
			}
		}
	}
}