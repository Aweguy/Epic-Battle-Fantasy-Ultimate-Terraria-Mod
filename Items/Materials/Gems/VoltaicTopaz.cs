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
			recipe.AddIngredient(ItemID.Topaz, 10);
			recipe.AddIngredient(ItemID.Wire, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}