using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
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
			item.width = 46;
			item.height = 30;

			item.damage = 33;
			item.useTime = 40;
			item.useAnimation = 40;

			item.knockBack = 5f;
			item.ranged = true;
			item.noMelee = true;

			item.value = Item.sellPrice(gold: 5);
			item.rare = ItemRarityID.Yellow;

			item.useAmmo = AmmoID.Bullet;
			item.UseSound = SoundID.Item41;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 11f;
			item.useStyle = ItemUseStyleID.HoldingOut;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IllegalGunParts);
			recipe.AddIngredient(ItemType<VolcanicRuby>());
			recipe.AddIngredient(ItemType<SteelPlate>(), 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}