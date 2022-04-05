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
    class BlackWidowProj : EpicComboBowProj
    {
		public override void SetSafeDefaults()
		{
			Projectile.width = 70;
			Projectile.height = 32;

			ArrowVolleyNum = 5;
			velocity = 1.5f;
			DamageMultiplier = 0.6f;
			MinimumDrawTime = 30;
			maxTime = 50;
			weaponDamage = 15;
			weaponKnockback = 6f;
		}

		/*public override void Kill(int timeLeft)
		{
			base.Kill(timeLeft);
		}*/
	}
}
