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
				.AddIngredient(ItemID.Diamond, 10)
				.AddIngredient(ModContent.ItemType<GlassShard>(), 150)
				.AddTile(TileID.Furnaces)
				.Register();
		}
	}
}