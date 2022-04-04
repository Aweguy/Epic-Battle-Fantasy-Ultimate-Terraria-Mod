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
			Item.width = Item.height = 72;

			Item.damage = 40;
			Item.knockBack = 1f;
			Item.DamageType = DamageClass.Melee;;

			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.SwingThrow;

			Item.rare = ItemRarityID.LightPurple;

			Item.shoot = ModContent.ProjectileType<IceNeedleProj>();
			Item.shootSpeed = 16f;

			Item.noUseGraphic = true;

		}

		/*public override void AddRecipes()
		{
			base.AddRecipes();
		}*/
	}
}
