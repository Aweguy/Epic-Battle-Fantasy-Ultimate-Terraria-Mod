using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials.Gems
{
	public class PristineDiamond : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pristine Diamond");
			Tooltip.SetDefault("A timeless gem that is less useful since the discovery of stronger materials.");
		}

		public override void SetDefaults()
		{
			item.width = 48;
			item.height = 48;

			item.value = Item.sellPrice(silver: 10);
			item.rare = ItemRarityID.Yellow;
			item.maxStack = 999;
			item.scale = 0.8f;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Diamond, 10);
			recipe.AddIngredient(ModContent.ItemType<GlassShard>(), 150);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}