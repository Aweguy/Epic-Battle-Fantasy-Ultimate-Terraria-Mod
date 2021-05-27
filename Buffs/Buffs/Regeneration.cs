using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Buffs
{
    public class Regeneration : ModBuff
    {
        private int timer = 1;

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Regeneration");
            Description.SetDefault("The player quickly regenerates health.");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            timer--;
            if (timer == 0)
            {
                player.statLife += 25;
                player.HealEffect(25);
                timer = 60;
            }
        }
    }
}