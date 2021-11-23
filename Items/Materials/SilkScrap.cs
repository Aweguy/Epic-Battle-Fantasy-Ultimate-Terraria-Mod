using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
	public class SilkScrap : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Silk Scrap");
			Tooltip.SetDefault("Used in seemingly everything and found seemingly nowhere.");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;

			item.value = Item.sellPrice(silver: 1);
			item.rare = ItemRarityID.Yellow;
			item.maxStack = 99;
		}
	}
}