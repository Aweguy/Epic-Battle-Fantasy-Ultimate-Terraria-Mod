using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using EpicBattleFantasyUltimate.Items.Materials;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
    public class FreezingPoint : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Freezing Point");
            Tooltip.SetDefault("Echoes with an irregular beat when fired.");
        }


        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 28;

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
            recipe.AddIngredient(ModContent.ItemType<AbyssalSapphire>());
            recipe.AddIngredient(ModContent.ItemType<SolidWater>());
            recipe.AddIngredient(ItemID.IceBlock, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }


















    }
}
