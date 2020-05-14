﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;

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
            item.rare = 11;
            item.maxStack = 999;


        }







    }
}