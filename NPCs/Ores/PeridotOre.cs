using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.OreExplosions;

namespace EpicBattleFantasyUltimate.NPCs.Ores
{
    public class PeridotOre : ModNPC
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Peridot Ore");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.width = 40;
			npc.height = 40;

			npc.lifeMax = 185;
			npc.damage = 25;
			npc.defense = 40;
			npc.lifeRegen = 4;


			npc.noTileCollide = true;
			npc.aiStyle = -1;



		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			//npc.life = 0;

			if (npc.life >= npc.lifeMax * 0.40)
			{

				if (Main.rand.NextFloat() < .1f)
				{
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<PeridotExplosion>(), 50, 5f, Main.myPlayer, 0, 1);
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
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<PeridotExplosion>(), 50, 5f, Main.myPlayer, 0, 1);
				npc.life = 0;
			}










		}















		#region AI

		public override void AI()
		{

			Direction(npc);
			Movement(npc);
		}

		#endregion


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


		private void Movement(NPC npc)
		{
			Vector2 value50 = Main.player[npc.target].Center - npc.Center;
			float num1276 = value50.Length();
			float num1277 = 2f;
			num1277 += num1276 / 200f;
			int num1278 = 50;
			value50.Normalize();
			value50 *= num1277;
			npc.velocity = (npc.velocity * (float)(num1278 - 1) + value50) / (float)num1278;



			npc.noGravity = true;
			npc.TargetClosest(true);
			Vector2 value49 = Main.player[npc.target].Center - npc.Center;
			value49.Y -= (float)(Main.player[npc.target].height / 4);
			float num1275 = value49.Length();




		}





		int Frame1 = 0;
		int Frame2 = 1;
		int Frame3 = 2;
		int Frame4 = 3;


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
			else
			{
				npc.frameCounter = 0;
			}
		}







	}
}
