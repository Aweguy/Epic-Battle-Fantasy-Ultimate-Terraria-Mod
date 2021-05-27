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
            item.width = 32;
            item.height = 32;

            item.value = Item.sellPrice(copper: 50);
            item.rare = ItemRarityID.LightRed;
            item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("IronBar", 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}