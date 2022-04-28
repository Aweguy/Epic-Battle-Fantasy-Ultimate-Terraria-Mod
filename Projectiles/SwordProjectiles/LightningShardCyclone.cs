#region Using

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#endregion Using

namespace EpicBattleFantasyUltimate.Projectiles.SwordProjectiles
{
	public class LightningShardCyclone : ModProjectile
	{
		#region Variables

		private bool stay;
		private bool check;
		private int timer = 60;
		private float num601;
		private float returnVel;
		private float returnDistance = 500f;
		bool Return;

		#endregion Variables

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lightning Shard");
		}

		public override void SetDefaults()
		{
			Projectile.Size = new Vector2(52, 52);
			Projectile.friendly = true;

			Projectile.DamageType = DamageClass.Melee;
			Projectile.maxPenetrate = -1;

			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            if (!Return)
            {
				stay = true;
			}
		}

		public override void AI()
		{
			float between = Vector2.Distance(Main.player[Projectile.owner].Center, Projectile.Center);

			Return = between > returnDistance;

			if (Return)
			{
				check = true;
			}

			if (stay && timer > 0 && !Return)
			{
				Projectile.velocity = Vector2.Zero;
				timer--;
				Projectile.ai[1] += 1f;
			}
			// JUST THROWN
			else if (Projectile.ai[0] == 0f)
			{
				// INCREASE COUNTER
				Projectile.ai[1] += 1f;
				Projectile.velocity *= 0.96f;
				// CHANGE DIRECTION BASED ON VELOCITY
				if (Projectile.velocity.X > 0f)
				{
					Projectile.spriteDirection = 1;
				}
				else
				{
					Projectile.spriteDirection = -1;
				}
				// AFTER THE COUNTER HITS 50 START RETURN
				if (Projectile.ai[1] >= 50f)
				{
					Projectile.ai[0] = 1f;
					Projectile.ai[1] = 0f;
					Projectile.netUpdate = true;
				}
			}
			else if (check) // RETURNING CODE
			{
				// SOMETHING TO DO WITH SPEED?

				// POSITION STUFF
				Vector2 vector63 = new Vector2(Projectile.position.X + (float)Projectile.width * 0.5f, Projectile.position.Y + (float)Projectile.height * 0.5f);
				float ownerX = Main.player[Projectile.owner].position.X + (float)(Main.player[Projectile.owner].width / 2) - vector63.X;
				float ownerY = Main.player[Projectile.owner].position.Y + (float)(Main.player[Projectile.owner].height / 2) - vector63.Y;
				float ownerRoot = (float)Math.Sqrt(ownerX * ownerX + ownerY * ownerY);
				if (ownerRoot > 3000f)
				{
					Projectile.Kill();
				}
				num601 += 0.20f;
				returnVel += 0.048f;
				// SPEED STUFF
				ownerRoot = num601 / ownerRoot;
				ownerX *= ownerRoot;
				ownerY *= ownerRoot;

				// HANDLE RETURN VELOCITY
				// X
				if (Projectile.velocity.X < ownerX)
				{
					Projectile.velocity.X = Projectile.velocity.X + returnVel;
					if (Projectile.velocity.X < 0f && ownerX > 0f)
					{
						Projectile.velocity.X = Projectile.velocity.X + returnVel;
					}
				}
				else if (Projectile.velocity.X > ownerX)
				{
					Projectile.velocity.X = Projectile.velocity.X - returnVel;
					if (Projectile.velocity.X > 0f && ownerX < 0f)
					{
						Projectile.velocity.X = Projectile.velocity.X - returnVel;
					}
				}
				// Y
				if (Projectile.velocity.Y < ownerY)
				{
					Projectile.velocity.Y = Projectile.velocity.Y + returnVel;
					if (Projectile.velocity.Y < 0f && ownerY > 0f)
					{
						Projectile.velocity.Y = Projectile.velocity.Y + returnVel;
					}
				}
				else if (Projectile.velocity.Y > ownerY)
				{
					Projectile.velocity.Y = Projectile.velocity.Y - returnVel;
					if (Projectile.velocity.Y > 0f && ownerY < 0f)
					{
						Projectile.velocity.Y = Projectile.velocity.Y - returnVel;
					}
				}
				// CHECK IF COLLIDE WITH OWNER
				if (Main.myPlayer == Projectile.owner)
				{
					Rectangle rect = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
					Rectangle value12 = new Rectangle((int)Main.player[Projectile.owner].position.X, (int)Main.player[Projectile.owner].position.Y, Main.player[Projectile.owner].width, Main.player[Projectile.owner].height);
					if (rect.Intersects(value12))
					{
						Projectile.Kill();
					}
				}
			}
			// ROTATION CODE
			Projectile.rotation += 0.4f;
		}
	}
}