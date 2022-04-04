using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
	public class GrapeLeaf : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Grape Leaf");
			Tooltip.SetDefault("You can use it to make dolmades. Mmmm... tasty");
		}
		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.value = Item.sellPrice(copper: 10);
			Item.maxStack = 999;
			Item.rare = ItemRarityID.Green;
		}
	}
}