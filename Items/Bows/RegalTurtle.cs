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
    class RegalTurtle : EpicBow
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Regal Turtle");
            Tooltip.SetDefault("Is it a shield, is it a bow?");
        }

        public override void SetDefaults()
        {
            BowProj = ModContent.ProjectileType<RegalTurtleProj>();

            item.width = 26;
            item.height = 66;

            item.damage = 45;
            item.knockBack = 5;

            item.useAnimation = 20;
            item.useTime = 20;

            item.shootSpeed = 10f;
            item.value = 150000;
            item.rare = ItemRarityID.Orange;
        }

        /*public override void AddRecipes()
		{
			base.AddRecipes();
		}*/
    }
}
