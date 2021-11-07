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
		public override void SetDefaults()
		{
			projectile.width = projectile.height = 16;
			projectile.aiStyle = -1;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 60 * 20;
			projectile.tileCollide = false;
			projectile.hide = true;
		}

		public override bool PreAI()
		{
			NPC npc = Main.npc[(int)projectile.ai[0]]; //Sets the npc that the projectile is spawned
			Player player = Main.player[npc.target];

			Homing(player);//the Homing functionality of the projectile.
			Dust();

			return false;
		}

		private void Homing(Player player)
		{
			Vector2 target = player.Center - projectile.Center;
			float num1276 = target.Length();
			float MoveSpeedMult = 6f; //How fast it moves and turns.
			MoveSpeedMult += num1276 / 120f; //Balancing the speed. Lowering the division value makes it have more sharp turns.
			int MoveSpeedBal = 50; //npc does the same as the above.... I do not understand.
			target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
			target *= MoveSpeedMult;
			projectile.velocity = (projectile.velocity * (float)(MoveSpeedBal - 1) + target) / (float)MoveSpeedBal;
		}

		private void Dust()
		{
			Dust dust;
			Vector2 position = projectile.position;
			if (Main.rand.NextFloat(0f, 1f) <= .3f)
			{

				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.	
				dust = Terraria.Dust.NewDustDirect(position, projectile.width, projectile.height, DustID.GlowingMushroom, 0f, 0f, 0, new Color(255, 255, 255), 1f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(83, Main.LocalPlayer);

			}
			if (Main.rand.NextFloat(0f, 1f) <= .3f)
			{	
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.

				dust = Terraria.Dust.NewDustDirect(position, projectile.width, projectile.height, DustID.GlowingMushroom, 0f, 0f, 0, new Color(255, 0, 0), 1f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(23, Main.LocalPlayer);
			}
		}


		/*public override void Kill(int timeLeft)
		{
			base.Kill(timeLeft);
		}*/
	}
}
