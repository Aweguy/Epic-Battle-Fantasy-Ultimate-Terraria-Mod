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

namespace EpicBattleFantasyUltimate.NPCs.Monoliths
{
	class TestMonolith : ModNPC
	{

		private bool spawned = false;

		private int animation = 0;
		private float animationTimer = 0.0f;

		private float attack = 0.0f;

		private bool attacking;
		private float attackingTimer = 0f;
		private int state = 1;
		private float timer = 0.0f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Test Monolith");
		}

		public override void SetDefaults()
		{
			npc.height = 100;
			npc.width = 30;

			npc.lifeMax = 100;
			npc.defense = 1000;

			npc.knockBackResist = 1;
			npc.aiStyle = -1;
		}

		public override bool PreAI()
		{

			Teleportation();

			return false;
		}


		private void Teleportation()
		{
			npc.TargetClosest(true);
			spawned = true;
			Player player = Main.player[npc.target];
			npc.netUpdate = true;
			if ((double)player.position.X > (double)npc.position.X) npc.spriteDirection = 1;
			else if ((double)player.position.X < (double)npc.position.X) npc.spriteDirection = -1;
			npc.TargetClosest(true);
			npc.velocity.X = npc.velocity.X * 0.93f;
			npc.velocity.X = 0.0f;
			npc.velocity.Y = 5.0f;

			if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1) npc.velocity.X = 0.0f;
			if (spawned && (double)npc.ai[0] == 0.0) npc.ai[0] = 500f;
			if ((double)npc.ai[2] != 0.0 && (double)npc.ai[3] != 0.0)
			{

				npc.position.X = (float)((double)npc.ai[2] * 16.0 - (double)(npc.width / 2) + 8.0);
				npc.position.Y = npc.ai[3] * 16f - (float)npc.height;
				npc.velocity.X = 0.0f;
				npc.velocity.Y = 0.0f;
				npc.ai[2] = 0.0f;
				npc.ai[3] = 0.0f;
			}


			++npc.ai[0];
			int spawn = Main.rand.Next(350, 350);
			if ((int)npc.ai[0] >= spawn)
			{
				npc.ai[0] = 1.0f;
				int pX = (int)player.position.X / 16;
				int pY = (int)player.position.Y / 16;
				int x = (int)npc.position.X / 16;
				int y = (int)npc.position.Y / 16;
				int rand = 50;
				int distance = 0;
				bool checkDistance = false;
				if ((double)Math.Abs(npc.position.X - player.position.X) + (double)Math.Abs(npc.position.Y - player.position.Y) > 2000)
				{
					distance = 500;
					checkDistance = true;
				}
				while (!checkDistance && distance < 500)
				{
					++distance;
					int k = Main.rand.Next(pX - rand, pX + rand);
					for (int j = Main.rand.Next(pY - rand, pY + rand); j < pY + rand; ++j)
					{
						if ((j < pY - 4 || j > pY + 4 || (k < pX - 4 || k > pX + 4)) && (j < y - 1 || j > y + 1 || (k < x - 1 || k > x + 1)) && Main.tile[k, j].nactive())
						{
							bool flag2 = true;
							if (Main.tile[k, j - 1].lava())
								flag2 = false;
							if (flag2 && Main.tileSolid[(int)Main.tile[k, j].type] && !Collision.SolidTiles(k - 1, k + 1, j - 4, j - 1))
							{
								state = 1;
								attack = 55.0f;
								npc.ai[2] = (float)k;
								npc.ai[3] = (float)j;
								checkDistance = true;
								spawned = false;
								break;
							}
						}
					}
				}
				npc.netUpdate = true;
			}



		}





	}  
}
