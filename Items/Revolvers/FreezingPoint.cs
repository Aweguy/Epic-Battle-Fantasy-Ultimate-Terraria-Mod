using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Revolvers
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
			Item.width = 48;
			Item.height = 28;

			Item.damage = 22;
			Item.useTime = 15;
			Item.useAnimation = 15;

			Item.crit = 1;
			Item.knockBack = 2f;
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

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts)
				.AddIngredient(ModContent.ItemType<AbyssalSapphire>())
				.AddIngredient(ModContent.ItemType<SolidWater>(), 3)
				.AddIngredient(ItemID.IceBlock, 25)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}