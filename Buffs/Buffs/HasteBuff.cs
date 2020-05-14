using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;


namespace EpicBattleFantasyUltimate.Buffs.Buffs
{
    public class HasteBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Haste");
            Description.SetDefault("You are sped up by magical energies.");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            
            player.meleeSpeed += 1f;

        }




       














    }
}
