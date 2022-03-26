using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Weapons.Launchers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
	public class VortexRevolver : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vortex Revolver");
			Tooltip.SetDefault("Propels bullets with the force of a cyclone.\nShots have inverted knockback.\nShots from this weapon home to enemies.");
		}
		public override void SetDefaults()
		{
			item.width = 46;
			item.height = 30;

			item.useTime = 30;
			item.useAnimation = 30;
			item.reuseDelay = 10;

			item.damage = 17;
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
		public Projectile shot;

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 trueSpeed = new Vector2(speedX, speedY);
			shot = Main.projectile[Projectile.NewProjectile(position.X, position.Y, trueSpeed.X, trueSpeed.Y, type, damage, knockBack, player.whoAmI)];
			shot.GetGlobalProjectile<LauncherProjectile>().B4Homingshot = true;

			return false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IllegalGunParts);
			recipe.AddIngredient(ModContent.ItemType<P2Processor>());
			recipe.AddIngredient(ModContent.ItemType<SteelPlate>(), 3);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}