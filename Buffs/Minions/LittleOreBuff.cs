using EpicBattleFantasyUltimate.Projectiles.Minions;
using EpicBattleFantasyUltimate.Projectiles.Minions.OreMinions;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace EpicBattleFantasyUltimate.Buffs.Minions
{
    public class LittleOreBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Little Ores");
            Description.SetDefault("Little Ores fight with you. Watch out for the fireworks");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ProjectileType<LittleAmethystOre>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}