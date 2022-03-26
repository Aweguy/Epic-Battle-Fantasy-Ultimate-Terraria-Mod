using EpicBattleFantasyUltimate.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Flybots.BlueFlybot
{
	public class BlueBubble : ModProjectile
	{
		bool HasPopped;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blue Bubble");
			Main.projFrames[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.aiStyle = -1;
			projectile.penetrate = -1;
			projectile.ranged = true;
			projectile.timeLeft = 60 * 7;
			projectile.hostile = true;
			projectile.tileCollide = false;
			projectile.friendly = false;
		}

		public override bool PreAI()
		{
			if (projectile.timeLeft > 60 * 4)
			{
				projectile.velocity.X *= 0.99f;
				projectile.velocity.Y -= 0.005f;
			}
			else if(projectile.timeLeft < 60 * 4 && projectile.timeLeft > 5)
			{
				projectile.velocity *= 0.95f;
			}
			else if(projectile.timeLeft <= 5)
			{
                if (!HasPopped)
                {
					for (int i = 0; i <= 20; i++)
					{
						Dust.NewDust(projectile.Center, 20, 20, ModContent.DustType<LittleBubble>(), 0, 0, 0, default, 1);
					}
					HasPopped = true;
				}
				projectile.velocity = Vector2.Zero;
				projectile.frame = 1;
			}
			return false;
		}

		/*public override void Kill(int timeLeft)
		{
			base.Kill(timeLeft);
		}*/
	}
}
