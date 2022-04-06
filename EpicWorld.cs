using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Spellbooks;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using Terraria.Chat;

namespace EpicBattleFantasyUltimate
{
    public class EpicWorld : ModSystem
    {
        public static bool OreEvent = false;

        public static int OreKills = 0;

        public static int MaxOreKills = 100;

        public static int MaxOreKillsHard = 150;

        public static bool downedOres;

        public static bool downedOresHard;

        public static int bossesDefeated = 0;

        public static bool downedWraith;

        public static int downedWraithTimes;

        public override void OnWorldLoad()
        {
            downedOres = false;

            downedOresHard = false;

            bossesDefeated = 0;
        }

        public override void OnWorldUnload()
        {
            downedOres = false;

            downedOresHard = false;

            bossesDefeated = 0;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            new TagCompound
            {
                {"downedOres", downedOres},
                {"downedOresHard", downedOresHard},
                {"bossesDefeated", bossesDefeated },
            };
        }

        public override void LoadWorldData(TagCompound tag)
        {
            downedOres = tag.GetBool("downedOres");
            downedOresHard = tag.GetBool("downedOresHard");
            bossesDefeated = tag.Get<int>("bossesDefeated");
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[5] = downedOres;
            flags[6] = downedOresHard;
            writer.Write(flags);
            flags = new BitsByte();
            flags[4] = OreEvent;
            writer.Write(flags);
            writer.Write(OreKills);
        }
        
        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedOres = flags[5];
            downedOresHard = flags[6];
            downedOresHard = flags[7];
            flags = reader.ReadByte();
            OreEvent = flags[4];
            OreKills = reader.ReadInt32();
        }

        public override void PreUpdateInvasions()
        {
            #region Ore Event

            MaxOreKills = 100;
            MaxOreKillsHard = 150;

            if (!Main.hardMode)
            {
                if (OreKills >= MaxOreKills && OreEvent)
                {
                    OreEvent = false;
                    downedOres = true;
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                    string key = "The ores have been defeated";
                    Color messageColor = Color.Orange;
                    if (Main.netMode == NetmodeID.Server) // Server
                    {
                        ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
                    {
                        Main.NewText(Language.GetTextValue(key), messageColor);
                    }
                    OreKills = 0;
                }
            }
            else
            {
                if (OreKills >= MaxOreKillsHard)
                {
                    OreEvent = false;
                    downedOresHard = true;
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                    string key = "The ores have been defeated";
                    Color messageColor = Color.Orange;
                    if (Main.netMode == NetmodeID.Server) // Server
                    {
                        ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                    }
                    else if (Main.netMode == NetmodeID.SinglePlayer) // Single Player
                    {
                        Main.NewText(Language.GetTextValue(key), messageColor);
                    }
                    OreKills = 0;
                }
            }

            #endregion Ore Event
        }

        public override void PostWorldGen()
        {
            #region Ice Chests

            int[] itemsToPlaceInIceChests = { ItemType<SpellHaste>() };
            int itemsToPlaceInIceChestsChoice = 0;
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding.
                if (Main.rand.NextFloat() < .33f && chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers && Main.tile[chest.x, chest.y].TileFrameX == 11 * 36)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInIceChests[itemsToPlaceInIceChestsChoice]);
                            itemsToPlaceInIceChestsChoice = (itemsToPlaceInIceChestsChoice + 1) % itemsToPlaceInIceChests.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
                            break;
                        }
                    }
                }
            }

            #endregion Ice Chests

            #region Gold Chests

            int[] itemsToPlaceInGoldChests = { ItemType<SpellProtect>() };
            int itemsToPlaceInGoldChestsChoice = 0;
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding.
                if (Main.rand.NextFloat() < .33f && chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers && Main.tile[chest.x, chest.y].TileFrameX == 1 * 36)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInGoldChests[itemsToPlaceInGoldChestsChoice]);
                            itemsToPlaceInGoldChestsChoice = (itemsToPlaceInGoldChestsChoice + 1) % itemsToPlaceInGoldChests.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
                            break;
                        }
                    }
                }
            }

            #endregion Gold Chests

            #region Locked Gold Chests

            /*int[] itemsToPlaceInLockedGoldChests = { ItemType<Airstrike>() };
            int itemsToPlaceInLockedGoldChestsChoice = 0;
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding.
                if (Main.rand.NextFloat() < .01f && chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 2 * 36)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInLockedGoldChests[itemsToPlaceInLockedGoldChestsChoice]);
                            itemsToPlaceInLockedGoldChestsChoice = (itemsToPlaceInLockedGoldChestsChoice + 1) % itemsToPlaceInLockedGoldChests.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
                            break;
                        }
                    }
                }
            }*/

            #endregion Locked Gold Chests

            #region Marble Chests

            int[] itemsToPlaceInMarbleChests = { ItemType<MoonPearl>() };
            int itemsToPlaceInMarbleChestsChoice = 0;
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                // If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding.
                if (Main.rand.NextFloat() < .50f && chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers && Main.tile[chest.x, chest.y].TileFrameX == 51 * 36)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == ItemID.None)
                        {
                            chest.item[inventoryIndex].SetDefaults(itemsToPlaceInMarbleChests[itemsToPlaceInMarbleChestsChoice]);
                            itemsToPlaceInMarbleChestsChoice = (itemsToPlaceInMarbleChestsChoice + 1) % itemsToPlaceInMarbleChests.Length;
                            // Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
                            break;
                        }
                    }
                }
            }

            #endregion Marble Chests
        }
    }
}