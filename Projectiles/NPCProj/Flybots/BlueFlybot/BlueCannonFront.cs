using EpicBattleFantasyUltimate.Projectiles.NPCProj.Flybots.RedFlybot;
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

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Flybots.BlueFlybot
{
	class BlueCannonFront : ModProjectile
	{
		private int ShootTimer = 60;//Determines when the cannon will shoot
		private int damage;//The damage of the projectiles
		private int ShotNum = 0;//Number of shots
		private float distance;// the distance of the player and the npc.
		private float rotDistance;//The distance between the player and the npc for the rotation of the cannon
		private float projectileSpeed = 10f;
		private Vector2 projectileVelocity;
		private Vector2 modifiedTargetPosition;
		private Vector2 modifiedRotTargetPosition;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blue Cannon");
		}

		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 25;
			projectile.aiStyle = -1;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.ranged = true;
			projectile.knockBack = 1f;
			projectile.tileCollide = false;
		}

		public override void AI()
		{
			NPC npc = Main.npc[(int)projectile.ai[0]];

			Player target = Main.player[npc.target];

			projectile.Center = new Vector2(npc.Center.X - 13 * npc.spriteDirection, npc.Center.Y + 10);

			rotDistance = (target.position - npc.position).Length();// Calculating the distance

			modifiedRotTargetPosition = target.Center + target.velocity * (rotDistance / projectileSpeed);// Calculating where the target will be

			projectile.rotation = (Vector2.Normalize(modifiedRotTargetPosition - npc.Center) * projectileSpeed).ToRotation();// Finalizing the rotation calculation

			projectile.timeLeft = 1000;

			if (!npc.active)
			{
				projectile.Kill();
			}

			if (npc.life <= 0)
			{
				projectile.Kill();
			}

			ShootTimer--;

			if (ShootTimer <= 0 && ShotNum < 3)
			{
				if (ShotNum < 2)
				{
					projectileSpeed = 10f;
					distance = (target.position - npc.position).Length();

					modifiedTargetPosition = target.Center + target.velocity * (distance / projectileSpeed);
					projectileVelocity = Vector2.Normalize(modifiedTargetPosition - npc.Center) * projectileSpeed;

					damage = 30;
				}
				else if (ShotNum == 2)
				{
					projectileSpeed = 20f;
					distance = (target.position - npc.position).Length();

					modifiedTargetPosition = target.Center + target.velocity * (distance / projectileSpeed);
					projectileVelocity = Vector2.Normalize(modifiedTargetPosition - npc.Center) * projectileSpeed;

					damage = 60;
				}

				ShotNum++;

				Projectile.NewProjectile(projectile.Center, projectileVelocity, ModContent.ProjectileType<RedLaser>(), damage, 10, Main.myPlayer, 0, 1);
				Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Flybots/SnipeShot").WithPitchVariance(.2f).WithVolume(.5f), projectile.position);

				if (ShotNum < 2)
				{
					ShootTimer = 35;
				}
				else if (ShotNum == 2)
				{
					ShootTimer = 70;
					Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Flybots/SnipeTarget").WithPitchVariance(.2f).WithVolume(.7f), projectile.position);
				}
				else
				{
					ShootTimer = 300;
					ShotNum = 0;
				}
			}
		}
	}
}
