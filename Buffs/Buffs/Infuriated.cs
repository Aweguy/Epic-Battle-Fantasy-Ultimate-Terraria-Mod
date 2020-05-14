using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EpicBattleFantasyUltimate.Buffs.Buffs
{
    public class Infuriated : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Infuriated!");
            Description.SetDefault("Do you know why you are infuriated?\nBecause you thought you had bought a normal pepper.\nDoubles your attack speed and damage");
            canBeCleared = false;
        }


        public override void Update(Player player, ref int buffIndex)
        {
            player.allDamage += 1f;

        }







    }
}
