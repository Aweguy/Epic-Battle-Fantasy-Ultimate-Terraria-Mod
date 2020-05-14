﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using EpicBattleFantasyUltimate.Projectiles.Bullets;
using static Terraria.ModLoader.ModContent;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
    public class ChainsawRevolver : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chainsaw Revolver");
            Tooltip.SetDefault("Now with extra safety warnings and no sharp edges!");
        }


        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 30;

            item.damage = 22;
            item.useTime = 10;
            item.useAnimation = 10;

            item.crit = 8;
            item.knockBack = 5f;
            item.melee = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 1);
            item.rare = 5;

            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item41;
            item.shoot = 10;
            item.shootSpeed = 11f;
            item.useStyle = 5;
        }





        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(mod.ItemType("RawTitanium"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
