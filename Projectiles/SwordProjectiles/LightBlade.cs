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
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = -1;


			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;

			projectile.knockBack = 7f;
			projectile.light = 1f;
			projectile.tileCollide = false;


			projectile.localNPCHitCooldown = -1;
			projectile.usesLocalNPCImmunity = true;

			projectile.scale = 1.3f;
		}

		public override void Kill(int timeLeft)
		{
			if (Main.rand.Next(1) == 0)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.AncientLight);
				Main.dust[dust].velocity.X *= 0.8f;
			}
		}


		public override bool CanDamage() //If it's not fully form, do not damage
		{
			if (projectile.frame == 4)
			{
				return true;
			}
			else
			{
				return false;
			}
			
		}


		public override void AI()
		{

			if (!DistanceSet)//Setting the distance of the projectile from the cursor.
			{

				SpawnPosition = Main.MouseWorld - Vector2.Normalize(new Vector2(projectile.ai[0], projectile.ai[1])) * 80f;

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


			#region animation and more
			if (!Stop)
			{
				if (++projectile.frameCounter > 2)
				{
					projectile.frameCounter = 0;

					if (++projectile.frame <= 3)
					{
						projectile.velocity = Vector2.Zero;
					}
					else if (projectile.frame == 4)
					{

						projectile.velocity = MoveSpeed;
						Stop = true;
					}
				   
					else if (projectile.frame > 4)
					{

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
			#endregion



			float velRotation = MoveSpeed.ToRotation();
			projectile.rotation = velRotation + MathHelper.ToRadians(90f);
			projectile.spriteDirection = projectile.direction;
		}
	}
}