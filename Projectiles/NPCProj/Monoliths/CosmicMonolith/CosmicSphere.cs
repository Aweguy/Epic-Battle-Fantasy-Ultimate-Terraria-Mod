using EpicBattleFantasyUltimate.HelperClasses;
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
	class CosmicSphere : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("NatureBlast");
			Main.projFrames[projectile.type] = 23;
		}

		private bool collision = false;


		private float BlastVel = 14f;
		private bool Veloc = false;
		Vector2 target;

		public override bool CanDamage()
	=> projectile.frame >= 22;

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.aiStyle = -1;
			projectile.friendly = false;
			projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 60 * 5;
			projectile.tileCollide = false;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (projectile.frame >= 22)
			{
				if (projectile.tileCollide == true)
				{
					Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);

					if (projectile.velocity.X != oldVelocity.X)
					{
						projectile.velocity.X = -oldVelocity.X;
					}

					if (projectile.velocity.Y != oldVelocity.Y)
					{
						projectile.velocity.Y = -oldVelocity.Y;
					}
				}
			}
			return false;
		}

		public override void AI()
		{
			if (++projectile.frameCounter >= 5) //reducing the frame timer
			{
				projectile.frameCounter = 0; //resetting it

				if (++projectile.frame >= 22) //Animation loop
				{
					projectile.frame = 22;
				}
			}

			if (projectile.frame < 22) //Positioning and shooting control
			{
				Velocity();
			}
			else
			{
				if (!Collision.SolidCollision(projectile.position, projectile.width, projectile.height) && collision == false)
				{
					projectile.tileCollide = true;

					collision = true;
				}

				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = projectile.position;
				dust = Terraria.Dust.NewDustDirect(position, projectile.width, projectile.height, DustID.Demonite, 0f, 0f, 0, new Color(255, 255, 255), 0.5f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);


				Velocity();

				//Dusting();
			}
		}

		private void Velocity()
		{

			NPC npc = Main.npc[(int)projectile.ai[0]]; //Sets the npc that the projectile is spawned and will orbit
			float SphereRotation = projectile.ai[1];

			#region Positioning

			if(projectile.frame < 22)
			{
				projectile.velocity *= 0.8f;
			}
			else if(projectile.frame >= 22)
			{
				if (Veloc == false)//Making sure that this won't run more than once
				{
					projectile.velocity = new Vector2(1,0).RotatedBy(SphereRotation) * BlastVel;

					Veloc = true;
				}
			}

			#endregion Positioning
		}

		private void Dusting()
		{
			if (Main.rand.NextFloat(2f) < 1f)
			{
				Dust.NewDustDirect(projectile.Center, projectile.width, projectile.height, DustID.GreenTorch, 0, 0, 0, default, 1);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			return this.DrawProjectileCentered(spriteBatch, lightColor);
		}
	}
}
