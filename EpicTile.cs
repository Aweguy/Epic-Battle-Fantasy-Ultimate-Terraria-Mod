using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.NPCs.Ores;

namespace EpicBattleFantasyUltimate
{
    public class EpicTile : GlobalTile
    {

        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            Player player = Main.LocalPlayer;


            if(Main.tile[i,j].type == TileID.IceBlock)
            {


                if(Main.rand.NextFloat() < .01)
                {
                    int npcToSpawn = ModContent.NPCType<ZirconOre>();

                    Vector2 spawnPos = new Vector2(i * 16, j * 16);

                    int npcIndex = NPC.NewNPC((int)(spawnPos.X), (int)(spawnPos.Y), npcToSpawn, 0, 0f, 0f, 0f, 0f, 255);

                }
            }
        }








    }
}
