using EpicBattleFantasyUltimate.HelperClasses;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Wraiths
{
	internal class OrbitingFireball : ModNPC
	{
		// How many ticks it will orbit the NPC.
		private int OrbitTimer;

		// The distance of the projectile from the NPC that is spawned.
		private float Distance = 90;

		// The bool that makes it not follow the player after launched. Sets its velocity to the last player's position.
		private bool shoot = false;

		// Decides how many ticks each fireball will orbit the wraith.
		private bool Orbit = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Orbiting Fireball");
			Main.npcFrameCount[NPC.type] = 3;
		}

		public override void SetDefaults()
		{
			NPC.width = NPC.height = 48;

			NPC.damage = 40;
			NPC.lifeMax = 1;

			NPC.dontTakeDamage = true;
			NPC.noGravity = true;

			NPC.noTileCollide = true;

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
			Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Torch, 0f, 0f, 0, drawColor, 0.8f);

			NPC FatherNpc = Main.npc[(int)NPC.ai[0]]; //Sets the NPC that the projectile is spawned and will orbit

			if (!FatherNpc.active)//killing the projectile if the NPC is not alive
			{
				NPC.life = 0;
			}

			if (FatherNpc.life <= 0)//killing the projectile if the NPC dies
			{
				NPC.life = 0;
			}

			if (Orbit == false)
			{
				// Again, networking compatibility.
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					NPC.netUpdate = true;
					OrbitTimer = Main.rand.Next(60 * 8, 60 * 13);
				}

				Orbit = true;
			}

			if (--OrbitTimer >= 0)
			{
				NPC.DoNPC_OrbitPosition(FatherNpc.Center, Distance, MathHelper.PiOver2);
			}
			else
			{
				if (!shoot)
				{
					NPC.velocity = NPC.DirectionTo(Main.player[FatherNpc.target].Center) * 10f;//sets the velocity of the projectile.
					NPC.netUpdate = true; // Eldrazi: Multiplayer compatibility.
					NPC.dontTakeDamage = false;
					shoot = true;
				}
			}
			return (false);
		}

		public override void FindFrame(int frameHeight)
		{
			if (++NPC.frameCounter >= 4)
			{
				NPC.frameCounter = 0;
				NPC.frame.Y = (NPC.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[NPC.type]);
			}
		}

		public override bool CheckDead()
		{

			NPC.width = NPC.height = 72;
			for(int i = 0; i <= 20; i++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, 0, 0, 0, new Color(255, 251, 0), 1);
			}

			return true;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
	}
}