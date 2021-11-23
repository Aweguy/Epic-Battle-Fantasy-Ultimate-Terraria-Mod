#region Using

using EpicBattleFantasyUltimate.ClassTypes;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion Using

namespace EpicBattleFantasyUltimate.Projectiles.SwordProjectiles
{
	public class BulletBob : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;

			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = 100;
			projectile.melee = true;

			projectile.knockBack = 7f;
			drawOffsetX = 0;
		}

		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
		}

		public override void AI()
		{
			

			projectile.tileCollide = true;

			float velXMult = 1f;
			projectile.velocity.X *= velXMult;

			projectile.ai[0]++;
			if (projectile.ai[0] == 30)
			{
				projectile.frame = 1;
				projectile.velocity *= 4.3f;
			}
			else if(projectile.ai[0] > 30)
			{
				if(++projectile.frameCounter >= 3)
				{
					projectile.frame++;
					if(projectile.frame > 2)
					{
						projectile.frame = 1;
					}
					Lighting.AddLight(projectile.Center, new Vector3(255, 165, 0)/255f);//Orange lighting coming from the center of the projectile.
				}
				Homing();
			}
			else
			{
				projectile.frame = 0;
			}

			float velRotation = projectile.velocity.ToRotation();
			projectile.rotation = velRotation + MathHelper.ToRadians(90);
			projectile.spriteDirection = projectile.direction;

		}

		private void Homing()
		{
			Vector2 prey;
			Vector2 possiblePrey;
			float distance;
			float maxDistance = 500f;
			float chaseDirection = projectile.velocity.ToRotation();

			possiblePrey = Main.MouseWorld;
			distance = (Main.MouseWorld - projectile.Center).Length();

				if (distance < maxDistance || !projectile.tileCollide)
				{
					prey = Main.MouseWorld;

					chaseDirection = (projectile.Center - Main.MouseWorld).ToRotation() - (float)Math.PI;
					maxDistance = (Main.MouseWorld - projectile.Center).Length();
				}
			float trueSpeed = projectile.velocity.Length();
			float actDirection = projectile.velocity.ToRotation();
			int f = 1;

			chaseDirection = new Vector2((float)Math.Cos(chaseDirection), (float)Math.Sin(chaseDirection)).ToRotation();
			if (Math.Abs(actDirection - chaseDirection) > Math.PI)
			{
				f = -1;
			}
			else
			{
				f = 1;
			}

			if (actDirection <= chaseDirection + MathHelper.ToRadians(8) && actDirection >= chaseDirection - MathHelper.ToRadians(8))
			{
				actDirection = chaseDirection;
			}
			else if (actDirection <= chaseDirection)
			{
				actDirection += MathHelper.ToRadians(1) * f;
			}
			else if (actDirection >= chaseDirection)
			{
				actDirection -= MathHelper.ToRadians(1) * f;
			}
			actDirection = new Vector2((float)Math.Cos(actDirection), (float)Math.Sin(actDirection)).ToRotation();
			projectile.velocity.X = (float)Math.Cos(actDirection) * trueSpeed;
			projectile.velocity.Y = (float)Math.Sin(actDirection) * trueSpeed;
			projectile.rotation = actDirection + (float)Math.PI / 3;
			actDirection = projectile.velocity.ToRotation();

		}
	}
}