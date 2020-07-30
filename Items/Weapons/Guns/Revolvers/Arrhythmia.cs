using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Items.Materials.Gems;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
    public class Arrhythmia : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arrhythmia");
            Tooltip.SetDefault("Echoes with an irregular beat when fired.");
        }


        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 30;

            item.damage = 19;
            item.useTime = 15;
            item.useAnimation = 15;
            item.reuseDelay = 2;

            item.crit = 1;
            item.knockBack = 3f;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 3);
            item.rare = ItemRarityID.LightPurple;

            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item41;
            item.shoot = 10;
            item.shootSpeed = 12f;
            item.useStyle = ItemUseStyleID.HoldingOut;
        }





        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(ModContent.ItemType<AncientAmber>());
            recipe.AddIngredient(ModContent.ItemType<VoltaicTopaz>());
            recipe.AddIngredient(ItemID.DirtBlock, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }












    }
}
