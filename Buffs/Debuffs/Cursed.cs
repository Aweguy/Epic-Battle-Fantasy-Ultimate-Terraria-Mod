using EpicBattleFantasyUltimate.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Debuffs
{
    public class Cursed : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Cursed");
            Description.SetDefault("What a horrible night to have a defense penalty!");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<EpicGlobalNPC>().Cursed = true;
        }

        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            npc.GetGlobalNPC<EpicGlobalNPC>().CursedStacks++;
            return false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<EpicPlayer>().numberOfDrawableBuffs++;

            player.GetModPlayer<EpicPlayer>().Cursed = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.GetModPlayer<EpicPlayer>().CursedStacks++;

            return false;
        }
    }
}