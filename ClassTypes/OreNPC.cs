using EpicBattleFantasyUltimate.NPCs.Ores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.ClassTypes
{
	public abstract class OreNPC : ModNPC
	{
		public static readonly SoundStyle OreHit = new("EpicBattleFantasyUltimate/Assets/Sounds/Custom/NPCHit/OreHit")
		{
			Volume = 2f,
			PitchVariance = 1f
		};

		public enum OreState
		{
			Chase = 0,//the state in which the ore only chases the player without dashing
			Dash = 1,//the state in which the ore will charge its dash while chasing the player.
			Stunned = 2//The state in which the ore will be stunned and unable to move.
		}

		public OreState State
		{
			get => (OreState)NPC.ai[0];
			set => NPC.ai[0] = (float)value;
		}
		public float Attack
		{
			get => NPC.ai[1];
			set => NPC.ai[1] = value;
		}
		public float AttackTimer
		{
			get => NPC.ai[2];
			set => NPC.ai[2] = value;
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

		public int HealTimer = 60;//When the NPC will be healed from the Quartz ore's aura
		public bool SpedUp = false;//Whether the NPC has sped up or not from the Topza ore's aura
		public bool DamUp = false;//Whether the NPC has buffed damage from the Amethyst ore's aura
		public bool DefUp = false;//Whether the NPC has buffed defense from the Peridot ore's aura


		public virtual void SetSafeDefaults()
		{

		}

		public override void SetDefaults()
		{
			SetSafeDefaults();
			if (!Main.dedServ)
				NPC.HitSound = OreHit;

			//Setting the ores to be immune
			NPC.lavaImmune = true;//Making the ores immune to lava
			NPC.trapImmune = true;//Making the ores immune to traps
			NPC.buffImmune[BuffID.Poisoned] = true;//Poison immunity
			NPC.buffImmune[BuffID.OnFire] = true;//Fire immunity
			NPC.buffImmune[BuffID.Venom] = true;//Venom immunity
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			CanHit = false;

			#region Stunned

			if (Dashing)//If the ore is dashing and it hits the player
			{
				State = OreState.Stunned;//it gets stunned

				NPC.noGravity = false;//and has some various effects applied to it
				NPC.noTileCollide = false;


				Dashing = false;//and we reset the Dashing variable

				AttackTimer = 0;//Resetting the attack
			}


			#endregion

			#region Death Check

			if (NPC.life >= NPC.lifeMax * 0.40)
			{
				if (Main.rand.NextFloat() < .1f)
				{

					NPC.netUpdate = true;

					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Explosion, 40, 5f, Main.myPlayer, 0, 1);

					CheckDead();

					NPC.life = 0;
				}
				else
				{
					Vector2 relativePosition = NPC.Center - target.Center;
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
							NPC.velocity.X *= -2;
						}
						else
						{
							NPC.velocity.Y *= -2;
						}

					}
					else//if the ore is stunned the bounce velocity is much lower.
					{
						if (leftRightCollision)
						{
							NPC.velocity.X *= -0.8f;
						}
						else
						{
							NPC.velocity.Y *= -0.8f;
						}
					}
				}
			}
			else
			{
				Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Explosion, 40, 5f, Main.myPlayer, 0, 1);

				CheckDead();

				NPC.life = 0;
			}

			#endregion Death Check
			//Death code for the ores.
		}


		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i <= 4; i++)
			{
				Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}
		}

		public override bool PreAI()
		{
			Player player = Main.player[NPC.target];

			Direction(NPC);
			MovementAndDash(NPC, player);

			return true;
		}

		private void Direction(NPC NPC)
		{
			if (NPC.velocity.X > 0f) // NPC is the code that makes the sprite turn. NPCd on the vanilla one.
			{
				NPC.direction = 1;
			}
			else if (NPC.velocity.X < 0f)
			{
				NPC.direction = -1;
			}
			else if (NPC.velocity.X == 0)
			{
				NPC.direction = NPC.oldDirection;
			}

			if (NPC.direction == 1)
			{
				NPC.spriteDirection = 1;
				if (State != OreState.Stunned)
				{
					NPC.rotation = MathHelper.ToRadians(0);//Resetting the ores' rotation to normal
				}
			}
			else if (NPC.direction == -1)
			{
				NPC.spriteDirection = -1;

				if (State != OreState.Stunned)
				{
					NPC.rotation = MathHelper.ToRadians(0);
				}
			}
		}

		private void MovementAndDash(NPC NPC, Player player)
		{
			NPC.TargetClosest(true);

			if (!Dashing && State != OreState.Stunned)//This boolean shows when the ore is actually dashing. We check if it's not true so it only chases the player while not dashing
			{
				Vector2 target = player.Center - NPC.Center;
				float num1276 = target.Length();
				float MoveSpeedMult = MoveSpeedMultval; //How fast it moves and turns. A multiplier maybe?
				MoveSpeedMult += num1276 / SpeedBalance; //Balancing the speed. Lowering the division value makes it have more sharp turns.
				int MoveSpeedBal = MoveSpeedBalval; //NPC does the same as the above.... I do not understand.
				target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
				target *= MoveSpeedMult;
				NPC.velocity = (NPC.velocity * (float)(MoveSpeedBal - 1) + target) / (float)MoveSpeedBal;


			}

			if (State == OreState.Chase)//Logic control for when to switch states from chasing to dashing.
			{

				if (++AttackTimer >= 60 * DashCooldown)
				{
					AttackTimer = 0;

					State = OreState.Dash;

					NPC.netUpdate = true;
				}

			}
			else if (State == OreState.Dash)//if the state of the ore is for dashing, then run this code.
			{
				if ((Vector2.Distance(player.Center, NPC.Center) <= 16 * DashDistance && AttackTimer < DashCharge))//Range control for the charging.
				{
					AttackTimer++;
					NPC.velocity *= 0.95f;//the slow down during the charge
				}
				else if (AttackTimer == DashCharge)
				{
					NPC.velocity = Vector2.Normalize(player.Center - NPC.Center) * DashVelocity;//the speed that the ore will dash towards the player


					AttackTimer++;

					Dashing = true;//Setting this to true since it's actually dashing

				}
				else if (AttackTimer > DashCharge)
				{
					AttackTimer++;

					if (AttackTimer >= DashDuration)
					{
						AttackTimer = 0;

						NPC.velocity *= 0.25f;

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

				if (NPC.collideX)//Rolling code and velocity.
				{
					NPC.velocity.X = -(NPC.velocity.X * 0.5f);
				}
				if (NPC.collideY)
				{
					NPC.velocity.Y = -(NPC.velocity.Y * 0.5f);
				}

				NPC.rotation += MathHelper.ToRadians(2) * NPC.velocity.X;

				if (AttackTimer >= 60 * StunDuration)//Reset the ore
				{
					AttackTimer = 0;

					NPC.noGravity = true;
					NPC.noTileCollide = true;

					State = OreState.Chase;
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)//Whether the ore can hit or not the player. Using it to make the ores not hit the player while stunned
		{
			if (!CanHit)
			{
				if (NPC.Hitbox.Intersects(target.Hitbox))
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
			else if ((NPC.type != ModContent.NPCType<AmethystOre>() && NPC.type != ModContent.NPCType<RubyOre>() && NPC.type != ModContent.NPCType<TopazOre>() && NPC.type != ModContent.NPCType<ZirconOre>()) && spawnInfo.Player.ZoneRockLayerHeight)
			{
				return 0.02f;
			}
			else if ((NPC.type == ModContent.NPCType<RubyOre>() || NPC.type == ModContent.NPCType<TopazOre>() || NPC.type == ModContent.NPCType<AmethystOre_Dark>()) && spawnInfo.Player.ZoneUnderworldHeight)
            {
				return 0.07f;
            }
			else if (NPC.type == ModContent.NPCType<ZirconOre>() && spawnInfo.Player.ZoneSnow && spawnInfo.Player.ZoneUnderworldHeight)
            {
				return 0.02f;
            }
			else if (NPC.type == ModContent.NPCType<AmethystOre>() && spawnInfo.Player.ZoneMarble)
			{
				return 0.05f;
			}
            else
            {
				return 0f;
            }
		}

		
		public override void OnKill()
		{
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
			}

			if (EpicWorld.OreEvent)
			{
				EpicWorld.OreKills += 1;
			}
		}
	}
}
