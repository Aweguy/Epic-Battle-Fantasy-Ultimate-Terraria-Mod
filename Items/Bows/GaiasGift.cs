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
    public class GaiasGift : EpicBow
    {
		public override void SetSafeStaticDefaults()
		{
			DisplayName.SetDefault("Gaia's Gift");
			Tooltip.SetDefault("Heavy enough to block a blade.");
		}

		public override void SetSafeDefaults()
		{
			BowProj = ModContent.ProjectileType<GaiasGiftProj>();

			Item.width = 66;
			Item.height = 26;

			Item.damage = 50;
			Item.knockBack = 5;

			Item.useAnimation = 20;
			Item.useTime = 20;

			Item.shootSpeed = 20f;
			Item.value = 150000;
			Item.rare = ItemRarityID.Orange;
		}
	}
}
