#region Using

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using EpicBattleFantasyUltimate.Buffs.Buffs;
using Terraria.DataStructures;

#endregion Using

namespace EpicBattleFantasyUltimate.Projectiles.StaffProjectiles
{
	public class BlackHole : ModProjectile
	{
		private int DrainTimer = 60;

		private List<Dust> effectDusts = new List<Dust>(); // create a list of dust object
		private float dustSpeed = 5f; //how fast the dust moves
		private float dustBoost = 2f; //The offset from the center from which the dust will spawn
		private int dustSpawnRate = 100; //bigger = more dust, total dust is dustSpawnTime*dustSpawnRate
		private float gravMagnitude; //The power of the gravitational force
		private float direction;//

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pulsar");
			Main.projFrames[projectile.type] = 10;
		}

		private int baseWidth;
		private int baseHeight;

		public override void SetDefaults()
		{
			projectile.width = 128;
			projectile.height = 128;

			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.magic = true;
			projectile.knockBack = 1f;

			projectile.timeLeft = 60 * 100000;
			projectile.tileCollide = false;

			baseWidth = projectile.width;
			baseHeight = projectile.height;
		}

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];

			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;

				//Dust generation when it begins growing.

				#region Start Dust

				if (projectile.frame == 8)
				{
					Vector2 origin = new Vector2(projectile.Center.X, projectile.Center.Y);

					for (int i = 0; i < dustSpawnRate; i++)
					{
						float rot = Main.rand.NextFloat() * (float)Math.PI * 2; //random angle

						dustSpeed = Main.rand.NextFloat(3f, 10f);

						effectDusts.Add(Dust.NewDustPerfect(origin + (new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot)) * dustBoost) * 20f, 31, new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot)) * dustSpeed, 0, new Color(255, 255, 255), 2.5f)); //add new dust to list
						effectDusts[effectDusts.Count - 1].noGravity = true;  //modify the newly created dust
						effectDusts[effectDusts.Count - 1].scale = 1f;
					}
				}

				#endregion Start Dust

				if (++projectile.frame >= 10)
				{
					//passive dust spawning

					#region Dust Spawning

					Color drawColor = Color.Black;
					if (Main.rand.Next(2) == 0)
					{
						drawColor = Color.Red;
					}

					Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Smoke, 0f, 0f, 0, drawColor, 1f);

					#endregion Dust Spawning

					Vector2 oldSize = projectile.Size;
					// In Multi Player (MP) This code only runs on the client of the projectile's owner, this is because it relies on mouse position, which isn't the same across all clients.
					if (Main.myPlayer == projectile.owner && projectile.ai[0] == 0f)
					{
						Movement(player);

						var epicPlayer = EpicPlayer.ModPlayer(player);

						// If the player channels the weapon, do something. This check only works if item.channel is true for the weapon.
						if (player.channel && epicPlayer.LimitCurrent > 0)
						{
							LimitDrain(player);

							Scaling(player, oldSize);

							Sucking();

							PlayerSucking(player);

							GoreSucking();

							DustSucking();
						}
						// If the player stops channeling, do something else.
						else if (projectile.ai[0] == 0f || epicPlayer.LimitCurrent <= 0)
						{
							projectile.timeLeft = 1;

							Damage();
						}
					}
					if (projectile.frame >= 16)
					{
						projectile.frame = 10;
					}
				}
			}
			return false;
		}

		private void LimitDrain(Player player)
		{
			DrainTimer--;

			if (DrainTimer <= 0 && player.GetModPlayer<EpicPlayer>().LimitCurrent > 0)
			{
				player.GetModPlayer<EpicPlayer>().LimitCurrent--;

				DrainTimer = 60;
			}
			else if (DrainTimer <= 0 && player.GetModPlayer<EpicPlayer>().LimitCurrent <= 0)
			{
				projectile.Kill();

				DrainTimer = 60;
			}
		}

		private void Movement(Player player)
		{
			float maxDistance = 3f; // This also sets the maximun speed the projectile can reach while following the cursor.
			Vector2 vectorToCursor = Main.MouseWorld - projectile.Center;
			float distanceToCursor = vectorToCursor.Length();

			// Here we can see that the speed of the projectile depends on the distance to the cursor.
			if (distanceToCursor > maxDistance)
			{
				distanceToCursor = maxDistance / distanceToCursor;
				vectorToCursor *= distanceToCursor;
			}

			int velocityXBy1000 = (int)(vectorToCursor.X * 3f);
			int oldVelocityXBy1000 = (int)(projectile.velocity.X * 3f);
			int velocityYBy1000 = (int)(vectorToCursor.Y * 3f);
			int oldVelocityYBy1000 = (int)(projectile.velocity.Y * 3f);

			// This code checks if the precious velocity of the projectile is different enough from its new velocity, and if it is, syncs it with the server and the other clients in MP.
			// We previously multiplied the speed by 1000, then casted it to int, this is to reduce its precision and prevent the speed from being synced too much.
			if (velocityXBy1000 != oldVelocityXBy1000 || velocityYBy1000 != oldVelocityYBy1000)
			{
				projectile.netUpdate = true;
			}

			projectile.velocity = vectorToCursor;
		}

		private void Scaling(Player player, Vector2 oldSize)
		{
			if (player.HasBuff(ModContent.BuffType<HasteBuff>()))
			{
				if (projectile.width <= 150)
				{
					projectile.scale += 0.2f;
				}
				else if (projectile.width <= 325 && projectile.width > 150)
				{
					projectile.scale += 0.1f;
				}
				else
				{
					projectile.scale += 0.05f;
				}
				projectile.width = (int)(baseWidth * projectile.scale);
				projectile.height = (int)(baseHeight * projectile.scale);
				projectile.position = projectile.position - (projectile.Size - oldSize) / 2f;
			}
			else
			{
				if (projectile.width <= 150)
				{
					projectile.scale += 0.1f;
				}
				else if (projectile.width <= 325 && projectile.width > 150)
				{
					projectile.scale += 0.05f;
				}
				else
				{
					projectile.scale += 0.025f;
				}
				projectile.width = (int)(baseWidth * projectile.scale);
				projectile.height = (int)(baseHeight * projectile.scale);
				projectile.position = projectile.position - (projectile.Size - oldSize) / 2f;
			}
		}

		private void Sucking()
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active)
				{
					float between = Vector2.Distance(projectile.Center, npc.Center);

					if(between < 0.1f)
					{
						between = 0.1f;
					}
					gravMagnitude = (projectile.scale * 75) / between; //gravitaional pull equation

					if (!npc.boss)
					{
						npc.velocity += npc.DirectionTo(projectile.Center) * gravMagnitude;//applying the gravitational pull force calculated above
					}
				}
			}
		}

		private void PlayerSucking(Player player)
		{
			if (player.active)
			{
				float between = Vector2.Distance(projectile.Center, player.Center);

				if (between < 0.1f)
				{
					between = 0.1f;
				}
				if(between <= 80f * projectile.scale)
				{
					player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} was turned into particles!"), (int)(10 * projectile.scale), 0, true, true, false);// Damaging the player if too close to the black hole
				}
				gravMagnitude = (projectile.scale * 14) / between; //gravitaional pull equation
				player.velocity += player.DirectionTo(projectile.Center) * gravMagnitude;
			}
		}

		private void GoreSucking()
		{
			for(int i = 0; i < Main.maxGore; i++)
			{
				Gore gore = Main.gore[i];
				if (gore.active)
				{
					float between = Vector2.Distance(projectile.Center, gore.position);

					if (between < 0.1f)
					{
						between = 0.1f;
					}
					gravMagnitude = (projectile.scale * 100) / between; //gravitaional pull equation
					gore.velocity -= Vector2.Normalize(gore.position - projectile.Center) * gravMagnitude;
				}
			}
		}

		private void DustSucking()
		{
			for(int i = 0; i < Main.maxDust; i++)
			{
				Dust dust = Main.dust[i];
				if (dust.active)
				{
					float between = Vector2.Distance(projectile.Center, dust.position);

					if (between < 0.1f)
					{
						between = 0.1f;
					}
					gravMagnitude = (projectile.scale * 100) / between; //gravitaional pull equation
					dust.velocity -= Vector2.Normalize(dust.position - projectile.Center) * gravMagnitude;
				}
			}
		}

		private void Damage()
		{
			projectile.tileCollide = false;
			// Set to transparent. This projectile technically lives as  transparent for about 3 frames
			// change the hitbox size, centered about the original projectile center. This makes the projectile damage enemies during the explosion.
			projectile.position = projectile.Center;
			//projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
			//projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
			if (projectile.width <= 150)
			{
				projectile.width += 80;
				projectile.height += 80;
				projectile.damage = (80 + projectile.width) * 3;
			}
			else if (projectile.width <= 325 && projectile.width > 150)
			{
				projectile.width += 220;
				projectile.height += 220;
				projectile.damage = (220 + projectile.width) * 4;
			}
			else
			{
				projectile.width += 500;
				projectile.height += 500;
				projectile.damage = (700 + projectile.width) * 5;
			}
			projectile.Center = projectile.position;
			//projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
			//projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
		}

		public override void Kill(int timeLeft)
		{
			
			if (projectile.width <= 150)
			{
				for (int i = 0; i < 25; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Red, 1f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity += Vector2.Normalize(Main.dust[dustIndex].position - projectile.Center) * 10;
					dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Black, 1.25f);
					Main.dust[dustIndex].velocity += Vector2.Normalize(Main.dust[dustIndex].position - projectile.Center) * 10;
				}
			}
			else if (projectile.width <= 325 && projectile.width > 150)
			{
				for (int i = 0; i < 50; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Red, 1.5f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity += Vector2.Normalize(Main.dust[dustIndex].position - projectile.Center) * 10;
					dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Black, 2f);
					Main.dust[dustIndex].velocity += Vector2.Normalize(Main.dust[dustIndex].position - projectile.Center) * 10;
				}
			}
			else
			{
				for (int i = 0; i < 200; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Red, 2f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity += Vector2.Normalize(Main.dust[dustIndex].position - projectile.Center) * 10;
					dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Black, 2.5f);
					Main.dust[dustIndex].velocity += Vector2.Normalize(Main.dust[dustIndex].position - projectile.Center) * 10;
				}
			}

			// Large Smoke Gore spawn
			// reset size to normal width and height.
			projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
			projectile.width = 10;
			projectile.height = 10;
			projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[projectile.type];

			// This is where the magic happens.
			int frameWidth = texture.Width / 2;
			int frameHeight = texture.Height / Main.projFrames[projectile.type];

			int frameX = (int)(projectile.frame / Main.projFrames[projectile.type]) * frameWidth;
			int frameY = (projectile.frame % Main.projFrames[projectile.type]) * frameHeight;
			Rectangle frame = new Rectangle(frameX, frameY, frameWidth, frameHeight);
			// This is where the magic stops :(

			Vector2 origin = frame.Size() / 2;

			spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, frame, lightColor, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);

			// Do not allow vanilla drawing code to execute.
			return (false);
		}
	}
}