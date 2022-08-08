using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials.Gems
{
	public class VolcanicRuby : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Volcanic Ruby");
			Tooltip.SetDefault("Worshipped by those of the magma, and the keepers of the sand.");
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
				.AddIngredient(ItemID.Ruby, 10)
				.AddIngredient(ItemID.Hellstone, 3)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}