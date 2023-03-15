/*using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Items.Materials.Gems;
using EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.DigiDevWep;
using EpicBattleFantasyUltimate.Projectiles.SwordProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.SignatureItems
{
	public class DigiDevWep: ModItem
	{
		int GunNumber = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			Item.width = 10;
			Item.height = 10;

			Item.damage = 100;
			Item.knockBack = 1;
			Item.DamageType = DamageClass.Ranged;

			Item.useTime = 30;//Attack speed in frames, the lower the faster
			Item.useAnimation = 30;//For how many frames it will keep shooting, UseTime and UseAnimation have interactions, we'll keep it the same as UseTime in that wep unless you want burst fire
			Item.autoReuse = true;//Whether the item will be reused with the mouse click held.
			Item.useStyle = ItemUseStyleID.Shoot;//Type of animation, since gun, we use gun

			Item.shoot = ModContent.ProjectileType<DigiDevWep_Proj>();//Which projectile will be shot
			Item.shootSpeed = 6f;//How fast the projectile will travel
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<DigiDevWep_Proj>(), Item.damage, Item.knockBack, Main.myPlayer, player.whoAmI);

			GunNumber++;
			return false;
		}

		public override bool? UseItem(Player player)
		{
			for(int i = 0; i <= Main.maxProjectiles;i++)
			{
				Projectile projectile = Main.projectile[i];
				if(projectile.type == ModContent.ProjectileType<DigiDevWep_Proj>())
				{
					if(GunNumber > 6)
					{
						projectile.Kill();
					}
				}
			}
			return true;
		}
	}
}
*/