using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
	public class SteelPlate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Steel Plate");
			Tooltip.SetDefault("A cheap but strong material forged into an unusual shape.");
		}
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;

			Item.value = Item.sellPrice(copper: 50);
			Item.rare = ItemRarityID.LightRed;
			Item.maxStack = 999;
		}


		public override void AddRecipes()
		{
			CreateRecipe()
				.AddRecipeGroup("IronBar", 10)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}