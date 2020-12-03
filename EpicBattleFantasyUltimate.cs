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

		private UserInterface _flairUserInterface;
		public FlairSlot SlotUI;

		private UserInterface _LimitBreakBarUI;
		internal LimitBreakBar LimitBreakBar;



		public static List<int> thrownProjectiles = new List<int>();
		public static List<int> BrushProj = new List<int> {ModContent.ProjectileType<RedBall>(), ModContent.ProjectileType<GreyBall>(), ModContent.ProjectileType<YellowBall>(), ModContent.ProjectileType<GreenBall>(), ModContent.ProjectileType<BlackBall>(), ModContent.ProjectileType<WhiteBall>(), ModContent.ProjectileType<BlueBall>(), ModContent.ProjectileType<IndigoBall>(), ModContent.ProjectileType<VioletBall>(), ModContent.ProjectileType<OrangeBall>(), };

		public static List<int> MasterWraithBasic => new List<int> {ModContent.ProjectileType<FrostBoneShot>(), ModContent.ProjectileType<BoneShot>(), ModContent.ProjectileType<MetalShot>(), ModContent.ProjectileType<ThornSpike>(), ModContent.ProjectileType<CursedSpike>() };
		public static List<int> MasterWraithTouchDebuffs => new List<int> {ModContent.BuffType<RampantBleed>(), BuffID.OnFire, BuffID.Poisoned, BuffID.Chilled, ModContent.BuffType<Cursed>()};



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
			/*if (!Main.dedServ)
			{
				_flairUserInterface = new UserInterface();
				SlotUI = new FlairSlot();

				SlotUI.Activate();
				_flairUserInterface.SetState(SlotUI);
			}*/

			if (!Main.dedServ)
			{

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
			text.SetDefault("The Ores are ascending, many more this time.");
			AddTranslation(text);










			#endregion


		}

        public override void UpdateUI(GameTime gameTime)
        {
			/*if (SlotUI.Visible)
			{
				_flairUserInterface?.Update(gameTime);
			}*/

			_LimitBreakBarUI?.Update(gameTime);

		}



		// make sure the ui can draw
		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			/*// this will draw on the same layer as the inventory
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
			}*/

			int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			if (resourceBarIndex != -1)
			{
				layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer("Limit Break: Limit Break Bar", delegate { _LimitBreakBarUI.Draw(Main.spriteBatch, new GameTime()); return true; }, InterfaceScaleType.UI));
			}

			EpicWorld modWorld = (EpicWorld)GetModWorld("EpicWorld");

            #region OreEvent

            if (EpicWorld.OreEvent)
            {
				int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));

				LegacyGameInterfaceLayer orionProgress = new LegacyGameInterfaceLayer("Ore Ascension",
					delegate
					{
						DrawOreEvent(Main.spriteBatch);
						return true;
					},
					InterfaceScaleType.UI);
				layers.Insert(index, orionProgress);
			}

            #endregion

        }
        #endregion




        public void DrawOreEvent(SpriteBatch spriteBatch)
		{
			if (EpicWorld.OreEvent && !Main.gameMenu)
			{
				float scaleMultiplier = 0.5f + 1 * 0.5f;
				float alpha = 0.5f;
				Texture2D progressBg = Main.colorBarTexture;
				Texture2D progressColor = Main.colorBarTexture;
				Texture2D orionIcon = GetTexture("NPCs/Ores/ZirconOre");
				const string orionDescription = "Ore Ascension";
				Color descColor = new Color(39, 86, 134);

				Color waveColor = new Color(255, 241, 51);
				Color barrierColor = new Color(255, 241, 51);

				try
				{
					//draw the background for the waves counter
					const int offsetX = 20;
					const int offsetY = 20;
					int width = (int)(200f * scaleMultiplier);
					int height = (int)(46f * scaleMultiplier);
					Rectangle waveBackground = Utils.CenteredRectangle(new Vector2(Main.screenWidth - offsetX - 100f, Main.screenHeight - offsetY - 23f), new Vector2(width, height));
					Utils.DrawInvBG(spriteBatch, waveBackground, new Color(63, 65, 151, 255) * 0.785f);

					//draw wave text

					string waveText = Language.GetTextValue("OreAscensionCleared") + (int)(((float)EpicWorld.OreKills / 150f) * 100) + "%";
					Utils.DrawBorderString(spriteBatch, waveText, new Vector2(waveBackground.X + waveBackground.Width / 2, waveBackground.Y), Color.White, scaleMultiplier, 0.5f, -0.1f);

					//draw the progress bar

					if (EpicWorld.OreKills == 0)
					{
					}
					// Main.NewText(MathHelper.Clamp((modWorld.DinoKillCount/modWorld.MaxDinoKillCount), 0f, 1f));
					Rectangle waveProgressBar = Utils.CenteredRectangle(new Vector2(waveBackground.X + waveBackground.Width * 0.5f, waveBackground.Y + waveBackground.Height * 0.75f), new Vector2(progressColor.Width, progressColor.Height));
					Rectangle waveProgressAmount = new Rectangle(0, 0, (int)(progressColor.Width * MathHelper.Clamp(((float)EpicWorld.OreKills / 150f), 0f, 1f)), progressColor.Height);
					Vector2 offset = new Vector2((waveProgressBar.Width - (int)(waveProgressBar.Width * scaleMultiplier)) * 0.5f, (waveProgressBar.Height - (int)(waveProgressBar.Height * scaleMultiplier)) * 0.5f);

					spriteBatch.Draw(progressBg, waveProgressBar.Location.ToVector2() + offset, null, Color.White * alpha, 0f, new Vector2(0f), scaleMultiplier, SpriteEffects.None, 0f);
					spriteBatch.Draw(progressBg, waveProgressBar.Location.ToVector2() + offset, waveProgressAmount, waveColor, 0f, new Vector2(0f), scaleMultiplier, SpriteEffects.None, 0f);

					//draw the icon with the event description

					//draw the background
					const int internalOffset = 6;
					Vector2 descSize = new Vector2(154, 40) * scaleMultiplier;
					Rectangle barrierBackground = Utils.CenteredRectangle(new Vector2(Main.screenWidth - offsetX - 100f, Main.screenHeight - offsetY - 19f), new Vector2(width, height));
					Rectangle descBackground = Utils.CenteredRectangle(new Vector2(barrierBackground.X + barrierBackground.Width * 0.5f, barrierBackground.Y - internalOffset - descSize.Y * 0.5f), descSize);
					Utils.DrawInvBG(spriteBatch, descBackground, descColor * alpha);

					//draw the icon
					int descOffset = (descBackground.Height - (int)(32f * scaleMultiplier)) / 2;
					Rectangle icon = new Rectangle(descBackground.X + descOffset, descBackground.Y + descOffset, (int)(32 * scaleMultiplier), (int)(32 * scaleMultiplier));
					spriteBatch.Draw(orionIcon, icon, Color.White);

					//draw text

					Utils.DrawBorderString(spriteBatch, Language.GetTextValue("OreAscension"), new Vector2(barrierBackground.X + barrierBackground.Width * 0.5f, barrierBackground.Y - internalOffset - descSize.Y * 0.5f), Color.White, 0.80f, 0.3f, 0.4f);
				}
				catch (Exception e)
				{
					ErrorLogger.Log(e.ToString());
				}


			}
		}










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
			if(EpicBattleFantasyUltimate.instance == null)
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