using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles
{
	public class a : ModProjectile
	{
		public int num90;
		public Color color10 = Color.Black;
		public Vector2 vector12;
		public float num65;
		public int timer;




		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rainbow of Balance");
			Main.projFrames[projectile.type] = 2;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override void SetDefaults()
		{
			projectile.arrow = false;
			projectile.width = 16;
			projectile.height = 30;
			projectile.aiStyle = 1;
			projectile.light = 0.25f;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.penetrate = -1;
			projectile.magic = true;
			projectile.timeLeft = 3600;
		}

		public override void AI()
		{
			timer++;
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			if (timer == 20)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 10, ProjectileID.CrystalStorm, projectile.damage, projectile.knockBack, projectile.owner);


			}

			if (timer >= 30)
			{
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 10, ProjectileID.CrystalPulse, projectile.damage, projectile.knockBack, projectile.owner);

				timer = 0;
			}
			//int dust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 75, projectile.velocity.X * 1.2f, projectile.velocity.Y * 1.2f, 130, Color.Green, 1.5f); //
		}


		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{

			//Redraw the projectile with the color not influenced by light

			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);

			for (int k = 0; k < projectile.oldPos.Length; k++)
			{

				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);

				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);

				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);

			}




			return true;

		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("spike"), projectile.damage, projectile.knockBack, projectile.owner);

		}
	}

}