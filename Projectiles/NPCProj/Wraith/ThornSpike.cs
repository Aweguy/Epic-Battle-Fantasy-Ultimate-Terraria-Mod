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
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.ranged = true;
			projectile.timeLeft = 120;
			drawOffsetX = -2;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.tileCollide = false;
			projectile.alpha = 100;
		}

		public override void AI()
		{
			float velRotation = projectile.velocity.ToRotation();
			projectile.rotation = velRotation + MathHelper.ToRadians(90f);
			projectile.spriteDirection = projectile.direction;
		}
	}
}