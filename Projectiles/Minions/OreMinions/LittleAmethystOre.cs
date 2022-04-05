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
			Main.projFrames[Projectile.type] = 6;
		}

		public override void SetSafeDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 40;

			DashCharge = 120;
			DashRange = 300f;
			DashVelocity = 12f;
			DashDuration = 60 * 5;

		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			 target.immune[Projectile.owner] = 5;

			Vector2 relativePosition = Projectile.Center - target.Center;
			float absRelativeRotation = Math.Abs(relativePosition.ToRotation());

			bool leftRightCollision = false;

			if (absRelativeRotation <= MathHelper.PiOver4 || // Projectile is on right side of NPC.
				absRelativeRotation >= MathHelper.PiOver4 * 3) // Projectile is on left side of NPC.
			{
				leftRightCollision = true;
			}

			if (leftRightCollision)
			{
				Projectile.velocity.X *= -1.5f;
			}
			else
			{
				Projectile.velocity.Y *= -1.5f;
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
			Projectile.rotation = Projectile.velocity.X * 0.05f;

			if (++Projectile.frameCounter >= 5)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame == 5)
				{
					Projectile.frame = 0;
				}
			}
			// Some visuals here
			Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);

			#endregion Animation and visuals
		}

		/*public override void Kill(int timeLeft)
		{
			base.Kill(timeLeft);
		}*/
	}
}
