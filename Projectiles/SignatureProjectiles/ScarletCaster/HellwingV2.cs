using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.ScarletCaster
{
	public class HellwingV2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellwing");
			Main.projFrames[Projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Bat);
			Projectile.penetrate = 1;
			Projectile.timeLeft = 1000;
			DrawOffsetX = -5;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 240);
		}

		public override void AI()
		{
			if (++Projectile.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 5)
				{
					Projectile.frame = 0;
				}
			}

			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;

			Dust dust;

			Vector2 position = Projectile.position;

			dust = Dust.NewDustDirect(position, Projectile.width, Projectile.height, DustID.Pixie, 0.2631578f, -2.368421f, 0, Color.PaleVioletRed, 0.5f);
		}
	}
}