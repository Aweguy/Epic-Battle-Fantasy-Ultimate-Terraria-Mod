using EpicBattleFantasyUltimate.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Revolvers
{
	public class ShadowBlasterGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shadow Revolver");
			Tooltip.SetDefault("'Oh my god, it’s full of stars.'\nUses bullets to fire\nHigh Critical Chance.");
		}
		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 30;

			Item.damage = 20;
			Item.useTime = 10;
			Item.useAnimation = 10;

			Item.crit = 10;
			Item.knockBack = 5f;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 2);
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
				.AddIngredient(ItemID.IllegalGunParts)
				.AddIngredient(ModContent.ItemType<DarkMatter>(), 5)
				.AddIngredient(ItemID.Obsidian, 10)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}