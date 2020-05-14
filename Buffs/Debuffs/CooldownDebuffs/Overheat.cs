using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs
{
    public class Overheat : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Overheat");
            Description.SetDefault("The gun is too hot to fire again safely! Except if you want to explode into pieces.");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }



    }
}
