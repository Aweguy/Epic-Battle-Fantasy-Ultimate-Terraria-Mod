using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Items
{
    public class EpicItem : GlobalItem
    {


        #region PickAmmo


        public override void PickAmmo(Item item, Player player, ref int type, ref float speed, ref int damage, ref float knockback)
        {

            #region Overheat

            if (item.type == mod.ItemType("Shot9") || item.type == mod.ItemType("Shot8") || item.type == mod.ItemType("Shot7"))
            {
                player.AddBuff(mod.BuffType("Overheat"), 60 * 4);
            }
            else if(item.type == mod.ItemType("Shot6") || item.type == mod.ItemType("Shot5") || item.type == mod.ItemType("Shot4"))
            {
                player.AddBuff(mod.BuffType("Overheat"), 60 * 2);
            }

            #endregion

        }

        #endregion

       






    }
}
