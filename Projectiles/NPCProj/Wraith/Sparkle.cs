using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
	public class Sparkle : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sparkle");
		}

		public override void SetDefaults()
		{
			Projectile.width = 12;
			Projectile.height = 12;
			Projectile.aiStyle = 8;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.alpha = 100;
			AIType = 15;
		}
	}
}