using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Guns.Revolvers
{
	public class DiseaseDeployer : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Disease Deployer");
			Tooltip.SetDefault("pending");
		}


		public override void SetDefaults()
		{
			item.width = 50;
			item.height = 30;

			item.damage = 14;
			item.useTime = 15;
			item.useAnimation = 15;
			item.reuseDelay = 2;

			item.crit = 1;
			item.knockBack = 3f;
			item.ranged = true;
			item.noMelee = true;

			item.value = Item.sellPrice(gold: 3);
			item.rare = ItemRarityID.LightPurple;

			item.useAmmo = AmmoID.Bullet;
			item.UseSound = SoundID.Item41;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 12f;
			item.useStyle = ItemUseStyleID.HoldingOut;
		}
	}
}
