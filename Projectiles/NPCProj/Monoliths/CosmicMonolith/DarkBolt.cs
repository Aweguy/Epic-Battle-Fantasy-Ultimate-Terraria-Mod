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
	class DarkBolt : ModProjectile
	{
		int JumpTimer = 0;//When the projectile will start moving towards the player.
		float JumpVelocity = 6f;//The velocity of each jump towards the player.
		int AnimationTimer = 60 * 10;//Decides when the animation will end and therefore when the projectile will die
        public override void SetStaticDefaults()
        {
			Main.projFrames[Projectile.type] = 12;
		}
        public override void SetDefaults()
		{
			Projectile.width = Projectile.height = 16;
			Projectile.aiStyle = -1;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			//Projectile.hide = true;
		}

		public override bool PreAI()
		{
			NPC npc = Main.npc[(int)Projectile.ai[0]]; //Sets the npc that the Projectile is spawned
			Player player = Main.player[npc.target];

			Homing(player);//the Homing functionality of the Projectile.
			Animation();
			Dust();

			return false;
		}

		private void Homing(Player player)
		{
			
			if(++JumpTimer < 60)
            {
				Projectile.velocity = Vector2.Zero;
            }
			else if(JumpTimer == 60)
            {
				Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * (JumpVelocity + Main.rand.NextFloat(1f,6f));//the speed that the ore will dash towards the player
			}
			else if(JumpTimer > 120)
            {
				JumpTimer = 0;
            }
		}

		private void Animation()
        {
			AnimationTimer--;//Decreasing the animation timer so it stops after some seconds
			if (++Projectile.frameCounter >= 5) //reducing the frame timer
			{
				Projectile.frameCounter = 0; //resetting it

				Projectile.frame++;
				if (Projectile.frame == 9 && AnimationTimer > 0) //Animation loop
				{
					Projectile.frame = 2;
				}
                else if(Projectile.frame >= 11)
                {
					Projectile.Kill();
                }
			}
		}
		private void Dust()
		{
			Dust dust;
			Vector2 position = Projectile.position;
			if (Main.rand.NextFloat(0f, 1f) <= .3f)
			{

				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.	
				dust = Terraria.Dust.NewDustDirect(position, Projectile.width, Projectile.height, DustID.GlowingMushroom, 0f, 0f, 0, new Color(255, 255, 255), 1f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(83, Main.LocalPlayer);

			}
			if (Main.rand.NextFloat(0f, 1f) <= .3f)
			{	
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.

				dust = Terraria.Dust.NewDustDirect(position, Projectile.width, Projectile.height, DustID.GlowingMushroom, 0f, 0f, 0, new Color(255, 0, 0), 1f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(23, Main.LocalPlayer);
			}
		}


		/*public override void Kill(int timeLeft)
		{
			base.Kill(timeLeft);
		}*/
	}
}
