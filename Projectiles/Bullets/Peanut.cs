#region Using
using System;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

using Microsoft.Xna.Framework;
#endregion
namespace EpicBattleFantasyUltimate.Projectiles.Bullets
{
	public class Peanut : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Peanut");
		}
		public override void SetDefaults()
		{
			projectile.width = 6;
			projectile.height = 6;
			projectile.penetrate = 10;

			projectile.damage = 7;
			projectile.knockBack = 5f;
			projectile.ranged = true;
			projectile.aiStyle = -1;

			aiType = ProjectileID.Bullet;
			projectile.friendly = true;
			drawOriginOffsetY = -5;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 0;

			Vector2 relativePosition = projectile.Center - target.Center;
			float absRelativeRotation = Math.Abs(relativePosition.ToRotation());

			bool leftRightCollision = false;

			if (absRelativeRotation <= MathHelper.PiOver4 || // Projectile is on right side of NPC.
				absRelativeRotation >= MathHelper.PiOver4 * 3) // Projectile is on left side of NPC.
			{
				leftRightCollision = true;
			}

			if (leftRightCollision)
			{
				projectile.velocity.X *= -1;
			}

			else
			{
				projectile.velocity.Y *= -1;

			}
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 10 times
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			else
			{
				Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
				Main.PlaySound(SoundID.Item10, projectile.position);

				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = -oldVelocity.X;

				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;

		}

		public override void AI()
		{
			projectile.velocity.Y += 0.10f;

			projectile.rotation += 0.4f * (float)projectile.direction;

		}
	}
}
