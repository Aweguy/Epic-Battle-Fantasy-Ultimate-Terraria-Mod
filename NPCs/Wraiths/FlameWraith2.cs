using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Wraiths
{
	public class FlameWraith2 : ModNPC
	{
		private int timer = 10;   //The timer that makes the first projectile be shot.
		private int timer2 = 12;  //The timer that makes the second projectile be shot. The two frames difference is there on purpose.
		private int spectimer = 60 * 5;//Defines when the fireballs will start spawning
		private int spintimer; //A timer that sets wthe interval between the orbiting fireballs.
		private int shootTimer = 60; //The timer that sets the shoot bool to false again.
		private bool shoot = false; //Definition of the bool that makes the NPC to move slower when it's ready to shoot
		private int currentFireballs = 0;
		private readonly int maxFireballs = 11;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sacred Flame Wraith");
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Wraith);
			NPC.width = (int)(53 * 0.8f);
			NPC.height = (int)(78 * 0.8f);

			NPC.lifeMax = 260;
			NPC.damage = 50;
			NPC.defense = 45;
			NPC.lifeRegen = 4;

			NPC.aiStyle = 22;
			AIType = NPCID.Wraith;
			NPC.noTileCollide = true;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 60 * 10);
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

			Shooting(player);

			if(--spectimer <= 0)
            {
				Fireballs();
			}

		}

		private void Shooting (Player player)
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
					Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, Vector2.Zero, ModContent.ProjectileType<SpinFireball>(), 20, 2, Main.myPlayer, NPC.whoAmI);
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

		#endregion AI

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.hardMode && spawnInfo.Player.ZoneUnderworldHeight && NPC.downedPlantBoss)
			{
				return 0.02f;
			}

			return 0f;
		}

		#region NPCLoot
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Wool>()));
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SilkScrap>(),5,2,4));
		}
		#endregion NPCLoot
	}
}