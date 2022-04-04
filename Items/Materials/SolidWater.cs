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
			Item.width = 32;
			Item.height = 32;

			Item.value = Item.sellPrice(silver: 1);
			Item.rare = ItemRarityID.Purple;
			Item.maxStack = 999;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IceBlock,10 )
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}