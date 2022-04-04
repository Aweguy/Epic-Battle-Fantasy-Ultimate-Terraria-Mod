using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace EpicBattleFantasyUltimate.Items.Accessories.Flairs
{
	public class TargetBadge : Flair
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Target Badge");
			Tooltip.SetDefault("Somehow putting a target on yourself makes you a better shot, who knew?\n1 defense\nIncreases critical chance by 10% for all types of damage.\nIncreases enemy aggression.");
		}

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.rare = ItemRarityID.LightRed;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense += 1;
			player.GetCritChance(DamageClass.Generic) += 10;
			player.aggro += 400;
		}

		public override int ChoosePrefix(UnifiedRandom rand)
		{
			return rand.Next(new int[] { PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing, PrefixID.Quick, PrefixID.Violent, PrefixID.Warding, PrefixID.Hard, PrefixID.Guarding, PrefixID.Armored, PrefixID.Precise, PrefixID.Jagged, PrefixID.Spiked, PrefixID.Angry, PrefixID.Brisk, PrefixID.Fleeting, PrefixID.Hasty, PrefixID.Wild, PrefixID.Rash, PrefixID.Intrepid, PrefixID.Arcane });
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AbyssalSapphire>())
				.AddIngredient(ModContent.ItemType<VolcanicRuby>())
				.AddRecipeGroup("EpicBattleFantasyUltimate:Silver", 10)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}