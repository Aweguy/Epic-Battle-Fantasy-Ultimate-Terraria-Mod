using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Projectiles;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.OreExplosions;
using EpicBattleFantasyUltimate.HelperClasses;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace EpicBattleFantasyUltimate.NPCs.Ores
{
	public class SapphireOre : OreNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapphire Ore");
			Main.npcFrameCount[NPC.type] = 6;
		}

		//bool Channeling = false;

		public override void SetSafeDefaults()
		{
			NPC.width = 40;
			NPC.height = 40;

			NPC.lifeMax = 95;
			NPC.damage = 30;
			NPC.defense = 6;
			NPC.lifeRegen = 4;
			NPC.knockBackResist = -0.2f;

			NPC.noGravity = true;

			DrawOffsetY = -5;

			NPC.noTileCollide = true;
			NPC.aiStyle = -1;

			Explosion = ModContent.ProjectileType<SapphireExplosion>();

			MoveSpeedMultval = 6f;
			MoveSpeedBalval = 75;
			SpeedBalance = 100f;

			DashCooldown = 5;

			DashDistance = 20f;
			DashCharge = 120;
			DashVelocity = 12f;
			DashDuration = 180;//Should be higher than the Dash Charge

			StunDuration = 3;
		}
        #region Unused
        /*public override void AI()
		{

			if (!Channeling)
			{
				Player player = Main.player[NPC.target];
				Projectile.NewProjectile(NPC.Center, NPC.velocity, ModContent.ProjectileType<TestLaser2>(), 100, 1, Main.myPlayer, player.whoAmI, NPC.whoAmI);
				Channeling = true;
			}
			//ExpertLaser();
		}*/


        /*private void ExpertLaser()
		{
			laserTimer--;
			if (laserTimer <= 0 && Main.netMode != NetmodeID.MultiplayerClient)
			{
				if (NPC.localAI[0] == 2f)
				{
					Player target = Main.player[NPC.target];

					Vector2 pos = NPC.Center;
					int damage = NPC.damage / 2;
					if (Main.expertMode)
					{
						damage = (int)(damage / Main.expertDamage);
					}
					Projectile.NewProjectile(pos.X, pos.Y, 0f, 0f, ModContent.ProjectileType<TestLaser>(), damage, 0f, Main.myPlayer, target.whoAmI, NPC.whoAmI);
				}
				else
				{
					NPC.localAI[0] = 2f;
				}

				laserTimer = 500 + Main.rand.Next(100);
				laserTimer = 60 + laserTimer * NPC.life / NPC.lifeMax;
				laser1 = Main.rand.Next(6) - 1;
				laser2 = Main.rand.Next(5) - 1;
				if (laser2 >= laser1)
				{
					laser2++;
				}
			}
		}*/
        #endregion
        #region FindFrame

        public override void FindFrame(int frameHeight)
		{
			if (++NPC.frameCounter >= 7)
			{
				NPC.frameCounter = 0;
				NPC.frame.Y = (NPC.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[NPC.type]);
			}
		}

		#endregion FindFrame

		public override bool CheckDead()
		{
			int goreIndex = Gore.NewGore(NPC.GetSource_Death(),NPC.position, (NPC.velocity * NPC.direction) * -1, Mod.Find<ModGore>("SapphireOre_Gore1").Type, 1f);
			int goreIndex2 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction) * -1, Mod.Find<ModGore>("SapphireOre_Gore2").Type, 1f);
			int goreIndex3 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction), Mod.Find<ModGore>("SapphireOre_Gore3").Type, 1f);
			int goreIndex4 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction), Mod.Find<ModGore>("SapphireOre_Gore4").Type, 1f);
			int goreIndex5 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction) * -1, Mod.Find<ModGore>("SapphireOre_Gore5").Type, 1f);
			int goreIndex6 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction) * -1, Mod.Find<ModGore>("SapphireOre_Gore6").Type, 1f);

			for (int i = 0; i <= 15; i++)
			{
				Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}
			for (int j = 0; j <= 5; j++)
			{
				Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.BlueCrystalShard, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}

			return true;
		}

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ItemID.Sapphire));
		}
	}
}