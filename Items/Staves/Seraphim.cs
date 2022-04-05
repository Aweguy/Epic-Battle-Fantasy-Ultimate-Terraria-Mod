using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Projectiles.StaffProjectiles.JudgementLaser;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Staves
{
	public class Seraphim : LimitItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seraphim");
			Tooltip.SetDefault("A glorious staff used by gorgeous angels.\nConsume Limit Break.");
		}

		public override void SetSafeDefaults()
		{
			Item.width = 90;
			Item.height = 90;

			Item.damage = 100;
			Item.DamageType = DamageClass.Magic;
			//Item.mana = 100;
			Item.noMelee = true;
			LimitCost = 25;

			Item.rare = ItemRarityID.Orange;

			Item.useTime = 1;
			Item.useAnimation = 1;
			Item.useStyle = ItemUseStyleID.Shoot;

			//Item.channel = true; //Channel so that you can held the weapon [Important]

			Item.shoot = ModContent.ProjectileType<Judgement>();
			Item.shootSpeed = 1f;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			int num233 = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
			int num234 = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
			if (player.gravDir == -1f)
			{
				num234 = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
			}
			for (; num234 < Main.maxTilesY && Main.tile[num233, num234] != null && !WorldGen.SolidTile2(num233, num234) && Main.tile[num233 - 1, num234] != null && !WorldGen.SolidTile2(num233 - 1, num234) && Main.tile[num233 + 1, num234] != null && !WorldGen.SolidTile2(num233 + 1, num234); num234++)
			{
			}
			Projectile.NewProjectile(source, (float)Main.mouseX + Main.screenPosition.X, (float)(num234 * 16), 0f, 0f, ModContent.ProjectileType<Judgement>(), Item.damage, 0f, player.whoAmI, 0f, 0f);

			return true;
		}
		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[ModContent.ProjectileType<Judgement>()] < 1 && base.CanUseItem(player);
		}
	}
}