using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Revolvers
{
	public class ThunderCoreGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Thunder Revolver");
			Tooltip.SetDefault("A miniature railgun that fires electrifying shots.\nUses bullets to fire\nQuick fire with 2 round bursts.");
		}
		public override void SetDefaults()
		{
			Item.width = 50;
			Item.height = 30;

			Item.damage = 15;
			Item.useTime = 4;
			Item.useAnimation = 9;
			Item.reuseDelay = 2;

			Item.crit = 1;
			Item.knockBack = 3f;
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
				.AddIngredient(ModContent.ItemType<PlutoniumCore>())
				.AddIngredient(ModContent.ItemType<VoltaicTopaz>())
				.AddIngredient(ItemID.Glass, 25)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}