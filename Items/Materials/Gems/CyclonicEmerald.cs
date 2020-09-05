using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

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
            recipe.AddIngredient(ItemID.Emerald, 10);
            recipe.AddIngredient(ItemID.VineRope, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }



    }
}
