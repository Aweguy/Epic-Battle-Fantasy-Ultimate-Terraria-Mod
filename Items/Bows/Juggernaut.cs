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
	class Juggernaut : EpicBow
	{
		public override void SetSafeStaticDefaults()
		{
			DisplayName.SetDefault("Juggernaut");
			Tooltip.SetDefault("Heavy enough to block a blade.");
		}

		public override void SetSafeDefaults()
		{
			BowProj = ModContent.ProjectileType<JuggernautProj>();

			item.width = 58;
			item.height = 26;

			item.damage = 60;
			item.knockBack = 10;

			item.useAnimation = 30;
			item.useTime = 30;

			item.shootSpeed = 20f;
			item.value = 150000;
			item.rare = ItemRarityID.Orange;
		}
	}
}
