using EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs;
using EpicBattleFantasyUltimate.Items.Ammo.Shots;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items
{
	public class EpicItem : GlobalItem
	{

        public override void PickAmmo(Item weapon, Item ammo, Player player, ref int type, ref float speed, ref StatModifier damage, ref float knockback)
        {
            #region Overheat
            if (ammo.type == ModContent.ItemType<Shot9>() || ammo.type == ModContent.ItemType<Shot8>() || ammo.type == ModContent.ItemType<Shot7>())
			{
				player.AddBuff(ModContent.BuffType<Overheat>(), 60 * 4);
			}
			else if (ammo.type == ModContent.ItemType<Shot6>() || ammo.type == ModContent.ItemType<Shot4>())
			{
				player.AddBuff(ModContent.BuffType<Overheat>(), 60 * 2);
			}
			#endregion Overheat
		}
	}
}