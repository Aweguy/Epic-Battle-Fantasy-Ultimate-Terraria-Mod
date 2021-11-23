using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
	public class Arrhythmia : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arrhythmia");
			Tooltip.SetDefault("Echoes with an irregular beat when fired.\nHas a slightly randomized rate of fire");
		}
		public override void SetDefaults()
		{
			item.width = 50;
			item.height = 30;

			item.damage = 14;
			item.useTime = 15;
			item.useAnimation = 15;
			item.reuseDelay = 2;

			item.crit = 1;
			item.knockBack = 3f;
			item.ranged = true;
			item.noMelee = true;

			item.value = Item.sellPrice(gold: 3);
			item.rare = ItemRarityID.LightPurple;

			item.useAmmo = AmmoID.Bullet;
			item.UseSound = SoundID.Item41;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 12f;
			item.useStyle = ItemUseStyleID.HoldingOut;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int Random = Main.rand.Next(5, 25);
			item.useTime = item.useAnimation = Random;

			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IllegalGunParts);
			recipe.AddIngredient(ModContent.ItemType<AncientAmber>());
			recipe.AddIngredient(ModContent.ItemType<VoltaicTopaz>());
			recipe.AddIngredient(ItemID.DirtBlock, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}