using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.BowProjectiles.Arrows
{
	class SpiderArrow : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 10;
			Projectile.extraUpdates = 2;

			Projectile.aiStyle = ProjectileID.WoodenArrowFriendly;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.arrow = true;
		}

		public override bool PreAI()
		{
			float velRotation = Projectile.velocity.ToRotation();
			Projectile.rotation = velRotation + MathHelper.ToRadians(90);
			Projectile.spriteDirection = Projectile.direction;

			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
			return true;
		}

		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ProjectileID.BabySpider, (int)Projectile.damage / 2, Projectile.knockBack, Projectile.owner);
		}
	}
}
