using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;



namespace EpicBattleFantasyUltimate.Items.Accessories
{
    public class IronCross : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Cross");
            Tooltip.SetDefault("A war medal which offers protection. Lance's favourite flair.\nBoosts ranged damage by 10%");
        }



        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.defense = 4;
            item.accessory = true;
            item.rare = ItemRarityID.LightPurple;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += 0.1f;
        }



    }
}
