using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
    public class FreezingPoint : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Freezing Point");
            Tooltip.SetDefault("Also comes with a slot to keep your drinks cold!");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 28;

            item.damage = 22;
            item.useTime = 15;
            item.useAnimation = 15;

            item.crit = 1;
            item.knockBack = 2f;
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
            recipe.AddIngredient(ModContent.ItemType<AbyssalSapphire>());
            recipe.AddIngredient(ModContent.ItemType<SolidWater>());
            recipe.AddIngredient(ItemID.IceBlock, 25);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}