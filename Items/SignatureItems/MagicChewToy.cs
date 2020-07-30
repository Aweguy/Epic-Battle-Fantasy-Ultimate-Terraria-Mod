using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Items.SignatureItems
{
    public class MagicChewToy : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Chew Toy");
            Tooltip.SetDefault("The only chew toy that can make a magic puppy happy\nSummons a pet puppy.\n[c/FF0000:By Renatm].");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 26;
            item.rare = ItemRarityID.Expert;
            item.value = Item.sellPrice(platinum: 1);
        }





    }
}
