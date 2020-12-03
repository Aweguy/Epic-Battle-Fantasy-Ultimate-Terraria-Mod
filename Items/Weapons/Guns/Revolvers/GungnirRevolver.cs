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
    public class GungnirRevolver : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regnir");
            Tooltip.SetDefault("You thought this gun would have a special power too.\nNo, its special power is being normal\nVery High Velocity shots.\nBoosts critical rate by 30% if Gungnir Rifle is in the inventory.");
        }


        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 30;

            item.damage = 46;
            item.useTime = 30;
            item.useAnimation = 30;
            item.reuseDelay = 10;

            item.knockBack = 3f;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 3);
            item.rare = ItemRarityID.LightPurple;

            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item41;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 30f;
            item.useStyle = ItemUseStyleID.HoldingOut;
        }





        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(mod.ItemType("PlutoniumCore"));
            recipe.AddIngredient(mod.ItemType("LeckoBrick"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

















    }
}
