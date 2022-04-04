using EpicBattleFantasyUltimate.ClassTypes;
using EpicBattleFantasyUltimate.Items.Materials;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Launchers
{
	public class VortexCannon : EpicLauncher
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vortex Cannon");
			Tooltip.SetDefault("A powerful wind turbine. Boeing may want this back.\nProjectiles shot by this weapon implode.\nShots from this weapon home to enemies.");
		}
		public override void SetSafeDefaults()
		{
			Item.width = 84;
			Item.height = 54;

			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.reuseDelay = 20;

			Item.damage = 89;
			Item.DamageType = DamageClass.Ranged;
			Item.noMelee = true;

			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Purple;

			Item.UseSound = SoundID.Item38;
			Item.shootSpeed = 11f;
		}
		public Projectile shot;

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 32f;
			//Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
			muzzleOffset += new Vector2(0, -6f * player.direction).RotatedBy(muzzleOffset.ToRotation());
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			Vector2 trueSpeed = new Vector2(velocity.X, velocity.Y);
			shot = Main.projectile[Projectile.NewProjectile(source,position, trueSpeed, type, damage, knockback, player.whoAmI)];
			shot.GetGlobalProjectile<LauncherProjectile>().B4Homingshot = true;

			return false;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-36, -8);
		}
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.IllegalGunParts, 3)
				.AddIngredient(ModContent.ItemType<P2Processor>(), 2)
				.AddIngredient(ModContent.ItemType<SteelPlate>(), 15)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
	public partial class LauncherProjectile : GlobalProjectile
	{
		public bool B4Homingshot;
		public override bool InstancePerEntity => true;

		public void Homing(Projectile projectile)
		{
			if (B4Homingshot)
			{
				NPC prey;
				NPC possiblePrey;
				float distance;
				float maxDistance = 500f;
				float chaseDirection = projectile.velocity.ToRotation();

				for (int k = 0; k < 200; k++)
				{
					possiblePrey = Main.npc[k];
					distance = (possiblePrey.Center - projectile.Center).Length();
					if (distance < maxDistance && possiblePrey.active && !possiblePrey.dontTakeDamage && !possiblePrey.friendly && possiblePrey.lifeMax > 5 && !possiblePrey.immortal && (Collision.CanHit(projectile.Center, 0, 0, possiblePrey.Center, 0, 0) || !projectile.tileCollide))
					{
						prey = Main.npc[k];

						chaseDirection = (projectile.Center - prey.Center).ToRotation() - (float)Math.PI;
						maxDistance = (prey.Center - projectile.Center).Length();
					}
				}
				float trueSpeed = projectile.velocity.Length();
				float actDirection = projectile.velocity.ToRotation();
				int f = 1;

				chaseDirection = new Vector2((float)Math.Cos(chaseDirection), (float)Math.Sin(chaseDirection)).ToRotation();
				if (Math.Abs(actDirection - chaseDirection) > Math.PI)
				{
					f = -1;
				}
				else
				{
					f = 1;
				}

				if (actDirection <= chaseDirection + MathHelper.ToRadians(8) && actDirection >= chaseDirection - MathHelper.ToRadians(8))
				{
					actDirection = chaseDirection;
				}
				else if (actDirection <= chaseDirection)
				{
					actDirection += MathHelper.ToRadians(4) * f;
				}
				else if (actDirection >= chaseDirection)
				{
					actDirection -= MathHelper.ToRadians(4) * f;
				}
				actDirection = new Vector2((float)Math.Cos(actDirection), (float)Math.Sin(actDirection)).ToRotation();
				projectile.velocity.X = (float)Math.Cos(actDirection) * trueSpeed;
				projectile.velocity.Y = (float)Math.Sin(actDirection) * trueSpeed;
				projectile.rotation = actDirection + (float)Math.PI / 2;
				actDirection = projectile.velocity.ToRotation();
			}
		}
	}
}