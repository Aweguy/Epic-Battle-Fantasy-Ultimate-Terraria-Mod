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
	class BlueCannonBehind : ModProjectile
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
			DisplayName.SetDefault("Blue Cannon");
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
			//drawOriginOffsetY = -8;
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

			ShootTimer--;

			if (ShootTimer <= 0 && ShotNum < 10)
			{
				ProjectileSpeed = 12f;
				distance = (target.position - npc.position).Length();

				modifiedTargetPosition = target.Center + target.velocity * (distance / ProjectileSpeed);
				ProjectileVelocity = Vector2.Normalize(modifiedTargetPosition - npc.Center).RotatedBy(Main.rand.NextFloat(-0.3f, 0.3f)) * ProjectileSpeed;

				damage = 5;

				ShotNum++;

				Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(),Projectile.Center, ProjectileVelocity, ModContent.ProjectileType<BlueBubble>(), damage, 10, Main.myPlayer, 0, 1);

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
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
			behindNPCs.Add(index);
        }
	}
}
