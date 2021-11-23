using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
	public class PlutoniumCore : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Plutonium Core");
			Tooltip.SetDefault("A well regulated energy source that you really shouldn't be carrying in your backpack.\nUsed in powerful Thunder items.");
		}
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(silver: 10);
			item.rare = ItemRarityID.Purple;
			item.maxStack = 999;
		}
	}
}