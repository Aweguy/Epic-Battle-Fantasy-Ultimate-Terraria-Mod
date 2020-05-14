﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EpicBattleFantasyUltimate.Items.Consumables
{
    public class ChilliPepper : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chilli Pepper");
            Tooltip.SetDefault("An organic alternative to sauce used by Limit Break aficionados.");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useTurn = true;
            item.maxStack = 30;
            item.consumable = true;
            item.UseSound = SoundID.Item2;
        }


        public override bool UseItem(Player player)
        {

            player.AddBuff(mod.BuffType("BurntMouth"), 60 * 600);
            player.AddBuff(mod.BuffType("Infuriated"), 60 * 10);

            return base.UseItem(player);
        }








        public override bool CanUseItem(Player player)
        {
            int buff = mod.BuffType("BurntMouth");
            return !player.HasBuff(buff);
        }







    }
}