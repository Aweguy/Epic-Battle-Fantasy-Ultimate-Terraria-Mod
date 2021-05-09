using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
{
	public partial class LauncherProjectile : GlobalProjectile
	{

		public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			OnHitNPC_LifeSteal(projectile, target, damage, knockback, crit);
			OnHitNPC_PoisonedRounds(projectile, target, damage, knockback, crit);

		}


		public override void AI(Projectile projectile)
		{
			Homing(projectile);
		}
	}
}
