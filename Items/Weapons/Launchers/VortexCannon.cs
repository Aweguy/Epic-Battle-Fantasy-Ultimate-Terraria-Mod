using EpicBattleFantasyUltimate.ClassTypes;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Items.Weapons.Launchers
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
			item.width = 84;
			item.height = 54;

			item.useTime = 30;
			item.useAnimation = 30;
			item.reuseDelay = 20;

			item.damage = 89;
			item.ranged = true;
			item.noMelee = true;

			item.value = Item.sellPrice(gold: 10);
			item.rare = ItemRarityID.Purple;

			item.UseSound = SoundID.Item38;
			item.shootSpeed = 11f;
		}

		public Projectile shot;

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 32f;
			//Added this bit.  gets an initial (0, -8 * player.direction) vector then rotates it to be properly aligned with the rotaiton of the weapon
			muzzleOffset += new Vector2(0, -6f * player.direction).RotatedBy(muzzleOffset.ToRotation());
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}

			Vector2 trueSpeed = new Vector2(speedX, speedY);
			shot = Main.projectile[Projectile.NewProjectile(position.X, position.Y, trueSpeed.X, trueSpeed.Y, type, damage, knockBack, player.whoAmI)];
			shot.GetGlobalProjectile<LauncherProjectile>().B4Homingshot = true;

			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-36, -8);
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IllegalGunParts, 3);
			recipe.AddIngredient(mod.ItemType("P2Processor"), 2);
			recipe.AddIngredient(mod.ItemType("SteelPlate"), 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
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