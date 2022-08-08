using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;


namespace EpicBattleFantasyUltimate.Items.Materials
{
    public class RAMChip : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("RAM Chip");
            Tooltip.SetDefault("A RAM Chip used in many electronic devices");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Purple;
            Item.maxStack = 999;
        }


    }
}
