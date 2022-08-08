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
    class EagleEyeProj : EpicPiercingBowProj
    {
		public override void SetSafeDefaults()
		{
			Projectile.width = 70;
			Projectile.height = 20;

			velocity = 5f;
			maxTime = 90;
			MinimumDrawTime = 20;
			DamageMultiplier = 1.3f;
			weaponDamage = 30;
			weaponKnockback = 6f;
		}
	}
}
