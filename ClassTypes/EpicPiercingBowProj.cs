using EpicBattleFantasyUltimate.Dusts;
using EpicBattleFantasyUltimate.HelperClasses;
using EpicBattleFantasyUltimate.Projectiles.BowProjectiles;
using EpicBattleFantasyUltimate.Projectiles.BowProjectiles.Arrows;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace EpicBattleFantasyUltimate.ClassTypes
{
	public abstract class EpicPiercingBowProj : ModProjectile
	{
		#region Variables
		public int timer = 0;//The current charge value
		public float direction;

		public float Radd;
		public bool runOnce = true;
		public Projectile arrow = null;
		public float speed;
		public float speedMultiplier;//Speed multiplier of the arrows will be set in the safe defaults of each bow Projectile.
		public float velocity;//The velocity multiplier of the arrows will be set in the defaults of each bow Projectile.
		public int maxTime;//The max charge time will be set in the safe defaults of each bow Projectile
		public int weaponDamage;//The weapon damage will be set in the safe defaults of each bow Projectile
		public int MinimumDrawTime;//The minimum time that the player will hold the bow will be set in the safe defaults of each bow Projectile.
		public float DamageMultiplier;//The damage multiplier of the arrows will be set in the safe defaults of each bow Projectile.
		public int Ammo = 0;
		public float weaponKnockback;//The weapon knockback will be set in the safe defaults of each bow Projectile
		public bool giveTileCollision = false;
		#endregion//The variables of the bow

		#region SafeMethods
		public override bool CloneNewInstances => true;
		public virtual void SetSafeDefaults()
		{
		}
		public virtual void SetSafeStaticDefaults()
		{
		}
		public virtual void SafeKill()
		{
		}
		public virtual void SafePreAI()
		{
		}
		#endregion

		public override void SetStaticDefaults()
		{
			SetSafeStaticDefaults();
		}

		public override void SetDefaults()
		{
			SetSafeDefaults();

			Projectile.friendly = false;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.hide = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.ignoreWater = true;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			if (runOnce)
			{
			runOnce = false;
			}
			Projectile.timeLeft = 2;

			var modPlayer = player.GetModPlayer<EpicPlayer>();
			bool firing = (player.channel || timer < MinimumDrawTime) && player.HasAmmo(player.HeldItem, true) && !player.noItems && !player.CCed;

			Ammo = AmmoID.Arrow;

			weaponDamage += player.GetWeaponDamage(player.inventory[player.selectedItem]);
			direction = (Main.MouseWorld - player.Center).ToRotation();
			weaponKnockback = player.inventory[player.selectedItem].knockBack;

			if (firing)
			{
				#region drill ai

				///////////////////////////////////// copied from vanilla drill/chainsaw AI
				Vector2 vector24 = Main.player[Projectile.owner].RotatedRelativePoint(Main.player[Projectile.owner].MountedCenter, true);
				if (Main.myPlayer == Projectile.owner)
				{
					if (Main.player[Projectile.owner].channel || timer < MinimumDrawTime)
					{
						float num264 = Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].shootSpeed * Projectile.scale;
						Vector2 vector25 = vector24;
						float num265 = (float)Main.mouseX + Main.screenPosition.X - vector25.X;
						float num266 = (float)Main.mouseY + Main.screenPosition.Y - vector25.Y;
						if (Main.player[Projectile.owner].gravDir == -1f)
						{
							num266 = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector25.Y;
						}
						float num267 = (float)Math.Sqrt((double)(num265 * num265 + num266 * num266));
						num267 = (float)Math.Sqrt((double)(num265 * num265 + num266 * num266));
						num267 = num264 / num267;
						num265 *= num267;
						num266 *= num267;
						if (num265 != Projectile.velocity.X || num266 != Projectile.velocity.Y)
						{
							Projectile.netUpdate = true;
						}
						Projectile.velocity.X = num265;
						Projectile.velocity.Y = num266;
					}
					else
					{
						Projectile.Kill();
					}
				}
				if (Projectile.velocity.X > 0f)
				{
					Main.player[Projectile.owner].ChangeDir(1);
				}
				else if (Projectile.velocity.X < 0f)
				{
					Main.player[Projectile.owner].ChangeDir(-1);
				}
				Projectile.spriteDirection = Projectile.direction;
				Main.player[Projectile.owner].ChangeDir(Projectile.direction);
				Main.player[Projectile.owner].heldProj = Projectile.whoAmI;
				Main.player[Projectile.owner].itemTime = 2;
				Main.player[Projectile.owner].itemAnimation = 2;
				Projectile.position.X = vector24.X - (float)(Projectile.width / 2);
				Projectile.position.Y = vector24.Y - (float)(Projectile.height / 2);
				Projectile.rotation = (float)(Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.5700000524520874);
				if (Main.player[Projectile.owner].direction == 1)
				{
					Main.player[Projectile.owner].itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
				}
				else
				{
					Main.player[Projectile.owner].itemRotation = (float)Math.Atan2((double)(Projectile.velocity.Y * (float)Projectile.direction), (double)(Projectile.velocity.X * (float)Projectile.direction));
				}
				Projectile.velocity.X = Projectile.velocity.X * (1f + (float)Main.rand.Next(-3, 4) * 0.01f);

				///////////////////////////////

				#endregion drill ai

				if (timer == 0)
				{
					player.PickAmmo(player.HeldItem, ref Ammo, ref speed, ref firing, ref weaponDamage, ref weaponKnockback,out _);

					if(Projectile.type == ModContent.ProjectileType<SharangaProj>() && Ammo == ProjectileID.WoodenArrowFriendly)
					{
						Ammo = ModContent.ProjectileType<SharangaArrow>();
					}
					if (Main.netMode != NetmodeID.Server)
					{
						arrow = Main.projectile[Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, 0, 0, Ammo, weaponDamage, weaponKnockback, Projectile.owner)];
					}
				}
				arrow.velocity = ProjectileExtensions.PolarVector(speed, Projectile.rotation - (float)Math.PI / 2);
				arrow.Center = Projectile.Center + ProjectileExtensions.PolarVector(40 - 2 * speed, Projectile.rotation - (float)Math.PI / 2);
				arrow.friendly = false;
				arrow.rotation = Projectile.rotation;
				arrow.timeLeft += arrow.extraUpdates + 1;
				arrow.alpha = 1 - (int)(((float)timer / maxTime) * 255f);
				speed = (8f * (float)timer / maxTime) + 7f;

				if (arrow.tileCollide)
				{
					giveTileCollision = true;
					arrow.tileCollide = false;
				}
				if(arrow.penetrate > -1)
				{
					arrow.penetrate = -1;
				}
				if (timer < maxTime)
				{
					timer++;
					for (int d = 0; d < 3; d++)
					{
						float theta = Main.rand.NextFloat(-(float)Math.PI, (float)Math.PI);
						Dust dust = Dust.NewDustPerfect(arrow.Center + ProjectileExtensions.PolarVector(40, theta), ModContent.DustType<LightFeather>(), ProjectileExtensions.PolarVector(-8, theta));
						dust.scale = .5f;
						dust.alpha = 255;
					}
					if (timer == maxTime)
					{
						SoundEngine.PlaySound(SoundID.MaxMana, player.position, 0);
						//Projectile.Kill();
					}
				}
			}
			else
			{
				Projectile.Kill();
			}

			return false;
		}

		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item5, Projectile.position);
			arrow.velocity = (ProjectileExtensions.PolarVector(speed, Projectile.rotation - (float)Math.PI / 2)) * velocity;
			arrow.damage *= (int)DamageMultiplier;
			arrow.extraUpdates = 1;
			arrow.friendly = true;
			if (arrow != null && giveTileCollision)
			{
				arrow.tileCollide = true;
			}
		}

	}
}
