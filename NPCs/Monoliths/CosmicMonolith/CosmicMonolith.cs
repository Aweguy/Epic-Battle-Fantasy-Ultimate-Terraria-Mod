using EpicBattleFantasyUltimate.Projectiles.NPCProj.Monoliths.CosmicMonolith;
using EpicBattleFantasyUltimate.Projectiles.StaffProjectiles.JudgementLaser;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Monoliths.CosmicMonolith
{
	class CosmicMonolith : ModNPC
	{
		
		public enum MonolithState// The general AI state of the monolith
		{
			Nothing = 0,
			Teleport = 1,
			Slam = 2
		}

		public enum AttackState// THe attack AI state of the Monolith
		{
			Nothing = 0,
			CosmicSpheres = 1,
			DarkBolt = 2
		}

		public MonolithState AIState// Making an auto property for the general AI
		{
			get => (MonolithState)AIstate;
			set => AIstate = (float)value;
		}
		float AIstate = 0;

		public AttackState AtState// The same as the above for the attack AI
		{
			get => (AttackState)Atstate;
			set => Atstate = (float)value;
		}
		float Atstate = 0;

		public float AttackTimer = 0;//The global timer for the attacks

		private int teleports = 0;//The number of teleports the monolith has done until now

		private bool spawned = false;//The teleport is to teleport

		private Vector2 CosmicSphereSpawn;//Where the spheres will spawn
		private int CosmicSphereMax = 9;//The number of the maximum spheres that will spawn
		private int CosmicSphereCurrent = 0;//The number of the current;y spawned spheres
		private float CosmicSphereRotation;//The rotation for the spheres to create the circles. Used in their velocity

		private int Attack = 0;//The randomized number that'll choose the attack

		private int SlamTimer = 0;//When the monolith will attempt to slam its target
		private int SlamNumber = 0;//The number of slams before its idle "rest"
		private bool HasSlammed = false;//The bool that decides whether gravity should be applied or not.
		private int SlamHeight = 160;//The default height over the player's head.

		private int DoomsdayCounter = 0;//The number of slam sessions before casting Giga Doomsday. 

		private int GlowmaskFrame = 0;
		private int GlowmaskTimer = 0;

		private int rippleCount = 1;//How many distortions there will be
		private int rippleSize = 10;//The size of the distortions
		private int rippleSpeed = 20;//How fast the distortions will travel. Not too applicable here
		private float distortStrength = 200f;//How much distortion is caused by each ripple. Can create super fun effects.

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cosmic Monolith");
		}

		public override void SetDefaults()
		{
			NPC.height = 100;
			NPC.width = 30;

			NPC.lifeMax = 10000;
			NPC.damage = 50;
			NPC.defense = 60;

			NPC.knockBackResist = -1;
			NPC.aiStyle = -1;
			NPC.noGravity = true;
			AIState = MonolithState.Teleport;
		}

		private void Direction(Player target)
		{
			NPC.spriteDirection = target.Center.X > NPC.Center.X ? -1 : 1;
		}

		public override bool PreAI()
		{
			NPC.TargetClosest(true);

			Player player = Main.player[NPC.target];
			float DistanceFromPlayer = Vector2.Distance(NPC.Center, player.Center);//Player viscinity teleportation.
			
			Direction(player);//Direction of the monolith towards the player.

			RippleEffect();
			
			if(AIState == MonolithState.Nothing)//If it does nothing it teleports behind the player when within range and if the player looks at it.
			{
				if (DistanceFromPlayer > 16 * 50)
				{
					AIState = MonolithState.Teleport;
				}

				/*{
					if (player.direction == 1 && NPC.direction == -1)
					{
						Teleportation();
					}
					else if (player.direction == -1 && NPC.direction == 1)
					{
						Teleportation();
					}
				}*/
			}

			if (AIState == MonolithState.Teleport)//The monolith teleports. After the teleport it'll attack.
			{
				Teleportation();
			}
			else if(AIState == MonolithState.Nothing)//The monolith waits around until its next teleport and attack
			{
				SlamTimer++;
				
				if (SlamTimer >= 10 && SlamNumber <= 3)//After half a second teleport and reset the timer
				{
					AIState = MonolithState.Teleport;
					
					SlamTimer = 0;
				}
                else if(SlamNumber == 4 && DoomsdayCounter < 5)
                {
					DoomsdayCounter++;
					SlamTimer = -60;
					SlamNumber = 0;
                }
				else if(DoomsdayCounter >= 5)
                {
					SlamTimer = -120;
					DoomsdayCounter = 0;
                }
			}

			if (AIState == MonolithState.Slam)//
			{
				HasSlammed = false;
				
				Slam(player);
				//Laser(player);
			}

			/*if(AtState == AttackState.Nothing)
            {
				AttackTimer++;
            }*/

			if(++AttackTimer >= 60 * 2)
            {
				Attack = Main.rand.Next(1, 3);
				AtState = (AttackState)Attack;//For now we only have one attack

				if (AtState == AttackState.CosmicSpheres)//Cosmic Spheres attack. Bouncy spheres.
				{
					CosmicSphere();
				}
				else if (AtState == AttackState.DarkBolt)//Homing dark bolt attack.
				{
					DarkBolt(player);
				}
			}
			return true;
		}

		private void RippleEffect()//The constant ripple effect around the monolith
		{
			if (NPC.localAI[0] == 0)
			{
				NPC.localAI[0] = 1;


				if (Main.netMode != NetmodeID.Server && !Filters.Scene["ShockwaveMonolith"].IsActive())
				{
					Filters.Scene.Activate("ShockwaveMonolith", NPC.Center).GetShader().UseColor(rippleCount, rippleSize, rippleSpeed).UseTargetPosition(NPC.Center);
				}
			}

			if (Main.netMode != NetmodeID.Server && Filters.Scene["ShockwaveMonolith"].IsActive())
			{
				Filters.Scene["ShockwaveMonolith"].GetShader().UseProgress(0.2f).UseOpacity(distortStrength).UseTargetPosition(NPC.Center);
			}

		}

		private void Teleportation()
		{
			NPC.TargetClosest(true);
			spawned = true;
			Player player = Main.player[NPC.target];
			NPC.netUpdate = true;
			if ((double)player.position.X > (double)NPC.position.X) NPC.spriteDirection = 1;
			else if ((double)player.position.X < (double)NPC.position.X) NPC.spriteDirection = -1;
			NPC.TargetClosest(true);
			NPC.velocity.X = NPC.velocity.X * 0.93f;
			NPC.velocity.X = 0.0f;
			NPC.velocity.Y = 5.0f;

			if ((double)NPC.velocity.X > -0.1 && (double)NPC.velocity.X < 0.1) NPC.velocity.X = 0.0f;

			if (teleports <= 10)//If it's teleported less than 10 times keep teleporting
			{
				if (spawned && (double)NPC.ai[0] == 0.0) NPC.ai[0] = 500f;

				if (teleports == 0 )//Initial teleportation cheaty effect
				{
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<Cosmolith_Teleport>(), 0, 0, player.whoAmI);
				}

				if ((double)NPC.ai[2] != 0.0 && (double)NPC.ai[3] != 0.0)
				{

					NPC.position.X = (float)((double)NPC.ai[2] * 16.0 - (double)(NPC.width / 2) + 8.0);
					NPC.position.Y = NPC.ai[3] * 16f - (float)NPC.height;
					NPC.velocity.X = 0.0f;
					NPC.velocity.Y = 0.0f;
					NPC.ai[2] = 0.0f;
					NPC.ai[3] = 0.0f;
					teleports++;
				}

				if(teleports == 11)//ending teleportation cheaty effect
				{
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero, ModContent.ProjectileType<Cosmolith_Teleport>(), 0, 0, player.whoAmI);
				}
			}
			else//Resetting the variables and passing to the attack state.
			{
				AIState = MonolithState.Slam;
				teleports = 0;
			}

			++NPC.ai[0];
			
			int spawn = Main.rand.Next(1, 1);//How fast the monolith teleports
			if ((int)NPC.ai[0] >= spawn)
			{
				NPC.ai[0] = 1.0f;
				int pX = (int)player.position.X / 16;
				int pY = (int)player.position.Y / 16;
				int x = (int)NPC.position.X / 16;
				int y = (int)NPC.position.Y / 16;
				int rand = 40;
				int distance = 0;
				bool checkDistance = false;
				if ((double)Math.Abs(NPC.position.X - player.position.X) + (double)Math.Abs(NPC.position.Y - player.position.Y) > 2000)
				{
					distance = 500;
					checkDistance = true;
				}

				while (!checkDistance && distance < 500)
				{

					++distance;
					int k = Main.rand.Next(pX - rand, pX + rand);
					for (int j = Main.rand.Next(pY - rand, pY + rand); j < pY + rand; ++j)
					{

						if ((j < pY - 4 || j > pY + 4 || (k < pX - 4 || k > pX + 4)) && (j < y - 1 || j > y + 1 || (k < x - 1 || k > x + 1)) && !Main.tile[k, j].IsActuated)
						{
							bool teleport = true;
							if (Main.tile[k, j - 1].LiquidType == LiquidID.Lava)
								teleport = false;
							if (teleport && Main.tileSolid[(int)Main.tile[k, j].TileType] && !Collision.SolidTiles(k - 1, k + 1, j - 4, j - 1))
							{
								NPC.ai[2] = (float)k;
								NPC.ai[3] = (float)j;
								checkDistance = true;
								spawned = false;
								break;
							}
						}
					}
				}
				NPC.netUpdate = true;
			}
		}

		private void CosmicSphere()//The cosmic bouncy  sphere attack code
		{
			if (++AttackTimer > 20)
			{
				CosmicSphereSpawn = Main.screenPosition + new Vector2((Main.screenWidth / 2) + Main.rand.Next(-750, 750), (Main.screenHeight / 2) + Main.rand.Next(-300, 300));
				for (CosmicSphereCurrent = 0; CosmicSphereCurrent <= CosmicSphereMax; CosmicSphereCurrent++)
				{
					CosmicSphereRotation = MathHelper.ToRadians(0 + 40 * CosmicSphereCurrent);

					Projectile.NewProjectile(NPC.GetSource_FromAI(), CosmicSphereSpawn, new Vector2(10, 0).RotatedBy(CosmicSphereRotation), ModContent.ProjectileType<CosmicSphere>(), 20, 0, Main.myPlayer, NPC.whoAmI, CosmicSphereRotation);
				}
				AttackTimer = 0;
			}		
			else//Resetting the variables.
			{
				AttackTimer = 0;
				Attack = 0;
				AtState = AttackState.Nothing;
			}
		}
		private void DarkBolt(Player target)//The code that shoots the dark bolt 
		{
			Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2(NPC.Center.X - 50, NPC.Center.Y), new Vector2(0, -1) * 10f, ModContent.ProjectileType<DarkBolt>(), 30, 0, Main.myPlayer, NPC.whoAmI, 0);

			AttackTimer = 0;
			Attack = 0;
			AtState = AttackState.Nothing;
		}

		private void Slam(Player player)//The slam melee attack of the monolith
		{
			//SlamTimer++;
			
			if (SlamTimer < 10)
			{
				/*if(AttackTimer == 1)
				{
					Projectile.NewProjectile(NPC.Center, Vector2.Zero, ModContent.ProjectileType<Cosmolith_Teleport>(), 0, 0, target.whoAmI);//teleportation cheaty effect for the slam attack
				}*/
				
				NPC.Center = new Vector2(player.Center.X,player.Center.Y - SlamHeight);//Positioning over the player's head.
				SlamHeight += 3;//Incrementing the height to simulate the animation of preparing to slam.
			}
			else if (SlamTimer > 10)
			{
                if (!HasSlammed)
                {
					NPC.velocity.Y = 10f;//Slaming downwards
					HasSlammed = true;
				}
				NPC.velocity.Y += 3f;
			}

			if (SlamTimer++ >= 30)//Resetting the variables shortly after slaming
			{
				SlamTimer = 0;
				SlamNumber++;
				SlamHeight = 160;//Back to the normal Slam Height
				AIState = MonolithState.Nothing;
			}
		}

		private void Laser(Player player)
		{
			if(++AttackTimer == 1)
			{
				int num233 = (int)((float)player.Center.X) / 16;
				int num234 = (int)((float)player.Center.Y) / 16;
				if (player.gravDir == -1f)
				{
					num234 = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)player.Center.Y) / 16;
				}
				for (; num234 < Main.maxTilesY && Main.tile[num233, num234] != null && !WorldGen.SolidTile2(num233, num234) && Main.tile[num233 - 1, num234] != null && !WorldGen.SolidTile2(num233 - 1, num234) && Main.tile[num233 + 1, num234] != null && !WorldGen.SolidTile2(num233 + 1, num234); num234++)
				{
				}
				Projectile.NewProjectile(NPC.GetSource_FromAI(), new Vector2((float)player.Center.X, (float)(num234 * 16)), new Vector2(0, -1), ModContent.ProjectileType<Doomsday>(), NPC.damage, 10f, Main.myPlayer, ai1: NPC.whoAmI);
			}
			else if (AttackTimer > 120)
			{
				AttackTimer = 0;
				AIState = MonolithState.Nothing;
			}

		}

		public override void FindFrame(int frameHeight)//Glowmask and NPC animation 
		{
			if (++GlowmaskTimer >= 5)
			{
				GlowmaskTimer = 0;
				if (++GlowmaskFrame >= 13)
				{
					GlowmaskFrame = 0;
				}
			}
		}

		public override bool CheckDead()
		{
			if (Main.netMode != NetmodeID.Server && Filters.Scene["ShockwaveMonolith"].IsActive())
			{
				Filters.Scene["ShockwaveMonolith"].Deactivate();
			}

			return true;
		}
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
		{
			#region GlowMask
			SpriteEffects spriteEffects = SpriteEffects.None;

			if (NPC.direction == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}

			Texture2D glowmask = ModContent.Request<Texture2D>("EpicBattleFantasyUltimate/NPCs/Monoliths/CosmicMonolith/CosmicMonolith_Glowmask").Value;

			int frameHeight = glowmask.Height / 13;

			int startY = frameHeight * GlowmaskFrame;

			Rectangle sourceRectangle = new Rectangle(0, startY, glowmask.Width, frameHeight);

			Vector2 origin = sourceRectangle.Size() / 2f;

			origin.X = (float)(NPC.spriteDirection == 1 ? sourceRectangle.Width - 20 : 20);

			Main.spriteBatch.Draw(glowmask, NPC.Center - Main.screenPosition, sourceRectangle, Color.White, NPC.rotation, origin, NPC.scale, spriteEffects, 0f);
			#endregion//Drawing the Glowmask
			base.PostDraw(spriteBatch, screenPos, drawColor);
        }
	}  
}
