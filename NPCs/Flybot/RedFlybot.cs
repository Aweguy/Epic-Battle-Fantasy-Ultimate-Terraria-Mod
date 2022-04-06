using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.Flybots.RedFlybot;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Flybot
{
	public class RedFlybot : ModNPC
	{
		private int BleepTimer = 20;//Determines when the check for the Bleep sound will happen
		private bool CannonSpawn2 = false;
		private bool CannonSpawn1 = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Red Flybot");
			Main.npcFrameCount[NPC.type] = 3;
		}

		public override void SetDefaults()
		{
			NPC.width = 68;
			NPC.height = 54;

			NPC.lifeMax = 300;
			NPC.damage = 25;
			NPC.defense = 30;
			NPC.value = 100;

			NPC.noGravity = true;

			NPC.HitSound = SoundID.NPCHit4;

			NPC.noTileCollide = false;
			NPC.aiStyle = -1;
		}

		public override void AI()
		{
			NPC.TargetClosest(true);

			CannonSpawning();//Cannon spawn, only runs once the flybot spawns
			movement();//Movement code from the bat
			Bleep();//Bleeping sound of the flybot
		}


		#region movement

		private void movement()
		{
			if (NPC.collideX)
			{
				NPC.velocity.X = NPC.oldVelocity.X * -0.5f;
				if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
				{
					NPC.velocity.X = 2f;
				}
				if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
				{
					NPC.velocity.X = -2f;
				}
			}
			if (NPC.collideY)
			{
				NPC.velocity.Y = NPC.oldVelocity.Y * -0.5f;
				if (NPC.velocity.Y > 0f && NPC.velocity.Y < 1f)
				{
					NPC.velocity.Y = 1f;
				}
				if (NPC.velocity.Y < 0f && NPC.velocity.Y > -1f)
				{
					NPC.velocity.Y = -1f;
				}
			}

			if (NPC.direction == -1 && NPC.velocity.X > -4f)
			{
				NPC.velocity.X -= 0.1f;
				if (NPC.velocity.X > 4f)
				{
					NPC.velocity.X -= 0.1f;
				}
				else if (NPC.velocity.X > 0f)
				{
					NPC.velocity.X += 0.05f;
				}
				if (NPC.velocity.X < -4f)
				{
					NPC.velocity.X = -4f;
				}
			}
			else if (NPC.direction == 1 && NPC.velocity.X < 4f)
			{
				NPC.velocity.X += 0.1f;
				if (NPC.velocity.X < -4f)
				{
					NPC.velocity.X += 0.1f;
				}
				else if (NPC.velocity.X < 0f)
				{
					NPC.velocity.X -= 0.05f;
				}
				if (NPC.velocity.X > 4f)
				{
					NPC.velocity.X = 4f;
				}
			}
			if (NPC.directionY == -1 && (double)NPC.velocity.Y > -1.5)
			{
				NPC.velocity.Y -= 0.04f;
				if ((double)NPC.velocity.Y > 1.5)
				{
					NPC.velocity.Y -= 0.05f;
				}
				else if (NPC.velocity.Y > 0f)
				{
					NPC.velocity.Y += 0.03f;
				}
				if ((double)NPC.velocity.Y < -1.5)
				{
					NPC.velocity.Y = -1.5f;
				}
			}
			else if (NPC.directionY == 1 && (double)NPC.velocity.Y < 1.5)
			{
				NPC.velocity.Y += 0.04f;
				if ((double)NPC.velocity.Y < -1.5)
				{
					NPC.velocity.Y += 0.05f;
				}
				else if (NPC.velocity.Y < 0f)
				{
					NPC.velocity.Y -= 0.03f;
				}
				if ((double)NPC.velocity.Y > 1.5)
				{
					NPC.velocity.Y = 1.5f;
				}
			}

			if (NPC.wet)
			{
				if (NPC.velocity.Y > 0f)
				{
					NPC.velocity.Y *= 0.95f;
				}
				NPC.velocity.Y -= 0.5f;
				if (NPC.velocity.Y < -4f)
				{
					NPC.velocity.Y = -4f;
				}
				NPC.TargetClosest(true);
			}

			NPC.ai[1] += 1f;

			if (NPC.ai[1] > 200f)
			{
				if (!Main.player[NPC.target].wet && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
				{
					NPC.ai[1] = 0f;
				}
				float num205 = 0.2f;
				float num206 = 0.1f;
				float num207 = 4f;
				float num208 = 1.5f;

				if (NPC.ai[1] > 1000f)
				{
					NPC.ai[1] = 0f;
				}
				NPC.ai[2] = 1f;
				if (NPC.ai[2] > 0f)
				{
					if (NPC.velocity.Y < num208)
					{
						NPC.velocity.Y += num206;
					}
				}
				else if (NPC.velocity.Y > 0f - num208)
				{
					NPC.velocity.Y -= num206;
				}
				if (NPC.ai[2] < -150f || NPC.ai[2] > 150f)
				{
					if (NPC.velocity.X < num207)
					{
						NPC.velocity.X += num205;
					}
				}
				else if (NPC.velocity.X > 0f - num207)
				{
					NPC.velocity.X -= num205;
				}
				if (NPC.ai[2] > 300f)
				{
					NPC.ai[2] = -300f;
				}
			}
		}

		#endregion movement

		#region Cannon Spawning

		private void CannonSpawning()
		{
			if (!CannonSpawn2)
			{
				Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X, NPC.Center.Y), Vector2.Zero, ModContent.ProjectileType<RedCannonBehind>(), 0, 0, Main.myPlayer, NPC.whoAmI);

				CannonSpawn2 = true;
			}
			if (!CannonSpawn1)
			{
				Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X, NPC.Center.Y), Vector2.Zero, ModContent.ProjectileType<RedCannonFront>(), 0, 0, Main.myPlayer, NPC.whoAmI);

				CannonSpawn1 = true;
			}
		}

		#endregion Cannon Spawning

		#region FindFrame

		private int Frame1 = 0;
		private int Frame2 = 1;
		private int Frame3 = 2;

		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter++;
			if (NPC.frameCounter < 10)
			{
				NPC.frame.Y = Frame1 * frameHeight;
			}
			else if (NPC.frameCounter < 20)
			{
				NPC.frame.Y = Frame2 * frameHeight;
			}
			else if (NPC.frameCounter < 30)
			{
				NPC.frame.Y = Frame3 * frameHeight;
			}
			else
			{
				NPC.frameCounter = 0;
			}
			if (NPC.velocity.X > 0f)
			{
				NPC.spriteDirection = 1;
			}
			if (NPC.velocity.X < 0f)
			{
				NPC.spriteDirection = -1;
			}
			NPC.rotation = NPC.velocity.X * 0.1f;
		}

		#endregion FindFrame

		private void Bleep()
		{
			if (--BleepTimer <= 0)
			{
				if (Main.rand.NextFloat() < .1f && !Main.dedServ)
				{
					SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/Flybots/FlybotBleep");
                }

                BleepTimer = 20;
			}
		}

		public override bool CheckDead()
		{
			Vector2 gorevel;

			if (NPC.velocity.Y > 0f)
			{
				gorevel = new Vector2(NPC.velocity.X / 3f, (NPC.velocity.Y * 8f) * -1);
			}
			else
			{
				gorevel = new Vector2(NPC.velocity.X / 3f, NPC.velocity.Y * 8f);
			}

			int goreIndex = Gore.NewGore(NPC.position, gorevel, Mod.Find<ModGore>("RedHelix").Type, 1f);

			for (int i = 0; i <= 20; i++)
			{
				Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Firefly, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}

			int a = Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),NPC.Center, NPC.velocity, ModContent.ProjectileType<BrokenRedFlybot>(), 40, 10f, Main.myPlayer, (int)(NPC.spriteDirection), 0);

			return true;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.hardMode && spawnInfo.Player.ZoneBeach)
			{
				return 0.03f;
			}

			return 0f;
		}

        #region NPCLoot
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
			if (Main.rand.NextFloat() < .33f)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SteelPlate>(), Main.rand.Next(2) + 1));
			}
			if (Main.rand.NextFloat() < .10f)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<P2Processor>(), Main.rand.Next(2) + 1));
			}
		}
		#endregion NPCLoot
	}
}