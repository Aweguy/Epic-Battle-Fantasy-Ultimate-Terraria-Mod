using EpicBattleFantasyUltimate.Items.Materials;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Revolvers
{
	public class SuperRevolverCA : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Super Revolver CA");
			Tooltip.SetDefault("Any time of year, you can snipe from here…");
		}
		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 28;

			Item.damage = 30;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;

			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.rare = ItemRarityID.Pink;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 10f;
			Item.useAmmo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts)
				.AddIngredient(ModContent.ItemType<P2Processor>())
				.AddIngredient(ModContent.ItemType<GlassShard>(), 25)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}