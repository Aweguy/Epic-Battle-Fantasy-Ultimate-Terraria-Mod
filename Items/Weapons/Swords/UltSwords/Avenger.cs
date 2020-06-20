using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;

namespace EpicBattleFantasyUltimate.Items.Weapons.Swords.UltSwords
{
    public class Avenger : ModItem
    {

        int missHP = 0;


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
            item.rare = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            
        }










        public override void HoldItem(Player player)
        {

            missHP = player.statLifeMax - player.statLife;

            if (player.statLife < player.statLifeMax)
            {
                item.damage = 1 + missHP;
            }
            
        }


        


    }
}
