using EpicBattleFantasyUltimate.Projectiles.Thrown;
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


namespace EpicBattleFantasyUltimate.Items.Spears.Throwable
{
	class IceNeedle : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Needle");
			Tooltip.SetDefault("Cold to the tip\nShoots a javelin that spawns snowflakes at the impact position.");
		}

		public override void SetDefaults()
		{
			item.width = item.height = 72;

			item.damage = 40;
			item.knockBack = 1f;
			item.melee = true;

			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.SwingThrow;

			item.rare = ItemRarityID.LightPurple;

			item.shoot = ModContent.ProjectileType<IceNeedleProj>();
			item.shootSpeed = 16f;

			item.noUseGraphic = true;

		}

		/*public override void AddRecipes()
		{
			base.AddRecipes();
		}*/
	}
}
