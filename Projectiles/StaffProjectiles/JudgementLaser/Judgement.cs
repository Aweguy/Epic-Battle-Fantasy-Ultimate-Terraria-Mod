#region Using

using EpicBattleFantasyUltimate.Buffs.Buffs;
using EpicBattleFantasyUltimate.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ID;

#endregion Using

namespace EpicBattleFantasyUltimate.Projectiles.StaffProjectiles.JudgementLaser
{
	public class Judgement : ModProjectile
	{
		#region Variables and Constants

		private const float MAX_CHARGE = 40f;

		//The distance charge particle from the player center
		public float MOVE_DISTANCE = 20f;

		// The actual distance is stored in the ai0 field
		// By making a property to handle this it makes our life easier, and the accessibility more readable
		public float Distance = 2500;

		//{
		//get => projectile.ai[0];
		//set => projectile.ai[0] = value;
		//}

		// The actual charge value is stored in the localAI0 field
		public float Charge
		{
			get => projectile.localAI[0];
			set => projectile.localAI[0] = value;
		}

		public bool IsAtMaxCharge => Charge == MAX_CHARGE;

		private float scaled = 5f;//Used for the animation illusion
		private float increaseY = 0f; //It increases the Y axis of the dust spawning
		private float increaseY2 = 0f;//Same as above
		private float WaveFrequency = 70f;//Dust spawning wave frequency on both spawners
		private float WaveLength = 100f;//Dust Spawning wave length on both spawners
		private float beamWidth = 100f;//Collision hitbox.
		private float offDistance = 0.6f;//Distance Reduction

		private Vector2 position;//the initial position of the laser
		private Vector2 spriterotation = new Vector2(0, -1);//rotation of the laser to look up

		private int timer = 0;//So the ground detection AI runs only once so the laser is not moving.
		private int timer2 = 0;//Dust spawning timer for the feathers
		private int timer3 = 0;//Dust spawning for the bubbles
		private int animation = 0;//Sets 0 or 1 for a small animation.

		#endregion Variables and Constants

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Judgement");
		}

		public override void SetDefaults()
		{
			projectile.width = 0;
			projectile.height = 0;

			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;

			projectile.timeLeft = 120 + (int)MAX_CHARGE;
			projectile.magic = true;
			projectile.hide = true;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (!IsAtMaxCharge)//When it's not at max charge have a small laser beam
			{
				scaled = 1f;
				MOVE_DISTANCE = 4f;
			}
			else if (IsAtMaxCharge && projectile.timeLeft <= 80)//if it's at max charge and some time has passed reduce its scale.
			{
				scaled -= 0.06f;
				MOVE_DISTANCE -= 0.24f;
			}
			else//The animation while damaging
			{
				if (animation == 0)
				{
					scaled = 5.5f;
					animation = 1;
					MOVE_DISTANCE = 20f;
				}
				else if (animation == 1)
				{
					scaled = 5f;
					animation = 0;
					MOVE_DISTANCE = 20f;
				}
			}
			if (scaled <= 0f)
			{
				projectile.Kill();
			}

			DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], position, spriterotation, 10, projectile.damage, -1.57f, 1f * scaled, 1000f, Color.White, (int)MOVE_DISTANCE);
			return false;
		}


		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float rotation = 0f, float scale = 1f, float maxDist = 2000f, Color color = default(Color), int transDist = 0)
		{
			float r = unit.ToRotation() + rotation;

			var origin = Vector2.Zero;

			// Draws the laser 'body'
			for (float i = transDist; i <= Distance; i += step)
			{
				Color c = Color.White;
				origin = start + i * unit;

				spriteBatch.Draw(texture, origin - Main.screenPosition, new Rectangle(0, 26, 28, 26), i < transDist ? Color.Transparent : c, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);
			}

			// Draws the laser 'tail'
			spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
				new Rectangle(0, 0, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);

			// Draws the laser 'head'
			/*spriteBatch.Draw(texture, start + (Distance + step) * unit - Main.screenPosition,
				new Rectangle(0, 52, 28, 26), Color.White, r, new Vector2(28 * .5f, 26 * .5f), scale, 0, 0);*/
		}

		// Change the way of collision check of the projectile
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (!IsAtMaxCharge)
			{
				return false;
			}
			// We can only collide if we are at max charge, which is when the laser is actually fired
			Vector2 unit = spriterotation;
			float point = 0f;

			// Run an AABB versus Line check to look for collisions, look up AABB collision first to see how it works
			// It will look for collisions on the given line using AABB
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), position,
				position + unit * Distance, beamWidth, ref point);
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 5;

			Projectile.NewProjectile(target.Center, Vector2.Zero, ModContent.ProjectileType<LightExplosion>(), projectile.damage, 0, projectile.owner);
		}

		public override void AI()
		{
			#region Ground Detection

			Player player = Main.player[projectile.owner];
			if (timer == 0)
			{
				int num233 = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
				int num234 = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
				if (player.gravDir == -1f)
				{
					num234 = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
				}
				for (; num234 < Main.maxTilesY && Main.tile[num233, num234] != null && !WorldGen.SolidTile2(num233, num234) && Main.tile[num233 - 1, num234] != null && !WorldGen.SolidTile2(num233 - 1, num234) && Main.tile[num233 + 1, num234] != null && !WorldGen.SolidTile2(num233 + 1, num234); num234++)
				{
				}

				projectile.position = new Vector2((float)Main.mouseX + Main.screenPosition.X, (float)(num234 * 16));
				position = projectile.position;
				timer--;
			}

			#endregion Ground Detection

			#region Beam Shrinking

			if (IsAtMaxCharge && projectile.timeLeft <= 80)
			{
				beamWidth -= 0.5f;//reducing the hitbox.
			}
			else
			{
				beamWidth = 100f;
			}

			#endregion Beam Shrinking

			// By separating large AI into methods it becomes very easy to see the flow of the AI in a broader sense
			// First we update player variables that are needed to channel the laser
			// Then we run our charging laser logic
			// If we are fully charged, we proceed to update the laser's position
			// Finally we spawn some effects like dusts and light

			UpdatePlayer(player);
			ChargeLaser(player);

			SetLaserPosition(player);
			SpawnDusts(player);
			CastLights();
		}

		private void SpawnDusts(Player player)
		{
			Vector2 unit = position * -1;

			#region generalDust

			if (!IsAtMaxCharge)
			{
				for (int i = 0; i < 1; ++i)
				{
					Vector2 dustVel = new Vector2(1, 0).RotatedBy(Main.rand.NextFloat(1.57f, 1.57f) + (Main.rand.Next(2) == 0 ? -1.0f : 1.0f) * 1.57f);

					Dust dust = Main.dust[Dust.NewDust(new Vector2(position.X, position.Y), 0, 0, DustID.Electric, dustVel.X * 10, dustVel.Y * 10, 0, Color.White)];
					dust.noGravity = true;
					dust.scale = 1.2f;
					dust.shader = GameShaders.Armor.GetSecondaryShader(64, Main.LocalPlayer);
					dust = Dust.NewDustDirect(new Vector2(position.X, position.Y), 0, 0, DustID.Smoke, -unit.X * Distance, -unit.Y * Distance);
					dust.fadeIn = 0f;
					dust.noGravity = true;
					dust.scale = 0.88f;
					dust.color = Color.White;
					dust.shader = GameShaders.Armor.GetSecondaryShader(64, Main.LocalPlayer);
				}
			}
			else
			{
				for (int i = 0; i < 3; ++i)
				{
					Vector2 dustVel = new Vector2(1, 0).RotatedBy(Main.rand.NextFloat(1.57f, 1.57f) + (Main.rand.Next(2) == 0 ? -1.0f : 1.0f) * 1.57f);

					Dust dust = Main.dust[Dust.NewDust(new Vector2(position.X, position.Y), 0, 0, DustID.Electric, dustVel.X * 10, dustVel.Y * 10, 0, Color.White)];
					dust.noGravity = true;
					dust.scale = 1.2f;
					dust.shader = GameShaders.Armor.GetSecondaryShader(64, Main.LocalPlayer);
					dust = Dust.NewDustDirect(new Vector2(position.X, position.Y), 0, 0, DustID.Smoke, -unit.X * Distance, -unit.Y * Distance);
					dust.fadeIn = 0f;
					dust.noGravity = true;
					dust.scale = 0.88f;
					dust.color = Color.White;
					dust.shader = GameShaders.Armor.GetSecondaryShader(64, Main.LocalPlayer);

					Dust dust2 = Main.dust[Dust.NewDust(new Vector2(position.X, position.Y - Distance + offDistance - 65), 0, 0, DustID.Electric, dustVel.X * 10, dustVel.Y * 10, 0, Color.White)];//it's offDistance - 65 since that's the number that fits here.
					dust2.noGravity = true;
					dust2.scale = 1.2f;
					dust.shader = GameShaders.Armor.GetSecondaryShader(64, Main.LocalPlayer);
					dust2 = Dust.NewDustDirect(new Vector2(position.X, position.Y), 0, 0, DustID.Smoke, -unit.X * Distance, -unit.Y * Distance);
					dust2.fadeIn = 0f;
					dust2.noGravity = true;
					dust2.scale = 0.88f;
					dust2.color = Color.White;
					dust.shader = GameShaders.Armor.GetSecondaryShader(64, Main.LocalPlayer);
				}
			}

			#endregion generalDust

			#region Feathers

			if (player.HasBuff(ModContent.BuffType<HasteBuff>()))
			{
				timer2 += 2;
			}
			else
			{
				timer2++;
			}

			if (timer2 == 40)
			{
				Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Spells/Judgement").WithPitchVariance(1f).WithVolume(.2f), projectile.position);

				for (int i = 0; i < 85; ++i)
				{
					Vector2 dustVel = new Vector2(1, 0).RotatedBy(Main.rand.NextFloat(1.57f, 2.57f) + (Main.rand.Next(2) == 0 ? -1.6f : 1.0f) * 1.57f);

					float rand = Main.rand.NextFloat(5f, 20f);

					Dust dust = Main.dust[Dust.NewDust(new Vector2(position.X, position.Y), 0, 0, ModContent.DustType<LightFeather>(), dustVel.X * rand, dustVel.Y * rand)];
					dust.noGravity = true;
					dust.noLight = true;
					dust.scale = 1.2f;
					dust.alpha += 2;
					dust = Dust.NewDustDirect(new Vector2(position.X, position.Y), 0, 0, DustID.Smoke,
						-unit.X * Distance, -unit.Y * Distance);
					dust.fadeIn = 0f;
					dust.noGravity = true;
					dust.scale = 0.88f;
					dust.alpha += 2;
					dust.color = Color.Cyan;
				}
			}

			#endregion Feathers

			#region SpiralDust

			if (player.HasBuff(ModContent.BuffType<HasteBuff>()))
			{
				timer3 += 2;
			}
			else
			{
				timer3++;
			}

			if (timer3 >= 40)
			{
				if (Main.GameUpdateCount % 2 == 0)
				{
					Dust dust = Dust.NewDustPerfect(new Vector2((float)(position.X + (WaveLength * Math.Sin(increaseY / WaveFrequency))), position.Y - increaseY), ModContent.DustType<LightBubble>(), new Vector2(0, 0));
					dust.noGravity = true;
					dust.scale = 1.2f;
					dust = Dust.NewDustDirect(new Vector2(position.X, position.Y), 0, 0, DustID.Smoke,
						-unit.X * Distance, -unit.Y * Distance);
					dust.fadeIn = 0f;
					dust.noGravity = true;
					dust.scale = 0.88f;
					dust.color = Color.Cyan;
				}

				if (increaseY < Distance)//Moving the spiral up
				{
					increaseY += 10;
				}
			}

			if (timer3 >= 40)
			{
				if (Main.GameUpdateCount % 2 == 0)
				{
					Dust dust2 = Dust.NewDustPerfect(new Vector2((float)(position.X - (WaveLength * Math.Sin(increaseY2 / WaveFrequency))), position.Y - increaseY2), ModContent.DustType<LightBubble>(), new Vector2(0, 0));
					dust2.noGravity = true;
					dust2.scale = 1.2f;
					dust2 = Dust.NewDustDirect(new Vector2(position.X, position.Y), 0, 0, DustID.Smoke,
						-unit.X * Distance, -unit.Y * Distance);
					dust2.fadeIn = 0f;
					dust2.noGravity = true;
					dust2.scale = 0.88f;
					dust2.color = Color.Cyan;
				}

				if (increaseY2 < Distance)//Moving the spiral up
				{
					increaseY2 += 10f;
				}
			}

			#endregion SpiralDust
		}

		/*
		 * Sets the end of the laser position based on where it collides with something
		 */

		private void SetLaserPosition(Player player)
		{
			for (Distance = MOVE_DISTANCE; Distance <= 2500f; Distance += 1f)
			{
				var start = position + spriterotation * Distance;
				if (!Collision.CanHit(position, 1, 1, start, 1, 1))
				{
					if (!IsAtMaxCharge)
					{
						Distance -= 0f;
						break;
					}
					else if (IsAtMaxCharge && projectile.timeLeft <= 80)
					{
						Distance -= 50f - offDistance;
						offDistance += 0.6f;
						break;
					}
					else
					{
						Distance -= 50f;
						break;
					}
				}
			}
		}

		private void ChargeLaser(Player player)
		{
			if (Charge < MAX_CHARGE)
			{
				if (player.HasBuff(ModContent.BuffType<HasteBuff>()))
				{
					Charge += 2;
				}
				else
				{
					Charge++;
				}
			}
		}

		private void UpdatePlayer(Player player)
		{
			// Multiplayer support here, only run this code if the client running it is the owner of the projectile
			if (projectile.owner == Main.myPlayer)
			{
				Vector2 diff = Main.MouseWorld - player.Center;
				diff.Normalize();
				projectile.velocity = diff;
				projectile.direction = Main.MouseWorld.X > player.position.X ? 1 : -1;
				projectile.netUpdate = true;
			}
			int dir = projectile.direction;
			player.heldProj = projectile.whoAmI; // Update player's held projectile
		}

		private void CastLights()
		{
			// Cast a light along the line of the laser
			DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
			Utils.PlotTileLine(position, position + spriterotation * (Distance - MOVE_DISTANCE), 50, DelegateMethods.CastLight);
		}
	}
}