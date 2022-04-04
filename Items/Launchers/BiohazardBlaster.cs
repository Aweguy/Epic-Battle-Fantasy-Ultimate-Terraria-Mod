using EpicBattleFantasyUltimate.ClassTypes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Launchers
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
			Item.width = 112;
			Item.height = 56;

			Item.useTime = 40;
			Item.useAnimation = 40;

			Item.damage = 50;
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
			muzzleOffset += new Vector2(0, -12f * player.direction).RotatedBy(muzzleOffset.ToRotation());
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			Vector2 trueSpeed = new Vector2(velocity.X, velocity.Y);
			shot = Main.projectile[Projectile.NewProjectile(source, position, trueSpeed, type, damage, knockback, player.whoAmI)];
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