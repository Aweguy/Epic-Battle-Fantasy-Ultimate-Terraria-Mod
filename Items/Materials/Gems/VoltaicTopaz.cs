using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials.Gems
{
	public class VoltaicTopaz : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Voltaic Topaz");
			Tooltip.SetDefault("Why couldn’t Topaz pick up anyone at the bar?\nTopaz has no game!");
		}
		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 48;

			Item.value = Item.sellPrice(silver: 10);
			Item.rare = ItemRarityID.Yellow;
			Item.maxStack = 999;
			Item.scale = 0.8f;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.Topaz, 10)
				.AddIngredient(ItemID.Wire, 20)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}