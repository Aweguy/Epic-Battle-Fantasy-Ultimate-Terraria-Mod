using EpicBattleFantasyUltimate.Buffs.Buffs;
using EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Spellbooks
{
	public class SpellProtect : ModItem
	{
		public static readonly SoundStyle ProtectSound = new("EpicBattleFantasyUltimate/Assets/Sounds/Item/Protect")
		{
			Volume = 2f,
			PitchVariance = 1f
		};


		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Protection Spell");
			Tooltip.SetDefault("This spell vastly protects you from enemy attacks.\nBlocks 25% of the damage received.");
		}

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 100;
			Item.useAnimation = 10;
			Item.mana = 5;
			Item.rare = ItemRarityID.Yellow;
			Item.value = Item.sellPrice(silver: 50);
			Item.useTurn = true;
			if (!Main.dedServ)
			{
				Item.UseSound = ProtectSound;
			}
		}

		public override bool CanUseItem(Player player)
		{
			int buff = ModContent.BuffType<Vulnerable>();
			return !player.HasBuff(buff);
		}

		public override bool? UseItem(Player player)
		{
			player.AddBuff(ModContent.BuffType<Protection>(), 60 * 10);
			player.AddBuff(ModContent.BuffType<Vulnerable>(), 60 * 40);

			return base.UseItem(player);
		}
	}
}