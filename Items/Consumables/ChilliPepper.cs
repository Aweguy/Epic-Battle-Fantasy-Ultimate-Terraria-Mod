using EpicBattleFantasyUltimate.Buffs.Buffs;
using EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs;
using EpicBattleFantasyUltimate.ClassTypes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Consumables
{
	public class ChilliPepper : LimitItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chilli Pepper");
			Tooltip.SetDefault("An organic alternative to sauce used by Limit Break aficionados.");
		}

		public override void SetSafeDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.useTime = 1;
			Item.useAnimation = 1;
			Item.useTurn = true;

			Item.maxStack = 30;
			Item.consumable = true;
			Item.UseSound = SoundID.Item2;
		}

		public override bool? UseItem(Player player)
		{
			var epicPlayer = EpicPlayer.ModPlayer(player);

			epicPlayer.LimitCurrent = epicPlayer.MaxLimit;

			player.AddBuff(ModContent.BuffType<BurntMouth>(), 60 * 600);
			player.AddBuff(ModContent.BuffType<Infuriated>(), 60 * 10);

			return true;
		}

		public override bool CanUseItem(Player player)
		{
			int buff = ModContent.BuffType<BurntMouth>();
			return !player.HasBuff(buff) && base.CanUseItem(player);
		}
	}
}