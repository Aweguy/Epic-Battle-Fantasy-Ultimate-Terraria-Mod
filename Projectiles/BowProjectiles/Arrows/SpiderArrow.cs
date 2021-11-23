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

namespace EpicBattleFantasyUltimate.Projectiles.BowProjectiles.Arrows
{
	class SpiderArrow : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 10;

			projectile.aiStyle = ProjectileID.WoodenArrowFriendly;
			projectile.ranged = true;
			projectile.arrow = true;
		}

		public override bool PreAI()
		{
			float velRotation = projectile.velocity.ToRotation();
			projectile.rotation = velRotation + MathHelper.ToRadians(90);
			projectile.spriteDirection = projectile.direction;

			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(SoundID.Dig, projectile.position);
			return true;
		}

		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);

			Projectile.NewProjectile(projectile.Center, Vector2.Zero, ProjectileID.BabySpider, (int)projectile.damage / 2, projectile.knockBack, projectile.owner);
		}
	}
}
