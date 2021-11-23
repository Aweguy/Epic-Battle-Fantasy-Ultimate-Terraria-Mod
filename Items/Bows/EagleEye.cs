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
	class EagleEye : EpicBow
	{
		public override void SetSafeStaticDefaults()
		{
			DisplayName.SetDefault("EagleEye");
			Tooltip.SetDefault("Heavy enough to block a blade.");
		}

		public override void SetSafeDefaults()
		{
			BowProj = ModContent.ProjectileType<EagleEyeProj>();

			item.width = 70;
			item.height = 20;

			item.damage = 80;
			item.knockBack = 10;

			item.useAnimation = 30;
			item.useTime = 30;

			item.shootSpeed = 20f;
			item.value = 150000;
			item.rare = ItemRarityID.Orange;
		}
	}
}
