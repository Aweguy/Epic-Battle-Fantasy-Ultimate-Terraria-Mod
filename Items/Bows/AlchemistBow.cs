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
    class AlchemistBow : EpicBow
    {
        public override void SetSafeStaticDefaults()
        {
            DisplayName.SetDefault("Alchemist's Bow");
            Tooltip.SetDefault("An unusual weapon that selects and shoots random arrows each volley.");
        }

        public override void SetSafeDefaults()
        {
            BowProj = ModContent.ProjectileType<AlchemistBowProj>();

            Item.width = 26;
            Item.height = 70;

            Item.damage = 30;
            Item.knockBack = 3f;

            Item.useAnimation = 15;
            Item.useTime = 15;

            Item.shootSpeed = 15f;
            Item.value = 150000;
            Item.rare = ItemRarityID.Orange;
        }

        /*public override void AddRecipes()
		{
			base.AddRecipes();
		}*/
    }
}
