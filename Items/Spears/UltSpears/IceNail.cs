using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using EpicBattleFantasyUltimate.Projectiles.SpearProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Spears.UltSpears
{
	public class IceNail : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Nail");
			Tooltip.SetDefault("An icicle massive and sharp enough to be a jousting lance.");
		}

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.useStyle = ItemUseStyleID.SwingThrow;
			Item.useAnimation = 20;
			Item.useTime = 30;
			Item.shootSpeed = 3.7f;
			Item.knockBack = 6.5f;
			Item.width = 32;
			Item.height = 32;
			Item.scale = 1f;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(gold: 10);
			Item.DamageType = DamageClass.Melee;;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.rand.Next(5) == 0)
			{
				target.AddBuff(BuffID.Frostburn, 60 * 3);
			}
		}

		public override void UseStyle(Player player)
		{
			player.ItemLocation = player.Center + new Vector2(0, 3);
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.damage = 60;
				Item.useStyle = ItemUseStyleID.HoldingOut;
				Item.useAnimation = 20;
				Item.useTime = 30;
				Item.shootSpeed = 3.7f;
				Item.knockBack = 6.5f;
				Item.width = 32;
				Item.height = 32;
				Item.scale = 1f;
				Item.rare = ItemRarityID.Pink;
				Item.value = Item.sellPrice(gold: 10);
				Item.DamageType = DamageClass.Melee;;
				Item.noMelee = true; // Important because the spear is actually a projectile instead of an Item. This prevents the melee hitbox of this Item.
				Item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this Item.
				Item.shoot = mod.ProjectileType("IceNeedleProj");
				Item.autoReuse = false;
			}
			else
			{
				Item.damage = 60;
				Item.useStyle = ItemUseStyleID.SwingThrow;
				Item.useAnimation = 20;
				Item.useTime = 30;
				Item.shootSpeed = 3.7f;
				Item.knockBack = 6.5f;
				Item.width = 32;
				Item.height = 32;
				Item.scale = 1f;
				Item.rare = ItemRarityID.Pink;
				Item.value = Item.sellPrice(gold: 10);
				Item.DamageType = DamageClass.Melee;;
				Item.noMelee = false; // Important because the spear is actually a projectile instead of an Item. This prevents the melee hitbox of this Item.
				Item.noUseGraphic = false; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this Item.
				Item.shoot = ProjectileID.None;
				Item.autoReuse = true;
			}
			return player.ownedProjectileCounts[ModContent.ProjectileType<IceNailProj>()] < 1;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<SolidWater>(), 10);
			recipe.AddRecipeGroup("EpicBattleFantasyUltimate:Silver", 10);
			recipe.AddIngredient(ModContent.ItemType<AbyssalSapphire>(), 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}