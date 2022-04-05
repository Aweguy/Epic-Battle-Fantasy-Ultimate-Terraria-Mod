using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.OreExplosions
{
	public class SapphireExplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sapphire Explosion");
			Main.projFrames[Projectile.type] = 8;
		}

		private int timer2 = 1;
		private int shrink = 0;
		private int baseWidth;
		private int baseHeight;

		public override void SetDefaults()
		{
			Projectile.width = 64;
			Projectile.height = 64;
			Projectile.aiStyle = -1;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.friendly = false;
			Projectile.hostile = true;
			Projectile.alpha = 1;
			baseWidth = Projectile.width;
			baseHeight = Projectile.height;
			Projectile.scale = 1.5f;
		}

		public override void AI()
		{
			Vector2 oldSize = Projectile.Size;

			timer2--;
			shrink++;
			if (timer2 == 0)
			{
				if (shrink < 5)
				{
					Projectile.scale += 0.1f;

					Projectile.width = (int)(baseWidth * Projectile.scale);
					Projectile.height = (int)(baseHeight * Projectile.scale);
					Projectile.position = Projectile.position - (Projectile.Size - oldSize) / 2f;

					timer2 = 1;
				}
				else if (shrink >= 5)
				{
					Projectile.scale -= 0.05f;

					Projectile.width = (int)(baseWidth * Projectile.scale);
					Projectile.height = (int)(baseHeight * Projectile.scale);
					Projectile.position = Projectile.position - (Projectile.Size - oldSize) / 2f;

					timer2 = 1;
				}
			}

			if (++Projectile.frameCounter >= 3)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= 7)
				{
					Projectile.Kill();
				}
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