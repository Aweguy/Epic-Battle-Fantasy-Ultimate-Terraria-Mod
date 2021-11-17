using EpicBattleFantasyUltimate.Dusts;
using EpicBattleFantasyUltimate.HelperClasses;
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

namespace EpicBattleFantasyUltimate.Projectiles.BowProjectiles
{
	class BowProj : ModProjectile
	{
		public int timer = 0;
		public int reloadTime;
		public float direction;

		public float Radd;
		public bool runOnce = true;
		private Projectile arrow = null;
		private float speed = 15f;
		private int maxTime = 60;
		private int weaponDamage = 10;
		private int Ammo = 0;
		private float weaponKnockback = 0;
		private bool giveTileCollision = false;

		public override void SetDefaults()
		{
			projectile.width = 50;
			projectile.height = 18;

			projectile.friendly = false;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.hide = false;
			projectile.ranged = true;
			projectile.ignoreWater = true;
		}


		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			//if (runOnce)
			//{
				//runOnce = false;
			//}
			projectile.timeLeft = 2;

			var modPlayer = player.GetModPlayer < EpicPlayer>();
			bool firing = (player.channel || timer < 30) && player.HasAmmo(player.HeldItem, true) && !player.noItems && !player.CCed;

			Ammo = AmmoID.Arrow;

			weaponDamage = player.GetWeaponDamage(player.inventory[player.selectedItem]);
			direction = (Main.MouseWorld - player.Center).ToRotation();
			weaponKnockback = player.inventory[player.selectedItem].knockBack;

			if (firing)
			{
				#region drill ai

				///////////////////////////////////// copied from vanilla drill/chainsaw AI
				Vector2 vector24 = Main.player[projectile.owner].RotatedRelativePoint(Main.player[projectile.owner].MountedCenter, true);
				if (Main.myPlayer == projectile.owner)
				{
					if (Main.player[projectile.owner].channel || timer < 30)
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
					if (Main.netMode != 2)
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
				//Main.NewText(arrow.damage);
				// Main.NewText("AI0: " + arrow.ai[0] + ", AI1: " + arrow.ai[1] + ", LocalAI0: " + arrow.localAI[0] + ", LocalAI1: " + arrow.localAI[1]);
				if (arrow.tileCollide)
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
						Main.PlaySound(25, player.position, 0);
						projectile.Kill();
					}
				}
			}
			else
			{
				projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item5, projectile.position);
			arrow.velocity = ProjectileExtensions.PolarVector(speed, projectile.rotation - (float)Math.PI / 2);
			arrow.friendly = true;
			if (arrow != null && giveTileCollision)
			{
				arrow.tileCollide = true;
			}
			for(int i = 0; i <= timer / 3; i++)
			{
				Projectile.NewProjectile(arrow.Center, arrow.velocity.RotatedByRandom(0.5) * .8f, arrow.type, arrow.damage, arrow.knockBack, projectile.owner);
			}

			if (timer >= maxTime)
			{
				Projectile.NewProjectile(arrow.Center, arrow.velocity * .8f, arrow.type, arrow.damage, arrow.knockBack, projectile.owner);
				Projectile.NewProjectile(arrow.Center, arrow.velocity * 1.2f, arrow.type, arrow.damage, arrow.knockBack, projectile.owner);
			}
		}
	}
}
