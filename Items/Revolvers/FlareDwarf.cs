using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace EpicBattleFantasyUltimate.Items.Revolvers
{
	public class FlareDwarf : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flare Dwarf");
			Tooltip.SetDefault("Made for a madman, each round is backed by enough fluid and power to level a small building.");
		}
		public override void SetDefaults()
		{
			Item.width = 46;
			Item.height = 30;

			Item.damage = 33;
			Item.useTime = 40;
			Item.useAnimation = 40;

			Item.knockBack = 5f;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 5);
			Item.rare = ItemRarityID.Yellow;

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
				.AddIngredient(ItemType<VolcanicRuby>())
				.AddIngredient(ItemType<SteelPlate>(), 10)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}