﻿using EpicBattleFantasyUltimate.Projectiles.NPCProj.OreExplosions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Ores
{
    public class SapphireOre : ModNPC
    {

		Vector2 center;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapphire Ore");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 40;
			npc.height = 40;

			npc.lifeMax = 120;
			npc.damage = 35;
			npc.defense = 10;
			npc.lifeRegen = 4;
			npc.knockBackResist = -0.2f;


			npc.noTileCollide = true;
			npc.aiStyle = -1;



		}

		#region OnHitPlayer

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			//npc.life = 0;

			if (npc.life >= npc.lifeMax * 0.40)
			{

				if (Main.rand.NextFloat() < .1f)
				{
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<SapphireExplosion>(), 35, 5f, Main.myPlayer, 0, 1);


					int goreIndex = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore1"), 1f);
					int goreIndex2 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore2"), 1f);
					int goreIndex3 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore3"), 1f);
					int goreIndex4 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore4"), 1f);
					int goreIndex5 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore5"), 1f);
					int goreIndex6 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore6"), 1f);

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

					if (leftRightCollision)
					{
						npc.velocity.X *= -2;
					}

					else
					{
						npc.velocity.Y *= -2;
					}

				}
			}
			else
			{
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<SapphireExplosion>(), 35, 5f, Main.myPlayer, 0, 1);


				int goreIndex = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore1"), 1f);
				int goreIndex2 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore2"), 1f);
				int goreIndex3 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore3"), 1f);
				int goreIndex4 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore4"), 1f);
				int goreIndex5 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore5"), 1f);
				int goreIndex6 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore6"), 1f);

				npc.life = 0;
			}









		}

		#endregion




		public override void HitEffect(int hitDirection, double damage)
		{
			for (int i = 0; i <= 3; i++)
			{
				Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}
			for (int j = 0; j <= 2; j++)
			{
				Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.BlueCrystalShard, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}

		}









		#region AI

		public override void AI()
		{

			Direction(npc);
			Movement(npc);


		}

		#endregion

		#region Direction

		private void Direction(NPC npc)
		{
			if (npc.velocity.X > 0f) // This is the code that makes the sprite turn. Based on the vanilla one.
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
			}
			else if (npc.direction == -1)
			{
				npc.spriteDirection = -1;
			}

		}

		#endregion

		#region Movement

		private void Movement(NPC npc)
		{
			Vector2 target = Main.player[npc.target].Center - npc.Center;
			float num1276 = target.Length(); //This seems totally useless, not used anywhere.
			float MoveSpeedMult = 6f; //How fast it moves and turns. A multiplier maybe?
			MoveSpeedMult += num1276 / 200f; //Balancing the speed. Lowering the division value makes it have more sharp turns.
			int MoveSpeedBal = 100; //This does the same as the above.... I do not understand.
			target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
			target *= MoveSpeedMult;
			npc.velocity = (npc.velocity * (float)(MoveSpeedBal - 1) + target) / (float)MoveSpeedBal;



			npc.noGravity = true;
			npc.TargetClosest(true);




		}

		#endregion

		#region FindFrame

		int Frame1 = 0;
		int Frame2 = 1;
		int Frame3 = 2;
		int Frame4 = 3;
		int Frame5 = 4;
		int Frame6 = 5;


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
			else if (npc.frameCounter < 40)
			{
				npc.frame.Y = Frame4 * frameHeight;

			}
			else if (npc.frameCounter < 50)
			{
				npc.frame.Y = Frame5 * frameHeight;

			}
			else if (npc.frameCounter < 60)
			{
				npc.frame.Y = Frame6 * frameHeight;
			}
			else
			{
				npc.frameCounter = 0;
			}
		}

		#endregion


		public override bool CheckDead()
		{

			int goreIndex = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore1"), 1f);
			int goreIndex2 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore2"), 1f);
			int goreIndex3 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore3"), 1f);
			int goreIndex4 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore4"), 1f);
			int goreIndex5 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore5"), 1f);
			int goreIndex6 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore6"), 1f);

			for (int i = 0; i <= 15; i++)
			{
				Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}
			for (int j = 0; j <= 5; j++)
			{
				Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.BlueCrystalShard, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}



			return true;
		}


		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{


			if (EpicWorld.OreEvent)
			{
				return 35f;
			}
			else if (Main.hardMode == true && spawnInfo.player.ZoneBeach)
			{
				return 0.02f;
			}
			else
			{
				return 0f;
			}

		}

		public override void NPCLoot()
		{

			EpicWorld.OreKills += 1;
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.

			}



			Item.NewItem(npc.getRect(), ItemID.Sapphire);


		}














	}
}