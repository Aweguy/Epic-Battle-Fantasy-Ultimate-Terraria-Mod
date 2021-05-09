using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Accessories
{
	public class AFprototypeArtifact : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Armor Hardener");
			Tooltip.SetDefault("Whoops, is Astroflux in Terraria now?");
		}

		public override void SetDefaults()
		{
			item.width = 23;
			item.height = 32;
			item.defense = Main.rand.Next(10, 1000);
			item.accessory = true;
			item.rare = ItemRarityID.Purple;
		}

	}
}
