using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace EpicBattleFantasyUltimate.Items.Revolvers
{
	public class CrystalRevolver : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crystal Revolver");
			Tooltip.SetDefault("Use holy water when cleaning.\nHeals 3 hp on target hit.\nBecomes 6 hp if you have Crystal Wing in your inventory.");
		}
		public override void SetDefaults()
		{
			Item.width = 46;
			Item.height = 30;

			Item.damage = 18;
			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.reuseDelay = 5;

			Item.knockBack = 3f;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 4);
			Item.rare = ItemRarityID.Lime;

			Item.useAmmo = AmmoID.Bullet;
			Item.UseSound = SoundID.Item41;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 11f;
			Item.useStyle = ItemUseStyleID.Shoot;
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts)
				.AddIngredient(ModContent.ItemType<PristineDiamond>())
				.AddIngredient(ItemID.MarbleBlock, 25)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}