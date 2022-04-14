using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Wraiths
{
	public class SparkWraith : ModNPC
	{
		private int timer = 10;   //The timer that makes the first projectile be shot.
		private int timer2 = 12;  //The timer that makes the second projectile be shot. The two frames difference is there on purpose.
		private int timer3 = 120; //The timer that defines when the Sparkle will be shot.
		private int shootTimer = 60; //The timer that sets the shoot bool to false again.
		private bool shoot = false; //Definition of the bool that makes the NPC to move slower when it's ready to shoot

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spark Wraith");
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Wraith);
			NPC.width = (int)(53 * 0.8f);
			NPC.height = (int)(78 * 0.8f);

			NPC.lifeMax = 195;
			NPC.damage = 20;
			NPC.defense = 50;
			NPC.lifeRegen = 4;

			NPC.aiStyle = 22;
			AIType = NPCID.Wraith;
			NPC.noTileCollide = true;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 45 * 2);
		}

		#region AI

		public override void AI()
		{
			Player player = Main.player[NPC.target]; //Target
			Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Firefly, 0.2631578f, -2.368421f, 0, Color.Orange, 0.6f);

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

			#region Shooting


			#endregion Shooting

		}

		private void Shooting(NPC NPC, Player player)
		{
			timer--;

			if (timer == 60) //Here the shoot bool becomes true, 60 ticks before it shoots
			{
				shoot = true;
			}

			if (player.statLife > 0)
			{
				if (timer <= 0) //If timer is 0 or less it shoots.
				{

					if (NPC.direction == 1)  //I did not find a better way to do this. This defines the positions the projectile based on its direction.
					{
						Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X + 20f, NPC.Center.Y), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<BoneShot>(), 30, 2, Main.myPlayer, 0, 1);
					}
					else if (NPC.direction == -1)
					{
						Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X - 28f, NPC.Center.Y), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<BoneShot>(), 30, 2, Main.myPlayer, 0, 1);
					}

					timer = 120; //Resetting the timer to 120 ticks (2 seconds).
				}

				timer2--; // Same logic as the first timer.

				if (timer2 <= 0)
				{

					if (NPC.direction == 1)
					{
						Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X + 11f, NPC.Center.Y + 9f), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<BoneShot>(), 30, 2, Main.myPlayer, 0, 1);
					}
					else if (NPC.direction == -1)
					{
						Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X - 21f, NPC.Center.Y + 9f), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<BoneShot>(), 30, 2, Main.myPlayer, 0, 1);
					}

					timer2 = 120;
				}
			}

			if (shoot == true) //If the shoot bool is true, then redcue the shoot timer otherwise do nothing.
			{
				shootTimer--;
			}

			if (shootTimer <= 0) //If it becomes 0 or less, reset the shoot bool to false.
			{
				shoot = false;

				shootTimer = 60; //Resets the timer to 60 ticks (1 second)
			}

			if (shoot == true) //If the shoot bool is true, its X speed is reduced by 75% of its initial. That is to generate the effects of it stopping a little before shooting.
			{
				NPC.velocity.X *= 0.9f;
			}

			#region little fireball
			timer3--;

			if (timer3 <= 0)
			{
				if (player.statLife > 0)
				{
					Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X, NPC.Center.Y - 11), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<Sparkle>(), 18, 2, Main.myPlayer, 0, 1);
				}

				timer3 = 360;
			}
			#endregion
		}

		#endregion AI

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.hardMode && spawnInfo.Player.ZoneDesert)
			{
				return 0.03f;
			}

			return 0f;
		}

		#region NPCLoot
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Wool>()));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SilkScrap>(), 10, 1, 3));
		}
		#endregion NPCLoot
	}
}