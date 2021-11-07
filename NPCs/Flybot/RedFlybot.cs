using EpicBattleFantasyUltimate.Projectiles.NPCProj.Flybots.RedFlybot;
using Microsoft.Xna.Framework;
using Terraria;
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
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.width = 68;
			npc.height = 54;

			npc.lifeMax = 300;
			npc.damage = 25;
			npc.defense = 30;
			npc.value = 100;

			npc.noGravity = true;

			npc.HitSound = SoundID.NPCHit4;

			npc.noTileCollide = false;
			npc.aiStyle = -1;
		}

		public override void AI()
		{
			npc.TargetClosest(true);

			BleepTimer--;

			CannonSpawning(npc);
			movement(npc);
			Bleep(npc);
		}

		#region movement

		private void movement(NPC npc)
		{
			if (npc.collideX)
			{
				npc.velocity.X = npc.oldVelocity.X * -0.5f;
				if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
				{
					npc.velocity.X = 2f;
				}
				if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
				{
					npc.velocity.X = -2f;
				}
			}
			if (npc.collideY)
			{
				npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
				if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
				{
					npc.velocity.Y = 1f;
				}
				if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
				{
					npc.velocity.Y = -1f;
				}
			}

			if (npc.direction == -1 && npc.velocity.X > -4f)
			{
				npc.velocity.X = npc.velocity.X - 0.1f;
				if (npc.velocity.X > 4f)
				{
					npc.velocity.X = npc.velocity.X - 0.1f;
				}
				else if (npc.velocity.X > 0f)
				{
					npc.velocity.X = npc.velocity.X + 0.05f;
				}
				if (npc.velocity.X < -4f)
				{
					npc.velocity.X = -4f;
				}
			}
			else if (npc.direction == 1 && npc.velocity.X < 4f)
			{
				npc.velocity.X = npc.velocity.X + 0.1f;
				if (npc.velocity.X < -4f)
				{
					npc.velocity.X = npc.velocity.X + 0.1f;
				}
				else if (npc.velocity.X < 0f)
				{
					npc.velocity.X = npc.velocity.X - 0.05f;
				}
				if (npc.velocity.X > 4f)
				{
					npc.velocity.X = 4f;
				}
			}
			if (npc.directionY == -1 && (double)npc.velocity.Y > -1.5)
			{
				npc.velocity.Y = npc.velocity.Y - 0.04f;
				if ((double)npc.velocity.Y > 1.5)
				{
					npc.velocity.Y = npc.velocity.Y - 0.05f;
				}
				else if (npc.velocity.Y > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + 0.03f;
				}
				if ((double)npc.velocity.Y < -1.5)
				{
					npc.velocity.Y = -1.5f;
				}
			}
			else if (npc.directionY == 1 && (double)npc.velocity.Y < 1.5)
			{
				npc.velocity.Y = npc.velocity.Y + 0.04f;
				if ((double)npc.velocity.Y < -1.5)
				{
					npc.velocity.Y = npc.velocity.Y + 0.05f;
				}
				else if (npc.velocity.Y < 0f)
				{
					npc.velocity.Y = npc.velocity.Y - 0.03f;
				}
				if ((double)npc.velocity.Y > 1.5)
				{
					npc.velocity.Y = 1.5f;
				}
			}

			if (npc.wet)
			{
				if (npc.velocity.Y > 0f)
				{
					npc.velocity.Y = npc.velocity.Y * 0.95f;
				}
				npc.velocity.Y = npc.velocity.Y - 0.5f;
				if (npc.velocity.Y < -4f)
				{
					npc.velocity.Y = -4f;
				}
				npc.TargetClosest(true);
			}

			npc.ai[1] += 1f;

			if (npc.ai[1] > 200f)
			{
				if (!Main.player[npc.target].wet && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
				{
					npc.ai[1] = 0f;
				}
				float num205 = 0.2f;
				float num206 = 0.1f;
				float num207 = 4f;
				float num208 = 1.5f;

				if (npc.ai[1] > 1000f)
				{
					npc.ai[1] = 0f;
				}
				npc.ai[2] = 1f;
				if (npc.ai[2] > 0f)
				{
					if (npc.velocity.Y < num208)
					{
						npc.velocity.Y = npc.velocity.Y + num206;
					}
				}
				else if (npc.velocity.Y > 0f - num208)
				{
					npc.velocity.Y = npc.velocity.Y - num206;
				}
				if (npc.ai[2] < -150f || npc.ai[2] > 150f)
				{
					if (npc.velocity.X < num207)
					{
						npc.velocity.X = npc.velocity.X + num205;
					}
				}
				else if (npc.velocity.X > 0f - num207)
				{
					npc.velocity.X = npc.velocity.X - num205;
				}
				if (npc.ai[2] > 300f)
				{
					npc.ai[2] = -300f;
				}
			}
		}

		#endregion movement

		#region Cannon Spawning

		private void CannonSpawning(NPC npc)
		{
			if (!CannonSpawn2)
			{
				Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), Vector2.Zero, ModContent.ProjectileType<RedCannonBehind>(), 0, 0, Main.myPlayer, npc.whoAmI);

				CannonSpawn2 = true;
			}
			if (!CannonSpawn1)
			{
				Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y), Vector2.Zero, ModContent.ProjectileType<RedCannonFront>(), 0, 0, Main.myPlayer, npc.whoAmI);

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
			npc.frameCounter++;
			if (npc.frameCounter < 10)
			{
				npc.frame.Y = Frame1 * frameHeight;
			}
			else if (npc.frameCounter < 20)
			{
				npc.frame.Y = Frame2 * frameHeight;
			}
			else if (npc.frameCounter < 30)
			{
				npc.frame.Y = Frame3 * frameHeight;
			}
			else
			{
				npc.frameCounter = 0;
			}

			if (npc.velocity.X > 0f)
			{
				npc.spriteDirection = 1;
			}
			if (npc.velocity.X < 0f)
			{
				npc.spriteDirection = -1;
			}
			npc.rotation = npc.velocity.X * 0.1f;
		}

		#endregion FindFrame

		private void Bleep(NPC npc)
		{
			if (BleepTimer <= 0)
			{
				if (Main.rand.NextFloat() < .1f)
				{
					Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Flybots/FlybotBleep").WithPitchVariance(.2f).WithVolume(.6f), npc.position);
				}

				BleepTimer = 20;
			}
		}

		public override bool CheckDead()
		{
			Vector2 gorevel;

			if (npc.velocity.Y > 0f)
			{
				gorevel = new Vector2(npc.velocity.X / 3f, (npc.velocity.Y * 8f) * -1);
			}
			else
			{
				gorevel = new Vector2(npc.velocity.X / 3f, npc.velocity.Y * 8f);
			}

			int goreIndex = Gore.NewGore(npc.position, gorevel, mod.GetGoreSlot("Gores/Flybots/RedFlybot/RedHelix"), 1f);

			for (int i = 0; i <= 20; i++)
			{
				Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Fire, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}

			int a = Projectile.NewProjectile(npc.Center, npc.velocity, ModContent.ProjectileType<BrokenRedFlybot>(), 40, 10f, Main.myPlayer, (int)(npc.spriteDirection), 0);

			return true;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.hardMode == true && spawnInfo.player.ZoneDesert)
			{
				return 0.03f;
			}

			return 0f;
		}

		#region NPCLoot

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .33f)
			{
				Item.NewItem(npc.getRect(), mod.ItemType("P2Processor"), 1);
			}

			if (Main.rand.NextFloat() < .01f)
			{
				Item.NewItem(npc.getRect(), mod.ItemType("DarkMatter"), 1);
			}

			if (Main.rand.NextFloat() < .10f)
			{
				Item.NewItem(npc.getRect(), mod.ItemType("SteelPlate"), 1);
			}
		}

		#endregion NPCLoot
	}
}