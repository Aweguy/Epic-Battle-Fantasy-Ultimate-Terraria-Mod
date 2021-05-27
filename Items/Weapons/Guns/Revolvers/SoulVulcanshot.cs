using EpicBattleFantasyUltimate.Items.Weapons.Launchers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
    public class SoulVulcanshot : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Vulcanshot");
            Tooltip.SetDefault("Perfect for anachronistic westerners and steampunk showdowns. Old-school rapidfire action. Heals user when damaging foes.");
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
            item.shoot = 1;
            item.shootSpeed = 12f;
            item.useStyle = ItemUseStyleID.HoldingOut;
        }

        public Projectile shot;

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 trueSpeed = new Vector2(speedX, speedY);
            shot = Main.projectile[Projectile.NewProjectile(position.X, position.Y, trueSpeed.X, trueSpeed.Y, type, damage, knockBack, player.whoAmI)];
            shot.GetGlobalProjectile<LauncherProjectile>().LifeSteal = true;

            return false;
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