using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
	public class DragonsFeather : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon's Feather"); 
			Tooltip.SetDefault("A blade that grants swiftness to its wielder.\nInfused with the power of the wind, it can send enemies flying far away\nWhen upgraded it will summon turbulant wind knocking back\nand damaging your enemies.");
		}

		public override void SetDefaults()
		{
			item.damage = 70;
			item.melee = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 50f;
			item.value = Item.sellPrice(gold: 7);
			item.rare = ItemRarityID.Cyan;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.useTurn = true;
		}



		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("EpicBattleFantasyUltimate:Palladium", 10);
			recipe.AddIngredient(ItemID.Shuriken, 10);
			recipe.AddIngredient(ItemID.SoulofFlight, 5);
			recipe.AddIngredient(ItemID.Feather, 30);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();





		}
	}
}