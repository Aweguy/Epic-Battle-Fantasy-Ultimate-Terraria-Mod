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
using EpicBattleFantasyUltimate.HelperClasses;
using EpicBattleFantasyUltimate.UI;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;
using EpicBattleFantasyUltimate.Buffs.Debuffs;
using EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.PaintSplatteredBrush;
using System;

namespace EpicBattleFantasyUltimate
{
	public class EpicBattleFantasyUltimate : Mod
	{

		private UserInterface _LimitBreakBarUI;
		internal LimitBreakBar LimitBreakBar;



		private UserInterface _flairUserInterface;
		public FlairSlot SlotUI;



		public static List<int> thrownProjectiles = new List<int>();
		public static List<int> BrushProj = new List<int> { ModContent.ProjectileType<RedBall>(), ModContent.ProjectileType<GreyBall>(), ModContent.ProjectileType<YellowBall>(), ModContent.ProjectileType<GreenBall>(), ModContent.ProjectileType<BlackBall>(), ModContent.ProjectileType<WhiteBall>(), ModContent.ProjectileType<BlueBall>(), ModContent.ProjectileType<IndigoBall>(), ModContent.ProjectileType<VioletBall>(), ModContent.ProjectileType<OrangeBall>(), };

		public static List<int> MasterWraithBasic => new List<int> { ModContent.ProjectileType<FrostBoneShot>(), ModContent.ProjectileType<BoneShot>(), ModContent.ProjectileType<MetalShot>(), ModContent.ProjectileType<ThornSpike>(), ModContent.ProjectileType<CursedSpike>() };
		public static List<int> MasterWraithTouchDebuffs => new List<int> { ModContent.BuffType<RampantBleed>(), BuffID.OnFire, BuffID.Poisoned, BuffID.Chilled, ModContent.BuffType<Cursed>() };



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

		public override void Load()
		{
			// you can only display the ui to the local player -- prevent an error message!
			if (!Main.dedServ)
			{
				_flairUserInterface = new UserInterface();
				SlotUI = new FlairSlot();

				SlotUI.Activate();
				_flairUserInterface.SetState(SlotUI);

				LimitBreakBar = new LimitBreakBar();
				_LimitBreakBarUI = new UserInterface();
				_LimitBreakBarUI.SetState(LimitBreakBar);


			}








			this.ProjEngine = new ProjHelperEngine(this);

			#region OreEvent


			ModTranslation text = CreateTranslation("OresDefeat");
			text.SetDefault("You defeated the ores");
			AddTranslation(text);

			text = CreateTranslation("OreEventStart");
			text.SetDefault("The Ores are ascending");
			AddTranslation(text);

			text = CreateTranslation("OreEventHardStart");
			text.SetDefault("Many Ores are ascending");
			AddTranslation(text);




			#endregion


		}

		public override void UpdateUI(GameTime gameTime)
		{
			if (SlotUI.Visible)
			{
				//If the Equipment page is active
				if(Main.EquipPage == 2)
                {

                }

				_flairUserInterface?.Update(gameTime);

				



			}

			_LimitBreakBarUI?.Update(gameTime);



		}



		// make sure the ui can draw
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));



			if (SlotUI.Visible)
            {
				if (resourceBarIndex != -1)
				{
					layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer("Limit Break", delegate { _LimitBreakBarUI.Draw(Main.spriteBatch, new GameTime()); return true; }, InterfaceScaleType.UI));
				}

			}






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

			EpicWorld modWorld = (EpicWorld)GetModWorld("EpicWorld");
		}

        


        #endregion







        public ProjHelperEngine ProjEngine
		{
			get;
			private set;
		}





		public static EpicBattleFantasyUltimate instance
		{
			get;
			private set;
		}


		public EpicBattleFantasyUltimate()
		{
			if (EpicBattleFantasyUltimate.instance == null)
			{
				EpicBattleFantasyUltimate.instance = this;
			}
		}



		internal enum EpicMessageType : byte
		{
			EpicPlayerSyncPlayer
		}





	


	}
}