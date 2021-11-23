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
			projectile.width = 12;
			projectile.height = 12;
			projectile.aiStyle = 8;
			projectile.penetrate = -1;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.alpha = 100;
			aiType = 15;
		}
	}
}