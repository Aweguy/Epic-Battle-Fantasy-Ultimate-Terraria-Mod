using Terraria;
using Terraria.ModLoader;
using Terraria.ID;



namespace EpicBattleFantasyUltimate.Buffs.Debuffs
{
    public class Tired : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Tired");
            Description.SetDefault("You are tired because of the battle. You are moving and attacking slower, and jump lower.");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<EpicPlayer>().Tired = true;
        }


        public override bool ReApply(Player player, int time, int buffIndex)
        {

            player.GetModPlayer<EpicPlayer>().TiredStacks += 0.1f;

            return false;
        }
    }
}
