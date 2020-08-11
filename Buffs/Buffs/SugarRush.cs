using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;


namespace EpicBattleFantasyUltimate.Buffs.Buffs
{
    public class SugarRush : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Sugar Rush");
            Description.SetDefault("You want more cake!\n You move and attack faster, resist attacks and have increased life and mana regeneration.\nAlso it increases you maximum health by 50.");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 += 50;
            player.manaRegen += 10;
            player.lifeRegen += 20;
            player.statDefense += 10;
            player.buffTime[buffIndex] = 18000;
            player.meleeSpeed += 0.30f;
        }
    }
}
