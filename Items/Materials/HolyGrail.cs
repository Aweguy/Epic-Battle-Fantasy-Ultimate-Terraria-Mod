using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
	public class HolyGrail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Holy Grail");
			Tooltip.SetDefault("A unique and mythical artifact sought by many. Now available in 5 packs!");
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;

			item.value = Item.sellPrice(silver: 10);
			item.rare = ItemRarityID.Purple;
			item.maxStack = 99;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("EpicBattleFantasyUltimate:Gold", 20);
			recipe.AddIngredient(mod.ItemType("VolcanicRuby"), 3);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}