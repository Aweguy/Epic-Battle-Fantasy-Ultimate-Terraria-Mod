using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using EpicBattleFantasyUltimate.Items.Ammo.Shots;
using EpicBattleFantasyUltimate.Projectiles.Bullets;
using static Terraria.ModLoader.ModContent;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
    public class VortexRevolver : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vortex Revolver");
            Tooltip.SetDefault("Propels bullets with the force of a cyclone.\nShots have inverted knockback.");
        }




        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 30;

            item.useTime = 30;
            item.useAnimation = 30;

            item.damage = 17;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 3);
            item.rare = 6;

            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item41;
            item.shoot = 10;
            item.shootSpeed = 12f;
            item.useStyle = 5;

        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(mod.ItemType("P2Processor"));
            recipe.AddIngredient(mod.ItemType("SteelPlate"), 3);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }






    }
}
