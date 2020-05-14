using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles
{
	public class blood : ModProjectile
	{
		public int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blood of Balance");
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.arrow = false;
			projectile.width = 110;
			projectile.height = 110;
			projectile.light = 1f;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.ranged = true;
		}
		/*
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			return false;
		}
		*/
		public override void AI()
		{
			timer++;
			Dust.NewDust(projectile.Center, 0, 0, DustID.Blood, 0f, 0f, 0, Color.Red, 5f);

			if (timer >= 1)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.NextFloat(-10, 5), Main.rand.NextFloat(-10, 5), ProjectileID.VampireKnife, projectile.damage, projectile.knockBack, projectile.owner);
				timer = 0;
			}
			if (++projectile.frameCounter >= 5)
			{

				projectile.frameCounter = 0;



				if (++projectile.frame >= 4)
				{
					projectile.Kill();

				}

			}

		}



	}

}