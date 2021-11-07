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
			projectile.width = 1;
			projectile.height = 1;

			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.magic = true;

			projectile.tileCollide = false;

			projectile.timeLeft = 180;

			projectile.alpha = 255;
			projectile.hide = true;
		}

		public override bool PreAI()
		{
			float ParentCenterX = projectile.ai[0];
			float ParentCenterY = projectile.ai[1];

			projectile.Center = new Vector2(ParentCenterX, ParentCenterY);

			if (projectile.localAI[0] == 0)
			{
				projectile.localAI[0] = 1; // Set state to exploded
				

				if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
				{
					Filters.Scene.Activate("Shockwave", projectile.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(projectile.Center);
				}
			}

			if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
			{
				float progress = (180f - projectile.timeLeft) / 60f;
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
