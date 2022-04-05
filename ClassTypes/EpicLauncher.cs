using EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs;
using EpicBattleFantasyUltimate.Items.Ammo.Shots;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace EpicBattleFantasyUltimate.ClassTypes
{
	public abstract class EpicLauncher : ModItem
	{
		public virtual void SetSafeDefaults()
		{
		}

		public sealed override void SetDefaults()
		{
			SetSafeDefaults();

			Item.shoot = ProjectileID.PurificationPowder;
			Item.useAmmo = ItemType<Shot>();
			Item.useStyle = ItemUseStyleID.Shoot;
		}

		public override bool CanUseItem(Player player)
		{
			int buff = ModContent.BuffType<Overheat>();
			return !player.HasBuff(buff);
		}
	}
}