using EpicBattleFantasyUltimate.ClassTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
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
			item.width = 84;
			item.height = 62;

			item.useTime = 40;
			item.useAnimation = 40;

			item.damage = 77;
			item.crit = 3;
			item.melee = true;
			item.noMelee = true;

			item.value = Item.sellPrice(gold: 2);
			item.rare = ItemRarityID.Purple;

			item.UseSound = SoundID.Item38;
			item.shootSpeed = 12f;
		}
		public Projectile shot;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 34f;
			//Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
			muzzleOffset += new Vector2(0, -10f * player.direction).RotatedBy(muzzleOffset.ToRotation());
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			Vector2 trueSpeed = new Vector2(speedX, speedY);
			shot = Main.projectile[Projectile.NewProjectile(position.X, position.Y, trueSpeed.X, trueSpeed.Y, type, damage, knockBack, player.whoAmI)];
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