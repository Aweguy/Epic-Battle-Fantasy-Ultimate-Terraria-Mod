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
            item.width = 30;
            item.height = 30;

            item.value = Item.sellPrice(silver: 10);
            item.rare = ItemRarityID.Purple;
            item.maxStack = 999;
        }
    }
}