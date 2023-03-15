using EpicBattleFantasyUltimate.Buffs.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Consumables
{
	public class Cake : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cake");
			Tooltip.SetDefault("A cake with sinfully high sugar content. Associated with magical stat boosts and bouncing off the walls.\nGives the Sugar Rush buff after consumed. Lasts until you die");
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 24;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.useTurn = true;
			Item.maxStack = 30;
			Item.consumable = true;
			Item.UseSound = SoundID.Item2;
			Item.buffType = ModContent.BuffType<SugarRush>();
			Item.buffTime = 2;
		}

		public override bool CanUseItem(Player player)
		{
			int buff = ModContent.BuffType<SugarRush>();
			return !player.HasBuff(buff);
		}
	}
}