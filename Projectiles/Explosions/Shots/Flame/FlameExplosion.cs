using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Flame
{
	public class FlameExplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flame Explosion");
			Main.projFrames[projectile.type] = 8;
		}

		private int timer2 = 1;
		private int shrink = 0;
		private int baseWidth;
		private int baseHeight;

		public override void SetDefaults()
		{
			projectile.width = 64;
			projectile.height = 64;

			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.ranged = true;

			projectile.tileCollide = false;
			projectile.alpha = 1;
			baseWidth = projectile.width;
			baseHeight = projectile.height;

			projectile.localNPCHitCooldown = -1;
			projectile.usesLocalNPCImmunity = true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.NextFloat() < 0.4f)
			{
				target.AddBuff(BuffID.OnFire, 300, false);
			}
		}

		public override void AI()
		{
			Vector2 oldSize = projectile.Size;

			timer2--;
			shrink++;
			if (timer2 == 0)
			{
				if (shrink < 5)
				{
					projectile.scale += 0.1f;

					projectile.width = (int)(baseWidth * projectile.scale);
					projectile.height = (int)(baseHeight * projectile.scale);
					projectile.position = projectile.position - (projectile.Size - oldSize) / 2f;

					timer2 = 1;
				}
				else if (shrink >= 5)
				{
					projectile.scale -= 0.05f;

					projectile.width = (int)(baseWidth * projectile.scale);
					projectile.height = (int)(baseHeight * projectile.scale);
					projectile.position = projectile.position - (projectile.Size - oldSize) / 2f;

					timer2 = 1;
				}
			}

			if (Main.rand.Next(3) == 0)
			{
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = projectile.position;
				dust = Dust.NewDustDirect(position, projectile.width, projectile.height, DustID.Pixie, 0.2631578f, -2.368421f, 0, new Color(255, 251, 0), 1.25f);
			}

			if (++projectile.frameCounter >= 2)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 7)
				{
					projectile.Kill();
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

			Texture2D texture = Main.projectileTexture[projectile.type];

			spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle(0, projectile.frame * 64, 64, 64), Color.White, projectile.rotation, new Vector2(32, 32), projectile.scale, SpriteEffects.None, 0);

			return false;

			//return true;
		}
	}
}