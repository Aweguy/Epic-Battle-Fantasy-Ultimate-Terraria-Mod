﻿using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.OreExplosions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Ores
{
	public class SapphireOre : OreNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapphire Ore");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetSafeDefaults()
		{
			npc.width = 40;
			npc.height = 40;

			npc.lifeMax = 95;
			npc.damage = 30;
			npc.defense = 6;
			npc.lifeRegen = 4;
			npc.knockBackResist = -0.2f;

			npc.noGravity = true;

			drawOffsetY = -5;

			npc.noTileCollide = true;
			npc.aiStyle = -1;

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

		public override bool CheckDead()
		{
			int goreIndex = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore1"), 1f);
			int goreIndex2 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore2"), 1f);
			int goreIndex3 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore3"), 1f);
			int goreIndex4 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore4"), 1f);
			int goreIndex5 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore5"), 1f);
			int goreIndex6 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/SapphireOre/SapphireOre_Gore6"), 1f);

			for (int i = 0; i <= 15; i++)
			{
				Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}
			for (int j = 0; j <= 5; j++)
			{
				Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.BlueCrystalShard, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
			}

			return true;
		}

		public override void SafeNPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemID.Sapphire, 1);
		}


	}
}