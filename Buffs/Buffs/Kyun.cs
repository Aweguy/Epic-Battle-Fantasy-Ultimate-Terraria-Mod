using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Buffs
{
    public class Kyun : ModBuff
    {
        private bool initial = true;

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Kyun");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (initial)
            {
                player.statLife += 500;

                initial = false;
            }
            player.lifeRegen += 50;
            player.manaRegen += 25;
            player.statDefense += 20;
            player.endurance += 10;
            player.allDamage += 0.75f;
        }
    }
}