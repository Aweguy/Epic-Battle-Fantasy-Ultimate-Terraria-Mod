using EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs;
using EpicBattleFantasyUltimate.Items.Ammo.Shots;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items
{
    public class EpicItem : GlobalItem
    {
        #region PickAmmo

        public override void PickAmmo(Item item, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {
            #region Overheat

            if (type == ModContent.ItemType<Shot9>() || type ==  ModContent.ItemType<Shot8>() || type == ModContent.ItemType<Shot7>())
            {
                player.AddBuff(ModContent.BuffType<Overheat>(), 60 * 4);
            }
            else if (type == ModContent.ItemType<Shot6>() || type == ModContent.ItemType<Shot4>())
            {
                player.AddBuff(ModContent.BuffType<Overheat>(), 60 * 2);
            }

            #endregion Overheat
        }

        #endregion PickAmmo
    }
}