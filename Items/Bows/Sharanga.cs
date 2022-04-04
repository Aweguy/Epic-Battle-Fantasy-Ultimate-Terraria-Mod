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

			Item.width = 20;
			Item.height = 70;

			Item.damage = 55;
			Item.knockBack = 10;

			Item.useAnimation = 30;
			Item.useTime = 30;

			Item.shootSpeed = 20f;
			Item.value = 150000;
			Item.rare = ItemRarityID.Orange;
		}
	}
}
