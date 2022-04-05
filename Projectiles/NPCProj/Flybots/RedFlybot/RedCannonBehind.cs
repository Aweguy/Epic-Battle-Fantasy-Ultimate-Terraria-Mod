using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Flybots.RedFlybot
{
	public class RedCannonBehind : ModProjectile
	{
		private int ShootTimer = 60;//Determines when the cannon will shoot
		private int damage;//The damage of the Projectiles
		private int ShotNum = 0;//Number of shots
		private float distance;// the distance of the player and the npc.
		private float rotDistance;//The distance between the player and the npc for the rotation of the cannon
		private float ProjectileSpeed = 10f;
		private Vector2 ProjectileVelocity;
		private Vector2 modifiedTargetPosition;
		private Vector2 modifiedRotTargetPosition;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Red Cannon");
		}

		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 25;
			Projectile.aiStyle = -1;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			Projectile.knockBack = 1f;
			Projectile.hide = true;
			Projectile.tileCollide = false;
		}

		public override void AI()
		{
			NPC npc = Main.npc[(int)Projectile.ai[0]]; //Sets the npc that the Projectile is spawned and will orbit

			Player target = Main.player[npc.target];

			Projectile.Center = new Vector2(npc.Center.X + 9 * npc.spriteDirection, npc.Center.Y + 22);

			rotDistance = (target.position - npc.position).Length();// Calculating the distance

			modifiedRotTargetPosition = target.Center + target.velocity * (rotDistance / ProjectileSpeed);// Calculating where the target will be

			Projectile.rotation = (Vector2.Normalize(modifiedRotTargetPosition - npc.Center) * ProjectileSpeed).ToRotation();// Finalizing the rotation calculation

			Projectile.timeLeft = 1000;

			if (!npc.active)
			{
				Projectile.Kill();
			}

			if (npc.life <= 0)
			{
				Projectile.Kill();
			}

			if (Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, target.position, target.width, target.height))
			{
				ShootTimer--;
			}

			if (ShootTimer <= 0 && ShotNum < 3)
			{
				if (ShotNum < 2)
				{
					ProjectileSpeed = 10f;
					distance = (target.position - npc.position).Length();

					modifiedTargetPosition = target.Center + target.velocity * (distance / ProjectileSpeed);
					ProjectileVelocity = Vector2.Normalize(modifiedTargetPosition - npc.Center) * ProjectileSpeed;

					damage = 30;
				}
				else if (ShotNum == 2)
				{
					ProjectileSpeed = 20f;
					distance = (target.position - npc.position).Length();

					modifiedTargetPosition = target.Center + target.velocity * (distance / ProjectileSpeed);
					ProjectileVelocity = Vector2.Normalize(modifiedTargetPosition - npc.Center) * ProjectileSpeed;

					damage = 60;
				}

				ShotNum++;

				Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.Center, ProjectileVelocity, ModContent.ProjectileType<RedLaser>(), damage, 10, Main.myPlayer, 0, 1);
				SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/Flybots/SnipeShot");

				if (ShotNum < 2)
				{
					ShootTimer = 35;
				}
				else if (ShotNum == 2)
				{
					ShootTimer = 70;
					SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/Flybots/SnipeTarget");
				}
				else
				{
					ShootTimer = 300;
					ShotNum = 0;
				}
			}
		}

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
			behindNPCs.Add(index);
		}
	}
}