using EpicBattleFantasyUltimate.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Debuffs
{
    public class Weaken : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Weakened");
            Description.SetDefault("Your strength ebbs...");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<EpicPlayer>().Weakened = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<EpicGlobalNPC>().Weakened = true;
        }

        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            npc.GetGlobalNPC<EpicGlobalNPC>().WeakenedStacks++;

            return false;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.GetModPlayer<EpicPlayer>().WeakenedStacks++;

            return false;
        }
    }
}