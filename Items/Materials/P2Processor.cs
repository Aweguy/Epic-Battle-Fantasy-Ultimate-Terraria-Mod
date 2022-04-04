using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
	public class P2Processor : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("P2 Processor");
			Tooltip.SetDefault("See also Decanium Processor");
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