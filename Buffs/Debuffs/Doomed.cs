using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace EpicBattleFantasyUltimate.Buffs.Debuffs
{
	class Doomed : ModBuff
	{
			public override void SetStaticDefaults()
			{
				DisplayName.SetDefault("Doomed");
				Description.SetDefault("HAPPY DOOMED YEAR :D!!!");
				Main.debuff[Type] = false;
			}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<EpicPlayer>().numberOfDrawableBuffs++;

			player.GetModPlayer<EpicPlayer>().Doom = true;
		}
	}
}
