using EpicBattleFantasyUltimate.Items.Weapons.Swords.Level1Swords;
using EpicBattleFantasyUltimate.Items.Spellbooks;
using EpicBattleFantasyUltimate.Items.Spells;
using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using static Terraria.ModLoader.ModContent;



namespace EpicBattleFantasyUltimate
{
    public class EpicWorld : ModWorld
    {

        #region PostWorldGen

        public override void PostWorldGen()
		{
            #region Sky Chests
            int[] itemsToPlaceInSkyChests = { ItemType<AethersBlade>() };
				int itemsToPlaceInSkyChestsChoice = 0;
				for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
				{
					Chest chest = Main.chest[chestIndex];
					// If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. 
					if (Main.rand.NextFloat() < .33f && chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 13 * 36)
					{
						for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
						{
							if (chest.item[inventoryIndex].type == 0)
							{
								chest.item[inventoryIndex].SetDefaults(itemsToPlaceInSkyChests[itemsToPlaceInSkyChestsChoice]);
								itemsToPlaceInSkyChestsChoice = (itemsToPlaceInSkyChestsChoice + 1) % itemsToPlaceInSkyChests.Length;
								// Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
								break;
							}
						}
					}
				}
            #endregion




            #region Ice Chests
            int[] itemsToPlaceInIceChests = { ItemType<SpellHaste>() };
				int itemsToPlaceInIceChestsChoice = 0;
				for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
				{
					Chest chest = Main.chest[chestIndex];
					// If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. 
					if (Main.rand.NextFloat() < .33f && chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 11 * 36)
					{
						for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
						{
							if (chest.item[inventoryIndex].type == 0)
							{
								chest.item[inventoryIndex].SetDefaults(itemsToPlaceInIceChests[itemsToPlaceInIceChestsChoice]);
								itemsToPlaceInIceChestsChoice = (itemsToPlaceInIceChestsChoice + 1) % itemsToPlaceInIceChests.Length;
								// Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
								break;
							}
						}
					}
				}
            #endregion




            #region Gold Chests
            int[] itemsToPlaceInGoldChests = { ItemType<SpellProtect>() };
			int itemsToPlaceInGoldChestsChoice = 0;
			for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
			{
				Chest chest = Main.chest[chestIndex];
				// If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. 
				if (Main.rand.NextFloat() < .33f && chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 1 * 36)
				{
					for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
					{
						if (chest.item[inventoryIndex].type == 0)
						{
							chest.item[inventoryIndex].SetDefaults(itemsToPlaceInGoldChests[itemsToPlaceInGoldChestsChoice]);
							itemsToPlaceInGoldChestsChoice = (itemsToPlaceInGoldChestsChoice + 1) % itemsToPlaceInGoldChests.Length;
							// Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
							break;
						}
					}
				}
			}
            #endregion




            #region Locked Gold Chests
            int[] itemsToPlaceInLockedGoldChests = { ItemType<Airstrike>() };
			int itemsToPlaceInLockedGoldChestsChoice = 0;
			for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
			{
				Chest chest = Main.chest[chestIndex];
				// If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. 
				if (Main.rand.NextFloat() < .01f && chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 2 * 36)
				{
					for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
					{
						if (chest.item[inventoryIndex].type == 0)
						{
							chest.item[inventoryIndex].SetDefaults(itemsToPlaceInLockedGoldChests[itemsToPlaceInLockedGoldChestsChoice]);
							itemsToPlaceInLockedGoldChestsChoice = (itemsToPlaceInLockedGoldChestsChoice + 1) % itemsToPlaceInLockedGoldChests.Length;
							// Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
							break;
						}
					}
				}
			}
            #endregion




            #region Marble Chests
            int[] itemsToPlaceInMarbleChests = { ItemType<MoonPearl>() };
			int itemsToPlaceInMarbleChestsChoice = 0;
			for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
			{
				Chest chest = Main.chest[chestIndex];
				// If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. 
				if (Main.rand.NextFloat() < .50f && chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 51 * 36)
				{
					for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
					{
						if (chest.item[inventoryIndex].type == 0)
						{
							chest.item[inventoryIndex].SetDefaults(itemsToPlaceInMarbleChests[itemsToPlaceInMarbleChestsChoice]);
							itemsToPlaceInMarbleChestsChoice = (itemsToPlaceInMarbleChestsChoice + 1) % itemsToPlaceInMarbleChests.Length;
							// Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInIceChests));
							break;
						}
					}
				}
			}
            #endregion




        }

        #endregion


    }
}
