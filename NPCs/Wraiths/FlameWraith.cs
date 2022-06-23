using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Wraiths
{
	public class FlameWraith : ModNPC
	{
		private int timer = 10;   //The timer that makes the first projectile be shot.
		private int timer2 = 12;  //The timer that makes the second projectile be shot. The two frames difference is there on purpose.
		private int spectimer = 60 * 5;//Defines when the fireballs will start spawning
		private int spintimer = 25; //A timer that sets wthe interval between the orbiting fireballs.
		private int shootTimer = 60; //The timer that sets the shoot bool to false again.
		private bool shoot = false; //Definition of the bool that makes the NPC to move slower when it's ready to shoot
		private int currentFireballs = 0;
		private readonly int maxFireballs = 3;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flame Wraith");
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Wraith);
			NPC.width = (int)(53 * 0.8f);
			NPC.height = (int)(78 * 0.8f);

			NPC.lifeMax = 185;
			NPC.damage = 25;
			NPC.defense = 40;
			NPC.lifeRegen = 4;

			NPC.aiStyle = 22;
			AIType = NPCID.Wraith;
			NPC.noTileCollide = true;
		}
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheUnderworld,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Lesser ghosts that prowl scorched woods. Fond of the scent of chestnuts.")
			});
		}
		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 60 * 2);
		}

		#region AI

		public override void AI()
		{
			Player player = Main.player[NPC.target]; //Target
			Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Torch, 0.2631578f, -2.368421f, 0, Color.Orange, 0.6f);

			#region Movement Direction

			if (NPC.direction == 1)
			{
				NPC.spriteDirection = 1;
			}
			else if (NPC.direction == -1)
			{
				NPC.spriteDirection = -1;
			}

			#endregion Movement Direction

			Shooting(player);//Shooting the bones
			if (--spectimer <= 0)//Fireball summoning
			{
				Fireballs();
			}
		}

		private void Fireballs()
		{
			Player player = Main.player[NPC.target]; //Target

			// Eldrazi: I've done some explicit variable statements, so you know what each of these variables is supposed to do.
			// You could shrink this code down, but I'd only do that if you're comfortable with understanding it.

			float fullRotationInFrames = 240;

			if (++spintimer >= fullRotationInFrames / maxFireballs)
			{
				// Do not attempt to spawn the projectile on clients. Only in singleplayer and server instances.
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					int NPCIndex = NPC.NewNPC(NPC.GetSource_FromAI(), (int)(NPC.Center.X), (int)(NPC.Center.Y), ModContent.NPCType<OrbitingFireball>(), 0, NPC.whoAmI, 0f, 0f, 0f, 255);//Spawnign the Wraith
				}

				spintimer = 0;
				currentFireballs++;
			}

			if (currentFireballs >= maxFireballs)
			{
				currentFireballs = 0;
				spectimer = 60 * 25; //Higher than the base value for balance purposes
			}
		}

		private void Shooting(Player player)
		{
			#region Shooting

			timer--;

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
						Projectile.NewProjectile(NPC.GetSource_FromAI(),new Vector2(NPC.Center.X + 22f, NPC.Center.Y), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<BoneShot>(), 40, 2, Main.myPlayer, 0, 1);
					}
					else if (NPC.direction == -1)
					{
						Projectile.NewProjectile(NPC.GetSource_FromAI(),new Vector2(NPC.Center.X - 30f, NPC.Center.Y), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<BoneShot>(), 40, 2, Main.myPlayer, 0, 1);
					}
				}

				timer = 120; //Resetting the timer to 120 ticks (2 seconds).
			}

			timer2--; // Same logic as the first timer.

			if (timer2 <= 0)
			{
				if (player.statLife > 0)
				{
					if (NPC.direction == 1)
					{
						Projectile.NewProjectile(NPC.GetSource_FromAI(),new Vector2(NPC.Center.X + 11f, NPC.Center.Y + 9f), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<BoneShot>(), 40, 2, Main.myPlayer, 0, 1);
					}
					else if (NPC.direction == -1)
					{
						Projectile.NewProjectile(NPC.GetSource_FromAI(),new Vector2(NPC.Center.X - 21f, NPC.Center.Y + 9f), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<BoneShot>(), 40, 2, Main.myPlayer, 0, 1);
					}
				}

				timer2 = 120;
			}

			#endregion Shooting

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

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.hardMode && spawnInfo.Player.ZoneUnderworldHeight)
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