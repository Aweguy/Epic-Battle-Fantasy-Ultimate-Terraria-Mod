using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace EpicBattleFantasyUltimate.Items.Materials
{
    public class SilkScrap : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silk Scrap");
            Tooltip.SetDefault("Used in seemingly everything and found seemingly nowhere.");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;

            item.value = Item.sellPrice(silver: 1);
            item.rare = 8;
            item.maxStack = 99;


        }






    }
}
