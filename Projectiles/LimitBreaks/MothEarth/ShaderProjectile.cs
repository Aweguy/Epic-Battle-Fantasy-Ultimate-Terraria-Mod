using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.LimitBreaks.MothEarth
{
	class ShaderProjectile : ModProjectile
	{
		private int rippleCount = 2;
		private int rippleSize = 6;
		private int rippleSpeed = 20;
		private float distortStrength = 129f;

		public override void SetDefaults()
		{
			Projectile.width = 1;
			Projectile.height = 1;

			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;

			Projectile.timeLeft = 180;

			Projectile.alpha = 255;
			Projectile.hide = true;
		}

		public override bool PreAI()
		{
			float ParentCenterX = Projectile.ai[0];
			float ParentCenterY = Projectile.ai[1];

			Projectile.Center = new Vector2(ParentCenterX, ParentCenterY);

			if (Projectile.localAI[0] == 0)
			{
				Projectile.localAI[0] = 1; // Set state to exploded
				

				if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
				{
					Filters.Scene.Activate("Shockwave", Projectile.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(Projectile.Center);
				}
			}

			if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
			{
				float progress = (180f - Projectile.timeLeft) / 60f;
				Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(distortStrength * (1 - progress / 3f));
			}

			return false;
		}

		public override void Kill(int timeLeft)
		{
			if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
			{
				Filters.Scene["Shockwave"].Deactivate();
			}
		}
	}
}
