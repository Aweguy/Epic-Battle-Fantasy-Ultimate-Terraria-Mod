﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
    public class Avenger : ModItem
    {
        private int missHP = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Avenger");
            Tooltip.SetDefault("For every scar, for every dishonor, the Avenger sharpens its edge. Grows stronger at low health.");
        }

        public override void SetDefaults()
        {
            item.damage = 5;
            item.knockBack = 5f;
            item.melee = true;

            item.width = 64;
            item.height = 64;

            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useTime = 40;
            item.useAnimation = 40;

            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override bool UseItem(Player player)
        {
            return true;
        }

        public override void HoldItem(Player player)
        {
            //Making the sword's damage increase based on the missing health
            missHP = player.statLifeMax - player.statLife;

            if (player.statLife < player.statLifeMax)
            {
                item.damage = 1 + missHP;
            }
        }
    }
}