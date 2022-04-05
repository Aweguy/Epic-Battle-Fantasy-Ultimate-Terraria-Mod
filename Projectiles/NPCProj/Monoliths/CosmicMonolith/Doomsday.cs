using EpicBattleFantasyUltimate.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
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
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		//The monolith which owns this laser
		public int owner
		{
			get => (int)Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Doomsday");
		}

		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.hide = true;
			Projectile.timeLeft = 120;

			//Projectile.usesLocalNPCImmunity = true;
			//Projectile.localNPCHitCooldown = 10;
		}
        public override bool PreDraw(ref Color lightColor)
        {
			DrawLaser(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, Projectile.Center, Projectile.velocity, 8, Projectile.damage, -1.57f, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
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
				Vector2 laserOffset = unit.RotatedBy(MathHelper.PiOver2) * (float)Math.Sin(MathHelper.TwoPi * x / numDraws + Projectile.timeLeft / 5f);

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
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
			behindNPCs.Add(index);
		}
		

		private void SpawnDusts()
		{
			Vector2 unit = Projectile.velocity * -1;
			Vector2 dustPos = Projectile.Center + Projectile.velocity * (Distance + 8);

			if (Main.rand.NextBool(5))
			{
				Vector2 offset = Projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
				Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, DustID.Electric, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.noGravity = true;
			}
		}

		// Change the way of collision check of the Projectile
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			Vector2 unit = Projectile.velocity;
			float point = 0f;
			// Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
			// It will look for collisions on the given line using AABB
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center,
				Projectile.Center + unit * Distance, 8, ref point);
		}

		public override void AI()
		{
			NPC Monolith = Main.npc[owner];
			Player target = Main.player[Monolith.target];
			
			float LaserRotation = (target.Center - Monolith.Center).ToRotation();
			Projectile.velocity = new Vector2(0, -1);//.RotatedBy(LaserRotation);
			if (!Monolith.active) { Projectile.Kill(); }

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
				var start = Projectile.Center + Projectile.velocity * Distance;
				if (!Collision.CanHit(Projectile.Center, 1, 1, start, 1, 1))
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
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
		}

		public override bool ShouldUpdatePosition() => false;

		public override bool? CanCutTiles()
		{
			return false;
		}
	}
}
