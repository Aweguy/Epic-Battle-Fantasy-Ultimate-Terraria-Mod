using EpicBattleFantasyUltimate.ClassTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Launchers
{
	public class SoulPistol : EpicLauncher
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soul Pistol");
			Tooltip.SetDefault("'I could have sworn...you said...eleven steps'- Duelist Larson\nHeals user when damaging foes.");
		}
		public override void SetSafeDefaults()
		{
			Item.width = 84;
			Item.height = 62;

			Item.useTime = 40;
			Item.useAnimation = 40;

			Item.damage = 77;
			Item.crit = 3;
			Item.DamageType = DamageClass.Melee;;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 2);
			Item.rare = ItemRarityID.Purple;

			Item.UseSound = SoundID.Item38;
			Item.shootSpeed = 12f;
		}
		public Projectile shot;
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 34f;
			//Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
			muzzleOffset += new Vector2(0, -10f * player.direction).RotatedBy(muzzleOffset.ToRotation());
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			Vector2 trueSpeed = new Vector2(velocity.X, velocity.Y);
			shot = Main.projectile[Projectile.NewProjectile(source,position.X, position.Y, trueSpeed.X, trueSpeed.Y, type, damage, knockback, player.whoAmI)];
			shot.GetGlobalProjectile<LauncherProjectile>().LifeSteal = true;

			return false;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, -10);
		}
	}
	public partial class LauncherProjectile : GlobalProjectile
	{
		public bool LifeSteal;
		public int HealDamage = 0;

		public void OnHitNPC_LifeSteal(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			if (LifeSteal)
			{
				Player player = Main.player[projectile.owner];

				if (player.statLife < player.statLifeMax2)
				{
					player.statLife += damage / 5;
					player.HealEffect(damage / 5);
				}
			}
		}
	}
}