using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using EpicBattleFantasyUltimate;
using Terraria.Localization;

namespace EpicBattleFantasyUltimate
{
	public class EpicBattleFantasyUltimate : Mod
	{
		public static List<int> thrownProjectiles = new List<int>();

        #region PostSetupContent

        public override void PostSetupContent()
		{
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


		public EpicBattleFantasyUltimate()
		{
		}
	}
}