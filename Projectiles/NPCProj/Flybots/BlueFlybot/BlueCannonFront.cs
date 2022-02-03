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
			//drawOriginOffsetY = -8;
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

			if (ShootTimer <= 0 && ShotNum < 10)
			{
				
				projectileSpeed = 11f;
				distance = (target.position - npc.position).Length();

				modifiedTargetPosition = target.Center + target.velocity * (distance / projectileSpeed);
				projectileVelocity = Vector2.Normalize(modifiedTargetPosition - npc.Center).RotatedBy(Main.rand.NextFloat(-0.3f, 0.3f)) * projectileSpeed;

				damage = 10;
				

				ShotNum++;

				Projectile.NewProjectile(projectile.Center, projectileVelocity, ModContent.ProjectileType<BlueBubble>(), damage, 10, Main.myPlayer, 0, 1);

				if (ShotNum < 10)
				{
					ShootTimer = 10;
				}
				else
				{
					ShootTimer = 450;
					ShotNum = 0;
				}
			}
		}
	}
}
