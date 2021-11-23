using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
	public class DarkMatter : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dark Matter");
			Tooltip.SetDefault("About one squirrel's worth of non-baryonic matter. Sets off bird feeders.\nUsed for crafting Dark equipment.");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 5));
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.value = Item.sellPrice(gold: 1);
			item.rare = ItemRarityID.Purple;
			item.maxStack = 99;
		}
	}
}