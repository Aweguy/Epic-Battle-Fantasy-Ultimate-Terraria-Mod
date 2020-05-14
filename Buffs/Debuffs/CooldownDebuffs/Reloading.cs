using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs
{
    public class Reloading : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Reloading");
            Description.SetDefault("The bombers are reloading their bombs.");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }

    }
}
