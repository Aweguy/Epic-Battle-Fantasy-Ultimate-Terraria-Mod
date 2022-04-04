using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
	public class LeckoBrick : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lecko Brick");
			Tooltip.SetDefault("Perfect for stubbing toes.");
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;

			Item.value = Item.sellPrice(silver: 10);
			Item.rare = ItemRarityID.Purple;
			Item.maxStack = 999;
		}
	}
}