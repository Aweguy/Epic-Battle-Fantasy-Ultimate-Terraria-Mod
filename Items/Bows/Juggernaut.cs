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

			Item.width = 58;
			Item.height = 26;

			Item.damage = 60;
			Item.knockBack = 10;

			Item.useAnimation = 30;
			Item.useTime = 30;

			Item.shootSpeed = 20f;
			Item.value = 150000;
			Item.rare = ItemRarityID.Orange;
		}
	}
}
