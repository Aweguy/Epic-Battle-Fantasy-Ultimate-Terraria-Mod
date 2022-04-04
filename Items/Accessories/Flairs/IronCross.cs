using EpicBattleFantasyUltimate.ClassTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace EpicBattleFantasyUltimate.Items.Accessories.Flairs
{
    public class IronCross : Flair
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Cross");
            Tooltip.SetDefault("A war medal which offers protection. Lance's favourite flair.\n4 defense\nBoosts ranged damage by 10%");
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
            player.statDefense += 4;
            player.GetDamage(DamageClass.Generic) += 0.1f;
        }
    }
}