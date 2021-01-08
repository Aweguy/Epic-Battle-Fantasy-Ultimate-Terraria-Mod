﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System;

namespace EpicBattleFantasyUltimate.Items.Materials
{
    public class RawTitanium : ModItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Raw Titanium");
            Tooltip.SetDefault("A shiny metal pure beyond the normal bar or ore forms.");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 30;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 4;

            item.value = Item.sellPrice(silver: 10);
            item.rare= ItemRarityID.Purple;
            item.maxStack = 99;



        }





    }
}
