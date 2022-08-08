using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Projectiles.BowProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace EpicBattleFantasyUltimate.Items.Bows
{
	public class EagleEye : EpicBow
	{
		public override void SetSafeStaticDefaults()
		{
			DisplayName.SetDefault("Eagle Eye");
			Tooltip.SetDefault("Shoots Arrows at extreme speeds");
		}

		public override void SetSafeDefaults()
		{
			BowProj = ModContent.ProjectileType<EagleEyeProj>();

			Item.width = 70;
			Item.height = 20;

			Item.damage = 80;
			Item.knockBack = 10;

			Item.useAnimation = 30;
			Item.useTime = 30;

			Item.shootSpeed = 20f;
			Item.value = 150000;
			Item.rare = ItemRarityID.Orange;
		}
	}
}
