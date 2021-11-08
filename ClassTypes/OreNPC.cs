﻿using Microsoft.Xna.Framework;
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

		public int DashCooldown;//The cooldown before the ore is able to dash again

		public int DashCharge;//How much the ore has charged to dash
		public float DashDistance;//The dash range
		public float DashVelocity;//The speed of the ore when it dashes
		public int DashDuration;//The duration of the dash

		public int StunDuration;//The duration of the stun when the ore hits the player

		public bool HasAura = false;//Whether the ore will have an aura

		public int Aura = 0;//What aura the ore has.

		public int HealTimer = 60;//When the npc will be healed from the Quartz ore's aura
		public bool SpedUp = false;//Whether the npc has sped up or not from the Topza ore's aura
		public bool DamUp = false;//Whether the npc has buffed damage from the Amethyst ore's aura
		public bool DefUp = false;//Whether the npc has buffed defense from the Peridot ore's aura


		public virtual void SetSafeDefaults()
		{

		}

		public override void SetDefaults()
		{
			SetSafeDefaults();

			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/OreHit");

			if(Main.rand.NextFloat(1f) < 0.1f)
			{
				HasAura = true;
			}
			//Setting the ores to be immune
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

			if (Dashing)//If the ore is dashing and it hits the player
			{
				State = OreState.Stunned;//it gets stunned

				npc.noGravity = false;//and has some various effects applied to it
				npc.noTileCollide = false;


				Dashing = false;//and we reset the Dashing variable

				AttackTimer = 0;//Resetting the attack
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
			//Death code for the ores.
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

			if (HasAura)
			{
				Auras();
			}

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

		private void Auras()
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npcIndex = Main.npc[i];

				if (npcIndex.active)
				{
					float distance = Vector2.Distance(npc.Center, npcIndex.Center);//Calculating the distance

					if (distance <= 80f)
					{
						if (Aura == 1 && npcIndex.life < npcIndex.lifeMax)//The buff aura for the quartz ore
						{
							if(--HealTimer == 0)
							{
								npcIndex.life += 10;
								if (npcIndex.life >= npcIndex.lifeMax)
								{
									npcIndex.life = npcIndex.lifeMax;
								}
								HealTimer = 60;
							}
						}
						if (Aura == 2 && !SpedUp)//The buff aura for the Topaz Ore
						{
							npcIndex.velocity *= 1.5f;
							SpedUp = true;
						}
						if (Aura == 3 && !DamUp)//The buff aura for the Ruby Ore
						{
							npcIndex.damage += (int)(npcIndex.defDamage * 0.2f);
							DamUp = true;
						}
						if (Aura == 4 && !DefUp)//The buff aura for the Peridot Ore
						{
							npcIndex.defense += (int)(npcIndex.defDefense * 0.2f);
							DefUp = true;
						}
					}
				}
			}

		}
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)//Whether the ore can hit or not the player. Using it to make the ores not hit the player while stunned
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

		public override void NPCLoot()//Shared NPC loot.
		{

			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
			}

			SafeNPCLoot();
			if (EpicWorld.OreEvent)
			{
				if (HasAura)
				{
					EpicWorld.OreKills += 2;

				}
				else
				{
					EpicWorld.OreKills += 1;

				}
			}

		}
	}
}
