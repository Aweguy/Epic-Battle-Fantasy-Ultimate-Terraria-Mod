#region Using

using EpicBattleFantasyUltimate.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#endregion Using

namespace EpicBattleFantasyUltimate.Projectiles.Thrown
{
	public class GlassShardProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glass Shard");
		}

		public override void SetDefaults()
		{
			Projectile.width = 6;
			Projectile.height = 6;

			Projectile.penetrate = 8;
			Projectile.knockBack = 5f;

			Projectile.DamageType = DamageClass.Ranged;
			Projectile.aiStyle = 2;
			AIType = ProjectileID.ThrowingKnife;
			Projectile.penetrate = 1;

			Projectile.friendly = true;
			Projectile.scale = 0.5f;
			DrawOffsetX = -16;

			DrawOriginOffsetX = 8;
			DrawOriginOffsetY = -8;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<RampantBleed>(), 60 * 10);
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item27,Projectile.position); 

			Projectile.Kill();
			return false;
		}

		public override void Kill(int timeLeft)
		{
			Dust dust;

			for (int i = 0; i <= 10; i++)
			{
				dust = Main.dust[Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Glass, 0f, 0f, 0, new Color(255, 255, 255), 0.6578947f)];
			}
		}
	}
}