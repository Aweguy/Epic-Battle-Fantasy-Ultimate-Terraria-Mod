using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Security.Cryptography.X509Certificates;

namespace EpicBattleFantasyUltimate.NPCs.Flybot
{
	public class RedFlybot : ModNPC
	{

		bool Cannon1 = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Red Flybot");
		}

		public override void SetDefaults()
		{
			npc.width = 40;
			npc.height = 40;

			npc.lifeMax = 125;
			npc.damage = 22;
			npc.defense = 35;
			npc.lifeRegen = 4;
			npc.knockBackResist = -0.2f;

			npc.noGravity = true;


			npc.noTileCollide = false;
			npc.aiStyle = -1;



		}


		public override void AI()
		{

			npc.TargetClosest(true);

            if (!Cannon1)
            {
				Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<RedCannon>(), 0, 0, Main.myPlayer, npc.whoAmI, npc.target);

				Cannon1 = true;
			}



			movement(npc);
		}


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
















		public override void FindFrame(int frameHeight)
        {
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














	}
}
