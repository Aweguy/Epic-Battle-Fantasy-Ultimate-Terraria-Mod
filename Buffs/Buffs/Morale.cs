using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;


namespace EpicBattleFantasyUltimate.Buffs.Buffs
{
    public class Morale : ModBuff
    {

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Morale");
			Description.SetDefault("You cannot die in one shot if you have  50% or more hp.");
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<EpicPlayer>().numberOfDrawableBuffs++;

			player.GetModPlayer<EpicPlayer>().Morale = true;
		}




	}
}
