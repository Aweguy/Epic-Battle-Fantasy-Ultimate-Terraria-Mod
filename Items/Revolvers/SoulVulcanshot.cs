using EpicBattleFantasyUltimate.Items.Launchers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Revolvers
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
			Item.width = 56;
			Item.height = 34;

			Item.damage = 13;
			Item.useTime = 10;
			Item.useAnimation = 10;

			Item.crit = 1;
			Item.knockBack = 3f;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;
			Item.autoReuse = true;

			Item.value = Item.sellPrice(gold: 3);
			Item.rare = ItemRarityID.LightPurple;

			Item.useAmmo = AmmoID.Bullet;
			Item.UseSound = SoundID.Item41;
			Item.shoot = ProjectileID.WoodenArrowFriendly;
			Item.shootSpeed = 12f;
			Item.useStyle = ItemUseStyleID.Shoot;
		}
		public Projectile shot;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			Vector2 trueSpeed = new Vector2(velocity.X, velocity.Y);
			shot = Main.projectile[Projectile.NewProjectile(source,position, trueSpeed, type, damage, knockback, player.whoAmI)];
			shot.GetGlobalProjectile<LauncherProjectile>().LifeSteal = true;

			return false;
		}
		/*public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts)
				.AddIngredient(ModContent.ItemType<AncientAmber>())
				.AddIngredient(ModContent.ItemType<VoltaicTopaz>())
				.AddIngredient(ItemID.DirtBlock, 25)
				.AddTile(TileID.Anvils)
				.Register();
		}*/
		
	}
}