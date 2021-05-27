using EpicBattleFantasyUltimate.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Debuffs
{
    public class Electrified : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Electrified");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<EpicGlobalNPC>().Electrified = true;
        }
    }
}