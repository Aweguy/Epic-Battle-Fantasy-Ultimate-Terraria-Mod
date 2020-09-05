using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using EpicBattleFantasyUltimate.Projectiles.Bullets;
using static Terraria.ModLoader.ModContent;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
    public class ThunderCoreGun : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thunder Revolver");
            Tooltip.SetDefault("A miniature railgun that fires electrifying shots.\nUses bullets to fire\nQuick fire with 2 round bursts.");
        }


        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 30;

            item.damage = 19;
            item.useTime = 4;
            item.useAnimation = 9;
            item.reuseDelay = 2;

            item.crit = 1;
            item.knockBack = 3f;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 3);
            item.rare = ItemRarityID.LightPurple;

            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item41;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 12f;
            item.useStyle = ItemUseStyleID.HoldingOut;
        }





        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(mod.ItemType("PlutoniumCore"));
            recipe.AddIngredient(mod.ItemType("VoltaicTopaz"));
            recipe.AddIngredient(ItemID.Glass, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


    }
}
