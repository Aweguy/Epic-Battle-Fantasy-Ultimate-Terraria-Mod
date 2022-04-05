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
using EpicBattleFantasyUltimate.ClassTypes;

namespace EpicBattleFantasyUltimate.Projectiles.BowProjectiles
{
	public class JuggernautProj : EpicPiercingBowProj
	{
		public override void SetSafeDefaults()
		{
			Projectile.width = 58;
			Projectile.height = 28;

			velocity = 1.2f;
			maxTime = 60;
			MinimumDrawTime = 30;
			DamageMultiplier = 1.5f;
			weaponDamage = 10;
			weaponKnockback = 5f;
		}
	}
}
