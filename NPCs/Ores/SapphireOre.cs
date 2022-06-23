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
using Terraria.GameContent.Bestiary;

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
		public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
		{
			bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
			{
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Some Ores are a clean gray and found by oceans and mountains. They fall under Poseidon’s domain.")
			});
		}

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