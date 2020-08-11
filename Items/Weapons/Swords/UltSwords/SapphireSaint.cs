using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
	public class SapphireSaint : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Sapphire Saint"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("Let the wise teachings of the prophet into your hearts, and Godcat's angels shall bring you eternal good fortune.");
		}

		public override void SetDefaults()
		{
			item.damage = 47;
			item.melee = true;
			item.width = 54;
			item.height = 54;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 15f;
			item.value = Item.sellPrice(silver: 50);
			item.rare= ItemRarityID.Pink;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.scale += 0.1f;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AbyssalSapphire"), 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}