using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords
{
    public class GWIP2GG : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("GWIP2GG");
            Tooltip.SetDefault("'The woodsman slices the wood of trees by day, and the bones of fiends by night.'\n Right-Click to throw a small axe. Cannot be used while the axe is thrown.");
        }

        public override void SetDefaults()
        {
            item.width = 64;
            item.height = 64;

            item.damage = 100;
            item.knockBack = 5f;
            item.melee = true;

            item.useTime = 30;
            item.useAnimation = 30;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;


            item.value = Item.sellPrice(gold: 10);
            item.rare = 10;
            item.autoReuse = true;
        }

    }
}
