using EpicBattleFantasyUltimate.ClassTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace EpicBattleFantasyUltimate.Items.Accessories.Flairs
{
	public class ShieldMedal : Flair
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shield Medal");
			Tooltip.SetDefault("Aid others where you can. Let all be helped and loved throughout the realm.\n20 defense");
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 19;
			Item.accessory = true;
			Item.rare = ItemRarityID.Lime;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.statDefense += 20;
		}

		public override int ChoosePrefix(UnifiedRandom rand)
		{
			return rand.Next(new int[] { PrefixID.Arcane, PrefixID.Lucky, PrefixID.Menacing, PrefixID.Quick, PrefixID.Violent, PrefixID.Warding, PrefixID.Hard, PrefixID.Guarding, PrefixID.Armored, PrefixID.Precise, PrefixID.Jagged, PrefixID.Spiked, PrefixID.Angry, PrefixID.Brisk, PrefixID.Fleeting, PrefixID.Hasty, PrefixID.Wild, PrefixID.Rash, PrefixID.Intrepid, PrefixID.Arcane });
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.HallowedBar, 10)
				.AddRecipeGroup("EpicBattleFantasyUltimate:Titanium", 5)
				.AddRecipeGroup("EpicBattleFantasyUltimate:GoldBar", 15)
				.AddTile(TileID.TinkerersWorkbench)
				.Register();
		}
	}
}