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

            Item.width = 26;
            Item.height = 66;

            Item.damage = 45;
            Item.knockBack = 5;

            Item.useAnimation = 20;
            Item.useTime = 20;

            Item.shootSpeed = 10f;
            Item.value = 150000;
            Item.rare = ItemRarityID.Orange;
        }

        /*public override void AddRecipes()
		{
			base.AddRecipes();
		}*/
    }
}
