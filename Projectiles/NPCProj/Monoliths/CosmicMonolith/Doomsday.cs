using EpicBattleFantasyUltimate.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;


namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Monoliths.CosmicMonolith
{
	class Doomsday : ModProjectile
	{
		// Use a different style for constant so it is very clear in code when a constant is used

		//The distance charge particle from the pixie center
		private const float MOVE_DISTANCE = 0f;

		// The actual distance is stored in the ai0 field
		// By making a property to handle this it makes our life easier, and the accessibility more readable
		public float Distance
		{
			get => projectile.ai[0];
			set => projectile.ai[0] = value;
		}

		//The monolith which owns this laser
		public int owner
		{
			get => (int)projectile.ai[1];
			set => projectile.ai[1] = value;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doomsday");
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.hide = true;
			projectile.timeLeft = 120;

			//projectile.usesLocalNPCImmunity = true;
			//projectile.localNPCHitCooldown = 10;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			// We start drawing the laser
			DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], projectile.Center, projectile.velocity, 8, projectile.damage, -1.57f, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
			return false;
		}

		// The core function of drawing a laser
		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
		{
			float r = unit.ToRotation() + rotation;

			Color c = Color.White;
			int numDraws = 2;
			c.A = 127;
			for (int x = 0; x < numDraws; x++)
			{
				Vector2 laserOffset = unit.RotatedBy(MathHelper.PiOver2) * (float)Math.Sin(MathHelper.TwoPi * x / numDraws + projectile.timeLeft / 5f);

				// Draws the laser 'body'
				for (float i = transDist; i <= Distance; i += step)
				{
					var origin = laserOffset + start + i * unit;

					spriteBatch.Draw(texture, origin - Main.screenPosition,
						new Rectangle(0, 0, 8, 8), i < transDist ? Color.Transparent : c, r,
						new Vector2(8 * .5f, 8 * .5f), scale, 0, 0);
				}
			}
		}

		public override void DrawBehind(int index, System.Collections.Generic.List<int> drawCacheProjsBehindNPCsAndTiles, System.Collections.Generic.List<int> drawCacheProjsBehindNPCs, System.Collections.Generic.List<int> drawCacheProjsBehindProjectiles, System.Collections.Generic.List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindNPCs.Add(index);
		}

		private void SpawnDusts()
		{
			Vector2 unit = projectile.velocity * -1;
			Vector2 dustPos = projectile.Center + projectile.velocity * (Distance + 8);

			if (Main.rand.NextBool(5))
			{
				Vector2 offset = projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * projectile.width;
				Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, DustID.Electric, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.noGravity = true;
			}
		}

		// Change the way of collision check of the projectile
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			Vector2 unit = projectile.velocity;
			float point = 0f;
			// Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
			// It will look for collisions on the given line using AABB
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center,
				projectile.Center + unit * Distance, 8, ref point);
		}

		public override void AI()
		{
			NPC Monolith = Main.npc[owner];
			Player target = Main.player[Monolith.target];
			
			float LaserRotation = (target.Center - Monolith.Center).ToRotation();
			projectile.velocity = new Vector2(0, -1);//.RotatedBy(LaserRotation);
			if (!Monolith.active) { projectile.Kill(); }

			SetLaserPosition();
			SpawnDusts();
			CastLights();
		}

		/*
		 * Sets the end of the laser position based on where it collides with something
		 */
		private void SetLaserPosition()
		{
			for (Distance = MOVE_DISTANCE; Distance <= 1600f; Distance += 4f)
			{
				var start = projectile.Center + projectile.velocity * Distance;
				if (!Collision.CanHit(projectile.Center, 1, 1, start, 1, 1))
				{
					//Distance -= 4f;
					break;
				}
			}
		}

		private void CastLights()
		{
			// Cast a light along the line of the laser
			DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
			Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
		}

		public override bool ShouldUpdatePosition() => false;

		public override bool? CanCutTiles()
		{
			return false;
		}
	}
}
