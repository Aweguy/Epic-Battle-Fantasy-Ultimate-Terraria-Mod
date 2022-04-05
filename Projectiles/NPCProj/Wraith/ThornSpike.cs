using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
	public class ThornSpike : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thorn Spike");
		}

		public override void SetDefaults()
		{
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 120;
			DrawOffsetX = -2;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.tileCollide = false;
			Projectile.alpha = 100;
		}

		public override void AI()
		{
			float velRotation = Projectile.velocity.ToRotation();
			Projectile.rotation = velRotation + MathHelper.ToRadians(90f);
			Projectile.spriteDirection = Projectile.direction;
		}
	}
}