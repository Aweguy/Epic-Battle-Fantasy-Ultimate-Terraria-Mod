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


namespace EpicBattleFantasyUltimate.Projectiles.SwordProjectiles
{
	class LightBladeMini : ModProjectile
	{
		float SpawnDistanceFromTarget;
		bool DistanceSet = false;
		bool Stop = false;
		Vector2 SpawnPosition;
		Vector2 OldTargetPosition;
		Vector2 MoveSpeed;

		Projectile Father;
		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 11;
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;

			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Melee;

			Projectile.light = 1f;
			Projectile.tileCollide = false;

			Projectile.localNPCHitCooldown = -1;
			Projectile.usesLocalNPCImmunity = true;

			Projectile.scale = 1.3f;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if(Father.localAI[0] <= 3f)
            {
				
				Projectile.localAI[0] = Father.localAI[0];
				Projectile.localAI[0]++;
				Projectile.localAI[1]++;
				if(Projectile.localAI[1] <= 1)
                {
					float rotation = Main.rand.NextFloat(360);
					Vector2 Velocity = Projectile.velocity.RotatedBy(rotation * 0.0174533f);
					Projectile.NewProjectile(Projectile.GetSource_FromThis(),target.Center - (Vector2.Normalize(Velocity) * 80f), Velocity, ModContent.ProjectileType<LightBladeMini>(), damage, Projectile.knockBack, Projectile.owner, target.whoAmI, Projectile.whoAmI);
				}
			}
		}
		public override void Kill(int timeLeft)
		{
			Vector2 DustPosition = Projectile.position;
			Vector2 DustOldVelocity = Projectile.oldVelocity;
			DustOldVelocity.Normalize();
			DustPosition += DustOldVelocity * 16f;
			for (int i = 0; i < 20; i++)
			{
				int Light = Dust.NewDust(DustPosition, Projectile.width, Projectile.height, DustID.AncientLight, 0f, 0f, 0, default(Color), 1f);
				Main.dust[Light].position = (Main.dust[Light].position + Projectile.Center) / 2f;
				Dust dust = Main.dust[Light];
				dust.velocity += Projectile.oldVelocity * 0.6f;
				dust = Main.dust[Light];
				dust.velocity *= 0.5f;
				Main.dust[Light].noGravity = true;
				DustPosition -= DustOldVelocity * 8f;
			}
		}

		public override bool? CanDamage() //If it's not fully form, do not damage
		{
			if (Projectile.frame == 4)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public override bool PreAI()
		{
			NPC target = Main.npc[(int)Projectile.ai[0]];
			Father = Main.projectile[(int)Projectile.ai[1]];
			if (!DistanceSet)//Setting the distance of the Projectile from the cursor
			{

				SpawnPosition = target.Center - Vector2.Normalize(Projectile.velocity) * 80f;

				SpawnDistanceFromTarget = Vector2.Distance(SpawnPosition, target.Center);
				OldTargetPosition = target.Center;
				DistanceSet = true;
				MoveSpeed = Projectile.velocity;
			}

			//Change the 5 to determine how much dust will spawn. lower for more, higher for less
			if (Main.rand.Next(3) == 0)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.AncientLight);
				Main.dust[dust].velocity.X *= 0.4f;
			}

            #region animation and more
            if (!Stop)
            {
				if (++Projectile.frameCounter > 2)
				{
					Projectile.frameCounter = 0;

					if (++Projectile.frame <= 3)
					{
						Projectile.velocity = Vector2.Zero;
					}
					else if (Projectile.frame == 4)
					{
						Projectile.velocity = MoveSpeed;
						Stop = true;
					}
					else if (Projectile.frame > 4)
					{
						Projectile.velocity = Vector2.Zero;

						if (Projectile.frame == 11)
						{
							Projectile.Kill();
						}
					}
				}

			}

			if (Stop && Vector2.Distance(OldTargetPosition, Projectile.Center) >= SpawnDistanceFromTarget * 2f)
			{
				Stop = false;
			}
			#endregion

			float velRotation = MoveSpeed.ToRotation();
			Projectile.rotation = velRotation + MathHelper.ToRadians(90f);
			Projectile.spriteDirection = Projectile.direction;

			return false;
		}
	}
}
