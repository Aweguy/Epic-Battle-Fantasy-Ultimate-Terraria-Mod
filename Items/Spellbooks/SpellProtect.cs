using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Spellbooks
{
	public class SpellProtect : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Protection Spell");
			Tooltip.SetDefault("This spell vastly protects you from enemy attacks.\nBlocks 25% of the damage received.");
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.useTime = 100;
			item.useAnimation = 10;
			item.mana = 5;
			item.rare = ItemRarityID.Yellow;
			item.value = Item.sellPrice(silver: 50);
			item.useTurn = true;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Protect").WithVolume(.5f).WithPitchVariance(1f);
		}

		public override bool CanUseItem(Player player)
		{
			int buff = mod.BuffType("Vulnerable");
			return !player.HasBuff(buff);
		}

		public override bool UseItem(Player player)
		{
			player.AddBuff(mod.BuffType("Protection"), 60 * 10);
			player.AddBuff(mod.BuffType("Vulnerable"), 60 * 40);

			return base.UseItem(player);
		}
	}
}