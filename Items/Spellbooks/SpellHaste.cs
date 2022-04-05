using EpicBattleFantasyUltimate.Buffs.Buffs;
using EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Spellbooks
{
	public class SpellHaste : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Haste Spell");
			Tooltip.SetDefault("This spell vastly increases speed of all your actions.");
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 100;
			Item.useAnimation = 10;
			Item.mana = 5;
			Item.value = Item.sellPrice(silver: 60);
			Item.rare = ItemRarityID.Yellow;
			Item.useTurn = true;
			Item.UseSound = SoundID.Item29;
		}

		public override bool CanUseItem(Player player)
		{
			int buff = ModContent.BuffType<Exhaustion>();
			return !player.HasBuff(buff);
		}

		public override bool? UseItem(Player player)
		{
			player.AddBuff(ModContent.BuffType<HasteBuff>(), 60 * 10);
			player.AddBuff(ModContent.BuffType<Exhaustion>(), 60 * 60);

			return base.UseItem(player);
		}
	}
}