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
	public class SharangaProj : EpicPiercingBowProj
	{
		public override void SetSafeDefaults()
		{
			projectile.width = 70;
			projectile.height = 20;

			velocity = 1.3f;
			maxTime = 45;
			MinimumDrawTime = 20;
			DamageMultiplier = 1.5f;
			weaponDamage = 15;
			weaponKnockback = 4f;
		}
	}
}
