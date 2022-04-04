using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials.Gems
{
	public class AncientAmber : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Amber");
			Tooltip.SetDefault("A chunk of sap that perfectly preserves mosquitoes, but isn’t enough to stop DNA degradation.");
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
				.AddIngredient(ItemID.Amber, 10)
				.AddIngredient(ItemID.FossilOre, 15)
				.AddIngredient(ItemID.Sandstone, 10)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}