using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs
{
    public class Vulnerable : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vulnerable");
            Description.SetDefault("The player cannot be protected for a short period of time.");
            Main.debuff[Type] = true;
        }
    }
}