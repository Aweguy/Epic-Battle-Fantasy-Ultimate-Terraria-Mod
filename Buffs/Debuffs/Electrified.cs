using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.NPCs;

namespace EpicBattleFantasyUltimate.Buffs.Debuffs
{
    public class Electrified : ModBuff
    {

        public override void SetDefaults()
        {
            DisplayName.SetDefault("Electrified");
            Main.debuff[Type] = true;
            canBeCleared = false;

        }


        

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<EpicGlobalNPC>().Electrified = true;
        }















    }
}
