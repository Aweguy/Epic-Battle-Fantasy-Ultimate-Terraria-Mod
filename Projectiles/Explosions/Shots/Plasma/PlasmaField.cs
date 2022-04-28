using EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Plasma;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Plasma
{
	public class PlasmaField : ModProjectile
	{
		private float rotation = 0; //The rotation of the wave to the right
		private int WaveTimer = 0; //The interval between the waves
		private int NumberOfBullets = 50;//The number of bullets each weave will spawn.

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Plasma Field");
			Main.projFrames[Projectile.type] = 13;
		}

		public override void SetDefaults()
		{
			Projectile.width = 64;
			Projectile.height = 64;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.damage = 100;
			Projectile.knockBack = 1f;
			Projectile.tileCollide = false;
			Projectile.scale = 5f;
		}

		public override void AI()
		{
			Shooting(Projectile);

			#region Frame Changing

			Projectile.frameCounter += 1;
			if (Projectile.frameCounter > 2)
			{
				Projectile.frame++;
				Projectile.frameCounter = 0;
				if (Projectile.frame >= 9 && Projectile.ai[0] < 3)
				{
					Projectile.frame = 2;
					Projectile.ai[0]++;
					if (Projectile.ai[0] >= 3)
					{
						Projectile.frame = 10;
					}
				}
				if (Projectile.frame >= 12)
				{
					Projectile.Kill();
				}
			}

			#endregion Frame Changing
		}

		private void Shooting(Projectile Projectile)
		{
			rotation = 0;
			WaveTimer--;

			if (WaveTimer <= 0)
			{
				for (int i = 0; i <= NumberOfBullets; i++)
				{
					Vector2 velocity = Vector2.One.RotatedBy(rotation * 0.0174533);

					Vector2 SpawnPos = Projectile.Center + velocity * 100f;

					Projectile.NewProjectile(Projectile.GetSource_FromThis(), SpawnPos, velocity * 6f, ModContent.ProjectileType<FieldWave>(), Projectile.damage, 0, Main.myPlayer);

					rotation += 360 / NumberOfBullets;
				}



				WaveTimer = 30;
			}
		}

		public override bool PreDraw(ref Color lightColor)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

			Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

			Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * 64, 64, 64), Color.White, Projectile.rotation, new Vector2(32, 32), Projectile.scale, SpriteEffects.None, 0);

			return false;
		}
	}
}