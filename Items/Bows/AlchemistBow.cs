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

            item.width = 26;
            item.height = 70;

            item.damage = 30;
            item.knockBack = 3f;

            item.useAnimation = 15;
            item.useTime = 15;
            item.useStyle = ItemUseStyleID.HoldingOut;

            item.shootSpeed = 15f;
            item.value = 150000;
            item.rare = ItemRarityID.Orange;
        }

        /*public override void AddRecipes()
		{
			base.AddRecipes();
		}*/
    }
}
