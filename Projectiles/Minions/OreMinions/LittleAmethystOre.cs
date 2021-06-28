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
using EpicBattleFantasyUltimate.Buffs.Minions;

namespace EpicBattleFantasyUltimate.Projectiles.Minions.OreMinions
{
	class LittleAmethystOre : ModProjectile
	{

		NPC npc;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Little Amethyst Ore");
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;

			Main.projPet[projectile.type] = true;
			Main.projFrames[projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			projectile.width = 24;
			projectile.height = 24;
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1f;
			projectile.penetrate = -1;

			projectile.localNPCHitCooldown = 30;
			projectile.usesLocalNPCImmunity = true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 5;

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
				projectile.velocity.X *= -1.5f;
			}
			else
			{
				projectile.velocity.Y *= -1.5f;
			}
		}


		public override bool MinionContactDamage()
		{
			return true;
		}


		public override bool PreAI()
		{

			Player player = Main.player[projectile.owner];

			#region Active check

			// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
			if (player.dead || !player.active)
			{
				player.ClearBuff(ModContent.BuffType<LittleOreBuff>());
			}
			if (player.HasBuff(ModContent.BuffType<LittleOreBuff>()))
			{
				projectile.timeLeft = 2;
			}

			#endregion Active check

			#region General behavior


			//Code for sprite direction

			if (projectile.velocity.X > 0f) // This is the code that makes the sprite turn. Based on the vanilla one.
			{
				projectile.direction = -1;
			}
			else if (projectile.velocity.X < 0f)
			{
				projectile.direction = 1;
			}
			else if (projectile.velocity.X == 0)
			{
				projectile.direction = projectile.oldDirection;
			}

			if (projectile.direction == 1)
			{
				projectile.spriteDirection = 1;
			}
			else if (projectile.direction == -1)
			{
				projectile.spriteDirection = -1;
			}

			Vector2 idlePosition = player.Center;
			idlePosition.Y -= 32f; // Go up 48 coordinates (three tiles from the center of the player)

			// If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
			// The index is projectile.minionPos
			float minionPositionOffsetX = (10 + projectile.minionPos * 40) * -player.direction;
			idlePosition.X += minionPositionOffsetX; // Go behind the player

			// All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

			// Teleport to player if distance is too big
			Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
			float distanceToIdlePosition = vectorToIdlePosition.Length();
			if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f)
			{
				// Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
				// and then set netUpdate to true
				projectile.position = idlePosition;
				projectile.velocity *= 0.1f;
				projectile.netUpdate = true;
			}

			// If your minion is flying, you want to do this independently of any conditions
			float overlapVelocity = 0.04f;
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				// Fix overlap with other minions
				Projectile other = Main.projectile[i];
				if (i != projectile.whoAmI && other.active && other.owner == projectile.owner && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
				{
					if (projectile.position.X < other.position.X) projectile.velocity.X -= overlapVelocity;
					else projectile.velocity.X += overlapVelocity;

					if (projectile.position.Y < other.position.Y) projectile.velocity.Y -= overlapVelocity;
					else projectile.velocity.Y += overlapVelocity;
				}
			}

			#endregion General behavior

			#region Find target

			// Starting search distance
			float distanceFromTarget = 700f;
			Vector2 targetCenter = projectile.position;
			bool foundTarget = false;

			// This code is required if your minion weapon has the targeting feature
			if (player.HasMinionAttackTargetNPC)
			{
				npc = Main.npc[player.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, projectile.Center);
				// Reasonable distance away so it doesn't target across multiple screens
				if (between < 200f)
				{
					distanceFromTarget = between;
					targetCenter = npc.Center;
					foundTarget = true;
				}
			}
			if (!foundTarget)
			{
				// This code is required either way, used for finding a target
				for (int i = 0; i < Main.maxNPCs; i++)
				{					npc = Main.npc[i];
					if (npc.CanBeChasedBy())
					{
						float between = Vector2.Distance(npc.Center, projectile.Center);
						bool closest = Vector2.Distance(projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height);
						// Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
						// The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
						bool closeThroughWall = between < 100f;
						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
						{
							distanceFromTarget = between;
							targetCenter = npc.Center;
							foundTarget = true;
						}
					}
				}
			}

			#endregion Find target

			#region Movement

			// Default movement parameters (here for attacking)
			float speed = 10f;
			float inertia = 10f;

			if (foundTarget)
			{
				// Minion has a target: attack (here, fly towards the enemy)
				if (distanceFromTarget > 40f)
				{
					// The immediate range around the target (so it doesn't latch onto it when close)
					//Vector2 direction = targetCenter - projectile.Center;
					//direction.Normalize();
					//direction *= speed;
					//projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;

					Vector2 target = targetCenter - projectile.Center;
					float Length = target.Length();
					float MoveSpeedMult = 7f; //How fast it moves and turns. A multiplier maybe?
					MoveSpeedMult += Length / 100f; //Balancing the speed. Lowering the division value makes it have more sharp turns.
					int MoveSpeedBal = 100; //This does the same as the above.... I do not understand.
					target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
					target *= MoveSpeedMult;
					projectile.velocity = (projectile.velocity * (float)(MoveSpeedBal - 1) + target) / (float)MoveSpeedBal;

				}
			}
			else
			{
				// Minion doesn't have a target: return to player and idle
				if (distanceToIdlePosition > 600f)
				{
					// Speed up the minion if it's away from the player
					speed = 12f;
					inertia = 60f;
				}
				else
				{
					// Slow down the minion if closer to the player
					speed = 4f;
					inertia = 80f;
				}
				if (distanceToIdlePosition > 20f)
				{
					// The immediate range around the player (when it passively floats about)

					// This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
					Vector2 target = idlePosition - projectile.Center;
					float Length = target.Length();
					float MoveSpeedMult = 7f; //How fast it moves and turns. A multiplier maybe?
					MoveSpeedMult += Length / 100f; //Balancing the speed. Lowering the division value makes it have more sharp turns.
					int MoveSpeedBal = 100; //This does the same as the above.... I do not understand.
					target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
					target *= MoveSpeedMult;
					projectile.velocity = (projectile.velocity * (float)(MoveSpeedBal - 1) + target) / (float)MoveSpeedBal;
				}
				/*else if (projectile.velocity == Vector2.Zero)
				{
					// If there is a case where it's not moving at all, give it a little "poke"
					projectile.velocity.X = -0.15f;
					projectile.velocity.Y = -0.05f;
				}*/
			}
			#endregion Movement

			#region Animation and visuals

			// So it will lean slightly towards the direction it's moving
			projectile.rotation = projectile.velocity.X * 0.05f;

			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame == 5)
				{
					projectile.frame = 0;
				}
			}

			// Some visuals here
			Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);

			#endregion Animation and visuals


			return false;
		}

		/*public override void Kill(int timeLeft)
		{
			base.Kill(timeLeft);
		}*/
	}
}
