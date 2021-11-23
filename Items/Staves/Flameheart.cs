using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Projectiles.SpellProjectiles.FirestormSpell;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Staves
{
	public class Flameheart : LimitItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flameheart");
			Tooltip.SetDefault("A common but powerful staff, used by mages to scorch foes.\nConsumes minor amounts of Limit Break");
		}

		public override void SetSafeDefaults()
		{
			item.damage = 70;
			item.width = 40;
			item.height = 40;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 30;
			item.useAnimation = 30;
			LimitCost = 1;
			item.rare = ItemRarityID.LightPurple;
			item.useTurn = true;
			item.shoot = ModContent.ProjectileType<Fireball>();
			item.shootSpeed = 0f;
			item.noMelee = true;
			item.magic = true;
			item.value = Item.sellPrice(silver: 10);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld;

			return true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.useTime = 100;
				item.useAnimation = 100;
				LimitCost = 5;
				item.shoot = ModContent.ProjectileType<Firestorm>();
				item.shootSpeed = 0f;
			}
			else
			{
				item.useTime = 30;
				item.useAnimation = 30;
				LimitCost = 1;
				item.shoot = ModContent.ProjectileType<Fireball>();
				item.shootSpeed = 0f;
			}

			return base.CanUseItem(player);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LivingFireBlock, 100);
			recipe.AddIngredient(ItemID.Book);
			recipe.AddTile(TileID.Bookcases);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}