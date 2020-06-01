using EpicBattleFantasyUltimate.Buffs.Minions;
using EpicBattleFantasyUltimate.Projectiles.Minions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace EpicBattleFantasyUltimate.Items.Summoning
{


	public class AngelicCandle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Angelic Candle");
			Tooltip.SetDefault("A blessed candle said to ward off magical attacks.");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 60;
			item.knockBack = 4f;
			item.mana = 30;
			item.width = 32;
			item.height = 32;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.sellPrice(gold: 50);
			item.rare = 9;
			item.UseSound = SoundID.Item44;

			// These below are needed for a minion weapon
			item.noMelee = true;
			item.summon = true;
			item.buffType = mod.BuffType("AngelicMirrorBuff");
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			item.shoot = mod.ProjectileType("AngelicMirror");
			item.scale = 1.15f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(item.buffType, 2);

			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position.
			position = Main.MouseWorld;
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SoulofLight, 25);
			recipe.AddIngredient(ItemID.BeeWax, 20);
			recipe.AddIngredient(ItemID.GoldBar);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();


			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SoulofLight, 25);
			recipe.AddIngredient(ItemID.BeeWax, 20);
			recipe.AddIngredient(ItemID.PlatinumBar);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}