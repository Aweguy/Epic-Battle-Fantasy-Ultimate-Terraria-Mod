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
	public class Bow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bow");
		}

		public override void SetDefaults()
		{

			item.useStyle = 5;
			item.useAnimation = 30;
			item.useTime = 30;
			item.shootSpeed = 20f;
			item.knockBack = 2f;
			item.width = 50;
			item.height = 18;
			item.damage = 60;
			//item.reuseDelay = 30;
			item.shoot = ModContent.ProjectileType<BowProj>();
			item.value = 150000;
			item.rare = 3;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.ranged = true;
			item.channel = true;
			item.useAmmo = AmmoID.Arrow;

			//item.autoReuse = true;

		}

		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = ModContent.ProjectileType<BowProj>();
			position = player.Center;
			for (int l = 0; l < Main.projectile.Length; l++)
			{                                                    //this make so you can only spawn one of this projectile at the time,
				Projectile proj = Main.projectile[l];
				if (proj.active && proj.type == item.shoot && proj.owner == player.whoAmI)
				{
					return false;
				}
			}
			return true;
		}

		public override bool ConsumeAmmo(Player player)
		{
			return false;
		}
	}
}
