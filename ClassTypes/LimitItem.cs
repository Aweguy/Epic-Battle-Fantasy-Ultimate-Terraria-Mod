﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.ClassTypes
{
    public abstract class LimitItem : ModItem
    {

        public override bool CloneNewInstances => true;


        public int LimitCost = 100;

        public virtual void SetSafeDefaults() { }

        public sealed override void SetDefaults()
        {
            SetSafeDefaults();
        }


        public virtual bool CanUseSpecial(Player player)
        {

            var epicPlayer = EpicPlayer.ModPlayer(player);
            if (epicPlayer.LimitCurrent >= LimitCost)
            {
                epicPlayer.LimitCurrent -= LimitCost;
                return true;
            }
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            return CanUseSpecial(player) && base.CanUseItem(player);
        }










    }
}
