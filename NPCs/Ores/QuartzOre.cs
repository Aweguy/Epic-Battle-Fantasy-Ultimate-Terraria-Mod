using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.OreExplosions;

namespace EpicBattleFantasyUltimate.NPCs.Ores
{
	public class QuartzOre : ModNPC
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Quartz Ore");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 40;
			npc.height = 40;

			npc.lifeMax = 160;
			npc.damage = 20;
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
            #region Death Check
            if (npc.life >= npc.lifeMax * 0.40)
			{

				if (Main.rand.NextFloat() < .1f)
				{
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<QuartzExplosion>(), 30, 5f, Main.myPlayer, 0, 1);

					int goreIndex = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/QuartzOre/QuartzOre_Gore1"), 1f);
					int goreIndex2 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/QuartzOre/QuartzOre_Gore2"), 1f);
					int goreIndex3 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/QuartzOre/QuartzOre_Gore3"), 1f);
					int goreIndex4 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/QuartzOre/QuartzOre_Gore4"), 1f);

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
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<QuartzExplosion>(), 30, 5f, Main.myPlayer, 0, 1);

				int goreIndex = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/QuartzOre/QuartzOre_Gore1"), 1f);
				int goreIndex2 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/QuartzOre/QuartzOre_Gore2"), 1f);
				int goreIndex3 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/QuartzOre/QuartzOre_Gore3"), 1f);
				int goreIndex4 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/QuartzOre/QuartzOre_Gore4"), 1f);

				npc.life = 0;
			}
			#endregion
			for (int i = 0; i <= 5; i++)
			{
				Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
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
				Dust.NewDustDirect(npc.Center, npc.width, npc.height, 11, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
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
			float MoveSpeedMult = 4.5f; //How fast it moves and turns. A multiplier maybe?
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
		public override void FindFrame(int frameHeight)
		{
			if (++npc.frameCounter >= 7)
			{
				npc.frameCounter = 0;
				npc.frame.Y = (npc.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[npc.type]);
			}
		}
		#endregion

		#region CheckDead

		public override bool CheckDead()
		{

			int goreIndex = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/QuartzOre/QuartzOre_Gore1"), 1f);
			int goreIndex2 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/QuartzOre/QuartzOre_Gore2"), 1f);
			int goreIndex3 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/QuartzOre/QuartzOre_Gore3"), 1f);
			int goreIndex4 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/QuartzOre/QuartzOre_Gore4"), 1f);

			for (int i = 0; i <= 15; i++)
			{
				Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}
			for (int j = 0; j <= 5; j++)
			{
				Dust.NewDustDirect(npc.Center, npc.width, npc.height, 11, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}



			return true;
		}

		#endregion

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{


			if (EpicWorld.OreEvent)
			{
				return 35f;
			}
			else if (Main.hardMode == true && spawnInfo.player.ZoneRockLayerHeight)
			{
				return 0.02f;
			}
			else
			{
				return 0f;
			}

		}


		#region NPCLoot

		public override void NPCLoot()
		{

			EpicWorld.OreKills += 1;
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.

			}


			Item.NewItem(npc.getRect(), ItemID.Diamond, 2);


		}

		#endregion






	}
}
