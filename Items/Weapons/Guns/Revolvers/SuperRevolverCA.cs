using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Items.Materials;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
    public class SuperRevolverCA : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Super Revolver CA");
            Tooltip.SetDefault("You can hear eagles when you reload it");
        }


        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 28;

            item.damage = 30;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingOut;

            item.ranged = true;
            item.noMelee = true;

            item.rare = ItemRarityID.Pink;
            item.shoot = 10;
            item.shootSpeed = 10f;
            item.useAmmo = AmmoID.Bullet;
            
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(ModContent.ItemType<P2Processor>());
            recipe.AddIngredient(ModContent.ItemType<GlassShard>(), 25);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }





    }
}
