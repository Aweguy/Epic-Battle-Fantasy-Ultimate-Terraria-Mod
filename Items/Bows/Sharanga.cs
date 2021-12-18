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
	class Sharanga : EpicBow
	{
		public override void SetSafeStaticDefaults()
		{
			DisplayName.SetDefault("Sharanga");
			Tooltip.SetDefault("Heavy enough to block a blade.");
		}

		public override void SetSafeDefaults()
		{
			BowProj = ModContent.ProjectileType<SharangaProj>();

			item.width = 20;
			item.height = 70;

			item.damage = 55;
			item.knockBack = 10;

			item.useAnimation = 30;
			item.useTime = 30;

			item.shootSpeed = 20f;
			item.value = 150000;
			item.rare = ItemRarityID.Orange;
		}
	}
}
