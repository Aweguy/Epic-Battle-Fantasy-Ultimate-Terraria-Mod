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
using Terraria.Audio;
using EpicBattleFantasyUltimate.Buffs.Buffs;

namespace EpicBattleFantasyUltimate.Projectiles.BowProjectiles.Arrows
{
    public class GaiasArrow : ModProjectile
    {
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 10;

			Projectile.penetrate = -1;
			Projectile.aiStyle = ProjectileID.WoodenArrowFriendly;
			Projectile.DamageType = DamageClass.Ranged;

			Projectile.friendly = true;
			Projectile.arrow = true;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Player player = Main.player[Projectile.owner];
			target.AddBuff(BuffID.Poisoned, 60 * 3);

			if(Main.rand.NextFloat(1f) < 0.3f)
            {
				player.AddBuff(ModContent.BuffType<Regeneration>(), 60 * 2);

			}
		}
    }
}
