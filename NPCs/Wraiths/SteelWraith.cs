using EpicBattleFantasyUltimate.Buffs.Debuffs;
using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Wraiths
{
	public class SteelWraith : ModNPC
	{
		private int timer = 10;  //The timer that makes the first projectile be shot.
		private int timer2 = 12;  //The timer that makes the second projectile be shot. The two frames difference is there on purpose.
		private int spectimer = 60 * 5;//Defines when the sawblade will spawn.
		private int shootTimer = 60; //The timer that sets the shoot bool to false again.
		private bool shoot; //Definition of the bool that makes the NPC to move slower when it's ready to shoot

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Steel Wraith");
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Wraith);
			NPC.width = (int)(53 * 0.8f);
			NPC.height = (int)(78 * 0.8f);

			NPC.lifeMax = 400;
			NPC.damage = 40;
			NPC.defense = 5;
			NPC.lifeRegen = 4;

			NPC.aiStyle = 22;
			AIType = NPCID.Wraith;
			NPC.noTileCollide = true;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(ModContent.BuffType<RampantBleed>(), 60 * 10);
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

			#region Shooting
			Shooting(player);
			
			spectimer--;

			if (spectimer <= 0)
			{
				Sawblade(NPC);
			}
			#endregion Shooting
		}
		private void Shooting(Player player)
		{
			timer--;

			if (timer == 60) //Here the shoot bool becomes true, 60 ticks before it shoots
			{
				shoot = true;
			}

			if(player.statLife > 0)
			{
				if (timer <= 0) //If timer is 0 or less it shoots.
				{
					if (NPC.direction == 1)  //I did not find a better way to do this. This defines the positions the projectile based on its direction.
					{
						Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X + 20f, NPC.Center.Y), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<MetalShot>(), 20, 2, Main.myPlayer, 0, 1);
					}
					else if (NPC.direction == -1)
					{
						Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X - 28f, NPC.Center.Y), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<MetalShot>(), 20, 2, Main.myPlayer, 0, 1);
					}
					timer = 120; //Resetting the timer to 120 ticks (2 seconds).
				}

				timer2--; // Same logic as the first timer.

				if (timer2 <= 0)
				{
					if (NPC.direction == 1)
					{
						Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X + 11f, NPC.Center.Y + 9f), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<MetalShot>(), 20, 2, Main.myPlayer, 0, 1);
					}
					else if (NPC.direction == -1)
					{
						Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(),new Vector2(NPC.Center.X - 21f, NPC.Center.Y + 9f), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<MetalShot>(), 20, 2, Main.myPlayer, 0, 1);
					}
					timer2 = 120;
				}
			}

			#region Logic Control

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

			#endregion Logic Control

		}

		#endregion AI

		#region SpawnChance

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.hardMode && spawnInfo.Player.ZoneRockLayerHeight)
			{
				return 0.03f;
			}

			return 0f;
		}

		#endregion SpawnChance

		private void Sawblade(NPC NPC)
		{
			int NPCIndex = NPC.NewNPC(NPC.GetSpawnSource_ForProjectile(),(int)(NPC.Center.X), (int)(NPC.Center.Y), ModContent.NPCType<WraithSawblade>(), 0, 0f, 0f, 0f, 0f, 255);

			spectimer = 60 * 10;
		}

		#region NPCLoot
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Wool>(), 1, 3, 9));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SteelPlate>(), 2, 1, 3));
		}
		#endregion NPCLoot
	}
}