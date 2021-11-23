﻿using EpicBattleFantasyUltimate.Dusts;
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
using Terraria.ID;
using Terraria.ModLoader;


namespace EpicBattleFantasyUltimate.ClassTypes
{
	public abstract class EpicComboBowProj : ModProjectile
	{

		public static List<int> RandomArrows = new List<int>();

		#region Variables
		public int timer = 0;//The current charge value
		public float direction;

		public float Radd;
		public bool runOnce = true;
		public Projectile arrow = null;
		public float speed;
		public float speedMultiplier;//Speed multiplier of the arrows will be set in the safe defaults of each bow projectile.
		public float velocity;//The velocity multiplier of the arrows will be set in the defaults of each bow projectile.
		public int maxTime;//The max charge time will be set in the safe defaults of each bow projectile
		public int ArrowVolleyNum;//The number of arrows shot at the initial volley will be set in the safe defaults of each bow projectile.
		public int RapidFireArrowNum;//The number of arrows of the contunuous shots will be set in the safe defaults of each bow projectile.
		public float DamageMultiplier;//The damage multiplier of the arrows will be set in the safe defaults of each bow projectile.
		public int MinimumDrawTime;//The minimum time that the player will hold the bow will be set in the safe defaults of each bow projectile.
		public int weaponDamage;//The weapon damage will be set in the safe defaults of each bow projectile
		public int Ammo = 0;
		public float weaponKnockback;//The weapon knockback will be set in the safe defaults of each bow projectile
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
		#endregion

		public override void SetStaticDefaults()
		{
			SetSafeStaticDefaults();
		}

		public override void SetDefaults()
		{
			SetSafeDefaults();

			projectile.friendly = false;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.hide = false;
			projectile.ranged = true;
			projectile.ignoreWater = true;
			
		}

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			if (runOnce)
			{
				if(projectile.type == ModContent.ProjectileType<AlchemistBowProj>())
				{
					for (int i = 0; i < ProjectileLoader.ProjectileCount; i++)
					{
						Projectile projectile1 = new Projectile();
						projectile1.SetDefaults(i);
						if ((projectile1.arrow && projectile1.modProjectile is null) || (projectile1.arrow && projectile1.modProjectile is ModProjectile modProj && modProj.mod == mod) )
						{
							RandomArrows.Add(i);
						}
					}
				}
				runOnce = false;
			}
			projectile.timeLeft = 2;

			var modPlayer = player.GetModPlayer<EpicPlayer>();
			bool firing = (player.channel || timer < MinimumDrawTime) && player.HasAmmo(player.HeldItem, true) && !player.noItems && !player.CCed;

			if (projectile.type == ModContent.ProjectileType<AlchemistBowProj>())
			{
				Ammo = Main.rand.Next(RandomArrows.ToArray());

			}
			else
			{
				Ammo = AmmoID.Arrow;
			}
			weaponDamage += player.GetWeaponDamage(player.inventory[player.selectedItem]);
			direction = (Main.MouseWorld - player.Center).ToRotation();
			weaponKnockback = player.inventory[player.selectedItem].knockBack;

			if (firing)
			{
				#region drill ai

				///////////////////////////////////// copied from vanilla drill/chainsaw AI
				Vector2 vector24 = Main.player[projectile.owner].RotatedRelativePoint(Main.player[projectile.owner].MountedCenter, true);
				if (Main.myPlayer == projectile.owner)
				{
					if (Main.player[projectile.owner].channel || timer < MinimumDrawTime)
					{
						float num264 = Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].shootSpeed * projectile.scale;
						Vector2 vector25 = vector24;
						float num265 = (float)Main.mouseX + Main.screenPosition.X - vector25.X;
						float num266 = (float)Main.mouseY + Main.screenPosition.Y - vector25.Y;
						if (Main.player[projectile.owner].gravDir == -1f)
						{
							num266 = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector25.Y;
						}
						float num267 = (float)Math.Sqrt((double)(num265 * num265 + num266 * num266));
						num267 = (float)Math.Sqrt((double)(num265 * num265 + num266 * num266));
						num267 = num264 / num267;
						num265 *= num267;
						num266 *= num267;
						if (num265 != projectile.velocity.X || num266 != projectile.velocity.Y)
						{
							projectile.netUpdate = true;
						}
						projectile.velocity.X = num265;
						projectile.velocity.Y = num266;
					}
					else
					{
						projectile.Kill();
					}
				}
				if (projectile.velocity.X > 0f)
				{
					Main.player[projectile.owner].ChangeDir(1);
				}
				else if (projectile.velocity.X < 0f)
				{
					Main.player[projectile.owner].ChangeDir(-1);
				}
				projectile.spriteDirection = projectile.direction;
				Main.player[projectile.owner].ChangeDir(projectile.direction);
				Main.player[projectile.owner].heldProj = projectile.whoAmI;
				Main.player[projectile.owner].itemTime = 2;
				Main.player[projectile.owner].itemAnimation = 2;
				projectile.position.X = vector24.X - (float)(projectile.width / 2);
				projectile.position.Y = vector24.Y - (float)(projectile.height / 2);
				projectile.rotation = (float)(Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.5700000524520874);
				if (Main.player[projectile.owner].direction == 1)
				{
					Main.player[projectile.owner].itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
				}
				else
				{
					Main.player[projectile.owner].itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
				}
				projectile.velocity.X = projectile.velocity.X * (1f + (float)Main.rand.Next(-3, 4) * 0.01f);

				///////////////////////////////

				#endregion drill ai

				if (timer == 0)
				{
					player.PickAmmo(player.HeldItem, ref Ammo, ref speed, ref firing, ref weaponDamage, ref weaponKnockback);
					if (projectile.type == ModContent.ProjectileType<BlackWidowProj>() && Ammo == ProjectileID.WoodenArrowFriendly)
					{
						Ammo = ModContent.ProjectileType<SpiderArrow>();
					}
					if (Main.netMode != NetmodeID.Server)
					{
						arrow = Main.projectile[Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, Ammo, weaponDamage, weaponKnockback, projectile.owner)];
					}
				}
				arrow.velocity = ProjectileExtensions.PolarVector(speed, projectile.rotation - (float)Math.PI / 2);
				arrow.Center = projectile.Center + ProjectileExtensions.PolarVector(40 - 2 * speed, projectile.rotation - (float)Math.PI / 2);
				arrow.friendly = false;
				arrow.rotation = projectile.rotation;
				arrow.timeLeft += arrow.extraUpdates + 1;
				arrow.alpha = 1 - (int)(((float)timer / maxTime) * 255f);
				speed = (8f * (float)timer / maxTime) + 7f;

				if (arrow.tileCollide && !giveTileCollision)
				{
					giveTileCollision = true;
					arrow.tileCollide = false;
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
						Main.PlaySound(SoundID.MaxMana, player.position, 0);
					}
				}
			}
			else
			{
				projectile.Kill();
				//projectile.Kill();
			}

			return false;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item5, projectile.position);
			arrow.velocity = (ProjectileExtensions.PolarVector(speed, projectile.rotation - (float)Math.PI / 2));
			arrow.tileCollide = true;
			arrow.noDropItem = true;
			arrow.extraUpdates = 1;
			arrow.friendly = true;

			for (int i = 0; i <= ArrowVolleyNum; i++)
			{
				int arrow2;
				if (projectile.type == ModContent.ProjectileType<AlchemistBowProj>())
				{
					arrow2 = Projectile.NewProjectile(projectile.Center, arrow.velocity.RotatedByRandom(0.2f), Main.rand.Next(RandomArrows.ToArray()), (int)(arrow.damage * DamageMultiplier), arrow.knockBack, projectile.owner);
				}
				else
				{
					arrow2 = Projectile.NewProjectile(projectile.Center, arrow.velocity.RotatedByRandom(0.2f), arrow.type, (int)(arrow.damage * DamageMultiplier), arrow.knockBack, projectile.owner);

				}
				Main.projectile[arrow2].noDropItem = true;
				Main.projectile[arrow2].extraUpdates = 1;
			}
		}

	}
}
