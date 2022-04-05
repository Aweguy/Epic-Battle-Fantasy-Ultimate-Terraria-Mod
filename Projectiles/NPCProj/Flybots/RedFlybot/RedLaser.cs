using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Flybots.RedFlybot
{
	public class RedLaser : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Red Laser");
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = -1;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 240;
			Projectile.hostile = true;
			Projectile.friendly = false;
		}

		public override void AI()
		{
			Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Firefly, 0.2631578f, -2.368421f, 0, Color.Orange, 0.5f);

			float velRotation = Projectile.velocity.ToRotation();
			Projectile.rotation = velRotation + MathHelper.ToRadians(90f);
			Projectile.spriteDirection = Projectile.direction;
		}
	}
}