using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;


namespace EpicBattleFantasyUltimate.Buffs.Buffs
{
    public class Protection : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Protection");
            Description.SetDefault("For all the years that Matt has been eating and standing still in his house\nhe has aquired a new magical ability that protects him by a quarter.\nNow he has give that ability to you. Don't worry you won't have to eat a lot\nnor stay still.");
        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.endurance += 0.25f;
        }
    }
}
