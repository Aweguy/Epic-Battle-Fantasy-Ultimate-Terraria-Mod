using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Revolvers
{
	public class CoconutGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coconut Gun");
			Tooltip.SetDefault("A gun for smaller primates, storable in barrels for easy access.\nFires in 4 shot bursts and converts musket balls to peanuts.");
		}
		public override void SetDefaults()
		{
			Item.width = 46;
			Item.height = 30;

			Item.useTime = 1;
			Item.useAnimation = 8;
			Item.reuseDelay = 60;

			Item.damage = 7;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 2);
			Item.rare = ItemRarityID.LightPurple;

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
				.AddIngredient(ItemID.Wood, 30)
				.AddIngredient(ModContent.ItemType<CyclonicEmerald>())
				.AddTile(TileID.Anvils)
				.Register();
		}
	}

}