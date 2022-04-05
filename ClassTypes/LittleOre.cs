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
			get => (OreState)Projectile.ai[0];
			set => Projectile.ai[0] = (float)value;
		}
		public float Attack
		{
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}
		public float AttackTimer
		{
			get => Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}

		public bool Dashing = false;

		public int DashCharge;//How much the ore has charged to dash
		public float DashRange;//The dash range
		public float DashVelocity;//The speed of the ore when it dashes
		public int DashDuration;//The duration of the dash

		public bool CanHit = true;

		public virtual void SetSafeStaticDefaults()
		{
			
		}

		public override void SetStaticDefaults()
		{
			SetSafeStaticDefaults();

			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;

			Main.projPet[Projectile.type] = true;
		}

		public virtual void SetSafeDefaults()
		{

		}

		public override void SetDefaults()
		{
			SetSafeDefaults();

			Projectile.tileCollide = false;
			Projectile.friendly = true;
			Projectile.minion = true;
			Projectile.minionSlots = 1f;
			Projectile.penetrate = -1;

			Projectile.localNPCHitCooldown = 10;
			Projectile.usesLocalNPCImmunity = true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			CanHit = false;

			if (Dashing)
            {
				State = OreState.Chase;
				Dashing = false;
			}
			

		}

		public override bool? CanHitNPC(NPC target)
		{
			if (!CanHit)
			{
				if (Projectile.Hitbox.Intersects(target.Hitbox))
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
			Player owner = Main.player[Projectile.owner];

			#region Active check

			// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
			if (owner.dead || !owner.active)
			{
				owner.ClearBuff(ModContent.BuffType<LittleOreBuff>());
			}
			if (owner.HasBuff(ModContent.BuffType<LittleOreBuff>()))
			{
				Projectile.timeLeft = 2;
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
			if (Projectile.velocity.X > 0f)
			{
				Projectile.spriteDirection = -1;
			}
			else if (Projectile.velocity.X < 0f)
			{
				Projectile.spriteDirection = 1;
			}
			else if (Projectile.velocity.X == 0)
			{
				Projectile.spriteDirection = Projectile.oldDirection;
			}
			#endregion
			Vector2 idlePosition = owner.Center;
			idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

			// If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
			// The index is Projectile.minionPos
			float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
			idlePosition.X += minionPositionOffsetX; // Go behind the player

			// All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

			// Teleport to player if distance is too big
			vectorToIdlePosition = idlePosition - Projectile.Center;
			distanceToIdlePosition = vectorToIdlePosition.Length();

			if (Main.myPlayer == owner.whoAmI && distanceToIdlePosition > 2000f)
			{
				// Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the Projectile,
				// and then set netUpdate to true
				Projectile.position = idlePosition;
				Projectile.velocity *= 0.1f;
				Projectile.netUpdate = true;
			}

			// If your minion is flying, you want to do this independently of any conditions
			float overlapVelocity = 0.04f;

			// Fix overlap with other minions
			for (int i = 0; i < Main.maxProjectiles; i++)
			{
				Projectile other = Main.projectile[i];

				if (i != Projectile.whoAmI && other.active && other.owner == Projectile.owner && Math.Abs(Projectile.position.X - other.position.X) + Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)
				{
					if (Projectile.position.X < other.position.X)
					{
						Projectile.velocity.X -= overlapVelocity;
					}
					else
					{
						Projectile.velocity.X += overlapVelocity;
					}

					if (Projectile.position.Y < other.position.Y)
					{
						Projectile.velocity.Y -= overlapVelocity;
					}
					else
					{
						Projectile.velocity.Y += overlapVelocity;
					}
				}
			}
		}

		private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
		{
			// Starting search distance
			distanceFromTarget = 700f;
			targetCenter = Projectile.position;
			foundTarget = false;

			// This code is required if your minion weapon has the targeting feature
			if (owner.HasMinionAttackTargetNPC)
			{
				NPC npc = Main.npc[owner.MinionAttackTargetNPC];
				float between = Vector2.Distance(npc.Center, Projectile.Center);

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
						float between = Vector2.Distance(npc.Center, Projectile.Center);
						bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
						bool inRange = between < distanceFromTarget;
						bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
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
			Projectile.friendly = foundTarget;
		}

		private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition)
		{
		   
			// Default movement parameters (here for attacking)
			float speed = 8f;
			float inertia = 10f;

			if(!Dashing)
			{
				if (foundTarget)
				{
					// Minion has a target: attack (here, fly towards the enemy)
					if (distanceFromTarget > 40f)
					{
						// The immediate range around the target (so it doesn't latch onto it when close)
						Vector2 direction = targetCenter - Projectile.Center;
						direction.Normalize();
						direction *= speed;

						Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
					}
				}
				else
				{
					// Minion doesn't have a target: return to player and idle
					if (distanceToIdlePosition > 600f)
					{
						// Speed up the minion if it's away from the player
						speed = 12f;
						inertia = 600f;
					}
					else
					{
						// Slow down the minion if closer to the player
						speed = 4f;
						inertia = 800f;
					}

					if (distanceToIdlePosition > 20f)
					{
						// The immediate range around the player (when it passively floats about)

						// This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
						vectorToIdlePosition.Normalize();
						vectorToIdlePosition *= speed;
						Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
					}
					else if (Projectile.velocity == Vector2.Zero)
					{
						// If there is a case where it's not moving at all, give it a little "poke"
						Projectile.velocity.X = -0.15f;
						Projectile.velocity.Y = -0.05f;
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
						Projectile.velocity *= 0.95f;//the slow down during the charge
					}
					else if (AttackTimer == DashCharge)
					{
						Dashing = true;//Setting this to true since it's actually dashing
						Projectile.velocity = Vector2.Normalize(targetCenter - Projectile.Center) * DashVelocity;//the speed that the ore will dash towards the player

						AttackTimer++;
					}
					else if (AttackTimer > DashCharge)
					{
						AttackTimer++;

						if (AttackTimer >= DashDuration)
						{
							AttackTimer = 0;

							Projectile.velocity *= 0.25f;

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