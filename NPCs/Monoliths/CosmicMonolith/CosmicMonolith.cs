using EpicBattleFantasyUltimate.Projectiles.NPCProj.Monoliths.CosmicMonolith;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
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
			Attack = 2
		}

		public enum AttackState// THe attack AI state of the Monolith
		{
			CosmicSpheres = 0,
			Slam = 1,
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
		private int CosmicSphereCirclesCurrent = 0;//The number of the currently spawned sphere circles
		private int CosmicSphereCirclesMax = 5;//The number of the maximum sphere circles that will spawn
		private int CosmicSphereMax = 9;//The number of the maximum spheres that will spawn
		private int CosmicSphereCurrent = 0;//The number of the current;y spawned spheres
		private float CosmicSphereRotation;//The rotation for the spheres to create the circles. Used in their velocity

		private bool AttackChosen = false;//Whether an attack is chosen after teleporting.
		private int Attack = 0;//The randomized number that'll choose the attack

		private int GlowmaskFrame = 0;
		private int GlowmaskTimer = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cosmic Monolith");
		}

		public override void SetDefaults()
		{
			npc.height = 100;
			npc.width = 30;

			npc.lifeMax = 1000;
			npc.defense = 60;

			npc.knockBackResist = -1;
			npc.aiStyle = -1;
			npc.noGravity = true;
			AIState = MonolithState.Teleport;
		}

		private void Direction(Player target)
		{
			npc.spriteDirection = target.Center.X > npc.Center.X ? -1 : 1;

		}

		public override bool PreAI()
		{
			npc.TargetClosest(true);

			Player player = Main.player[npc.target];

			Direction(player);//Direction of the monolith towards the player.
			
			if(AIState == MonolithState.Nothing)//If it does nothing it teleports behind the player when within range and if the player looks at it.
			{

				if (AttackChosen == false)//Choose an attack and random.
				{
					Attack = Main.rand.Next(0, 3);

					AttackChosen = true;
				}

				/*float DistanceFromPlayer = Vector2.Distance(npc.Center, player.Center);//Player viscinity teleportation.
				if(DistanceFromPlayer <= 16 * 30)
				{
					if (player.direction == 1 && npc.direction == -1)
					{
						Teleportation();
					}
					else if (player.direction == -1 && npc.direction == 1)
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
				AttackTimer++;

				if(AttackTimer >= 60 * 5)//After 5 seconds teleport and reset the timer
				{
					AIState = MonolithState.Teleport;

					AttackTimer = 0;
				}
			}
			else if (AIState == MonolithState.Attack)//Choosing an attack from the roster
			{

				if (AttackChosen == false)//Choose an attack and random.
				{
					Attack = Main.rand.Next(0, 3);

					AttackChosen = true;
				}

				AtState = (AttackState)Attack;//For now we only have one attack

				if(AtState == AttackState.CosmicSpheres)//Cosmic Spheres attack. Bouncy spheres.
				{
					CosmicSphere();
				}
				else if (AtState == AttackState.Slam)//Slam attack.
				{
					Slam(player);
				}
				else if (AtState == AttackState.DarkBolt)//Homing dark bolt attack.
				{
					DarkBolt(player);
				}
			}
			return false;
		}

		private void DarkBolt(Player target)//The code that shoots the dark bolt 
		{
			Projectile.NewProjectile(new Vector2(npc.Center.X - 50, npc.Center.Y), new Vector2(0, -1) * 10f, ModContent.ProjectileType<DarkBolt>(), 30, 0, Main.myPlayer, npc.whoAmI, 0);

			AIState = MonolithState.Nothing;
			AttackChosen = false;
		}

		private void Teleportation()
		{
			npc.TargetClosest(true);
			spawned = true;
			Player player = Main.player[npc.target];
			npc.netUpdate = true;
			if ((double)player.position.X > (double)npc.position.X) npc.spriteDirection = 1;
			else if ((double)player.position.X < (double)npc.position.X) npc.spriteDirection = -1;
			npc.TargetClosest(true);
			npc.velocity.X = npc.velocity.X * 0.93f;
			npc.velocity.X = 0.0f;
			npc.velocity.Y = 5.0f;

			if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1) npc.velocity.X = 0.0f;

			if(teleports <= 10)//If it's teleported less than 10 times keep teleporting
			{
				if (spawned && (double)npc.ai[0] == 0.0) npc.ai[0] = 500f;

				if ((double)npc.ai[2] != 0.0 && (double)npc.ai[3] != 0.0)
				{

					npc.position.X = (float)((double)npc.ai[2] * 16.0 - (double)(npc.width / 2) + 8.0);
					npc.position.Y = npc.ai[3] * 16f - (float)npc.height;
					npc.velocity.X = 0.0f;
					npc.velocity.Y = 0.0f;
					npc.ai[2] = 0.0f;
					npc.ai[3] = 0.0f;
					teleports++;
				}
			}
			else//Resetting the variables and passing to the attack state.
			{
				AIState = MonolithState.Attack;
				teleports = 0;
			}

			++npc.ai[0];
			
			int spawn = Main.rand.Next(1, 1);//How fast the monolith teleports
			if ((int)npc.ai[0] >= spawn)
			{
				npc.ai[0] = 1.0f;
				int pX = (int)player.position.X / 16;
				int pY = (int)player.position.Y / 16;
				int x = (int)npc.position.X / 16;
				int y = (int)npc.position.Y / 16;
				int rand = 40;
				int distance = 0;
				bool checkDistance = false;
				if ((double)Math.Abs(npc.position.X - player.position.X) + (double)Math.Abs(npc.position.Y - player.position.Y) > 2000)
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
						
						if ((j < pY - 4 || j > pY + 4 || (k < pX - 4 || k > pX + 4)) && (j < y - 1 || j > y + 1 || (k < x - 1 || k > x + 1)) && Main.tile[k, j].nactive())
						{
							bool teleport = true;
							if (Main.tile[k, j - 1].lava())
								teleport = false;
							if (teleport && Main.tileSolid[(int)Main.tile[k, j].type] && !Collision.SolidTiles(k - 1, k + 1, j - 4, j - 1))
							{
								npc.ai[2] = (float)k;
								npc.ai[3] = (float)j;
								checkDistance = true;
								spawned = false;
								break;
							}
						}	
					}
				}
				npc.netUpdate = true;
				}
		}

		private void CosmicSphere()//The cosmic bouncy  sphere attack code
		{
			
			if(CosmicSphereCirclesCurrent <= CosmicSphereCirclesMax)
			{	
				if (++AttackTimer > 20)
				{
					CosmicSphereSpawn = Main.screenPosition + new Vector2((Main.screenWidth / 2) + Main.rand.Next(-750, 750), (Main.screenHeight / 2) + Main.rand.Next(-300, 300));
					for (CosmicSphereCurrent = 0; CosmicSphereCurrent <= CosmicSphereMax; CosmicSphereCurrent++)
					{
						CosmicSphereRotation = MathHelper.ToRadians(0 + 40 * CosmicSphereCurrent);

						Projectile.NewProjectile(CosmicSphereSpawn, new Vector2(10, 0).RotatedBy(CosmicSphereRotation), ModContent.ProjectileType<CosmicSphere>(), 20, 0, Main.myPlayer, npc.whoAmI, CosmicSphereRotation);
					}
					AttackTimer = 0;
					CosmicSphereCirclesCurrent++;
				}		
			}
			else//Resetting the variables.
			{
				AttackTimer = 0;
				CosmicSphereCirclesCurrent = 0;
				AttackChosen = false;
				AIState = MonolithState.Nothing;
			}

		}

		private void Slam(Player target)
		{
			AttackTimer++;
			if (AttackTimer >= 90)//Resetting the variables shortly after slaming
			{
				AttackTimer = 0;
				AIState = MonolithState.Teleport;
				AttackChosen = false;
			}

			if (AttackTimer <= 30)
			{
				npc.Center = new Vector2(target.Center.X,target.Center.Y - 150);//Positioning over the player's head.
			}
			else if (AttackTimer > 30)
			{
				npc.velocity.Y = 10f;//Slaming downwards
			}
		}

		public override void FindFrame(int frameHeight)//Glowmask and npc animation 
		{
			if (++GlowmaskTimer >= 5)
			{
				GlowmaskTimer = 0;
				if (++GlowmaskFrame >= 13)
					GlowmaskFrame = 0;
			}

			/*if (++npc.frameCounter >= 7)
			{
				npc.frameCounter = 0;
				npc.frame.Y = (npc.frame.Y + frameHeight) % (frameHeight * 1);
			}*/
		}


		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;

			if (npc.direction == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}

			Texture2D texture = mod.GetTexture("NPCs/Monoliths/CosmicMonolith/CosmicMonolith_Glowmask");

			int frameHeight = texture.Height / 13;

			int startY = frameHeight * GlowmaskFrame;

			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);

			Vector2 origin = sourceRectangle.Size() / 2f;

			origin.X = (float)(npc.spriteDirection == 1 ? sourceRectangle.Width - 20 : 20);

			Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, sourceRectangle, Color.White, npc.rotation, origin, npc.scale, spriteEffects, 0f);
		}

	}  
}
