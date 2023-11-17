using EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Buffs
{
	public class BlessedBuff : ModBuff
	{
        	public override void SetStaticDefaults()
        	{
			DisplayName.SetDefault("Blessed");
			Description.SetDefault("You have been granted status immunity, Godcat be praised!");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<EpicPlayer>().numberOfDrawableBuffs++;

			player.GetModPlayer<EpicPlayer>().Blessed = true;

			for (int j = 0; j < BuffLoader.BuffCount; ++j)
			{
				if (Main.debuff[j])
				{

					if(j != ModContent.BuffType<BurntMouth>() && j != ModContent.BuffType<Overheat>())
					{
						player.buffImmune[j] = true;
					}
				}
			}
		}
	}
}
