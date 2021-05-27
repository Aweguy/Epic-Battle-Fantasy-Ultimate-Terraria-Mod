using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs
{
    public class BurntMouth : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Burnt Mouth");
            Description.SetDefault("Your mouth is on fire!");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }
    }
}