using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
	public class Wool : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Wool");
			Tooltip.SetDefault("Used in the making of garments and electric sheep.");
		}
		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 36;

			Item.value = Item.sellPrice(copper: 30);
			Item.rare = ItemRarityID.Orange;
			Item.maxStack = 999;
			Item.scale = 0.3f;
		}
	}
}