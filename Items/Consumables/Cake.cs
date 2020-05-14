using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EpicBattleFantasyUltimate.Items.Consumables
{
    public class Cake : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cake");
            Tooltip.SetDefault("A cake with sinfully high sugar content. Associated with magical stat boosts and bouncing off the walls.\nGives the Sugar Rush buff after consumed. Lasts until you die");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 24;
            item.useStyle = ItemUseStyleID.EatingUsing;
            item.useTime = 35;
            item.useAnimation = 35;
            item.useTurn = true;
            item.maxStack = 30;
            item.consumable = true;
            item.UseSound = SoundID.Item2;
            item.buffType = mod.BuffType("SugarRush");
            item.buffTime = 6000;
        }

        

        public override bool CanUseItem(Player player)
        {
            int buff = mod.BuffType("SugarRush");
            return !player.HasBuff(buff);
        }






    }
}
