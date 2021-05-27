using EpicBattleFantasyUltimate.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
    public class ShadowBlasterGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Revolver");
            Tooltip.SetDefault("'Oh my god, it’s full of stars.'\nUses bullets to fire\nHigh Critical Chance.");
        }

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 30;

            item.damage = 20;
            item.useTime = 10;
            item.useAnimation = 10;

            item.crit = 10;
            item.knockBack = 5f;
            item.ranged = true;
            item.noMelee = true;

            item.value = Item.sellPrice(gold: 2);
            item.rare = ItemRarityID.Pink;

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
            recipe.AddIngredient(ModContent.ItemType<DarkMatter>(), 3);
            recipe.AddIngredient(ItemID.Obsidian, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}