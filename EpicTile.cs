using EpicBattleFantasyUltimate.NPCs.Ores;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate
{
    public class EpicTile : GlobalTile
    {
        public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            Player player = Main.LocalPlayer;

            #region Zircon Spawn

            if (Main.tile[i, j].type == TileID.IceBlock)
            {
                if (Main.rand.NextFloat() < .001)
                {
                    int npcToSpawn = ModContent.NPCType<ZirconOre>();

                    Vector2 spawnPos = new Vector2(i * 16, j * 16);

                    int npcIndex = NPC.NewNPC((int)(spawnPos.X), (int)(spawnPos.Y), npcToSpawn, 0, 0f, 0f, 0f, 0f, 255);
                }
            }

            #endregion Zircon Spawn

            #region Peridot Spawn

            if (Main.tile[i, j].type == TileID.Emerald)
            {
                if (Main.rand.NextFloat() < .01)
                {
                    int npcToSpawn = ModContent.NPCType<PeridotOre>();

                    Vector2 spawnPos = new Vector2(i * 16, j * 16);
                }
            }

            #endregion Peridot Spawn

            #region Quartz Spawn

            if (Main.tile[i, j].type == TileID.Diamond)
            {
                if (Main.rand.NextFloat() < .01)
                {
                    int npcToSpawn = ModContent.NPCType<QuartzOre>();

                    Vector2 spawnPos = new Vector2(i * 16, j * 16);

                    int npcIndex = NPC.NewNPC((int)(spawnPos.X), (int)(spawnPos.Y), npcToSpawn, 0, 0f, 0f, 0f, 0f, 255);
                }
            }

            #endregion Quartz Spawn

            #region Ruby Spawn

            if (Main.tile[i, j].type == TileID.Ruby)
            {
                if (Main.rand.NextFloat() < .01)
                {
                    int npcToSpawn = ModContent.NPCType<RubyOre>();

                    Vector2 spawnPos = new Vector2(i * 16, j * 16);

                    int npcIndex = NPC.NewNPC((int)(spawnPos.X), (int)(spawnPos.Y), npcToSpawn, 0, 0f, 0f, 0f, 0f, 255);
                }
            }

            #endregion Ruby Spawn

            #region Amethyst Spawn

            if (Main.tile[i, j].type == TileID.Amethyst)
            {
                if (Main.rand.NextFloat() < .01)
                {
                    int npcToSpawn = ModContent.NPCType<QuartzOre>();

                    Vector2 spawnPos = new Vector2(i * 16, j * 16);

                    int npcIndex = NPC.NewNPC((int)(spawnPos.X), (int)(spawnPos.Y), npcToSpawn, 0, 0f, 0f, 0f, 0f, 255);
                }
            }

            #endregion Amethyst Spawn
        }
    }
}