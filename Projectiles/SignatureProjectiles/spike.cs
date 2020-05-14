using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Steamworks;

namespace EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles
{
	public class spike : ModProjectile
	{
		public int timer;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mountain of Balance");
			Main.projFrames[projectile.type] = 5;
		}
		public override void SetDefaults()
		{
			projectile.arrow = false;
			projectile.width = 55;
			projectile.height = 55;
			//projectile.aiStyle = 22;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.penetrate = -1;
			projectile.magic = true;
			projectile.timeLeft = 400;

		}

		public override void Kill(int timeLeft)
		{
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("blood"), projectile.damage, projectile.knockBack, projectile.owner);
		}
		public override void AI()
		{
			timer++;
			//projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			if (++projectile.frameCounter >= 5)
			{

				projectile.frameCounter = 0;

				if (++projectile.frame >= 5)
				{

					projectile.frame = 4;

				}

			}

			if (timer == 10)
			{

				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(-20, -1), ProjectileID.TinyEater, projectile.damage, projectile.knockBack, projectile.owner);


			}

			if (timer == 20)
			{

				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, Main.rand.NextFloat(-20, 20), Main.rand.NextFloat(-20, -1), ProjectileID.EatersBite, projectile.damage, projectile.knockBack, projectile.owner);
				timer = 0;

			}
		}
	}
}