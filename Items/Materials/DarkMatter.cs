using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace EpicBattleFantasyUltimate.Items.Materials
{
    public class DarkMatter : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Matter");
            Tooltip.SetDefault("Remarkably hard to turn into sentient, sociopathic metastable life hellbent on eradicating all baryonic organisms.\nUsed for crafting Dark equipment.");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 5));
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }


        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.value = Item.sellPrice(gold: 1);
            item.rare = 11;
            item.maxStack = 99;
        }







    }
}
