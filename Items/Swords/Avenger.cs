using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Swords
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
            Item.damage = 50;
            Item.knockBack = 5f;
            Item.DamageType = DamageClass.Melee;

            Item.width = 64;
            Item.height = 64;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 40;
            Item.useAnimation = 40;

            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void HoldItem(Player player)
        {
            //Making the sword's damage increase based on the missing health
            missHP = player.statLifeMax - player.statLife;

            if (player.statLife < player.statLifeMax)
            {
                Item.damage = 1 + (int)(missHP / 2);
            }
        }
    }
}