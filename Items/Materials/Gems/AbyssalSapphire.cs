﻿using Terraria;
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
            recipe.AddIngredient(ItemID.Sapphire, 10);
            recipe.AddIngredient(mod.ItemType("SolidWater"), 15);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}