using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Projectiles.SpellProjectiles.FirestormSpell;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
			Item.damage = 70;
			Item.width = 40;
			Item.height = 40;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 30;
			Item.useAnimation = 30;
			LimitCost = 1;
			Item.rare = ItemRarityID.LightPurple;
			Item.useTurn = true;
			Item.shoot = ModContent.ProjectileType<Fireball>();
			Item.shootSpeed = 0f;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.value = Item.sellPrice(silver: 10);
		}

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
			position = Main.MouseWorld;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.useTime = 100;
				Item.useAnimation = 100;
				LimitCost = 5;
				Item.shoot = ModContent.ProjectileType<Firestorm>();
				Item.shootSpeed = 0f;
			}
			else
			{
				Item.useTime = 30;
				Item.useAnimation = 30;
				LimitCost = 1;
				Item.shoot = ModContent.ProjectileType<Fireball>();
				Item.shootSpeed = 0f;
			}

			return base.CanUseItem(player);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.LivingFireBlock, 100)
				.AddIngredient(ItemID.Wood, 10)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
}