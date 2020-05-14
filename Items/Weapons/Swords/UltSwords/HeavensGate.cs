using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
	public class HeavensGate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Heaven's Gate"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("A legendary sword belonging to a line of famed corsairs.");
		}

		public override void SetDefaults()
		{
			item.damage = 100;
			item.melee = true;
			item.width = 64;
			item.height = 64;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 8f;
			item.value = Item.sellPrice(gold: 10);
			item.rare = 10;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shootSpeed = 10f;
			item.shoot = mod.ProjectileType("LightBlade");

		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.AncientLight);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HallowedBar, 10);
			recipe.AddRecipeGroup("EpicBattleFantasyUltimate:Gold", 20);
			recipe.AddRecipeGroup("EpicBattleFantasyUltimate:Silver", 20);
			recipe.AddIngredient(mod.ItemType("HolyGrail"));
			recipe.AddIngredient(ItemID.SoulofLight, 45);
			recipe.AddIngredient(mod.ItemType("VolcanicRuby"), 3);
			recipe.AddIngredient(mod.ItemType("AethersBlade"));
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}



		
	}
}