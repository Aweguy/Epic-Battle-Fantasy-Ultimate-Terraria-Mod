﻿using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
	public class HolyGrail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Holy Grail");
			Tooltip.SetDefault("A unique and mythical artifact sought by many. Now available in 5 packs!");
		}
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;

			Item.value = Item.sellPrice(silver: 10);
			Item.rare = ItemRarityID.Purple;
			Item.maxStack = 99;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddRecipeGroup("GoldBar", 20)
				.AddIngredient(ModContent.ItemType<VolcanicRuby>(), 4)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}