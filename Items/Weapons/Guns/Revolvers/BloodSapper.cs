using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
    public class BloodSapper : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Sapper");
            Tooltip.SetDefault("A sidearm so cruel, it seems to aim itself at the bloodiest of organs.");
        }

        public override void SetDefaults()
        {
            item.width = 56;
            item.height = 34;

            item.damage = 13;
            item.useTime = 10;
            item.useAnimation = 10;

            item.crit = 1;
            item.knockBack = 3f;
            item.ranged = true;
            item.noMelee = true;
            item.autoReuse = true;

            item.value = Item.sellPrice(gold: 3);
            item.rare = ItemRarityID.LightPurple;

            item.useAmmo = AmmoID.Bullet;
            item.UseSound = SoundID.Item41;
            item.shoot = ProjectileID.WoodenArrowFriendly;
            item.shootSpeed = 12f;
            item.useStyle = ItemUseStyleID.HoldingOut;
        }

        /*public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IllegalGunParts);
			recipe.AddIngredient(ModContent.ItemType<AncientAmber>());
			recipe.AddIngredient(ModContent.ItemType<VoltaicTopaz>());
			recipe.AddIngredient(ItemID.DirtBlock, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}*/
    }
}