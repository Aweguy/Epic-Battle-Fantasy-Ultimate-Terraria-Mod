using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace EpicBattleFantasyUltimate.Items.SignatureItems
{
	public class f : ModItem //Not even I remember why I called it f
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Staff of Balance");
			Tooltip.SetDefault("Represents the lack of balance in the world\n[c/FF0000:By Loren71]");
			Item.staff[item.type] = true;
		}

		public override void SetDefaults()
		{

			item.damage = 130;
			item.magic = true;
			item.width = 40;
			item.height = 38;
			item.useTime = 80;
			item.useAnimation = 80;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 5f;
			item.value = Item.buyPrice(platinum: 2);
			item.rare = -12;
			item.UseSound = SoundID.Item13;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("a");
			item.shootSpeed = 10f;

		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.UnholyWater, 1);
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.AddIngredient(ItemID.HolyWater, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BloodWater, 1);
			recipe.AddIngredient(ItemID.LunarBar, 1);
			recipe.AddIngredient(ItemID.HolyWater, 1);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}

	}
}