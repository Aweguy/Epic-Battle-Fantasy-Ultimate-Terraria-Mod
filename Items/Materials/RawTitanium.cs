using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace EpicBattleFantasyUltimate.Items.Materials
{
    public class RawTitanium : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Raw Titanium");
            Tooltip.SetDefault("A shiny metal unavailable in bar or ore forms.");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;

            item.value = Item.sellPrice(silver: 10);
            item.rare = 11;
            item.maxStack = 99;


        }









    }
}
