using EpicBattleFantasyUltimate.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EpicBattleFantasyUltimate.HelperClasses;
using Terraria.Enums;
using Terraria.GameContent;

namespace EpicBattleFantasyUltimate.Projectiles
{
	class TestLaser2 : ModProjectile
	{
		// Use a different style for constant so it is very clear in code when a constant is used

		// The maximum charge value
		private const float MAX_CHARGE = 50f;
		//The distance charge particle from the player center
		private const float MOVE_DISTANCE = 60f;

		// The actual distance is stored in the ai0 field
		// By making a property to handle this it makes our life easier, and the accessibility more readable
		public float Distance
		{
			get => Projectile.localAI[1];
			set => Projectile.localAI[1] = value;
		}

		// The actual charge value is stored in the localAI0 field
		public float Charge
		{
			get => Projectile.localAI[0];
			set => Projectile.localAI[0] = value;
		}

		// Are we at max charge? With c#6 you can simply use => which indicates this is a get only property
		public bool IsAtMaxCharge => Charge == MAX_CHARGE;

		public override void SetDefaults()
		{
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.hide = true;
		}

		public override bool PreDraw(ref Color lightColor)
		{
			return false;
		}

		public override void PostDraw(Color lightColor)
		{

			NPC npc = Main.npc[(int)Projectile.ai[1]];
			// We start drawing the laser if we have charged up
			//if (IsAtMaxCharge)
			//{
			DrawLaser(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, npc.Center,
				Projectile.velocity, 10, Projectile.damage, -1.57f, 1f, 1000f, Color.White, (int)MOVE_DISTANCE);
			//}
		}

		// The core function of drawing a laser
		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 50)
		{
			float r = unit.ToRotation() + rotation;

			// Draws the laser 'body'
			for (float i = transDist; i <= Distance; i += step)
			{
				Color c = Color.White;
				var origin = start + i * unit;
				spriteBatch.Draw(texture, origin - Main.screenPosition,
					new Rectangle(0, 26, 28, 26), i < transDist ? Color.Transparent : c, r,
					new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
			}

			// Draws the laser 'tail'
			spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
				new Rectangle(0, 0, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);

			// Draws the laser 'head'
			spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
				new Rectangle(0, 52, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
		}

		// Change the way of collision check of the Projectile
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			// We can only collide if we are at max charge, which is when the laser is actually fired
			if (!IsAtMaxCharge) return false;

			NPC npc = Main.npc[(int)Projectile.ai[1]];
			Vector2 unit = Projectile.velocity;
			float point = 0f;
			// Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
			// It will look for collisions on the given line using AABB
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), npc.Center,
				npc.Center + unit * Distance, 22, ref point);
		}

		// The AI of the Projectile
		public override void AI()
		{
			NPC npc = Main.npc[(int)Projectile.ai[1]];
			Projectile.position = npc.Center + Projectile.velocity * MOVE_DISTANCE;
			Projectile.timeLeft = 2;

			// By separating large AI into methods it becomes very easy to see the flow of the AI in a broader sense
			// First we update player variables that are needed to channel the laser
			// Then we run our charging laser logic
			// If we are fully charged, we proceed to update the laser's position
			// Finally we spawn some effects like dusts and light

			UpdatePlayer(npc);
			ChargeLaser(npc);

			// If laser is not charged yet, stop the AI here.
			if (Charge < MAX_CHARGE) return;

			SetLaserPosition(npc);
			SpawnDusts(npc);
			CastLights();
		}

		private void SpawnDusts(NPC npc)
		{
			Vector2 unit = Projectile.velocity * -1;
			Vector2 dustPos = npc.Center + Projectile.velocity * Distance;

			for (int i = 0; i < 2; ++i)
			{
				float num1 = Projectile.velocity.ToRotation() + (Main.rand.Next(2) == 1 ? -1.0f : 1.0f) * 1.57f;
				float num2 = (float)(Main.rand.NextDouble() * 0.8f + 1.0f);
				Vector2 dustVel = new Vector2((float)Math.Cos(num1) * num2, (float)Math.Sin(num1) * num2);
				Dust dust = Main.dust[Dust.NewDust(dustPos, 0, 0, DustID.Electric, dustVel.X, dustVel.Y)];
				dust.noGravity = true;
				dust.scale = 1.2f;
				dust = Dust.NewDustDirect(Main.player[Projectile.owner].Center, 0, 0, DustID.Smoke,
					-unit.X * Distance, -unit.Y * Distance);
				dust.fadeIn = 0f;
				dust.noGravity = true;
				dust.scale = 0.88f;
				dust.color = Color.Cyan;
			}

			if (Main.rand.NextBool(5))
			{
				Vector2 offset = Projectile.velocity.RotatedBy(1.57f) * ((float)Main.rand.NextDouble() - 0.5f) * Projectile.width;
				Dust dust = Main.dust[Dust.NewDust(dustPos + offset - Vector2.One * 4f, 8, 8, DustID.Smoke, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.velocity *= 0.5f;
				dust.velocity.Y = -Math.Abs(dust.velocity.Y);
				unit = dustPos - Main.player[Projectile.owner].Center;
				unit.Normalize();
				dust = Main.dust[Dust.NewDust(Main.player[Projectile.owner].Center + 55 * unit, 8, 8, DustID.Smoke, 0.0f, 0.0f, 100, new Color(), 1.5f)];
				dust.velocity = dust.velocity * 0.5f;
				dust.velocity.Y = -Math.Abs(dust.velocity.Y);
			}
		}

		/*
		 * Sets the end of the laser position based on where it collides with something
		 */
		private void SetLaserPosition(NPC npc)
		{
			for (Distance = MOVE_DISTANCE; Distance <= 2200f; Distance += 5f)
			{
				var start = npc.Center + Projectile.velocity * Distance;
				if (!Collision.CanHit(npc.Center, 1, 1, start, 1, 1))
				{
					Distance -= 5f;
					break;
				}
			}
		}

		private void ChargeLaser(NPC npc)
		{
			// Kill the Projectile if the player stops channeling
			
			
				// Do we still have enough mana? If not, we kill the Projectile because we cannot use it anymore
				
				Vector2 offset = Projectile.velocity;
				offset *= MOVE_DISTANCE - 20;
				Vector2 pos = npc.Center + offset - new Vector2(10, 10);
				if (Charge < MAX_CHARGE)
				{
					Charge++;
				}
				int chargeFact = (int)(Charge / 20f);
				Vector2 dustVelocity = Vector2.UnitX * 18f;
				dustVelocity = dustVelocity.RotatedBy(Projectile.rotation - 1.57f);
				Vector2 spawnPos = Projectile.Center + dustVelocity;
				for (int k = 0; k < chargeFact + 1; k++)
				{
					Vector2 spawn = spawnPos + ((float)Main.rand.NextDouble() * 6.28f).ToRotationVector2() * (12f - chargeFact * 2);
					Dust dust = Main.dust[Dust.NewDust(pos, 20, 20, DustID.Electric, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f)];
					dust.velocity = Vector2.Normalize(spawnPos - spawn) * 1.5f * (10f - chargeFact * 2f) / 10f;
					dust.noGravity = true;
					dust.scale = Main.rand.Next(10, 20) * 0.05f;
				}
			
		}

		private void UpdatePlayer(NPC npc)
		{
			Player player = Main.player[(int)Projectile.ai[0]];
			// Multiplayer support here, only run this code if the client running it is the owner of the Projectile
			if (Projectile.owner == Main.myPlayer)
			{
				Vector2 diff = player.Center - npc.Center;
				diff.Normalize();
				Projectile.velocity = diff;
				Projectile.direction = player.Center.X > npc.position.X ? 1 : -1;
				Projectile.netUpdate = true;
			}
		}

		private void CastLights()
		{
			// Cast a light along the line of the laser
			DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * (Distance - MOVE_DISTANCE), 26, DelegateMethods.CastLight);
		}

		public override bool ShouldUpdatePosition() => false;

		/*
		 * Update CutTiles so the laser will cut tiles (like grass)
		 */
		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			Vector2 unit = Projectile.velocity;
			Utils.PlotTileLine(Projectile.Center, Projectile.Center + unit * Distance, (Projectile.width + 16) * Projectile.scale, DelegateMethods.CutTiles);
		}
	}
}
