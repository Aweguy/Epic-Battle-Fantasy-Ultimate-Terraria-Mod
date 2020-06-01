using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using EpicBattleFantasyUltimate.Projectiles.Bullets;
using static Terraria.ModLoader.ModContent;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
    public class CrystalRevolver : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Revolver");
            Tooltip.SetDefault("Use holy water when cleaning.\nHeals 3 hp on target hit.\nBecomes 6 hp if you have Crystal Wing in your inventory.");
        }


        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 30;

            item.damage = 19;
            item.useTime = 12;
            item.useAnimation = 12;

            item.knockBack = 3f;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 4);
            item.rare = 7;

            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item41;
            item.shoot = 10;
            item.shootSpeed = 11f;
            item.useStyle = ItemUseStyleID.HoldingOut;
        }





        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(mod.ItemType("PristineDiamond"));
            recipe.AddIngredient(ItemID.MarbleBlock, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }





    }
}
