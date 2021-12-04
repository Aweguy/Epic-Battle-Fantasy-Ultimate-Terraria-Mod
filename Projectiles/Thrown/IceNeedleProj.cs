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

namespace EpicBattleFantasyUltimate.Projectiles.Thrown
{
	public class IceNeedleProj : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 26;

			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.friendly = true;
			projectile.aiStyle = 1;
			projectile.timeLeft = 30;
			projectile.localNPCHitCooldown = -1;
			projectile.usesLocalNPCImmunity = true;
			aiType = ProjectileID.JavelinFriendly;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
			return true;
		}

		public override void Kill(int timeLeft)
		{
			int NumOfProjectiles = 9;
			float projRotation = 0f;
			#region projectile spawn
			for (int p = 0; p <= NumOfProjectiles; p++)
			{
				Vector2 velocity = projectile.oldVelocity.RotatedBy(projRotation * 0.0174533f) * 0.5f;

				Projectile.NewProjectile(projectile.position, velocity, ModContent.ProjectileType<IceNeedleIcicle>(), projectile.damage / 2, projectile.knockBack, Main.myPlayer);

				projRotation += 360 / NumOfProjectiles;
			}
			#endregion
			#region dust spawn
			Vector2 DustPosition = projectile.position;
			Vector2 DustOldVelocity = projectile.oldVelocity;
			DustOldVelocity.Normalize();
			DustPosition += DustOldVelocity * 16f;
			for (int i = 0; i < 30; i++)
			{
				int icy = Dust.NewDust(DustPosition, projectile.width, projectile.height, DustID.IceTorch, 0f, 0f, 0, default(Color), 1f);
				Main.dust[icy].position = (Main.dust[icy].position + projectile.Center) / 2f;
				Dust dust = Main.dust[icy];
				dust.velocity += projectile.oldVelocity * 0.6f;
				dust = Main.dust[icy];
				dust.velocity *= 0.5f;
				Main.dust[icy].noGravity = true;
				DustPosition -= DustOldVelocity * 8f;
			}
			#endregion
		}
	}

	public class IceNeedleIcicle : ModProjectile
	{
		public override string Texture => "Terraria/Projectile_" + ProjectileID.NorthPoleSnowflake;

		public enum Behaviour//The behaviour of the snowflake
		{
			Idle = 0,
			Chase = 1
		}

		public Behaviour Behave
		{
			get => (Behaviour)behave;
			set => behave = (float)value;
		}
		float behave = 0f;

		bool FrameFound;
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 3;
		}

		public override void SetDefaults()
		{
			projectile.width = projectile.height = 26;

			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.melee = true;

			projectile.timeLeft = 60 * 2;

			projectile.light = 1f;
			projectile.tileCollide = false;
		}

		public override bool PreAI()
		{
			if (!FrameFound)//Setting the frame of the snowflake
			{
				FrameFound = true;
				projectile.frame = Main.rand.Next(0, 2);
			}
			if(Behave == Behaviour.Idle)//If the projectile is idle then slow down smoothly
			{
				projectile.velocity *= 0.90f;
			}
			else if (Behave == Behaviour.Chase)
			{
				projectile.timeLeft = projectile.timeLeft;
			}

			FindTarget();//Finding target and chasing.

			return false;
		}

		private void FindTarget()
		{
			if (projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref projectile.velocity);
				projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 125f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
				{
					Vector2 newMove = Main.npc[k].Center - projectile.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target)
			{
				Behave = Behaviour.Chase;
				AdjustMagnitude(ref move);
				projectile.velocity = (8 * projectile.velocity + move) / 11f;
				AdjustMagnitude(ref projectile.velocity);
			}
			else
			{
				Behave = Behaviour.Idle;
			}
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 6f)
			{
				vector *= 9f / magnitude;
			}
		}

		public override void Kill(int timeLeft)
		{
			for(int i = 0; i <= 6; i++)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.IceTorch, projectile.oldVelocity.X, projectile.oldVelocity.Y);
			}
		}
	}
}
