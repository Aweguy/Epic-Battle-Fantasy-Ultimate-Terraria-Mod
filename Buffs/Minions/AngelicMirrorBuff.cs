using EpicBattleFantasyUltimate.Projectiles.Minions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace EpicBattleFantasyUltimate.Buffs.Minions
{
	public class AngelicMirrorBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Angelic Mirror");
			Description.SetDefault("An Angelic Mirror will fight with you. Providing help in fights, a little mana regeneration\nand a slight defense boost.");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			if (player.ownedProjectileCounts[ProjectileType<AngelicMirror>()] > 0)
			{
				player.buffTime[buffIndex] = 18000;
			}
			else
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}

			player.statDefense += 5;
			player.manaRegen += 7;
		}
	}
}
