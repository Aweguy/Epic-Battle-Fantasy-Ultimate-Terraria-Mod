/*using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.MagicPuppy
{
    public class MagicPuppy : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magic Puppy");
            Main.projFrames[projectile.type] = 9;
        }

        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 28;
            projectile.aiStyle = ProjectileID.MiniMinotaur;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.damage = 0;
            projectile.knockBack = 1f;
            projectile.tileCollide = true;
			aiType = 398;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			return false;
        }

        public override bool PreAI()
        {
			Player player = Main.player[projectile.owner];

			return true;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];

			EpicPlayer modPlayer = player.GetModPlayer<EpicPlayer>();

			if (player.dead)
			{
				modPlayer.MagicPuppyBuff = false;
			}
			if (modPlayer.MagicPuppyBuff)
			{
				projectile.timeLeft = 2;

				if ((double)projectile.velocity.X > 0.5)
				{
					projectile.spriteDirection = 1;
				}
				else if ((double)projectile.velocity.X < -0.5)
				{
					projectile.spriteDirection = -1;
				}

				projectile.spriteDirection = projectile.direction;

				if (projectile.ai[1] > 0f)
				{
					if (projectile.localAI[1] == 0f)
					{
						projectile.localAI[1] = 1f;
						projectile.frame = 1;
					}
					if (projectile.frame != 0)
					{
						projectile.frameCounter++;
						if (projectile.frameCounter > 4)
						{
							projectile.frame++;
							projectile.frameCounter = 0;
						}
						if (projectile.frame == 4)
						{
							projectile.frame = 0;
						}
					}
				}
				else if (projectile.velocity.Y == 0f)
				{
					projectile.localAI[1] = 0f;
					if (projectile.velocity.X == 0f)
					{
						projectile.frame = 0;
						projectile.frameCounter = 0;
					}
					else if ((double)projectile.velocity.X < -0.8 || (double)projectile.velocity.X > 0.8)
					{
						projectile.frameCounter += (int)Math.Abs(projectile.velocity.X);
						projectile.frameCounter++;
						if (projectile.frameCounter > 6)
						{
							projectile.frame++;
							projectile.frameCounter = 0;
						}
						if (projectile.frame < 5)
						{
							projectile.frame = 5;
						}
						if (projectile.frame >= 11)
						{
							projectile.frame = 5;
						}
					}
					else
					{
						projectile.frame = 0;
						projectile.frameCounter = 0;
					}
				}
				else if (projectile.velocity.Y < 0f)
				{
					projectile.frameCounter = 0;
					projectile.frame = 4;
				}
				else if (projectile.velocity.Y > 0f)
				{
					projectile.frameCounter = 0;
					projectile.frame = 4;
				}
				projectile.velocity.Y = projectile.velocity.Y + 0.4f;
				if (projectile.velocity.Y > 10f)
				{
					projectile.velocity.Y = 10f;
				}
				Vector2 velocity = projectile.velocity;

				if (projectile.velocity.Y == 0f)
				{
					if (projectile.velocity.X == 0f)
					{
						projectile.frame = 0;
						projectile.frameCounter = 0;
					}
					else if ((double)projectile.velocity.X < -0.8 || (double)projectile.velocity.X > 0.8)
					{
						projectile.frameCounter += (int)Math.Abs(projectile.velocity.X);
						projectile.frameCounter++;
						if (projectile.frameCounter > 6)
						{
							projectile.frame++;
							projectile.frameCounter = 0;
						}
						if (projectile.frame >= 5)
						{
							projectile.frame = 0;
						}
					}
					else
					{
						projectile.frame = 0;
						projectile.frameCounter = 0;
					}
				}
				else if (projectile.velocity.Y != 0f)
				{
					projectile.frameCounter = 0;
					projectile.frame = 5;
				}
				projectile.velocity.Y = projectile.velocity.Y + 0.4f;
				if (projectile.velocity.Y > 10f)
				{
					projectile.velocity.Y = 10f;
				}
			}
		}
	}
}*/