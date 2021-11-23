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
			item.width = 30;
			item.height = 30;

			item.value = Item.sellPrice(silver: 10);
			item.rare = ItemRarityID.Purple;
			item.maxStack = 999;
		}
	}
}