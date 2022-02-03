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
			Dash = 1,//the state in, which the ore will charge its dash while chasing the player.
			Stunned = 2//Whe state in which the ore will be unable to move or attack
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
			get => projectile.ai[2];
			set => projectile.ai[2] = value;
		}

		public bool CanHit = true;

		public bool Dashing = false;

		public int Explosion;

		public float MoveSpeedMultval;
		public int MoveSpeedBalval;
		public float SpeedBalance;

		public int DashCooldown;//The cooldown before the ore is able to dash again

		public int DashCharge;//How much the ore has charged to dash
		public float DashDistance;//The dash range
		public float DashVelocity;//The speed of the ore when it dashes
		public int DashDuration;//The duration of the dash

		public int StunDuration;//The duration of the stun when the ore hits the player

		Vector2 idlePosition;
		float distanceToIdlePosition;
		NPC target;

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

			/*#region Bouncing
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
			#endregion*/
			CanHit = false;

			#region Stunned

			if (Dashing)//If the ore is dashing and it hits the player
			{
				State = OreState.Stunned;//it gets stunned

				projectile.tileCollide = true;


				Dashing = false;//and we reset the Dashing variable

				AttackTimer = 0;//Resetting the attack
			}

			#endregion

			
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

		public virtual void SafePreAI()
		{

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
			projectile.rotation = projectile.velocity.X * 0.05f;

			#endregion Active check

			GeneralBehaviour(player);
			TargetingAndMovementAndDash(player);
			SafePreAI();

			return false;
		}

		private void GeneralBehaviour(Player player)
		{
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

			if (!Dashing && State != OreState.Stunned)//This boolean shows when the ore is actually dashing. We check if it's not true so it only chases the player while not dashing
			{
				idlePosition = player.Center;
				idlePosition.Y -= 32f; // Go up 48 coordinates (three tiles from the center of the player)

				// If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
				// The index is projectile.minionPos
				float minionPositionOffsetX = (10 + projectile.minionPos * 40) * -player.direction;
				idlePosition.X += minionPositionOffsetX; // Go behind the player

				// All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

				// Teleport to player if distance is too big
				Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
				distanceToIdlePosition = vectorToIdlePosition.Length();
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
			}
		}

		private void TargetingAndMovementAndDash(Player player)
		{

			// Starting search distance
			float distanceFromTarget = 700f;
			Vector2 targetCenter = projectile.position;
			bool foundTarget = false;
			NPC npc;//The npc that will be targeted

			#region Targeting
			if (player.HasMinionAttackTargetNPC)
			{
				// This code is required if your minion weapon has the targeting feature
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
				{
					npc = Main.npc[i];
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
			#endregion

			#region NoDash

			if (!Dashing && State != OreState.Stunned)//This boolean shows when the ore is actually dashing. We check if it's not true so it only chases the player while not dashing
			{
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
				
			}
			#endregion

			if (State == OreState.Chase)//Logic control for when to switch states from chasing to dashing.
			{
				if (++AttackTimer >= 60 * DashCooldown)
				{
					AttackTimer = 0;

					State = OreState.Dash;

					projectile.netUpdate = true;
				}

			}
			else if (State == OreState.Dash)//if the state of the ore is for dashing, then run this code.
			{
				if ((Vector2.Distance(target.Center, projectile.Center) <= 16 * DashDistance && AttackTimer < DashCharge))//Range control for the charging.
				{
					AttackTimer++;
					projectile.velocity *= 0.95f;//the slow down during the charge
				}
				else if (AttackTimer == DashCharge)
				{
					projectile.velocity = Vector2.Normalize(target.Center - projectile.Center) * DashVelocity;//the speed that the ore will dash towards the player


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
			else if (State == OreState.Stunned)//If the ore is stunned
			{
				AttackTimer++;

				if (projectile.)//Rolling code and velocity.
				{
					projectile.velocity.X = -(projectile.velocity.X * 0.5f);
				}
				if (projectile.collideY)
				{
					projectile.velocity.Y = -(projectile.velocity.Y * 0.5f);
				}

				projectile.rotation += MathHelper.ToRadians(2) * npc.velocity.X;


				if (AttackTimer >= 60 * StunDuration)//Reset the ore
				{
					AttackTimer = 0;

					npc.noGravity = true;
					projectile.tileCollide = false;


					State = OreState.Chase;
				}
			}
		}

		/*public override void Kill(int timeLeft)
		{
			base.Kill(timeLeft);
		}*/
	}
}