using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System;

namespace EpicBattleFantasyUltimate.Items.Materials
{
    public class RainbowOre : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rainbow Ore");
            Tooltip.SetDefault("A mysterious ore that can summon destruction.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 4;

            item.value = Item.sellPrice(gold: 1);
            item.rare = ItemRarityID.Yellow;
            item.maxStack = 99;
            item.consumable = true;

            
        }








        public override bool CanUseItem(Player player)
        {
            return true;
        }

        public override bool UseItem(Player player)
        {
            string key = "Mods.EpicBattleFantasyUltimate.OreEventStart";
            Color messageColor = Color.Orange;
            if (Main.netMode == 2) // Server
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
            }
            else if (Main.netMode == 0) // Single Player
            {
                Main.NewText(Language.GetTextValue(key), messageColor);
            }

            if (Main.netMode == 0)
            {
                Main.PlaySound(SoundID.Roar, player.position, 0);
                EpicWorld.OreEvent = true;
            }

            return true;
        }












    }
}
