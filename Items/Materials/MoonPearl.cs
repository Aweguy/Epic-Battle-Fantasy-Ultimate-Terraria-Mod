using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
	public class MoonPearl : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Moon Pearl");
			Tooltip.SetDefault("Has a beautiful shine that echoes through the heavens and all of space.");
		}
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;

			Item.value = Item.sellPrice(silver: 10);
			Item.rare = ItemRarityID.Purple;
			Item.maxStack = 999;
		}
	}
}