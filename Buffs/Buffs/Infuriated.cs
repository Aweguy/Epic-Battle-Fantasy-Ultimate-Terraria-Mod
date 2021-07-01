using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Buffs
{
    public class Infuriated : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Infuriated!");
            Description.SetDefault("And here you thought you had bought a normal pepper.\nDoubles your attack speed and damage");
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.allDamage += 1f;
        }
    }
}