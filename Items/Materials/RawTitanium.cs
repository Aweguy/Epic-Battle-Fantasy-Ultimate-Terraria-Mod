using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
	public class RawTitanium : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Raw Titanium");
			Tooltip.SetDefault("A shiny metal pure beyond the normal bar or ore forms.");
		}
		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 30;

			Item.value = Item.sellPrice(silver: 10);
			Item.rare = ItemRarityID.Purple;
			Item.maxStack = 99;
		}
	}
}