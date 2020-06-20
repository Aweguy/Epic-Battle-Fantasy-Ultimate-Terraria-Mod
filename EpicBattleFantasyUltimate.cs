using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using EpicBattleFantasyUltimate;
using Terraria.Localization;
using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using EpicBattleFantasyUltimate.UI.FlairSlots;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace EpicBattleFantasyUltimate
{
	public class EpicBattleFantasyUltimate : Mod
	{

		//private UserInterface _flairUserInterface;
		//public FlairSlot SlotUI;
		

        public static List<int> thrownProjectiles = new List<int>();

	


		#region PostSetupContent

		public override void PostSetupContent()
		{

            #region Doodad of the Squire

            for (int i = 0; i < ProjectileLoader.ProjectileCount; i++)
			{
				Projectile projectile = new Projectile();
				projectile.SetDefaults(i);
				if (projectile.thrown && (projectile.friendly || i == ProjectileID.BoneJavelin || i == ProjectileID.JavelinFriendly || i == ProjectileID.Daybreak) && projectile.modProjectile is null)
				{
					if (i != ProjectileID.Beenade && i != ProjectileID.SpikyBall)
					{
						thrownProjectiles.Add(i);
					}
				}
			}

            #endregion

        }

        #endregion

        #region AddRecipeGroups

        public override void AddRecipeGroups()
		{
			RecipeGroup group = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gold Bar", new int[]
			{
				ItemID.GoldBar,
				ItemID.PlatinumBar
			});
			RecipeGroup.RegisterGroup("EpicBattleFantasyUltimate:Gold", group);

			RecipeGroup group2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Titanium Bar", new int[]
			{
				ItemID.TitaniumBar,
				ItemID.AdamantiteBar
			});
			RecipeGroup.RegisterGroup("EpicBattleFantasyUltimate:Titanium", group2);


			RecipeGroup group3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Silver Bar", new int[]
			{
				ItemID.SilverBar,
				ItemID.TungstenBar
			});
			RecipeGroup.RegisterGroup("EpicBattleFantasyUltimate:Silver", group3);


			RecipeGroup group4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Palladium Bar", new int[]
{
				ItemID.PalladiumBar,
				ItemID.CobaltBar
});
			RecipeGroup.RegisterGroup("EpicBattleFantasyUltimate:Palladium", group4);



			RecipeGroup group5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Bar", new int[]
{
				ItemID.CrimtaneBar,
				ItemID.DemoniteBar
});
			RecipeGroup.RegisterGroup("EpicBattleFantasyUltimate:Evil", group5);






		}

		#endregion




		#region UI

		/*public override void Load()
		{
			// you can only display the ui to the local player -- prevent an error message!
			if (!Main.dedServ)
			{
				_flairUserInterface = new UserInterface();
				SlotUI = new FlairSlot();

				SlotUI.Activate();
				_flairUserInterface.SetState(SlotUI);
			}
		}

        public override void UpdateUI(GameTime gameTime)
        {
			if (SlotUI.Visible)
			{
				_flairUserInterface?.Update(gameTime);
			}
		}



        // make sure the ui can draw
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			// this will draw on the same layer as the inventory
			int inventoryLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));

			if (inventoryLayer != -1)
			{
				layers.Insert(
					inventoryLayer,
					new LegacyGameInterfaceLayer("My Mod: My Slot UI", () => {
						if (SlotUI.Visible)
						{
							_flairUserInterface.Draw(Main.spriteBatch, new GameTime());
						}

						return true;
					},
					InterfaceScaleType.UI));
			}
		}*/










		#endregion




















		public EpicBattleFantasyUltimate()
		{
		}
	}
}