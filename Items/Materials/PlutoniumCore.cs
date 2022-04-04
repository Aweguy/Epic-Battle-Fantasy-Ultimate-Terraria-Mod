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
			Tooltip.SetDefault("A well regulated energy source that you really shouldn't be carrying in your backpack.\nUsed in powerful Thunder Items.");
		}
		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.value = Item.sellPrice(silver: 10);
			Item.rare = ItemRarityID.Purple;
			Item.maxStack = 999;
		}
	}
}