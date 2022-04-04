using EpicBattleFantasyUltimate.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Revolvers
{
	public class ChainsawRevolver : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chainsaw Revolver");
			Tooltip.SetDefault("Now with extra safety warnings and no sharp edges!");
		}
		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 30;

			Item.damage = 22;
			Item.useTime = 25;
			Item.useAnimation = 25;

			Item.crit = 8;
			Item.knockBack = 6f;
			Item.DamageType = DamageClass.Melee;;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.Pink;

			Item.useAmmo = AmmoID.Bullet;
			Item.UseSound = SoundID.Item41;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 11f;
			Item.useStyle = ItemUseStyleID.Shoot;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<RawTitanium>(),5)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}