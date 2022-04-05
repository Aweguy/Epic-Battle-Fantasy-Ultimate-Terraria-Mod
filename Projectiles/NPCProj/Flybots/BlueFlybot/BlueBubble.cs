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
			Main.projFrames[Projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			Projectile.width = 20;
			Projectile.height = 20;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 60 * 7;
			Projectile.hostile = true;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
		}

		public override bool PreAI()
		{
			if (Projectile.timeLeft > 60 * 4)
			{
				Projectile.velocity.X *= 0.99f;
				Projectile.velocity.Y -= 0.005f;
			}
			else if(Projectile.timeLeft < 60 * 4 && Projectile.timeLeft > 5)
			{
				Projectile.velocity *= 0.95f;
			}
			else if(Projectile.timeLeft <= 5)
			{
                if (!HasPopped)
                {
					for (int i = 0; i <= 20; i++)
					{
						Dust.NewDust(Projectile.Center, 20, 20, ModContent.DustType<LittleBubble>(), 0, 0, 0, default, 1);
					}
					HasPopped = true;
				}
				Projectile.velocity = Vector2.Zero;
				Projectile.frame = 1;
			}
			return false;
		}

		/*public override void Kill(int timeLeft)
		{
			base.Kill(timeLeft);
		}*/
	}
}
