#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;
using EpicBattleFantasyUltimate.Buffs.Debuffs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EpicBattleFantasyUltimate.Items.Materials;
#endregion

namespace EpicBattleFantasyUltimate.NPCs.Wraiths
{
	public class MasterWraith : ModNPC
	{

		#region Variables

		bool Enraged = false;//Determines whether the Wraith is enraged or not.

		int Spiketimer = 60;   //The timer that makes the first projectile be shot.
		int Spiketimer2 = 60;  //The timer that makes the second projectile be shot.

		int Firetimer = 60 * 5;//Defines when the fireballs will start spawning
		int spintimer = 8; //A timer that sets wthe interval between the orbiting fireballs.
		private int currentFireballs = 0;
		private readonly int maxFireballs = 11;


		int LeafStartTimer = 240;//The timer that determines when the Leaves will be shot
		int LeafTimer = 10;//The interval between Special shots.
		int LeafEndStacks = 0;//The stacks that will define when the Wraith will stop the special attack

		int Sawtimer = 60 * 10; //Defines the cooldown of the saw blade attack when enraged.

		int SparkTimer = 120;// The timer that determines when the Spark will be shot when enraged



		int icetimer2 = 4; //The timer that defines the interval between each icicle.
		private int currentIcicles = 0; //How many icicles are currently alive
		private readonly int maxIcicles = 20; //The maximum amount of icicles that will be spawned
		int Icetimer = 60 * 20; //The timer defines when the icicles will spawn when enraged

		int BlinkTimer = 60 * 20;//Determines when the wraith will blink
		bool Blinking = false;//Determines when the Wraith will not attack and blink in a random location around the player
		bool Blinked = false;//Determines if the Wraith has blinked

		int ChoiceTimer;//The interval between attack decision
		int Choice = 0;//The current attack choice
		int PrevChoice = 0;//The previous attack choice

		int SpecChoiceTimer;//The interval between the special attack decision
		int SpecChoice = 0;//The current special attack choice
		int PrevSpecChoice = 0;//The previous special attack choice

		int CursingTimer = 60 * 5;//The timer that makes the Wraith Curse you.


		Vector2 velocity;

		#endregion

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.Wraith);
			npc.width = (int)(72 * 0.8);
			npc.height = (int)(96 * 0.8);

			npc.lifeMax = 6000;
			npc.damage = 150;
			npc.defense = 100;
			npc.lifeRegen = 4;
			npc.alpha = 100;
			npc.knockBackResist = 1;

			npc.aiStyle = 22;
			aiType = NPCID.Wraith;
			npc.noTileCollide = true;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(Main.rand.Next(EpicBattleFantasyUltimate.MasterWraithTouchDebuffs.ToArray()), 60 * 20);
		}

		


		public override void AI()
		{
			#region Attack AI

			Player player = Main.player[npc.target]; //Target

			if(npc.life <= (int)(npc.lifeMax * 0.5f))
			{
				Enraged = true;
			}

			Blink(npc, player);

			MovementDirection(npc);

			if (!Enraged)
			{
				if (Choice == 0)
				{
					ChoiceTimer--;
				}

				if (SpecChoice == 0)
				{
					SpecChoiceTimer--;
				}

				if (ChoiceTimer <= 0 && Choice == 0)
				{
					Choice = Choosing(npc);
				}

				if (SpecChoiceTimer <= 0 && SpecChoice == 0)
				{
					SpecChoice = SpecChoosing(npc);
				}

				if (!Blinking && player.statLife > 0)
				{
					Shooting(npc);


					if (SpecChoice == 1)
					{
						Fireballs(npc);
					}
					else if (SpecChoice == 2)
					{
						Icicles(npc);
					}
					else if (SpecChoice == 3)
					{
						Curse(npc);
					}
					else if (SpecChoice == 4)
					{
						Sawblade(npc);
					}

					if (Choice == 1)
					{
						SparkBall(npc);
					}
					else if (Choice == 2)
					{
						Leaves(npc);
					}

				}

			}
			else if (Enraged)
			{
				Shooting(npc);
				Fireballs(npc);
				Sawblade(npc);
				Icicles(npc);
				Leaves(npc);
				Curse(npc);
				SparkBall(npc);
			}

			#endregion

		}

		private void Blink(NPC npc, Player player)
		{
			if (Enraged)
			{
				BlinkTimer -= 2;
			}
			else
			{
				BlinkTimer--;
			}

			if (BlinkTimer <= 0)
			{
				Blinking = true;

				if (!Blinked)
				{
					npc.alpha += 2;
				}

				if (npc.alpha >= 255)
				{
					if (!Blinked)
					{
						int SpawnChoice = Main.rand.Next(EpicBattleFantasyUltimate.MasterWraithSummoning.ToArray()); //The Wraith that will be spawned based on the List

						int npcIndex = NPC.NewNPC((int)(npc.Center.X), (int)(npc.Center.Y), SpawnChoice, 0, 0f, 0f, 0f, 0f, 255);//Spawnign the Wraith

						npc.position = new Vector2(player.Center.X + Main.rand.Next(-1000, 1000) * player.direction, player.Center.Y - Main.rand.Next(100, 300));//Blinking
					}

					Blinked = true;
				}

				if(Blinked && npc.alpha > 100)
				{
					npc.alpha -= 2;

					if(npc.alpha <= 100)
					{
						Blinked = false;
						Blinking = false;
						BlinkTimer = 60 * 20;
					}
				}
			}
		}

		#region MovementDirection

		private void MovementDirection(NPC npc)
		{


			if (npc.velocity.X > 0f) // This is the code that makes the sprite turn. Based on the vanilla one.
			{
				npc.direction = 1;
			}
			else if (npc.velocity.X < 0f)
			{
				npc.direction = -1;
			}
			else if (npc.velocity.X == 0)
			{
				npc.direction = npc.oldDirection;
			}


			if (npc.direction == 1)
			{
				npc.spriteDirection = 1;
			}
			else if (npc.direction == -1)
			{
				npc.spriteDirection = -1;
			}


		}

		#endregion

		private void Shooting(NPC npc)
		{
			Spiketimer--;
			Spiketimer2--;

			if(Spiketimer <= 60)
			{
				npc.velocity.X *= 0.9f;
			}

			if(Spiketimer <= 0)
			{
				int Shot = Main.rand.Next(EpicBattleFantasyUltimate.MasterWraithBasic.ToArray());

				if (npc.direction == 1)  //I did not find a better way to do this. This defines the positions the projectile based on its direction.
				{
					int proj = Projectile.NewProjectile(new Vector2(npc.Center.X + 20f, npc.Center.Y), npc.DirectionTo(Main.player[npc.target].Center) * 10f, Shot, 30, 2, Main.myPlayer, 0, 1);
				}
				else if (npc.direction == -1)
				{
					int proj = Projectile.NewProjectile(new Vector2(npc.Center.X - 28f, npc.Center.Y), npc.DirectionTo(Main.player[npc.target].Center) * 10f, Shot, 30, 2, Main.myPlayer, 0, 1);
				}

				Spiketimer = 120;

			}

			if(Spiketimer2 <= 0)
			{

				int Shot = Main.rand.Next(EpicBattleFantasyUltimate.MasterWraithBasic.ToArray());

				if (npc.direction == 1)
				{
				   int proj2 = Projectile.NewProjectile(new Vector2(npc.Center.X + 11f, npc.Center.Y + 12f), npc.DirectionTo(Main.player[npc.target].Center) * 10f, Shot, 30, 2, Main.myPlayer, 0, 1);
				}
				else if (npc.direction == -1)
				{
				   int proj2 = Projectile.NewProjectile(new Vector2(npc.Center.X - 21f, npc.Center.Y + 12f), npc.DirectionTo(Main.player[npc.target].Center) * 10f, Shot, 30, 2, Main.myPlayer, 0, 1);
				}

				Spiketimer2 = 120;
			}
		}

		#region Specials


		private void Fireballs(NPC npc)
		{

			// Eldrazi: I've done some explicit variable statements, so you know what each of these variables is supposed to do.
			// You could shrink this code down, but I'd only do that if you're comfortable with understanding it.

			float fullRotationInFrames = 240;

			if (Enraged)
			{
				Firetimer--;
			}

			if (!Enraged)
			{
				if (++spintimer >= fullRotationInFrames / maxFireballs)
				{
					// Do not attempt to spawn the projectile on clients. Only in singleplayer and server instances.
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<SpinFireball>(), 20, 2, Main.myPlayer, npc.whoAmI);
					}

					spintimer = 0;
					currentFireballs++;
				}

				if (currentFireballs >= maxFireballs)
				{
					currentFireballs = 0;
					SpecChoice = 0;
					SpecChoiceTimer = 60 * 25; //Higher than the base value for balance purposes
				}

			}
			else if (Enraged && Firetimer <= 0)//enraged code
			{
				if (++spintimer >= fullRotationInFrames / maxFireballs)
				{
					// Do not attempt to spawn the projectile on clients. Only in singleplayer and server instances.
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<SpinFireball>(), 20, 2, Main.myPlayer, npc.whoAmI);
					}

					spintimer = 0;
					currentFireballs++;
				}

				if (currentFireballs >= maxFireballs)
				{
					currentFireballs = 0;
					Firetimer = 60 * 20;
				}
			}
		}



		private void Icicles(NPC npc)
		{

			// Eldrazi: I've done some explicit variable statements, so you know what each of these variables is supposed to do.
			// You could shrink this code down, but I'd only do that if you're comfortable with understanding it.
			float fullRotationInFrames = 240;

			if (Enraged)
			{
				Icetimer--;
			}

			if (!Enraged)
			{
				if (++icetimer2 >= fullRotationInFrames / maxIcicles)
				{
					// Do not attempt to spawn the projectile on clients. Only in singleplayer and server instances.
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<SpinIcicle>(), 20, 2, Main.myPlayer, npc.whoAmI);
					}

					icetimer2 = 0;
					currentIcicles++;
				}

				if (currentIcicles >= maxIcicles)
				{
					currentIcicles = 0;
					SpecChoice = 0;
					SpecChoiceTimer = 60 * 25; //Higher than the base value for balance purposes
				}

			}
			else if (Enraged && Icetimer <= 0)
			{
				if (++icetimer2 >= fullRotationInFrames / maxIcicles)
				{
					// Do not attempt to spawn the projectile on clients. Only in singleplayer and server instances.
					if (Main.netMode != NetmodeID.MultiplayerClient)
					{
						Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<SpinIcicle>(), 20, 2, Main.myPlayer, npc.whoAmI);
					}

					icetimer2 = 0;
					currentIcicles++;
				}

				if (currentIcicles >= maxIcicles)
				{
					currentIcicles = 0;
					Icetimer = 60 * 20;
				}
			}
		}


		private void Curse(NPC npc)
		{

			Player player = Main.player[npc.target]; //Target

			if (Enraged)
			{
				CursingTimer--;
			}

			if (!Enraged)
			{
				Projectile.NewProjectile(new Vector2(player.Center.X, player.Center.Y), Vector2.Zero, ModContent.ProjectileType<CursingRune>(), 1, 2, Main.myPlayer, 0, 1);

				SpecChoice = 0;
				SpecChoiceTimer = 60 * 10;
			}
			else if (Enraged && CursingTimer <= 0)
			{
				Projectile.NewProjectile(new Vector2(player.Center.X, player.Center.Y), Vector2.Zero, ModContent.ProjectileType<CursingRune>(), 1, 2, Main.myPlayer, 0, 1);

				CursingTimer = 60 * 5;
			}
		}


		private void Sawblade (NPC npc)
		{
			if (Enraged)
			{
				Sawtimer--;
			}

			if (!Enraged)
			{


				for(int i = 0; i <=5; i++)
				{
					Vector2 spawnPosition = Main.screenPosition - new Vector2(Main.rand.Next(-2000, 500), Main.screenHeight / 2);

					int npcIndex = NPC.NewNPC((int)(spawnPosition.X), (int)(spawnPosition.Y), ModContent.NPCType<WraithSawblade>(), 0, 0f, 0f, 0f, 0f, 255); ;
				}

				SpecChoice = 0;
				SpecChoiceTimer = 60 * 25;


			}
			else if (Enraged && Sawtimer <= 0)//Enraged code
			{

				for(int i = 0; i <= 10; i++)
				{
					Vector2 spawnPosition = Main.screenPosition - new Vector2(Main.rand.Next(-2000, 500), Main.screenHeight / 2);

					int npcIndex = NPC.NewNPC((int)(spawnPosition.X), (int)(spawnPosition.Y), ModContent.NPCType<WraithSawblade>(), 0, 0f, 0f, 0f, 0f, 255);
				}

				Sawtimer = 60 * 5;
			}

		}


		#endregion

		#region Basic Attacks

		private void Leaves(NPC npc)
		{

			if (Enraged)
			{
				LeafStartTimer--;
			}

			if (!Enraged)
			{
				if (LeafEndStacks <= 3)
				{
					LeafTimer--;

					if (LeafTimer <= 0)
					{
						float mult = Main.rand.NextFloat(5f, 10f); //velocity randomization

						velocity = npc.DirectionTo(new Vector2(Main.player[npc.target].Center.X, Main.player[npc.target].Center.Y + 18)) * mult; //Leaf velocity

						Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 18f), velocity, mod.ProjectileType("LeafShot"), 20, 2, Main.myPlayer, 0, 1); //Leaf spawning/

						LeafTimer = 40;
						LeafEndStacks++;

					}
					if (LeafEndStacks == 3)
					{
						LeafEndStacks = 0;
						Choice = 0;
						ChoiceTimer = 150;
					}


				}

			}
			else if (Enraged && LeafStartTimer <= 0)
			{
				if (LeafEndStacks <= 3)
				{
					LeafTimer--;

					if (LeafTimer <= 0)
					{
						float mult = Main.rand.NextFloat(5f, 10f); //velocity randomization



						velocity = npc.DirectionTo(new Vector2(Main.player[npc.target].Center.X, Main.player[npc.target].Center.Y + 18)) * mult; //Leaf velocity

						Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 18f), velocity, mod.ProjectileType("LeafShot"), 20, 2, Main.myPlayer, 0, 1); //Leaf spawning/



						LeafTimer = 40;
						LeafEndStacks++;

					}

					if (LeafEndStacks == 3)
					{
						LeafEndStacks = 0;
						LeafStartTimer = 60 * 5;
					}


				}

			}


		}


		private void SparkBall(NPC npc)
		{
			Player player = Main.player[npc.target]; //Target

			if (Enraged)
			{
				SparkTimer--;
			}

			if (!Enraged)
			{
				int proj4 = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 11), npc.DirectionTo(Main.player[npc.target].Center) * 10f, ModContent.ProjectileType<Sparkle>(), 18, 2, Main.myPlayer, 0, 1);

				Choice = 0;
				ChoiceTimer = 100;

			}
			else if (Enraged && SparkTimer <= 0)
			{
				int proj4 = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 11), npc.DirectionTo(Main.player[npc.target].Center) * 10f, ModContent.ProjectileType<Sparkle>(), 18, 2, Main.myPlayer, 0, 1);

				SparkTimer = 60 * 2;
			}

		}

		#endregion



		private int Choosing(NPC npc)
		{

			Choice = Main.rand.Next(1, 3);

			if (Choice == PrevChoice)
			{
				if (Choice == 1)
				{
					Choice += 1;
				}
				else if (Choice == 2)
				{
					Choice -= 1;
				}
			}

			PrevChoice = Choice;



			return Choice;
		}//Choosing the basic attacks

		private int SpecChoosing(NPC npc)
		{

			SpecChoice = Main.rand.Next(1, 5);

			if (SpecChoice == PrevSpecChoice)
			{
				if(SpecChoice == 1)
				{
					SpecChoice += 1;
				}
				else if (SpecChoice == 2)
				{
					SpecChoice = (Main.rand.NextFloat() > .5f) ? SpecChoice + 1 : SpecChoice - 1;
				}
				else if(SpecChoice == 3)
				{
					SpecChoice = (Main.rand.NextFloat() > .5f) ? SpecChoice + 1 : SpecChoice - 1;
				}
				else if(SpecChoice == 4)
				{
					SpecChoice -= 1;
				}
			}

			PrevSpecChoice = SpecChoice;

			return SpecChoice;
		}//Choosing the Special attacks


		#region Shading

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			/*Texture2D texture = Main.npcTexture[npc.type];
			Vector2 origin = texture.Size() / 2;
			SpriteEffects effects = npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

			DrawData data = new DrawData(texture, npc.Center - Main.screenPosition, null, drawColor * npc.Opacity, npc.rotation, origin, npc.scale, effects, 0);
			GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(ItemID.AcidDye), npc, data);

			data.Draw(spriteBatch);*/

			return (true);
		}
		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
		}

		#endregion

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{

			

			if (Blinking)
			{
				return false;
			}
			else
			{
				return null;
			}

		}

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ModContent.ItemType<SilkScrap>(), 10);
			Item.NewItem(npc.getRect(), ItemID.Bone, 10);
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (NPC.downedPlantBoss && spawnInfo.player.ZoneRockLayerHeight && !Main.dayTime)
			{
				return 0.001f;
			}
			return 0f;
		}
	}
}
