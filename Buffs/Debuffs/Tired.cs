using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Debuffs
{
    public class Tired : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tired");
            Description.SetDefault("Your joints feel stiff and slow.");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<EpicPlayer>().Tired = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.GetModPlayer<EpicPlayer>().TiredStacks += 1;

            return false;
        }
    }
}