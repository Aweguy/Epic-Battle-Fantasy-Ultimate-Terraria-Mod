using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Chat;

namespace EpicBattleFantasyUltimate.Items.Consumables.Summoning
{
	public class CrystallizedSoul : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crystallized Soul");
			Tooltip.SetDefault("Summons the Ore Ascension.\nA chunk of crystals imbued with the dim life force of an Ore; calls Ores to the surface as part of the Thaumatosphere’s Ore cycle.");
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.HoldUp;

			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.Purple;
			Item.maxStack = 99;
			Item.consumable = true;
		}

		public override bool CanUseItem(Player player)
		{
			return true;
		}

		public override bool? UseItem(Player player)
		{
			string key = "Mods.EpicBattleFantasyUltimate.OreEventStart";
			Color messageColor = Color.Orange;
			if (Main.netMode == NetmodeID.Server) // Server
			{ 
				ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
			}
			else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
			{
				Main.NewText(Language.GetTextValue(key), messageColor);
			}

			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				SoundEngine.PlaySound(SoundID.Roar, player.Center);
				EpicWorld.OreEvent = true;
			}

			return true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<DarkMatter>(), 20)
				.AddIngredient(ModContent.ItemType<RainbowOre>(), 5)
				.AddTile(TileID.DemonAltar)
				.Register();
		}
	}
}