using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpicBattleFantasyUltimate.ClassTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace EpicBattleFantasyUltimate.Items.Accessories.Flairs
{
    public class LuckyClover : Flair
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lucky Clover");
            Tooltip.SetDefault("A Lucky Clover.");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.rare = ItemRarityID.LightPurple;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if(player.statLife <= (player.statLifeMax2 / 100) * 50)
            {
                player.GetModPlayer<EpicPlayer>().Lucky = true;
                player.GetModPlayer<EpicPlayer>().BlessedCD--;
            }
        }


    }
}
