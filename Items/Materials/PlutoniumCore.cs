using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace EpicBattleFantasyUltimate.Items.Materials
{
    public class PlutoniumCore : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plutonium Core");
            Tooltip.SetDefault("A well regulated energy source that you really shouldn't be carrying in your backpack.\nUsed in powerful Thunder items.");
        }


        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = Item.sellPrice(silver: 10);
            item.rare = 11;
            item.maxStack = 999;
        }







    }
}
