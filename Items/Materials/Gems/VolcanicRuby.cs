using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

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
            recipe.AddIngredient(ItemID.Ruby, 10);
            recipe.AddIngredient(ItemID.Hellstone, 5);
            recipe.AddIngredient(ItemID.Obsidian);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }





    }
}
