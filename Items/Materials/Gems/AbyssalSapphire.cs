using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials.Gems
{
	public class AbyssalSapphire : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abyssal Sapphire");
			Tooltip.SetDefault("Honored by those of the sea, and the keepers of the ice.");
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
				.AddIngredient(ItemID.Sapphire, 10)
				.AddIngredient(ModContent.ItemType<SolidWater>(),15)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}