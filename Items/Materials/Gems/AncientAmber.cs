using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Items.Materials.Gems
{
    public class AncientAmber : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Amber");
            Tooltip.SetDefault("Perfectly preserves mosquitoes, but the DNA is ruined.");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;

            item.value = Item.sellPrice(silver: 10);
            item.rare = 8;
            item.maxStack = 999;
            item.scale = 0.8f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Amber, 10);
            recipe.AddIngredient(ItemID.FossilOre, 5);
            recipe.AddIngredient(ItemID.Sandstone, 3);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }
}
