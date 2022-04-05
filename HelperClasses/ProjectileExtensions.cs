using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.HelperClasses
{
	public static class ProjectileExtensions
	{
		public static void DoProjectile_OrbitPosition(this Projectile Projectile, Vector2 position, float distance, float rotationPerSecond = MathHelper.PiOver2)
		{
			Projectile.ai[1] += rotationPerSecond / 60;

			Projectile.position.X = position.X - (int)(Math.Cos(Projectile.ai[1]) * distance) - Projectile.width / 2;
			Projectile.position.Y = position.Y - (int)(Math.Sin(Projectile.ai[1]) * distance) - Projectile.height / 2;
		}

		public static Vector2 PolarVector(float radius, float theta)//Taken from qwerty's code
		{
			return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
		}

		public static bool DrawProjectileCentered(this ModProjectile p, SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = TextureAssets.Projectile[p.Projectile.type].Value;
			Rectangle frame = texture.Frame(1, Main.projFrames[p.Projectile.type], 0, p.Projectile.frame);
			Vector2 origin = frame.Size() / 2 + new Vector2(p.DrawOriginOffsetX, p.DrawOriginOffsetY);
			SpriteEffects effects = p.Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			Vector2 drawPosition = p.Projectile.Center - Main.screenPosition + new Vector2(p.DrawOffsetX, 0);

			spriteBatch.Draw(texture, drawPosition, frame, lightColor, p.Projectile.rotation, origin, p.Projectile.scale, effects, 0f);

			return (false);
		}

		#region After Effects

		public static void DrawProjectileTrailCentered(this ModProjectile p, SpriteBatch spriteBatch, Color drawColor, float initialOpacity = 0.8f, float opacityDegrade = 0.2f, int stepSize = 1)
		{
			Texture2D texture = TextureAssets.Projectile[p.Projectile.type].Value;

			p.DrawProjectileTrailCenteredWithTexture(texture, spriteBatch, drawColor, initialOpacity, opacityDegrade, stepSize);
		}

		public static void DrawProjectileTrailCenteredWithTexture(this ModProjectile p, Texture2D texture, SpriteBatch spriteBatch, Color drawColor, float initialOpacity = 0.8f, float opacityDegrade = 0.2f, int stepSize = 1)
		{
			Rectangle frame = texture.Frame(1, Main.projFrames[p.Projectile.type], 0, p.Projectile.frame);
			Vector2 origin = frame.Size() / 2;
			SpriteEffects effects = p.Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[p.Projectile.type]; i += stepSize)
			{
				float opacity = initialOpacity - opacityDegrade * i;
				spriteBatch.Draw(texture, p.Projectile.oldPos[i] + p.Projectile.Hitbox.Size() / 2 - Main.screenPosition, frame, drawColor * opacity, p.Projectile.oldRot[i], origin, p.Projectile.scale, effects, 0f);
			}
		}

		#endregion After Effects
	}
}