using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace EpicBattleFantasyUltimate.Items.Spellbooks
{
    public class SpellHaste : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Haste Spell");
            Tooltip.SetDefault("This spell vastly increases speed of all your actions.");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.useTime = 100;
            item.useAnimation = 10;
            item.mana = 5;
            item.value = Item.sellPrice(silver: 60);
            item.rare = 8;
            item.useTurn = true;
            item.UseSound = SoundID.Item29;
        }






        public override bool CanUseItem(Player player)
        {
            int buff = mod.BuffType("Exhaustion");
            return !player.HasBuff(buff);
        }

        public override bool UseItem(Player player)
        {
            player.AddBuff(mod.BuffType("HasteBuff"), 60 * 10);
            player.AddBuff(mod.BuffType("Exhaustion"), 60 * 60);

            return base.UseItem(player);
        }

    }
}
