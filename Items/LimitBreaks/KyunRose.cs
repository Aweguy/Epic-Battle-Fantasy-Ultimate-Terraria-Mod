/*using EpicBattleFantasyUltimate.Buffs.Buffs;
using EpicBattleFantasyUltimate.ClassTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.LimitBreaks
{
    public class KyunRose : LimitItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kyun Rose");
            Tooltip.SetDefault("");
        }

        public override void SetSafeDefaults()
        {
            Item.width = 10;
            Item.height = 10;

            LimitCost = 100;

            Item.useTime = 50;
            Item.useAnimation = 50;

            Item.useTurn = true;
            Item.noMelee = true;
            Item.value = Item.sellPrice(platinum: 1);

            Item.rare = ItemRarityID.Expert;
        }

        public override bool UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<Kyun>(), 60 * 15);

            return base.UseItem(player);
        }
    }
}*/