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
using Terraria.GameContent;

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

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pulsar");
			Main.projFrames[Projectile.type] = 10;
		}

		private int baseWidth;
		private int baseHeight;

		public override void SetDefaults()
		{
			Projectile.width = 128;
			Projectile.height = 128;

			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.knockBack = 1f;

			Projectile.timeLeft = 60 * 100000;
			Projectile.tileCollide = false;

			baseWidth = Projectile.width;
			baseHeight = Projectile.height;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];

			if (++Projectile.frameCounter >= 5)//frame calculation for the animation
			{
				Projectile.frameCounter = 0;

				//Dust generation when it begins growing.

				#region Start Dust

				if (Projectile.frame == 8)
				{
					Vector2 origin = new Vector2(Projectile.Center.X, Projectile.Center.Y);

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

				if (++Projectile.frame >= 10)
				{
					//passive dust spawning

					#region Dust Spawning

					Color drawColor = Color.Black;
					if (Main.rand.Next(2) == 0)
					{
						drawColor = Color.Red;
					}

					Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 0, drawColor, 1f);

					#endregion Dust Spawning

					Vector2 oldSize = Projectile.Size;
					// In Multi Player (MP) This code only runs on the client of the Projectile's owner, this is because it relies on mouse position, which isn't the same across all clients.
					if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0f)
					{
						Movement(player);//The movement of the black hole

						var epicPlayer = EpicPlayer.ModPlayer(player);

						// If the player channels the weapon, do something. This check only works if item.channel is true for the weapon.
						if (player.channel && epicPlayer.LimitCurrent > 0)
						{
							LimitDrain(player);//Draining limit break

							Scaling(player, oldSize);//The growth of the black hole

							Sucking();//Sucking enemy npcs (or friendly) not bosses

							PlayerSucking(player);//Sucking the player and damaging them if too close

							GoreSucking();//Sucking gore

							DustSucking();//Sucking dust
						}
						// If the player stops channeling, do something else.
						else if (Projectile.ai[0] == 0f || epicPlayer.LimitCurrent <= 0)//The damage when it ends
						{
							Projectile.timeLeft = 1;

							Damage();//The method that calculates the damage
						}
					}
					if (Projectile.frame >= 16)
					{
						Projectile.frame = 10;
					}
				}
			}
			return false;
		}

		//The limit break draining
		private void LimitDrain(Player player)
		{
			if (--DrainTimer <= 0 && player.GetModPlayer<EpicPlayer>().LimitCurrent > 0)
			{
				player.GetModPlayer<EpicPlayer>().LimitCurrent--;

				DrainTimer = 60;
			}
			else if (--DrainTimer <= 0 && player.GetModPlayer<EpicPlayer>().LimitCurrent <= 0)
			{
				Projectile.Kill();

				DrainTimer = 60;
			}
		}
		//Movement of the black hole
		private void Movement(Player player)
		{
			float maxDistance = 3f; // This also sets the maximun speed the Projectile can reach while following the cursor.
			Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;
			float distanceToCursor = vectorToCursor.Length();

			// Here we can see that the speed of the Projectile depends on the distance to the cursor.
			if (distanceToCursor > maxDistance)
			{
				distanceToCursor = maxDistance / distanceToCursor;
				vectorToCursor *= distanceToCursor;
			}

			int velocityXBy1000 = (int)(vectorToCursor.X * 3f);
			int oldVelocityXBy1000 = (int)(Projectile.velocity.X * 3f);
			int velocityYBy1000 = (int)(vectorToCursor.Y * 3f);
			int oldVelocityYBy1000 = (int)(Projectile.velocity.Y * 3f);

			// This code checks if the precious velocity of the Projectile is different enough from its new velocity, and if it is, syncs it with the server and the other clients in MP.
			// We previously multiplied the speed by 1000, then casted it to int, this is to reduce its precision and prevent the speed from being synced too much.
			if (velocityXBy1000 != oldVelocityXBy1000 || velocityYBy1000 != oldVelocityYBy1000)
			{
				Projectile.netUpdate = true;
			}

			Projectile.velocity = vectorToCursor;
		}
		//The growth rate of the black hole
		private void Scaling(Player player, Vector2 oldSize)
		{
			if (player.HasBuff(ModContent.BuffType<HasteBuff>()))
			{
				if (Projectile.width <= 150)
				{
					Projectile.scale += 0.2f;
				}
				else if (Projectile.width <= 325 && Projectile.width > 150)
				{
					Projectile.scale += 0.1f;
				}
				else
				{
					Projectile.scale += 0.05f;
				}
				Projectile.width = (int)(baseWidth * Projectile.scale);
				Projectile.height = (int)(baseHeight * Projectile.scale);
				Projectile.position = Projectile.position - (Projectile.Size - oldSize) / 2f;
			}
			else
			{
				if (Projectile.width <= 150)
				{
					Projectile.scale += 0.1f;
				}
				else if (Projectile.width <= 325 && Projectile.width > 150)
				{
					Projectile.scale += 0.05f;
				}
				else
				{
					Projectile.scale += 0.025f;
				}
				Projectile.width = (int)(baseWidth * Projectile.scale);
				Projectile.height = (int)(baseHeight * Projectile.scale);
				Projectile.position = Projectile.position - (Projectile.Size - oldSize) / 2f;
			}
		}
		//NPC sucking
		private void Sucking()
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active)
				{
					float between = Vector2.Distance(Projectile.Center, npc.Center);//Calculating the distance

					if(between < 0.1f)
					{
						between = 0.1f;
					}
					gravMagnitude = (Projectile.scale * 75) / between; //gravitaional pull equation

					if (!npc.boss)
					{
						npc.velocity += npc.DirectionTo(Projectile.Center) * gravMagnitude;//applying the gravitational pull force calculated above
					}
				}
			}
		}
		//Player Sucking
		private void PlayerSucking(Player player)
		{
			if (player.active)
			{
				float between = Vector2.Distance(Projectile.Center, player.Center);//Distance

				if (between < 0.1f)
				{
					between = 0.1f;
				}
				if(between <= 50f * Projectile.scale)
				{
					player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} was turned into particles!"), (int)(12 * Projectile.scale), 0, true, false, false);// Damaging the player if too close to the black hole
				}
				gravMagnitude = (Projectile.scale * 14) / between; //gravitaional pull equation
				player.velocity += player.DirectionTo(Projectile.Center) * gravMagnitude;//Final calculation
			}
		}
		//Gore Sucking
		private void GoreSucking()
		{
			for(int i = 0; i < Main.maxGore; i++)
			{
				Gore gore = Main.gore[i];
				if (gore.active)
				{
					float between = Vector2.Distance(Projectile.Center, gore.position);

					if (between < 0.1f)
					{
						between = 0.1f;
					}
					gravMagnitude = (Projectile.scale * 100) / between; //gravitaional pull equation
					gore.velocity -= Vector2.Normalize(gore.position - Projectile.Center) * gravMagnitude;//Final Calculation
				}
			}
		}
		//Dust Sucking
		private void DustSucking()
		{
			for(int i = 0; i < Main.maxDust; i++)
			{
				Dust dust = Main.dust[i];
				if (dust.active)
				{
					float between = Vector2.Distance(Projectile.Center, dust.position);

					if (between < 0.1f)
					{
						between = 0.1f;
					}
					gravMagnitude = (Projectile.scale * 100) / between; //gravitaional pull equation
					dust.velocity -= Vector2.Normalize(dust.position - Projectile.Center) * gravMagnitude;//Final Calculation
				}
			}
		}
		//Damage after it blows up
		private void Damage()
		{
			Projectile.tileCollide = false;
			// Set to transparent. This Projectile technically lives as  transparent for about 3 frames
			// change the hitbox size, centered about the original Projectile center. This makes the Projectile damage enemies during the explosion.
			Projectile.position = Projectile.Center;
			//Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			//Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			if (Projectile.width <= 150)
			{
				Projectile.width += 80;
				Projectile.height += 80;
				Projectile.damage = (80 + Projectile.width) * 3;
			}
			else if (Projectile.width <= 325 && Projectile.width > 150)
			{
				Projectile.width += 220;
				Projectile.height += 220;
				Projectile.damage = (220 + Projectile.width) * 4;
			}
			else
			{
				Projectile.width += 500;
				Projectile.height += 500;
				Projectile.damage = (700 + Projectile.width) * 5;
			}
			Projectile.Center = Projectile.position;
			//Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			//Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
		}
		//The dust when the Projectile dies
		public override void Kill(int timeLeft)
		{
			
			if (Projectile.width <= 150)
			{
				for (int i = 0; i < 25; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Red, 1f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity += Vector2.Normalize(Main.dust[dustIndex].position - Projectile.Center) * 10;
					dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Black, 1.25f);
					Main.dust[dustIndex].velocity += Vector2.Normalize(Main.dust[dustIndex].position - Projectile.Center) * 10;
				}
			}
			else if (Projectile.width <= 325 && Projectile.width > 150)
			{
				for (int i = 0; i < 50; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Red, 1.5f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity += Vector2.Normalize(Main.dust[dustIndex].position - Projectile.Center) * 10;
					dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Black, 2f);
					Main.dust[dustIndex].velocity += Vector2.Normalize(Main.dust[dustIndex].position - Projectile.Center) * 10;
				}
			}
			else
			{
				for (int i = 0; i < 200; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Red, 2f);
					Main.dust[dustIndex].noGravity = true;
					Main.dust[dustIndex].velocity += Vector2.Normalize(Main.dust[dustIndex].position - Projectile.Center) * 10;
					dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 1f, 1f, 0, Color.Black, 2.5f);
					Main.dust[dustIndex].velocity += Vector2.Normalize(Main.dust[dustIndex].position - Projectile.Center) * 10;
				}
			}

			// Large Smoke Gore spawn
			// reset size to normal width and height.
			Projectile.position.X = Projectile.position.X + (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y + (float)(Projectile.height / 2);
			Projectile.width = 10;
			Projectile.height = 10;
			Projectile.position.X = Projectile.position.X - (float)(Projectile.width / 2);
			Projectile.position.Y = Projectile.position.Y - (float)(Projectile.height / 2);
		}
		//Code for making thte Projectile animate while its position is centered
		public override bool PreDraw(ref Color lightColor)
		{
			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			// This is where the magic happens.
			int frameWidth = texture.Width / 2;
			int frameHeight = texture.Height / Main.projFrames[Projectile.type];

			int frameX = (int)(Projectile.frame / Main.projFrames[Projectile.type]) * frameWidth;
			int frameY = (Projectile.frame % Main.projFrames[Projectile.type]) * frameHeight;
			Rectangle frame = new Rectangle(frameX, frameY, frameWidth, frameHeight);
			// This is where the magic stops :(

			Vector2 origin = frame.Size() / 2;

			Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, frame, lightColor, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0f);

			// Do not allow vanilla drawing code to execute.
			return (false);
		}
	}
}