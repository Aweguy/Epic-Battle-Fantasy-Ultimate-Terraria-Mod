using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Dark
{
	public class SurgeExplosion : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Surge Explosion");
			Main.projFrames[projectile.type] = 13;
		}

	   

		public override void SetDefaults()
		{
			projectile.width = 48;
			projectile.height = 48;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.usesLocalNPCImmunity = true;

		}




		public override void AI()
		{
			if (++projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 12)
				{
					projectile.Kill();
				}
			}

		}
	}
}
