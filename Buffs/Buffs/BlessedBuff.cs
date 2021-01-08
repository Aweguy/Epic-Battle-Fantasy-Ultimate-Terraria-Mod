using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.NPCs;

namespace EpicBattleFantasyUltimate.Buffs.Buffs
{
    public class BlessedBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Blessed");
            Description.SetDefault("You are immune to all debuffs");

        }

        public override void Update(Player player, ref int buffIndex)
        {

            player.GetModPlayer<EpicPlayer>().numberOfDrawableBuffs++;

            player.GetModPlayer<EpicPlayer>().Blessed = true;


            for (int j = 0; j < BuffLoader.BuffCount; ++j)
            {

               

                if (Main.debuff[j])
                {

                    player.buffImmune[j] = true;

                }

            }


        }






    }
}
