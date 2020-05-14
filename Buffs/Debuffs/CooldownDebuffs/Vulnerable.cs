using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs
{
    public class Vulnerable : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Vulnerable");
            Description.SetDefault("The player cannot be protected for a short period of time.");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }

    }
}
