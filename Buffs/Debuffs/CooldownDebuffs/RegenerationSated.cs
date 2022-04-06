using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs
{
    public class RegenerationSated : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regeneration Sated");
            Description.SetDefault("The player cannot use the Regeneration Spell because of recent use.");
            Main.debuff[Type] = true;
        }
    }
}