using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Buffs
{
    public class Protection : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Protection");
            Description.SetDefault("Blocks a quarter of damage taken, no Defend needed!");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance += 0.25f;
        }
    }
}