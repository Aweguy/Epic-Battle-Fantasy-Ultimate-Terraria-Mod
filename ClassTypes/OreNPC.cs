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
	public abstract class OreNPC : ModNPC
	{
		public enum OreState
		{
			Chase = 0,//the state in which the ore only chases the player without dashing
			Dash = 1,//the state in which the ore will charge its dash while chasing the player.
			Stunned = 2//The state in which the ore will be stunned and unable to move.
		}

		public OreState State
		{
			get => (OreState)npc.ai[0];
			set => npc.ai[0] = (float)value;
		}

		public float Attack
		{
			get => npc.ai[1];
			set => npc.ai[1] = value;
		}

		public float AttackTimer
		{
			get => npc.ai[2];
			set => npc.ai[2] = value;
		}

		public bool CanHit = true;

		public bool Dashing = false;

		public int Explosion;

		public float MoveSpeedMultval;
		public int MoveSpeedBalval;
		public float SpeedBalance;

		public int DashCooldown;

		public int DashCharge;
		public float DashDistance;
		public float DashVelocity;
		public int DashDuration;

		public int StunDuration;


        public virtual void SetSafeDefaults()
		{

		}

		public override void SetDefaults()
		{
			SetSafeDefaults();

			npc.lavaImmune = true;
			npc.trapImmune = true;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.buffImmune[BuffID.Venom] = true;

		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			CanHit = false;

			#region Stunned

			if (Dashing)
			{
				State = OreState.Stunned;

				npc.noGravity = false;
				npc.noTileCollide = false;


				Dashing = false;

				AttackTimer = 0;
			}


			#endregion

			#region Death Check

			if (npc.life >= npc.lifeMax * 0.40)
			{
				if (Main.rand.NextFloat() < .1f)
				{

					npc.netUpdate = true;

					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, Explosion , 40, 5f, Main.myPlayer, 0, 1);

					CheckDead();

					npc.life = 0;
				}
				else
				{
					Vector2 relativePosition = npc.Center - target.Center;
					float absRelativeRotation = Math.Abs(relativePosition.ToRotation());

					bool leftRightCollision = false;

					if (absRelativeRotation <= MathHelper.PiOver4 || // Projectile is on right side of NPC.
						absRelativeRotation >= MathHelper.PiOver4 * 3) // Projectile is on left side of NPC.
					{
						leftRightCollision = true;
					}
					if (State != OreState.Stunned)//if the ore is not stunned the bounce velocity is higher
					{
						if (leftRightCollision)
						{
							npc.velocity.X *= -2;
						}
						else
						{
							npc.velocity.Y *= -2;
						}

					}
					else//if the ore is stunned the bounce velocity is much lower.
					{
						if (leftRightCollision)
						{
							npc.velocity.X *= -0.8f;
						}
						else
						{
							npc.velocity.Y *= -0.8f;
						}
					}
				}
			}
			else
			{
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, Explosion, 40, 5f, Main.myPlayer, 0, 1);

				CheckDead();

				npc.life = 0;
			}

			#endregion Death Check

		}


		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i <= 4; i++)
			{
				Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}

		}

        public override bool PreAI()
        {
			Player player = Main.player[npc.target];

			Direction(npc);
			MovementAndDash(npc, player);

			return true;
        }

		private void Direction(NPC npc)
		{
			if (npc.velocity.X > 0f) // npc is the code that makes the sprite turn. npcd on the vanilla one.
			{
				npc.direction = 1;
			}
			else if (npc.velocity.X < 0f)
			{
				npc.direction = -1;
			}
			else if (npc.velocity.X == 0)
			{
				npc.direction = npc.oldDirection;
			}

			if (npc.direction == 1)
			{
				npc.spriteDirection = 1;
				if (State != OreState.Stunned)
				{
					npc.rotation = MathHelper.ToRadians(0);//Resetting the ores' rotation to normal
				}
			}
			else if (npc.direction == -1)
			{
				npc.spriteDirection = -1;

				if (State != OreState.Stunned)
				{
					npc.rotation = MathHelper.ToRadians(0);
				}
			}
		}

		private void MovementAndDash(NPC npc, Player player)
		{


			npc.TargetClosest(true);

			if (!Dashing && State != OreState.Stunned)//This boolean shows when the ore is actually dashing. We check if it's not true so it only chases the player while not dashing
			{
				Vector2 target = player.Center - npc.Center;
				float num1276 = target.Length();
				float MoveSpeedMult = MoveSpeedMultval; //How fast it moves and turns. A multiplier maybe?
				MoveSpeedMult += num1276 / SpeedBalance; //Balancing the speed. Lowering the division value makes it have more sharp turns.
				int MoveSpeedBal = MoveSpeedBalval; //npc does the same as the above.... I do not understand.
				target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
				target *= MoveSpeedMult;
				npc.velocity = (npc.velocity * (float)(MoveSpeedBal - 1) + target) / (float)MoveSpeedBal;


			}


			if (State == OreState.Chase)//Logic control for when to switch states from chasing to dashing.
			{

				if (++AttackTimer >= 60 * DashCooldown)
				{
					AttackTimer = 0;

					State = OreState.Dash;

					npc.netUpdate = true;
				}

			}
			else if (State == OreState.Dash)//if the state of the ore is for dashing, then run this code.
			{
				if ((Vector2.Distance(player.Center, npc.Center) <= 16 * DashDistance && AttackTimer < DashCharge))//Range control for the charging.
				{
					AttackTimer++;
					npc.velocity *= 0.95f;//the slow down during the charge
				}
				else if (AttackTimer == DashCharge)
				{
					npc.velocity = Vector2.Normalize(player.Center - npc.Center) * DashVelocity;//the speed that the ore will dash towards the player


					AttackTimer++;

					Dashing = true;//Setting this to true since it's actually dashing

				}
				else if (AttackTimer > DashCharge)
				{
					AttackTimer++;

					if (AttackTimer >= DashDuration)
					{
						AttackTimer = 0;

						npc.velocity *= 0.25f;

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

				if (npc.collideX)//Rolling code and velocity.
				{
					npc.velocity.X = -(npc.velocity.X * 0.5f);
				}
				if (npc.collideY)
				{
					npc.velocity.Y = -(npc.velocity.Y * 0.5f);
				}

				npc.rotation += MathHelper.ToRadians(2) * npc.velocity.X;


				if (AttackTimer >= 60 * StunDuration)//Reset the ore
				{
					AttackTimer = 0;

					npc.noGravity = true;
					npc.noTileCollide = true;


					State = OreState.Chase;
				}

			}

		}



		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			if (!CanHit)
			{
				if (npc.Hitbox.Intersects(target.Hitbox))
				{
					return false;
				}
			}

			CanHit = true;
			return true;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (EpicWorld.OreEvent)
			{
				return 35f;
			}
			else if (spawnInfo.player.ZoneRockLayerHeight)
			{
				return 0.02f;
			}
			else
			{
				return 0f;
			}
		}

		public virtual void SafeNPCLoot()
		{

		}

		public override void NPCLoot()
		{

			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
			}

			SafeNPCLoot();
			if (EpicWorld.OreEvent)
			{
				EpicWorld.OreKills += 1;
			}

		}
	}
}
