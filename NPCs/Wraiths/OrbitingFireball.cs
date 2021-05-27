using EpicBattleFantasyUltimate.HelperClasses;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Wraiths
{
	internal class OrbitingFireball : ModNPC
	{
		// How many ticks it will orbit the npc.
		private int OrbitTimer;

		// The distance of the projectile from the npc that is spawned.
		private float Distance = 90;

		// The bool that makes it not follow the player after launched. Sets its velocity to the last player's position.
		private bool shoot = false;

		// Decides how many ticks each fireball will orbit the wraith.
		private bool Orbit = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Orbiting Fireball");
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.width = npc.height = 48;

			npc.damage = 40;
			npc.lifeMax = 1;

			npc.dontTakeDamage = true;
			npc.noGravity = true;

			npc.noTileCollide = true;

			Orbit = false;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 2 * 60);
		}

		public override bool PreAI()
		{
			Color drawColor = Color.Orange;
			if (Main.rand.Next(2) == 0)
			{
				drawColor = Color.Red;
			}
			Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Fire, 0f, 0f, 0, drawColor, 0.8f);

			NPC FatherNpc = Main.npc[(int)npc.ai[0]]; //Sets the npc that the projectile is spawned and will orbit

			if (!FatherNpc.active)//killing the projectile if the npc is not alive
			{
				npc.life = 0;
			}

			if (FatherNpc.life <= 0)//killing the projectile if the npc dies
			{
				npc.life = 0;
			}

			if (Orbit == false)
			{
				// Again, networking compatibility.
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					npc.netUpdate = true;
					OrbitTimer = Main.rand.Next(60 * 8, 60 * 13);
				}

				Orbit = true;
			}

			if (--OrbitTimer >= 0)
			{
				npc.DoNPC_OrbitPosition(FatherNpc.Center, Distance, MathHelper.PiOver2);
			}
			else
			{
				if (!shoot)
				{
					npc.velocity = npc.DirectionTo(Main.player[FatherNpc.target].Center) * 10f;//sets the velocity of the projectile.
					npc.netUpdate = true; // Eldrazi: Multiplayer compatibility.
					npc.dontTakeDamage = false;
					shoot = true;
				}
			}
			return (false);
		}

		public override void FindFrame(int frameHeight)
		{
			if (++npc.frameCounter >= 4)
			{
				npc.frameCounter = 0;
				npc.frame.Y = (npc.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[npc.type]);
			}
		}

		public override bool PreNPCLoot()
		{
			return false;
		}

		public override bool CheckDead()
		{

			npc.width = npc.height = 72;
			for(int i = 0; i <= 20; i++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.Fire, 0, 0, 0, default, 1);
			}

			return true;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
	}
}