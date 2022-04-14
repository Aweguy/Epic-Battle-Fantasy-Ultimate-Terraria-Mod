using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Wraiths
{
	public class LeafWraith : ModNPC
	{
		#region variables

		private int timer = 10;   //The timer that makes the first projectile be shot.
		private int timer2 = 12;  //The timer that makes the second projectile be shot. The two frames difference is there on purpose.
		private int shootTimer = 60; //The timer that sets the shoot bool to false again.
		private int specialStacks = 0;//The stacks that will define when the wraith will use its special attack
		private int specialTimer = 10;//The interval between Special shots.
		private int specialEndStacks = 0;//The stacks that will define when the Wraith will stop the special attack
		private bool shoot = false; //Definition of the bool that makes the NPC to move slower when it's ready to shoot
		private bool special = false;//Definition of the bool that will make the wraith use its special attack
		private bool attack = true;//Definition of the bool that will make the wraith use its normal attack.

		public Vector2 velocity;

		#endregion variables

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leaf Wraith");
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Wraith);
			NPC.width = (int)(53 * 0.8f);
			NPC.height = (int)(78 * 0.8f);

			NPC.lifeMax = 150;
			NPC.damage = 25;
			NPC.defense = 15;

			NPC.HitSound = SoundID.NPCHit54;
			NPC.DeathSound = SoundID.NPCDeath52;

			NPC.aiStyle = 22;
			AIType = NPCID.Wraith;
			NPC.noTileCollide = true;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Poisoned, 60 * 2);
		}

		#region AI

		public override void AI()
		{
			Player player = Main.player[NPC.target]; //Target
			#region Movement Direction

			if (NPC.velocity.X > 0f) // This is the code that makes the sprite turn. Based on the vanilla one.
			{
				NPC.direction = 1;
			}
			else if (NPC.velocity.X < 0f)
			{
				NPC.direction = -1;
			}
			else if (NPC.velocity.X == 0)
			{
				NPC.direction = NPC.oldDirection;
			}

			if (NPC.direction == 1)
			{
				NPC.spriteDirection = 1;
			}
			else if (NPC.direction == -1)
			{
				NPC.spriteDirection = -1;
			}

			#endregion Movement Direction
			ShootingAndLeaves(player);


		}

		private void ShootingAndLeaves(Player player)
		{
			#region Shooting

			if (attack)
			{
				timer--;
			}

			if (timer == 60) //Here the shoot bool becomes true, 60 ticks before it shoots
			{
				shoot = true;
			}

			if (timer <= 0) //If timer is 0 or less it shoots.
			{
				if (player.statLife > 0)
				{
					if (NPC.direction == 1)  //I did not find a better way to do this. This defines the positions the projectile based on its direction.
					{
						Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X + 20f, NPC.Center.Y), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<ThornSpike>(), 20, 2, Main.myPlayer, 0, 1);
					}
					else if (NPC.direction == -1)
					{
						Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X - 28f, NPC.Center.Y), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<ThornSpike>(), 20, 2, Main.myPlayer, 0, 1);
					}
				}

				timer = 120; //Resetting the timer to 120 ticks (2 seconds).
			}

			if (attack)
			{
				timer2--; // Same logic as the first timer.
			}
			if (timer2 <= 0)
			{
				if (player.statLife > 0)
				{
					if (NPC.direction == 1)
					{
						Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X + 11f, NPC.Center.Y + 9f), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<ThornSpike>(), 20, 2, Main.myPlayer, 0, 1);
					}
					else if (NPC.direction == -1)
					{
						Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X - 21f, NPC.Center.Y + 9f), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<ThornSpike>(), 20, 2, Main.myPlayer, 0, 1);
					}
				}

				timer2 = 120;
				specialStacks++;
			}


			#endregion
			#region Special Attack

			if (special)
			{
				specialTimer--;

				if (specialTimer <= 0)
				{
					float mult = Main.rand.NextFloat(5f, 10f); //velocity randomization

					velocity = NPC.DirectionTo(new Vector2(Main.player[NPC.target].Center.X, Main.player[NPC.target].Center.Y + 18)) * mult; //Leaf velocity

					Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X, NPC.Center.Y - 18f), velocity, ModContent.ProjectileType<LeafShot>(), 20, 2, Main.myPlayer, 0, 1); //Leaf spawning/

					specialTimer = 40;
					specialEndStacks++;
				}
			}

			#endregion Special Attack

			#region Logic Control

			#region Attack Logic

			if (shoot) //If the shoot bool is true, then redcue the shoot timer otherwise do nothing.
			{
				shootTimer--;
			}

			if (shootTimer <= 0) //If it becomes 0 or less, reset the shoot bool to false.
			{
				shoot = false;

				shootTimer = 60; //Resets the timer to 60 ticks (1 second)
			}

			if (shoot) //If the shoot bool is true, its X speed is reduced by 75% of its initial. That is to generate the effects of it stopping a little before shooting.
			{
				NPC.velocity.X *= 0.9f;
			}

			#endregion Attack Logic

			#region Special Logic

			if (specialStacks == 5)
			{
				special = true;
				attack = false;
			}

			if (specialEndStacks == 2)
			{
				special = false;
				attack = true;
				specialStacks = 0;
				specialEndStacks = 0;
			}

			#endregion Special Logic

			#endregion Logic Control

		}


		#endregion AI

		public static bool PlayerIsInForest(Player player)//so we can check if the player is in the forest
		{
			return !player.ZoneJungle
				&& !player.ZoneDungeon
				&& !player.ZoneCorrupt
				&& !player.ZoneCrimson
				&& !player.ZoneHallow
				&& !player.ZoneSnow
				&& !player.ZoneDesert
				&& !player.ZoneUndergroundDesert
				&& !player.ZoneGlowshroom
				&& !player.ZoneMeteor
				&& !player.ZoneBeach
				&& player.ZoneOverworldHeight;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = Main.player[Main.myPlayer];

			if (Main.hardMode && (spawnInfo.Player.ZoneJungle || (PlayerIsInForest(player) && !Main.dayTime)) && !spawnInfo.Invasion)
			{
				return 0.03f;
			}

			return 0f;
		}

		#region NPCLoot
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Wool>(), 1, 3, 3));
			npcLoot.Add(ItemDropRule.Common(ItemID.Silk));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SilkScrap>(), 10, 1, 3));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GrapeLeaf>(), 5, 1, 4));
		}
		#endregion NPCLoot
	}
}