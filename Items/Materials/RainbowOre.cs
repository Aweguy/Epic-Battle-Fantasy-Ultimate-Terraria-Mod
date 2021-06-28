using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Materials
{
    public class RainbowOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rainbow Ore");
            Tooltip.SetDefault("Glistening ore used in a variety of magical equipment.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.HoldingUp;

            item.value = Item.sellPrice(gold: 1);
            item.rare = ItemRarityID.Yellow;
            item.maxStack = 99;
            item.consumable = true;
        }
    }
}