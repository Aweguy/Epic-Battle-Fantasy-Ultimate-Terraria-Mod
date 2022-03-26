using EpicBattleFantasyUltimate.Buffs.Minions;
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

namespace EpicBattleFantasyUltimate.ClassTypes
{
	public abstract class LittleOre : ModProjectile
	{
		public enum OreState
		{
			Chase = 0,//the state in which the ore only chases the player without dashing
			Dash = 1,//the state in which the ore will charge its dash while chasing the player.
		}
		public OreState State
		{
			get => (OreState)projectile.ai[0];
			set => projectile.ai[0] = (float)value;
		}
		public float Attack
		{
			get => projectile.ai[1];
			set => projectile.ai[1] = value;
		}
		public float AttackTimer
		{
			get => projectile.localAI[0];
			set => projectile.localAI[0] = value;
		}

		public bool Dashing = false;
		public int DashCharge = 300;//How much the ore has charged to dash
		public float DashRange = 300f;//The dash range
		public float DashVelocity = 10f;//The speed of the ore when it dashes
		public int DashDuration = 60 * 2;//The duration of the dash

		public bool CanHit = true;

		public virtual void SetSafeStaticDefaults()
		{
			
		}

		public override void SetStaticDefaults()
		{
			SetSafeStaticDefaults();

			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;

			Main.projPet[projectile.type] = true;
		}

		public virtual void SetSafeDefaults()
		{

		}

		public override void SetDefaults()
		{
			SetSafeDefaults();

			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1f;
			projectile.penetrate = -1;

			projectile.localNPCHitCooldown = 10;
			projectile.usesLocalNPCImmunity = true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			CanHit = false;
		}

		public override bool? CanHitNPC(NPC target)
		{
			if (!CanHit)
			{
				if (projectile.Hitbox.Intersects(target.Hitbox))
				{
					return false;
				}
			}
			CanHit = true;
			return true;
		}

		// This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
		public override bool MinionContactDamage()
		{
			return true;
		}

		public virtual void SafePreAI()
		{

		}
		public override bool PreAI()
		{
			Player owner = Main.player[projectile.owner];

			#region Active check

			// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
			if (owner.dead || !owner.active)
			{
				owner.ClearBuff(ModContent.BuffType<LittleOreBuff>());
			}
			if (owner.HasBuff(ModContent.BuffType<LittleOreBuff>()))
			{
				projectile.timeLeft = 2;
			}

			#endregion Active check

			GeneralBehavior(owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
			SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
			Movement(foundTarget, distanceFromTarget, targetCenter, distanceToIdlePosition, vectorToIdlePosition);
			SafePreAI();

			return false;
		}

		private void GeneralBehavior(Player owner, out Vector2 vectorToIdlePosition, out float distanceToIdlePosition)
		{
			#region Direction
			if (projectile.velocity.X > 0f)
			{
				projectile.spriteDirection = -1;
			}
			else if (projectile.velocity.X < 0f)
			{
				projectile.spriteDirection = 1;
			}
			else if (projectile.velocity.X == 0)
			{
				projectile.spriteDirection = projectile.oldDirection;
			}
			#endregion
			Vector2 idlePosition = owner.Center;
			idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

			// If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
			// The index is projectile.minionPos
			float minionPositionOffsetX = (10 + projectile.minionPos * 40) * -owner.direction;
			idlePosition.X += minionPositionOffsetX; // Go behind the player

			// All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

			// Teleport to player if distance is too big
			vectorToIdlePosition = idlePosition - projectile.Center;
			distanceToIdlePosition = vectorToIdlePosition.Length();

			if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
			{
				// Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
				// and then set netUpdate to true
				projectile.position = idlePosition;
				projectile.velocity *= 0.1f;
				projectile.netUpdate = true;
			}

			// If your minion is flying, you want to do this independently of any conditions
			float overlapVelocity = 0.04f;

			// Fix overlap with other minions
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile other = Main.projectile[i];

				if (i != projectile.whoAmI && other.active && other.owner == projectile.owner && Math.Abs(projectile.position.X - other.position.X) + Math.Abs(projectile.position.Y - other.position.Y) < projectile.width)
				{
					if (projectile.position.X < other.position.X)
					{
						projectile.velocity.X -= overlapVelocity;
					}
					else
					{
						projectile.velocity.X += overlapVelocity;
					}

					if (projectile.position.Y < other.position.Y)
					{
						projectile.velocity.Y -= overlapVelocity;
					}
					else
					{
						projectile.velocity.Y += overlapVelocity;
					}
				}
			}
		}

		private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
		{
			// Starting search distance
			distanceFromTarget = 700f;
			targetCenter = projectile.position;
			foundTarget = false;

			// This code is required if your minion weapon has the targeting feature
			if (owner.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[owner.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, projectile.Center);

				// Reasonable distance away so it doesn't target across multiple screens
				if (between < 2000f)
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
				{
					NPC npc = Main.npc[i];

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

			// friendly needs to be set to true so the minion can deal contact damage
			// friendly needs to be set to false so it doesn't damage things like target dummies while idling
			// Both things depend on if it has a target or not, so it's just one assignment here
			// You don't need this assignment if your minion is shooting things instead of dealing contact damage
			projectile.friendly = foundTarget;
		}

		private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
		{
		   
			// Default movement parameters (here for attacking)
			float speed = 8f;
			float inertia = 20f;
			if(!Dashing)
			{
				if (foundTarget)
				{
					// Minion has a target: attack (here, fly towards the enemy)
					if (distanceFromTarget > 40f)
					{
						// The immediate range around the target (so it doesn't latch onto it when close)
						Vector2 direction = targetCenter - projectile.Center;
						direction.Normalize();
						direction *= speed;

						projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
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
						vectorToIdlePosition.Normalize();
						vectorToIdlePosition *= speed;
						projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
					}
					else if (projectile.velocity == Vector2.Zero)
					{
						// If there is a case where it's not moving at all, give it a little "poke"
						projectile.velocity.X = -0.15f;
						projectile.velocity.Y = -0.05f;
					}
				}
			}
			
			if (State == OreState.Chase)
			{
				
				if(++AttackTimer >= 300)
				{
					State = OreState.Dash;
					AttackTimer = 0;
				}
			}
			else if(State == OreState.Dash)
			{
				if(foundTarget)
				{
					if (distanceFromTarget <= 16f * DashRange && AttackTimer < DashCharge)
					{
						AttackTimer++;
						projectile.velocity *= 0.95f;//the slow down during the charge
					}
					else if (AttackTimer == DashCharge)
					{
						projectile.velocity = Vector2.Normalize(targetCenter - projectile.Center) * DashVelocity;//the speed that the ore will dash towards the player


						AttackTimer++;

						Dashing = true;//Setting this to true since it's actually dashing

					}
					else if (AttackTimer > DashCharge)
					{
						AttackTimer++;

						if (AttackTimer >= DashDuration)
						{
							AttackTimer = 0;

							projectile.velocity *= 0.25f;

							if (++Attack >= 1)//resetting every variable related to the dash
							{
								Attack = 0;
								Dashing = false;
								State = OreState.Chase;//Back to player chasing state
							}
						}
					}
				}
				else
				{
					State = OreState.Chase;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			base.Kill(timeLeft);
		}
	}
}