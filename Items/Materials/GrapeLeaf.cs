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
            item.width = 32;
            item.height = 32;
            item.value = Item.sellPrice(copper: 10);
            item.maxStack = 999;
            item.rare = ItemRarityID.Green;
        }
    }
}