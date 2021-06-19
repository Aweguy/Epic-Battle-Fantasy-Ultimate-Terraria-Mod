#region

using EpicBattleFantasyUltimate.HelperClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion

namespace EpicBattleFantasyUltimate.Projectiles.SwordProjectiles
{
	public class LightBlade : ModProjectile
	{
		float SpawnDistanceFromClick;
		bool DistanceSet = false;
		bool Stop = false;
		Vector2 SpawnPosition;
		Vector2 OldMouseWorld;

		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 11;
		}


		public override void SetDefaults()
		{
			projectile.width = 0;
			projectile.height = 0;
			projectile.aiStyle = -1;

			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;

			projectile.knockBack = 7f;
			projectile.light = 1f;
			projectile.tileCollide = false;


			projectile.localNPCHitCooldown = -1;
			projectile.usesLocalNPCImmunity = true;
		}

		public override void Kill(int timeLeft)
		{
			if (Main.rand.Next(1) == 0)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.AncientLight);
				Main.dust[dust].velocity.X *= 0.8f;
			}
		}

		public override void AI()
		{

			if (!DistanceSet)
			{

				SpawnPosition = Main.MouseWorld - Vector2.Normalize(new Vector2(projectile.ai[0], projectile.ai[1])) * 100f;

				SpawnDistanceFromClick = Vector2.Distance(SpawnPosition, Main.MouseWorld);
				OldMouseWorld = Main.MouseWorld;
				DistanceSet = true;
			}


			Vector2 MoveSpeed = new Vector2(projectile.ai[0], projectile.ai[1]);
			//Change the 5 to determine how much dust will spawn. lower for more, higher for less
			if (Main.rand.Next(3) == 0)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.AncientLight);
				Main.dust[dust].velocity.X *= 0.4f;
			}



			if (!Stop)
			{
				if (++projectile.frameCounter > 3)
				{
					projectile.frameCounter = 0;

					if (++projectile.frame <= 3)
					{
						projectile.velocity = Vector2.Zero;
					}
					else if (projectile.frame == 4)
					{
						projectile.width = projectile.height = 14;

						projectile.velocity = MoveSpeed;
						Stop = true;
					}
				   
					else if (projectile.frame > 4)
					{
						projectile.width = projectile.height = 0;

						projectile.velocity = Vector2.Zero;

						if (projectile.frame == 11)
						{
							projectile.Kill();
						}
					}

				}
			}
			if (Stop)
			{
				if(Vector2.Distance(OldMouseWorld, projectile.Center) >= SpawnDistanceFromClick * 2f)
				{
					Stop = false;
				}
			}

			if (projectile.alpha >= 255)
			{
				projectile.Kill();
			}

			float velRotation = MoveSpeed.ToRotation();
			projectile.rotation = velRotation + MathHelper.ToRadians(90f);
			projectile.spriteDirection = projectile.direction;
		}





	}
}