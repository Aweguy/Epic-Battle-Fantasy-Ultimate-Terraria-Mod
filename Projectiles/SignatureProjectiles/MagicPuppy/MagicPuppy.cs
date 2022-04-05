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
            Main.projFrames[Projectile.type] = 9;
        }

        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 28;
            Projectile.aiStyle = ProjectileID.MiniMinotaur;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.damage = 0;
            Projectile.knockBack = 1f;
            Projectile.tileCollide = true;
			aiType = 398;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			return false;
        }

        public override bool PreAI()
        {
			Player player = Main.player[Projectile.owner];

			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			EpicPlayer modPlayer = player.GetModPlayer<EpicPlayer>();

			if (player.dead)
			{
				modPlayer.MagicPuppyBuff = false;
			}
			if (modPlayer.MagicPuppyBuff)
			{
				Projectile.timeLeft = 2;

				if ((double)Projectile.velocity.X > 0.5)
				{
					Projectile.spriteDirection = 1;
				}
				else if ((double)Projectile.velocity.X < -0.5)
				{
					Projectile.spriteDirection = -1;
				}

				Projectile.spriteDirection = Projectile.direction;

				if (Projectile.ai[1] > 0f)
				{
					if (Projectile.localAI[1] == 0f)
					{
						Projectile.localAI[1] = 1f;
						Projectile.frame = 1;
					}
					if (Projectile.frame != 0)
					{
						Projectile.frameCounter++;
						if (Projectile.frameCounter > 4)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame == 4)
						{
							Projectile.frame = 0;
						}
					}
				}
				else if (Projectile.velocity.Y == 0f)
				{
					Projectile.localAI[1] = 0f;
					if (Projectile.velocity.X == 0f)
					{
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
					else if ((double)Projectile.velocity.X < -0.8 || (double)Projectile.velocity.X > 0.8)
					{
						Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
						Projectile.frameCounter++;
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame < 5)
						{
							Projectile.frame = 5;
						}
						if (Projectile.frame >= 11)
						{
							Projectile.frame = 5;
						}
					}
					else
					{
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
				}
				else if (Projectile.velocity.Y < 0f)
				{
					Projectile.frameCounter = 0;
					Projectile.frame = 4;
				}
				else if (Projectile.velocity.Y > 0f)
				{
					Projectile.frameCounter = 0;
					Projectile.frame = 4;
				}
				Projectile.velocity.Y = Projectile.velocity.Y + 0.4f;
				if (Projectile.velocity.Y > 10f)
				{
					Projectile.velocity.Y = 10f;
				}
				Vector2 velocity = Projectile.velocity;

				if (Projectile.velocity.Y == 0f)
				{
					if (Projectile.velocity.X == 0f)
					{
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
					else if ((double)Projectile.velocity.X < -0.8 || (double)Projectile.velocity.X > 0.8)
					{
						Projectile.frameCounter += (int)Math.Abs(Projectile.velocity.X);
						Projectile.frameCounter++;
						if (Projectile.frameCounter > 6)
						{
							Projectile.frame++;
							Projectile.frameCounter = 0;
						}
						if (Projectile.frame >= 5)
						{
							Projectile.frame = 0;
						}
					}
					else
					{
						Projectile.frame = 0;
						Projectile.frameCounter = 0;
					}
				}
				else if (Projectile.velocity.Y != 0f)
				{
					Projectile.frameCounter = 0;
					Projectile.frame = 5;
				}
				Projectile.velocity.Y = Projectile.velocity.Y + 0.4f;
				if (Projectile.velocity.Y > 10f)
				{
					Projectile.velocity.Y = 10f;
				}
			}
		}
	}
}*/