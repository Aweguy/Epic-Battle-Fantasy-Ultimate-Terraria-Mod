using EpicBattleFantasyUltimate.Items.Materials.Gems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
	public class RainbowOre : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rainbow Ore");
			Tooltip.SetDefault("Glistening ore used in a variety of magical equipment.");
		}
		public override void SetDefaults()
		{
			Item.width = 16;
			Item.height = 16;
			Item.useTime = 10;
			Item.useAnimation = 10;

			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ItemRarityID.Yellow;
			Item.maxStack = 99;
			Item.consumable = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<VolcanicRuby>(), 1)
				.AddIngredient(ModContent.ItemType<VoltaicTopaz>(), 1)
				.AddIngredient(ModContent.ItemType<AbyssalSapphire>(), 1)
				.AddIngredient(ModContent.ItemType<CyclonicEmerald>(), 1)
				.AddIngredient(ModContent.ItemType<PristineDiamond>(), 1)
				.AddIngredient(ModContent.ItemType<AncientAmber>(), 1)
				.AddIngredient(ItemID.StoneBlock, 10)
				.AddTile(TileID.Furnaces)
				.Register();
		}
	}
}