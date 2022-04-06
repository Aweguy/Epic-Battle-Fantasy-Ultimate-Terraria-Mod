using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Buffs
{
    public class Regeneration : ModBuff
    {
        private int timer = 1;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regeneration");
            Description.SetDefault("The player quickly regenerates health.");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            timer--;
            if (timer <= 0)
            {
                int Regen = player.statLifeMax2 / 100 * 7;
                player.statLife += Regen;
                player.HealEffect(Regen);
                timer = 60;
            }
        }
    }
}