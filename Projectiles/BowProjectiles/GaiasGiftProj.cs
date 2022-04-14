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
    public class GaiasGiftProj : EpicPiercingBowProj
    {
		public override void SetSafeDefaults()
		{
			Projectile.width = 66;
			Projectile.height = 26;

			velocity = 1f;
			maxTime = 45;
			MinimumDrawTime = 20;
			DamageMultiplier = 1.5f;
			weaponDamage = 15;
			weaponKnockback = 1f;
		}
	}
}
