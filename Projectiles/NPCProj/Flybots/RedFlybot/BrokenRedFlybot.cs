using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Flybots.RedFlybot
{
	public class BrokenRedFlybot : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Broken Red Flybot");
		}

		public override void SetDefaults()
		{
			Projectile.width = 46;
			Projectile.height = 46;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.alpha = 255;

			Projectile.tileCollide = false;

			Projectile.timeLeft = 2;

			Projectile.width *= 2;
			Projectile.height *= 2;

			SoundEngine.PlaySound(SoundID.Item15, Projectile.position);

			for (int i = 0; i <= 40; i++)
			{
				Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Firefly, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}
			return false;
		}

		public override void AI()
		{
			int NpcSpriteDirection = (int)(Projectile.ai[0]);

			Projectile.rotation += MathHelper.ToRadians(10) * NpcSpriteDirection;

			if (Projectile.velocity.Y < 10f)
			{
				Projectile.velocity.Y += 0.15f;
				Projectile.velocity.X *= 0.95f;
			}
			else
			{
				Projectile.velocity.Y = 9f;
				Projectile.velocity.X *= 0.95f;
			}
		}
	}
}