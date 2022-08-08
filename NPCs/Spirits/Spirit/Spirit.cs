using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Spirits.Spirit
{
	public class Spirit : ModNPC
	{
		bool LoopingAnim = true;
		float AIstate = 0;
		int AttackTImer = 60 * 3;

		int ProjectileCount = 0;
		int ProjectileTimer = 10;

		int HandNumber = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spirit");
			Main.npcFrameCount[NPC.type] = 22;
		}

		public override void SetDefaults()
		{
			NPC.CloneDefaults(NPCID.Wraith);
			NPC.width = 16;
			NPC.height = 16;

			NPC.lifeMax = 195;
			NPC.damage = 20;
			NPC.defense = 50;
			NPC.lifeRegen = 4;

			NPC.aiStyle = 22;
			AIType = NPCID.Wraith;
			NPC.knockBackResist = -1;
			NPC.noTileCollide = true;
			NPC.noGravity = true;
		}

		public override bool PreAI()
		{
			NPC.TargetClosest(true);
			Player Target = Main.player[NPC.target]; //Target

			#region Movement Direction

			if (NPC.direction == 1)
			{
				NPC.spriteDirection = 1;
			}
			else if (NPC.direction == -1)
			{
				NPC.spriteDirection = -1;
			}

			#endregion Movement Direction

			if (HandNumber <= 1)
			{
				SpawnHand();
			}
			
			if(--AttackTImer <= 0)
			{
				LoopingAnim = false;
				Projectiles(Target);
				
			}

			return true;
		}

		private void Projectiles(Player Target)
        {
			if(--ProjectileTimer <= 0)
            {
				if(++ProjectileCount <= 5)
                {
					Vector2 Velocity = Vector2.Normalize(Target.Center - NPC.Center) * 5f;
					Vector2 perturbedSpeed = new Vector2(Velocity.X, Velocity.Y).RotatedByRandom(MathHelper.ToRadians(15));
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, perturbedSpeed, ModContent.ProjectileType<SpiritProj>(), NPC.damage, 3f, Main.myPlayer);

					ProjectileTimer = 5;
				}
                else
                {
					ProjectileCount = 0;
					ProjectileTimer = 10;
					AttackTImer = 60 * 10;
					LoopingAnim = true;
                }
			}

			
		}

		private void SpawnHand()
		{
			NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SpiritHands>(), 0, NPC.whoAmI, HandNumber, AIstate);
			HandNumber++;
		}

		public override void FindFrame(int frameHeight)
		{
			if (++NPC.frameCounter >= 5)
			{
				
				if (LoopingAnim)
				{
					NPC.frame.Y = (NPC.frame.Y + frameHeight) % (frameHeight * 14);
					NPC.frameCounter = 0;
				}
				else if(!LoopingAnim)
				{
					if(NPC.frameCounter < 5)
                    {
						NPC.frame.Y = 13 * frameHeight;
                    }
					else if(NPC.frameCounter < 10)
                    {
						NPC.frame.Y = 14 * frameHeight;
					}
					else if (NPC.frameCounter < 15)
					{
						NPC.frame.Y = 15 * frameHeight;
					}
					else if (NPC.frameCounter < 20)
					{
						NPC.frame.Y = 16 * frameHeight;
					}
					else if (NPC.frameCounter < 25)
					{
						NPC.frame.Y = 17 * frameHeight;
					}
					else if (NPC.frameCounter < 30)
					{
						NPC.frame.Y = 18 * frameHeight;
					}
					else if (NPC.frameCounter < 35)
					{
						NPC.frame.Y = 19 * frameHeight;
					}
					else if (NPC.frameCounter < 40)
					{
						NPC.frame.Y = 20 * frameHeight;
					}
					else if (NPC.frameCounter < 45)
					{
						NPC.frame.Y = 21 * frameHeight;
					}
					else if (NPC.frameCounter < 50)
					{
						NPC.frame.Y = 22 * frameHeight;
						NPC.frameCounter = 0;
					}
					NPC.frame.Y = (NPC.frame.Y + frameHeight) % (frameHeight * 22);
				}
			}

			/*NPC.frameCounter++;
				if (NPC.frameCounter < 5)
				{
					NPC.frame.Y = 17 * frameHeight;
				}
				else if (NPC.frameCounter < 10)
				{
					NPC.frame.Y = 18 * frameHeight;
				}
				else if (NPC.frameCounter < 15)
				{
					NPC.frame.Y = 19 * frameHeight;
				}
				else
				{
					NPC.frameCounter = 0;
				}*/

		}

        public override void HitEffect(int hitDirection, double damage)
        {

        }
    }

	public class SpiritHands : ModNPC
	{

		int HandNumber;
		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[NPC.type] = 4;
		}

		public override void SetDefaults()
		{
			NPC.width = 8;
			NPC.height = 8;
			NPC.lifeMax = 195;
			NPC.damage = 20;
			NPC.defense = 50;
			NPC.lifeRegen = 4;
			//NPC.scale = 2;
			NPC.knockBackResist = -1;
			NPC.noTileCollide = true;
			NPC.noGravity = true;
			NPC.dontTakeDamage = true;
		}

		public override bool PreAI()
		{
			HandNumber = (int)NPC.ai[1];
			NPC npc = Main.npc[(int)NPC.ai[0]]; //Sets the npc that the Projectile is spawned and will orbit
			Player Target = Main.player[npc.target];

			if (!npc.active)
			{
				NPC.life = 0;
			}
			if (npc.life <= 0)
			{
				NPC.life = 0;
			}

			if(Vector2.Distance(npc.Center,Target.Center) <= 160f)
            {
				Vector2 target = Target.Center - NPC.Center;
				float num1276 = target.Length();
				float MoveSpeedMult = 12f; //How fast it moves and turns. A multiplier maybe?
				MoveSpeedMult += num1276 /100f; //Balancing the speed. Lowering the division value makes it have more sharp turns.
				int MoveSpeedBal = 50; //NPC does the same as the above.... I do not understand.
				target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
				target *= MoveSpeedMult;
				NPC.velocity = (NPC.velocity * (float)(MoveSpeedBal - 1) + target) / (float)MoveSpeedBal;
			}
            else
            {
				
				if (HandNumber == 0)
				{
					Vector2 target = (npc.Center + new Vector2(16f, 0)) - NPC.Center;
					float num1276 = target.Length();
					float MoveSpeedMult = 12f; //How fast it moves and turns. A multiplier maybe?
					MoveSpeedMult += num1276 / 100f; //Balancing the speed. Lowering the division value makes it have more sharp turns.
					int MoveSpeedBal = 50; //NPC does the same as the above.... I do not understand.
					target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
					target *= MoveSpeedMult;
					NPC.velocity = (NPC.velocity * (float)(MoveSpeedBal - 1) + target) / (float)MoveSpeedBal;
					//NPC.Center = npc.Center + new Vector2(16f, 0);
				}
				else
				{
					Vector2 target = (npc.Center + new Vector2(-16f, 0)) - NPC.Center;
					float num1276 = target.Length();
					float MoveSpeedMult = 12f; //How fast it moves and turns. A multiplier maybe?
					MoveSpeedMult += num1276 / 100f; //Balancing the speed. Lowering the division value makes it have more sharp turns.
					int MoveSpeedBal = 50; //NPC does the same as the above.... I do not understand.
					target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
					target *= MoveSpeedMult;
					NPC.velocity = (NPC.velocity * (float)(MoveSpeedBal - 1) + target) / (float)MoveSpeedBal;
					//NPC.Center = npc.Center + new Vector2(-16f, 0);
				}
			}
			
			return false;
		}

		public override void FindFrame(int frameHeight)
		{
			if (++NPC.frameCounter >= 5)
			{
				NPC.frameCounter = 0;
				NPC.frame.Y = (NPC.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[NPC.type]);
			}
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
	}

	public class SpiritProj : ModProjectile
    {

        public override void SetStaticDefaults()
        {
			Main.projFrames[Projectile.type] = 10;
        }

        public override void SetDefaults()
        {
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.aiStyle = -1;

			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.penetrate = 1;
			Projectile.knockBack = 1f;

			Projectile.timeLeft = 600;
			Projectile.tileCollide = false;
		}

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			target.AddBuff(BuffID.Confused, 60);
        }

        public override bool PreAI()
        {
			if (++Projectile.frameCounter > 4)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 10)
				{
					Projectile.frame = 0;
				}
			}
			return false;
        }
    }
}
