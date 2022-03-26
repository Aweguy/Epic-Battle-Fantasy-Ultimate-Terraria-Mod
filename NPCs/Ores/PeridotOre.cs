using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.OreExplosions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Ores
{
	public class PeridotOre : OreNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Peridot Ore");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetSafeDefaults()
		{
			npc.width = 40;
			npc.height = 40;

			npc.lifeMax = 180;
			npc.damage = 40;
			npc.defense = 10;
			npc.lifeRegen = 4;
			npc.knockBackResist = -0.2f;

			npc.noGravity = true;

			drawOffsetY = -5;

			npc.noTileCollide = true;
			npc.aiStyle = -1;

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
			if (++npc.frameCounter >= 7)
			{
				npc.frameCounter = 0;
				npc.frame.Y = (npc.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[npc.type]);
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

			int goreIndex = Gore.NewGore(npc.position, npc.velocity * vel1, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore1"), 1f);

			int goreIndex2 = Gore.NewGore(npc.position, npc.velocity * vel2, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore2"), 1f);

			int goreIndex3 = Gore.NewGore(npc.position, npc.velocity * vel3, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore3"), 1f);

			int goreIndex4 = Gore.NewGore(npc.position, npc.velocity * vel4, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore4"), 1f);

			int goreIndex5 = Gore.NewGore(npc.position, npc.velocity * vel5, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore5"), 1f);

			int goreIndex6 = Gore.NewGore(npc.position, npc.velocity * vel6, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore6"), 1f);

			int goreIndex7 = Gore.NewGore(npc.position, npc.velocity * vel7, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore7"), 1f);

			int goreIndex8 = Gore.NewGore(npc.position, npc.velocity * vel8, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore8"), 1f);

			int goreIndex9 = Gore.NewGore(npc.position, npc.velocity * vel9, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore9"), 1f);

			for (int i = 0; i <= 15; i++)
			{
				Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}
			for (int j = 0; j <= 5; j++)
			{
				Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.GrassBlades, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}

			return true;
		}

		#endregion CheckDead

		public override void SafeNPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemID.Emerald, 1);
		}


	}
}