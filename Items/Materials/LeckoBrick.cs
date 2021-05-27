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
            item.width = 30;
            item.height = 20;

            item.value = Item.sellPrice(silver: 10);
            item.rare = ItemRarityID.Purple;
            item.maxStack = 999;
        }
    }
}