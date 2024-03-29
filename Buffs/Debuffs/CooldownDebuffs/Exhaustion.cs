﻿using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Buffs.Debuffs.CooldownDebuffs
{
    public class Exhaustion : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Exhaustion");
            Description.SetDefault("The player is exhausted and cannot use the Haste Spell.");
            Main.debuff[Type] = true;
        }
    }
}