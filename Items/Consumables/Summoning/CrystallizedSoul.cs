using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Consumables.Summoning
{
    public class CrystallizedSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystallized Soul");
            Tooltip.SetDefault("Summons the Ore Ascension.\nA chunk of crystals imbued with the dim life force of an Ore; calls Ores to the surface as part of the Thaumatosphere’s Ore cycle.");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.HoldingUp;

            item.value = Item.sellPrice(gold: 1);
            item.rare = ItemRarityID.Purple;
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
            if (Main.netMode == NetmodeID.Server) // Server
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
            }
            else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
            {
                Main.NewText(Language.GetTextValue(key), messageColor);
            }

            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.PlaySound(SoundID.Roar, player.position, 0);
                EpicWorld.OreEvent = true;
            }

            return true;
        }
    }
}