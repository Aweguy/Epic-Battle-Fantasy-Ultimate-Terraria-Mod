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
			Main.projFrames[Projectile.type] = 23;
		}

		private bool collision = false;
		private float BlastVel = 14f;
		private bool Veloc = false;

		public override bool? CanDamage()
	=> Projectile.frame >= 22;

		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.aiStyle = -1;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 60 * 5;
			Projectile.tileCollide = false;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (Projectile.frame >= 22)
			{
				if (Projectile.tileCollide)
				{
					Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

					if (Projectile.velocity.X != oldVelocity.X)
					{
						Projectile.velocity.X = -oldVelocity.X;
					}

					if (Projectile.velocity.Y != oldVelocity.Y)
					{
						Projectile.velocity.Y = -oldVelocity.Y;
					}
				}
			}
			return false;
		}

		public override void AI()
		{
			if (++Projectile.frameCounter >= 5) //reducing the frame timer
			{
				Projectile.frameCounter = 0; //resetting it

				if (++Projectile.frame >= 22) //Animation loop
				{
					Projectile.frame = 22;
				}
			}

			if (Projectile.frame < 22) //Positioning and shooting control
			{
				Velocity();
			}
			else
			{
				if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height) && collision == false)
				{
					Projectile.tileCollide = true;

					collision = true;
				}

				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = Projectile.position;
				dust = Terraria.Dust.NewDustDirect(position, Projectile.width, Projectile.height, DustID.Demonite, 0f, 0f, 0, new Color(255, 255, 255), 0.5f);
				dust.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);


				Velocity();

				//Dusting();
			}
		}

		private void Velocity()
		{

			NPC npc = Main.npc[(int)Projectile.ai[0]]; //Sets the npc that the Projectile is spawned and will orbit
			float SphereRotation = Projectile.ai[1];

			#region Positioning

			if (Projectile.frame < 22)
			{
				Projectile.velocity *= 0.8f;
			}
			else if (Projectile.frame >= 22)
			{
				if (Veloc == false)//Making sure that this won't run more than once
				{
					Projectile.velocity = new Vector2(1, 0).RotatedBy(SphereRotation) * BlastVel;

					Veloc = true;
				}
			}

			#endregion Positioning
		}

		private void Dusting()
		{
			if (Main.rand.NextFloat(2f) < 1f)
			{
				Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.GreenTorch, 0, 0, 0, default, 1);
			}
		}
		public override bool PreDraw(ref Color lightColor)
		{
			return this.DrawProjectileCentered(Main.spriteBatch, lightColor);
		}
	}
}
