using EpicBattleFantasyUltimate.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Debuffs
{
    public class RampantBleed : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Rampant Bleeding");
            Description.SetDefault("Paper cuts deeper...");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<EpicPlayer>().RBleed = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<EpicGlobalNPC>().RBleed = true;
        }

        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            npc.GetGlobalNPC<EpicGlobalNPC>().RBleedStacks++;

            return false;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.GetModPlayer<EpicPlayer>().RBleedStacks += 3;

            return false;
        }
    }
}