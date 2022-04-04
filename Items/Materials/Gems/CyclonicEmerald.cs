using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials.Gems
{
	public class CyclonicEmerald : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cyclonic Emerald");
			Tooltip.SetDefault("Blessed by those of the sky, and the keepers of the leaf.");
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
				.AddIngredient(ItemID.Emerald, 10)
				.AddIngredient(ItemID.Cloud, 15)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}