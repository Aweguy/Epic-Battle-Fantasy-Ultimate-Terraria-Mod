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
			Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			Projectile.width = 12;
			Projectile.height = 12;

			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = 100;
			Projectile.DamageType = DamageClass.Melee;

			Projectile.knockBack = 7f;

			Projectile.localNPCHitCooldown = -1;
			Projectile.usesLocalNPCImmunity = true;
		}

		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
		}

		public override void AI()
		{
			Projectile.tileCollide = true;
			float velXMult = 1f;
			Projectile.velocity.X *= velXMult;

			Projectile.ai[0]++;
			if (Projectile.ai[0] == 30)
			{
				Projectile.frame = 1;
				Projectile.velocity *= 4.3f;
			}
			else if(Projectile.ai[0] > 30)
			{
				if(++Projectile.frameCounter >= 3)
				{
					Projectile.frame++;
					if(Projectile.frame > 2)
					{
						Projectile.frame = 1;
					}
					DustAI();
					Lighting.AddLight(Projectile.Center, new Vector3(255, 165, 0)/255f);//Orange lighting coming from the center of the Projectile.
				}
				Homing();
			}
			else
			{
				Projectile.frame = 0;
			}

			float velRotation = Projectile.velocity.ToRotation();
			Projectile.rotation = velRotation + MathHelper.ToRadians(90f);
			Projectile.spriteDirection = Projectile.direction;
		}

		private void DustAI()
		{
			for (int i = 0; i <= 2; i++)
			{
				Dust.NewDust(Projectile.position, Projectile.width / 2, Projectile.height / 2, DustID.Firefly);
			}
		}
		private void Homing()
		{
			Vector2 prey;
			Vector2 possiblePrey;
			float distance;
			float maxDistance = 500f;
			float chaseDirection = Projectile.velocity.ToRotation();

			possiblePrey = Main.MouseWorld;
			distance = (Main.MouseWorld - Projectile.Center).Length();

				if (distance < maxDistance || !Projectile.tileCollide)
				{
					prey = Main.MouseWorld;

					chaseDirection = (Projectile.Center - Main.MouseWorld).ToRotation() - (float)Math.PI;
					maxDistance = (Main.MouseWorld - Projectile.Center).Length();
				}
			float trueSpeed = Projectile.velocity.Length();
			float actDirection = Projectile.velocity.ToRotation();
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
			Projectile.velocity.X = (float)Math.Cos(actDirection) * trueSpeed;
			Projectile.velocity.Y = (float)Math.Sin(actDirection) * trueSpeed;
			Projectile.rotation = actDirection + (float)Math.PI / 3;
			actDirection = Projectile.velocity.ToRotation();

		}
	}
}