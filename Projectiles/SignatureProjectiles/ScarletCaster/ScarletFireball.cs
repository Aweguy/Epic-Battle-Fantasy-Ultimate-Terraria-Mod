using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.ScarletCaster
{
	public class ScarletFireball : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scarlet Fireball");
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.knockBack = 1f;
			Projectile.timeLeft = 1000;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 240);
		}

		public override void AI()
		{
			Dust dust;
			// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
			Vector2 position = Projectile.position;
			dust = Dust.NewDustDirect(position, Projectile.width, Projectile.height, DustID.Pixie, 0.2631578f, -2.368421f, 0, Color.PaleVioletRed, 1f);
		}

		public override void Kill(int timeLeft)
		{
			Dust dust;

			Vector2 position = Projectile.position;

			for (int i = 0; i < 10; i++)
			{
				dust = Dust.NewDustDirect(position, Projectile.width, Projectile.height, DustID.Pixie, 0.2631578f, -2.368421f, 0, Color.PaleVioletRed, 1.25f);
			}
		}
	}
}