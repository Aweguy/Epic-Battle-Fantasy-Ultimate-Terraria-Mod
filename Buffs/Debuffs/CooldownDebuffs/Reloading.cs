using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs
{
    public class Reloading : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reloading");
            Description.SetDefault("The bombers are reloading their bombs.");
            Main.debuff[Type] = true;
        }
    }
}