using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Buffs.Buffs;

namespace EpicBattleFantasyUltimate.Items.Spells
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
            item.width = 10;
            item.height = 10;

            LimitCost = 100;

            item.useTime = 50;
            item.useAnimation = 50;

            item.useTurn = true;
            item.noMelee = true;
            item.value = Item.sellPrice(platinum: 1);



            item.rare = ItemRarityID.Expert;


        }



        public override bool UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<Kyun>(), 60 * 15);

            return base.UseItem(player);
        }



    }
}
