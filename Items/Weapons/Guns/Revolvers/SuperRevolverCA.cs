using EpicBattleFantasyUltimate.Items.Materials;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
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
			item.width = 50;
			item.height = 28;

			item.damage = 30;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;

			item.ranged = true;
			item.noMelee = true;

			item.rare = ItemRarityID.Pink;
			item.shoot = ProjectileID.PurificationPowder;
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