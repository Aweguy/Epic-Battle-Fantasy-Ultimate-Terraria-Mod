using EpicBattleFantasyUltimate.Items.Ammo.Shots;
using EpicBattleFantasyUltimate.Items.Consumables;
using EpicBattleFantasyUltimate.Items.Swords;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace EpicBattleFantasyUltimate.NPCs.TownNPCs
{
	[AutoloadHead]
	public class Matt : ModNPC
	{
		public override string Texture => "EpicBattleFantasyUltimate/NPCs/TownNPCs/Matt";

		public override void SetStaticDefaults()
		{
			// DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
			//DisplayName.SetDefault("Matt");
			Main.npcFrameCount[NPC.type] = 25;
			NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
			NPCID.Sets.AttackFrameCount[NPC.type] = 4;
			NPCID.Sets.DangerDetectRange[NPC.type] = 16 * 5;
			NPCID.Sets.AttackType[NPC.type] = 3;
			NPCID.Sets.AttackTime[NPC.type] = 30;
			NPCID.Sets.AttackAverageChance[NPC.type] = 30;
			NPCID.Sets.HatOffsetY[NPC.type] = 4;
			NPCID.Sets.ActsLikeTownNPC[Type] = true;
			NPCID.Sets.SpawnsWithCustomName[Type] = true;
		}

		public override void SetDefaults()
		{
			NPC.townNPC = true;
			NPC.friendly = true;
			NPC.width = 30;
			NPC.height = 50;
			NPC.aiStyle = 7;
			NPC.damage = 30;
			NPC.defense = 25;
			NPC.lifeMax = 750;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath1;
			NPC.knockBackResist = 0.9f;
			AnimationType = NPCID.Guide;
		}

		public override string TownNPCName()
		{
			return "Matt";
		}

		public override string GetChat()
		{
			switch (Main.rand.Next(9))
			{
				case 0:
					return "Have you ever tried Sandworm? It goes great with these Bad Mushrooms I found...why are you looking at me like that?";

				case 1:
					return "This is a sword!";

				case 2:
					return "I’m not a pirate, I am a Canadian of Piratical Descent.";

				case 3:
					return "Have you ever been to Hope Harbor? It’s a nice place, especially at sunset.";

				case 4:
					return "Jeff? Who’s Jeff?";

				case 5:
					return "You can’t be the main character on an empty stomach! Do you want anything?";

				case 6:
					return "Lance is currently out on a ''recon mission'', which means he's not prepared to make social contact. So I'm marketing his technologic thingamajigs instead of him for the time being.";

				case 7:
					return "It's not easy for me to find the code either!";

				case 8:
					return "I got some gifts for you, from Anna. But since she's not here to give them herself you must pay me. What? I gotta earn a living somehow";

				default:
					return "It’s important to know what you want in life. Like food, shiny things, and fuzzy blue cats to pet.";
			}
		}

		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Shot>());
			shop.item[nextSlot].shopCustomPrice = 10000;
			nextSlot++;

			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Shot2>());
			shop.item[nextSlot].shopCustomPrice = 10000;
			nextSlot++;

			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Shot3>());
			shop.item[nextSlot].shopCustomPrice = 10000;
			nextSlot++;

			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Shot4>());
			shop.item[nextSlot].shopCustomPrice = 2500;
			nextSlot++;

			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Shot5>());
			shop.item[nextSlot].shopCustomPrice = 2500;
			nextSlot++;

			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Shot6>());
			shop.item[nextSlot].shopCustomPrice = 2500;
			nextSlot++;

			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Shot7>());
			shop.item[nextSlot].shopCustomPrice = 5000;
			nextSlot++;

			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Shot8>());
			shop.item[nextSlot].shopCustomPrice = 5000;
			nextSlot++;

			shop.item[nextSlot].SetDefaults(ModContent.ItemType<Shot9>());
			shop.item[nextSlot].shopCustomPrice = 5000;
			nextSlot++;

			if (Main.hardMode)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<ChilliPepper>());
				shop.item[nextSlot].shopCustomPrice = 1500000;
				nextSlot++;

				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Avenger>());
				shop.item[nextSlot].shopCustomPrice = 550000;
				nextSlot++;

				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Berzerker>());
				shop.item[nextSlot].shopCustomPrice = 500000;
				nextSlot++;

				shop.item[nextSlot].SetDefaults(ModContent.ItemType<BlackFang>());
				shop.item[nextSlot].shopCustomPrice = 200000;
				nextSlot++;

				shop.item[nextSlot].SetDefaults(ModContent.ItemType<BoneBlade>());
				shop.item[nextSlot].shopCustomPrice = 200000;
				nextSlot++;

				shop.item[nextSlot].SetDefaults(ModContent.ItemType<DragonsFeather>());
				shop.item[nextSlot].shopCustomPrice = 220000;
				nextSlot++;

				shop.item[nextSlot].SetDefaults(ModContent.ItemType<FusionBlade>());
				shop.item[nextSlot].shopCustomPrice = 100000;
				nextSlot++;

				shop.item[nextSlot].SetDefaults(ModContent.ItemType<GaiaAxe>());
				shop.item[nextSlot].shopCustomPrice = 200000;
				nextSlot++;

				shop.item[nextSlot].SetDefaults(ModContent.ItemType<HeavensGate>());
				shop.item[nextSlot].shopCustomPrice = 500000;
				nextSlot++;

				shop.item[nextSlot].SetDefaults(ModContent.ItemType<Inferno>());
				shop.item[nextSlot].shopCustomPrice = 100000;
				nextSlot++;

				shop.item[nextSlot].SetDefaults(ModContent.ItemType<LightningShard>());
				shop.item[nextSlot].shopCustomPrice = 450000;
				nextSlot++;

				/*shop.item[nextSlot].SetDefaults(ModContent.ItemType<SapphireSaint>());
				shop.item[nextSlot].shopCustomPrice = 100000;
				nextSlot++;*/

				shop.item[nextSlot].SetDefaults(ModContent.ItemType<SoulEater>());
				shop.item[nextSlot].shopCustomPrice = 500000;
				nextSlot++;

			}
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			if (NPC.downedBoss1)//If EoC is killed
			{
				return true;
			}

			return false;
		}

		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = Language.GetTextValue("LegacyInterface.28");
			//button2 = "Time for Money";
		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if (firstButton)
			{
				shop = true;
			}
			/*else
			{
				List<int> options = new List<int> { mod.ItemType("PristineDiamond"), mod.ItemType("CyclonicEmerald"), mod.ItemType("AncientAmber"), mod.ItemType("AbyssalSapphire"), mod.ItemType("VolcanicRuby"), mod.ItemType("VoltaicTopaz") };
				int index = Main.LocalPlayer.FindItem(options);

				if (index != -1)
				{
					Main.LocalPlayer.inventory[index].stack -= 1;
					Main.LocalPlayer.QuickSpawnItem(ItemID.SilverCoin, Main.rand.Next(15, 30));
					Main.NPCChatText = $"Wow, it’s so shiny! Thanks for the loot!";
					return;
				}
				else
				{
					Main.NPCChatText = $"Don't try to fool me by coming with no gems! I have food to buy.";
					return;
				}
			}*/
		}

		#region Npc Attack

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 70;
			knockback = 5f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 10;
			randExtraCooldown = 10;
		}

		public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{
			itemWidth = itemHeight = 64;
		}

		public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
		{
			item = TextureAssets.Item[ModContent.ItemType<HeavensGate>()].Value;
		}

		#endregion Npc Attack

		public override bool CheckDead()
		{
			int goreIndex = Gore.NewGore(new Vector2(NPC.position.X + (float)(NPC.width / 2) - 24f, NPC.position.Y + (float)(NPC.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
			Main.gore[goreIndex].scale = 1.5f;
			Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
			Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;

			return true;
		}
	}
}