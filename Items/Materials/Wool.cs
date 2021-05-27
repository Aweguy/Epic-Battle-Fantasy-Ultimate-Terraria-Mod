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
            item.width = 36;
            item.height = 36;

            item.value = Item.sellPrice(copper: 30);
            item.rare = ItemRarityID.Orange;
            item.maxStack = 999;
            item.scale = 0.3f;
        }
    }
}