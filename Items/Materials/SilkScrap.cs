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
			Item.width = 20;
			Item.height = 20;

			Item.value = Item.sellPrice(silver: 1);
			Item.rare = ItemRarityID.Yellow;
			Item.maxStack = 99;
		}
	}
}