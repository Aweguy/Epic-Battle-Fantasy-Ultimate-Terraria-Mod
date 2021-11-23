using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
	public class SolidWater : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Solid Water");
			Tooltip.SetDefault("Only found in freezers and frosty climates like Winnipeg.");
		}
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;

			item.value = Item.sellPrice(silver: 1);
			item.rare = ItemRarityID.Purple;
			item.maxStack = 999;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IceBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}