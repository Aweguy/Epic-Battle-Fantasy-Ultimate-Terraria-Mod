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
using EpicBattleFantasyUltimate.Buffs.Minions;
using EpicBattleFantasyUltimate.ClassTypes;

namespace EpicBattleFantasyUltimate.Projectiles.Minions.OreMinions
{
	class LittleAmethystOre : LittleOre
	{

		NPC npc;

		public override void SetSafeStaticDefaults()
		{
			DisplayName.SetDefault("Little Amethyst Ore");
			Main.projFrames[projectile.type] = 6;
		}

		public override void SetSafeDefaults()
		{
			projectile.width = 40;
			projectile.height = 40;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			 target.immune[projectile.owner] = 5;

			Vector2 relativePosition = projectile.Center - target.Center;
			float absRelativeRotation = Math.Abs(relativePosition.ToRotation());

			bool leftRightCollision = false;

			if (absRelativeRotation <= MathHelper.PiOver4 || // Projectile is on right side of NPC.
				absRelativeRotation >= MathHelper.PiOver4 * 3) // Projectile is on left side of NPC.
			{
				leftRightCollision = true;
			}

			if (leftRightCollision)
			{
				projectile.velocity.X *= -1.5f;
			}
			else
			{
				projectile.velocity.Y *= -1.5f;
			}
		}

		public override bool MinionContactDamage()
		{
			return true;
		}

		public override void SafePreAI()
		{
			#region Animation and visuals

			// So it will lean slightly towards the direction it's moving
			projectile.rotation = projectile.velocity.X * 0.05f;

			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame == 5)
				{
					projectile.frame = 0;
				}
			}
			// Some visuals here
			Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);

			#endregion Animation and visuals
		}

		/*public override void Kill(int timeLeft)
		{
			base.Kill(timeLeft);
		}*/
	}
}
