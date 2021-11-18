using EpicBattleFantasyUltimate.ClassTypes;
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

namespace EpicBattleFantasyUltimate.Projectiles.BowProjectiles
{
	class AlchemistBowProj : EpicComboBowProj
	{
		public override void SetSafeDefaults()
		{
			projectile.width = 58;
			projectile.height = 28;

			ArrowVolleyNum = 4;
			speed = 25f;
			velocity = 2f;
			DamageMultiplier = 0.5f;
			MinimumDrawTime = 20;
			maxTime = 30;
			weaponDamage = 10;
			weaponKnockback = 5f;
		}

		

		/*public override void Kill(int timeLeft)
		{
			base.Kill(timeLeft);
		}*/
	}
}
