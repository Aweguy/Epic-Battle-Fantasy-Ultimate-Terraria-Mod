using EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.ScarletCaster;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.SignatureItems
{
	public class ScarletCaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scarlet Caster");
			Tooltip.SetDefault("A torch-like staff that shoots two fireballs and a seeking firebat\n[c/FF0000:By Nab]");
			Item.staff[Item.type] = true;
		}
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
			Item.damage = 67;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.mana = 10;
			Item.noMelee = true;
			Item.DamageType = DamageClass.Magic;
			Item.autoReuse = true;
			Item.rare = -12;
			Item.shoot = ModContent.ProjectileType<ScarletFireball>();
			Item.shootSpeed = 9.6f;
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Projectile.NewProjectile(source,position, velocity, ModContent.ProjectileType<HellwingV2>(), damage, knockback, player.whoAmI, 0f, 0f);

			int degrees = Main.rand.Next(10);
			float numberProjectiles = 2; // 2 shots
			float rotation = MathHelper.ToRadians(10); //10 degrees spread
			position += Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .75f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(source,position, perturbedSpeed, ModContent.ProjectileType<ScarletFireball>(), damage, knockback, player.whoAmI);
			}
			return false;
		}
	}
}