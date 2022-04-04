using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Launchers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;

namespace EpicBattleFantasyUltimate.Items.Revolvers
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
			Item.width = 46;
			Item.height = 30;

			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.reuseDelay = 10;

			Item.damage = 17;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 3);
			Item.rare = ItemRarityID.LightPurple;

			Item.useAmmo = AmmoID.Bullet;
			Item.UseSound = SoundID.Item41;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 12f;
			Item.useStyle = ItemUseStyleID.Shoot;
		}
		public Projectile shot;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 trueSpeed = new Vector2(velocity.Y, velocity.X);
			shot = Main.projectile[Projectile.NewProjectile(source, position, trueSpeed, type, damage, knockback, player.whoAmI)];
			shot.GetGlobalProjectile<LauncherProjectile>().B4Homingshot = true;

			return false;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts)
				.AddIngredient(ModContent.ItemType<P2Processor>())
				.AddIngredient(ModContent.ItemType<SteelPlate>(), 25)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}