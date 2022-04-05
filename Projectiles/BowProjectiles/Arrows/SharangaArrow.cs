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

namespace EpicBattleFantasyUltimate.Projectiles.BowProjectiles.Arrows
{
	public class SharangaArrow : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 10;

			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Ranged;

			Projectile.friendly = true;
			Projectile.arrow = true;
		}

		public override bool PreAI()
		{
			float velRotation = Projectile.velocity.ToRotation();
			Projectile.rotation = velRotation + MathHelper.ToRadians(90);
			Projectile.spriteDirection = Projectile.direction;

			return false;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			SoundEngine.PlaySound(SoundID.Dig,Projectile.Center);
			return true;
		}

		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
		}
	}
}
