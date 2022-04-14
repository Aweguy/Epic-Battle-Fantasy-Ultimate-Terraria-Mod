using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.OreExplosions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Ores
{
	public class PeridotOre : OreNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Peridot Ore");
			Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetSafeDefaults()
		{
			NPC.width = 40;
			NPC.height = 40;

			NPC.lifeMax = 180;
			NPC.damage = 40;
			NPC.defense = 10;
			NPC.lifeRegen = 4;
			NPC.knockBackResist = -0.2f;

			NPC.noGravity = true;

			DrawOffsetY = -5;

			NPC.noTileCollide = true;
			NPC.aiStyle = -1;

			Explosion = ModContent.ProjectileType<PeridotExplosion>();

			MoveSpeedMultval = 4f;
			MoveSpeedBalval = 100;
			SpeedBalance = 100f;

			DashCooldown = 5;

			DashDistance = 20f;
			DashCharge = 120;
			DashVelocity = 10f;
			DashDuration = 170;//Should be higher than the Dash Charge

			StunDuration = 3;
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

		#region CheckDead

		public override bool CheckDead()
		{
			Vector2 vel1 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));
			Vector2 vel2 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));
			Vector2 vel3 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));
			Vector2 vel4 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));
			Vector2 vel5 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));
			Vector2 vel6 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));
			Vector2 vel7 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));
			Vector2 vel8 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));
			Vector2 vel9 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

			int goreIndex = Gore.NewGore(NPC.position, NPC.velocity * vel1, Mod.Find<ModGore>("PeridotOre_Gore1").Type, 1f);
			int goreIndex2 = Gore.NewGore(NPC.position, NPC.velocity * vel2, Mod.Find<ModGore>("PeridotOre_Gore2").Type, 1f);
			int goreIndex3 = Gore.NewGore(NPC.position, NPC.velocity * vel3, Mod.Find<ModGore>("PeridotOre_Gore3").Type, 1f);
			int goreIndex4 = Gore.NewGore(NPC.position, NPC.velocity * vel4, Mod.Find<ModGore>("PeridotOre_Gore4").Type, 1f);
			int goreIndex5 = Gore.NewGore(NPC.position, NPC.velocity * vel5, Mod.Find<ModGore>("PeridotOre_Gore5").Type, 1f);
			int goreIndex6 = Gore.NewGore(NPC.position, NPC.velocity * vel6, Mod.Find<ModGore>("PeridotOre_Gore6").Type, 1f);
			int goreIndex7 = Gore.NewGore(NPC.position, NPC.velocity * vel7, Mod.Find<ModGore>("PeridotOre_Gore7").Type, 1f);
			int goreIndex8 = Gore.NewGore(NPC.position, NPC.velocity * vel8, Mod.Find<ModGore>("PeridotOre_Gore8").Type, 1f);
			int goreIndex9 = Gore.NewGore(NPC.position, NPC.velocity * vel9, Mod.Find<ModGore>("PeridotOre_Gore9").Type, 1f);

			for (int i = 0; i <= 15; i++)
			{
				Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}
			for (int j = 0; j <= 5; j++)
			{
				Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.GrassBlades, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}

			return true;
		}

		#endregion CheckDead

		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			npcLoot.Add(ItemDropRule.Common(ItemID.Emerald));
		}
	}
}