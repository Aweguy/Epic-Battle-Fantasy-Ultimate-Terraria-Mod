using EpicBattleFantasyUltimate.Buffs.Buffs;
using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Projectiles.LimitBreaks.MothEarth;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.LimitBreaks
{
	public class GaiaSeal : LimitItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gaia's Seal");
			Tooltip.SetDefault("A small emblem given to Greenwood’s defenders. Strikes foes with toxins while Gaia’s blessing shields you from debuffs.\n Gets stronger as you progress further in the game");
		}

		public override void SetSafeDefaults()
		{
			Item.width = 34;
			Item.height = 30;

			Item.damage = 100;
			Item.DamageType = DamageClass.Magic;
			//Item.mana = 100;
			LimitCost = 100;
			Item.buffType = ModContent.BuffType<BlessedBuff>();
			Item.buffTime = 60 * 60;

			Item.shoot = ModContent.ProjectileType<MotherEarth>();
			Item.shootSpeed = 0f;

			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;

			//Item.channel = true; //Channel so that you can held the weapon [Important]
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			//Rainbow Line

			var line = new TooltipLine(Mod, "Gaia's Seal", "LIMIT BREAK!!!")
			{
				OverrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB)
			};
			tooltips.Add(line);
		}

		public override bool? UseItem(Player player)
		{
			for (int i = 0; i < Player.MaxBuffs; ++i)
			{
				if (player.buffType[i] != 0 && Main.debuff[player.buffType[i]])
				{
					player.DelBuff(i);
					i--;
				}
			}

			player.AddBuff(ModContent.BuffType<BlessedBuff>(), 60 * 10);

			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];

				if (!Main.npc[i].active)
				{
					continue;
				}

				npc.AddBuff(BuffID.Poisoned, 60 * 600);

				if (player.whoAmI == Main.myPlayer)
				{
					player.ApplyDamageToNPC(Main.npc[i], Item.damage + (100 * EpicWorld.bossesDefeated), 0f, (npc.Center.X - player.Center.X > 0f).ToDirectionInt(), true);
				}
			}

			return true;
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[ModContent.ProjectileType<MotherEarth>()] < 1 && base.CanUseItem(player);
		}

		/*public override void HoldItem(Player player)
		{
			Item.damage = 100 + (100 * EpicWorld.bossesDefeated);
		}*/
	}
}