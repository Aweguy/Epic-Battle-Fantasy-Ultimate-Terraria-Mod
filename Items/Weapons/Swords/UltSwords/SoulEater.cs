﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
    public class SoulEater : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Eater");
            Tooltip.SetDefault("Honestly, it could have been worse. It could kill you instantly.\nWhen held, increases your damage by 80% but reduces your defenst by 50%.");
        }

        public override void SetDefaults()
        {
            item.damage = 150;
            item.melee = true;
            item.width = 64;
            item.height = 64;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 35;
            item.useAnimation = 35;
            item.knockBack = 9f;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.scale = 1.15f;
        }

        public override void HoldItem(Player player)
        {
            player.allDamage += 0.8f;
            player.statDefense /= (int)2f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddRecipeGroup("EpicBattleFantasyUltimate:Evil", 20);
            recipe.AddIngredient(ItemID.BlackLens, 2);
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddIngredient(ItemID.SoulofNight, 15);
            recipe.AddIngredient(ItemID.SoulofSight, 15);
            recipe.AddIngredient(ItemID.SoulofFright, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}