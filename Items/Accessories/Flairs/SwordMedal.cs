﻿using EpicBattleFantasyUltimate.ClassTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace EpicBattleFantasyUltimate.Items.Accessories.Flairs
{
	public class SwordMedal : Flair
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sword Medal");
			Tooltip.SetDefault("True might is the mark of discipline, honor and courage.\n Increases Ranged and Melee damage by 20%");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.rare = ItemRarityID.Lime;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Ranged) += 0.2f;
			player.GetDamage(DamageClass.Melee) += 0.2f;
		}

		public override int ChoosePrefix(UnifiedRandom rand)
		{
			return rand.Next(new int[] { PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing, PrefixID.Quick, PrefixID.Violent, PrefixID.Warding, PrefixID.Hard, PrefixID.Guarding, PrefixID.Armored, PrefixID.Precise, PrefixID.Jagged, PrefixID.Spiked, PrefixID.Angry, PrefixID.Brisk, PrefixID.Fleeting, PrefixID.Hasty, PrefixID.Wild, PrefixID.Rash, PrefixID.Intrepid, PrefixID.Arcane });
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HallowedBar, 10)
				.AddRecipeGroup("TitaniumBar", 10)
				.AddRecipeGroup("GoldBar", 20)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}