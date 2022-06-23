#region using

using EpicBattleFantasyUltimate.Buffs.Debuffs;
using EpicBattleFantasyUltimate.NPCs.Wraiths;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;
using EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.PaintSplatteredBrush;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

#endregion

namespace EpicBattleFantasyUltimate
{
	public class EpicBattleFantasyUltimate : Mod
	{
		//private UserInterface _LimitBreakBarUI;
		//internal LimitBreakBar LimitBreakBar;

		public static List<int> thrownProjectiles = new List<int>();

		public static List<int> BrushProj = new List<int> { ModContent.ProjectileType<RedBall>(), ModContent.ProjectileType<GreyBall>(), ModContent.ProjectileType<YellowBall>(), ModContent.ProjectileType<GreenBall>(), ModContent.ProjectileType<BlackBall>(), ModContent.ProjectileType<WhiteBall>(), ModContent.ProjectileType<BlueBall>(), ModContent.ProjectileType<IndigoBall>(), ModContent.ProjectileType<VioletBall>(), ModContent.ProjectileType<OrangeBall>(), };

		public static List<int> MasterWraithBasic => new List<int> { ModContent.ProjectileType<FrostBoneShot>(), ModContent.ProjectileType<BoneShot>(), ModContent.ProjectileType<MetalShot>(), ModContent.ProjectileType<ThornSpike>(), ModContent.ProjectileType<CursedSpike>() };
		public static List<int> MasterWraithTouchDebuffs => new List<int> { ModContent.BuffType<RampantBleed>(), BuffID.OnFire, BuffID.Poisoned, BuffID.Chilled, ModContent.BuffType<Cursed>() };
		public static List<int> MasterWraithSummoning => new List<int> { ModContent.NPCType<FlameWraith>(), ModContent.NPCType<FrostWraith>(), ModContent.NPCType<SteelWraith>(), ModContent.NPCType<LeafWraith>(), ModContent.NPCType<SparkWraith>() };

		public static RecipeGroup GoldBar;


		#region PostSetupContent

		public override void PostSetupContent()
		{
			#region Doodad of the Squire

			for (int i = 0; i < ProjectileLoader.ProjectileCount; i++)
			{
				Projectile projectile = new Projectile();
				projectile.SetDefaults(i);
				if ((projectile.friendly || i == ProjectileID.BoneJavelin || i == ProjectileID.JavelinFriendly || i == ProjectileID.Daybreak) && projectile.ModProjectile is null)
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

		public override void AddRecipeGroups()
		{
			GoldBar = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Gold Bar", new int[]
			{
				ItemID.GoldBar,
				ItemID.PlatinumBar
			});
			RecipeGroup.RegisterGroup("GoldBar", GoldBar);

			RecipeGroup group2 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Titanium Bar", new int[]
			{
				ItemID.TitaniumBar,
				ItemID.AdamantiteBar
			});
			RecipeGroup.RegisterGroup("TitaniumBar", group2);

			RecipeGroup group3 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Silver Bar", new int[]
			{
				ItemID.SilverBar,
				ItemID.TungstenBar
			});
			RecipeGroup.RegisterGroup("SilverBar", group3);

			RecipeGroup group4 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Palladium Bar", new int[]
{
				ItemID.PalladiumBar,
				ItemID.CobaltBar
});
			RecipeGroup.RegisterGroup("PalladiumBar", group4);

			RecipeGroup group5 = new RecipeGroup(() => Language.GetTextValue("LegacyMisc.37") + " Evil Bar", new int[]
{
				ItemID.CrimtaneBar,
				ItemID.DemoniteBar
});
			RecipeGroup.RegisterGroup("EvilBar", group5);
		}

		#region UI

		public override void Load()
		{
			// ...other Load stuff goes here

			if (Main.netMode != NetmodeID.Server)
			{
				Ref<Effect> screenRef = new Ref<Effect>(this.Assets.Request<Effect>("Assets/Effects/ShockwaveEffect", AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
				Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
				Filters.Scene["Shockwave"].Load();
				Filters.Scene["ShockwaveMonolith"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
				Filters.Scene["ShockwaveMonolith"].Load();
			}

			// you can only display the ui to the local player -- prevent an error message!
			/*if (!Main.dedServ)
			{
				LimitBreakBar = new LimitBreakBar();
				_LimitBreakBarUI = new UserInterface();
				_LimitBreakBarUI.SetState(LimitBreakBar);
			}*/

			#region OreEvent
			ModTranslation text = LocalizationLoader.CreateTranslation(this, "OresDefeat");
			text.SetDefault("You defeated the ores");
			LocalizationLoader.AddTranslation(text);

			text = LocalizationLoader.CreateTranslation(this, "OreEventStart");
			text.SetDefault("The Ores are ascending");
			LocalizationLoader.AddTranslation(text);

			text = LocalizationLoader.CreateTranslation(this, "OreEventHardStart");
			text.SetDefault("Many Ores are ascending");
			LocalizationLoader.AddTranslation(text);
			#endregion
		}

		public override void Unload()
		{
			instance = null;
		}


		#endregion

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