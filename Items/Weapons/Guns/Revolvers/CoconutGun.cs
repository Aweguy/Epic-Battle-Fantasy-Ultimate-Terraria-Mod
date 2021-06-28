using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
    public class CoconutGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Coconut Gun");
            Tooltip.SetDefault("A gun for smaller primates, storable in barrels for easy access.\nFires in 4 shot bursts and converts musket balls to peanuts.");
        }

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 30;

            item.useTime = 1;
            item.useAnimation = 8;
            item.reuseDelay = 60;

            item.damage = 7;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 2);
            item.rare = ItemRarityID.LightPurple;

            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item41;
            item.shoot = ProjectileID.PurificationPowder;
            item.shootSpeed = 11f;
            item.useStyle = ItemUseStyleID.HoldingOut;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IllegalGunParts);
            recipe.AddIngredient(ItemID.Wood, 30);
            recipe.AddIngredient(mod.ItemType("CyclonicEmerald"));
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }

}