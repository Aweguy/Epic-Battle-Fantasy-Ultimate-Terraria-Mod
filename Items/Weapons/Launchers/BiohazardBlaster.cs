using EpicBattleFantasyUltimate.ClassTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
{
	public class BiohazardBlaster : EpicLauncher
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Biohazard Blaster");
			Tooltip.SetDefault("The person who approved this project assumed it was for eliminating potential biohazards. They were sorely mistaken.\nPoisons targets hit.");
		}

		public override void SetSafeDefaults()
		{
			item.width = 112;
			item.height = 56;

			item.useTime = 40;
			item.useAnimation = 40;

			item.damage = 50;
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
			muzzleOffset += new Vector2(0, -12f * player.direction).RotatedBy(muzzleOffset.ToRotation());
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			Vector2 trueSpeed = new Vector2(speedX, speedY);
			shot = Main.projectile[Projectile.NewProjectile(position.X, position.Y, trueSpeed.X, trueSpeed.Y, type, damage, knockBack, player.whoAmI)];
			shot.GetGlobalProjectile<LauncherProjectile>().PoisonedRounds = true;

			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-33, -14);
		}
	}

	public partial class LauncherProjectile : GlobalProjectile
	{
		public bool PoisonedRounds;

		public void OnHitNPC_PoisonedRounds(Projectile projectile, NPC target, int damage, float knockback, bool crit)
		{
			if (PoisonedRounds)
			{
				target.AddBuff(BuffID.Poisoned, 60 * 3);
			}
		}
	}
}