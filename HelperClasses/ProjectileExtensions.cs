﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.HelperClasses
{
	public static class ProjectileExtensions
	{
		public static void DoProjectile_OrbitPosition(this Projectile projectile, Vector2 position, float distance, float rotationPerSecond = MathHelper.PiOver2)
		{
			projectile.ai[1] += rotationPerSecond / 60;

			projectile.position.X = position.X - (int)(Math.Cos(projectile.ai[1]) * distance) - projectile.width / 2;
			projectile.position.Y = position.Y - (int)(Math.Sin(projectile.ai[1]) * distance) - projectile.height / 2;
		}


		public static bool DrawProjectileCentered(this ModProjectile p, SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.projectileTexture[p.projectile.type];
			Rectangle frame = texture.Frame(1, Main.projFrames[p.projectile.type], 0, p.projectile.frame);
			Vector2 origin = frame.Size() / 2 + new Vector2(p.drawOriginOffsetX, p.drawOriginOffsetY);
			SpriteEffects effects = p.projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			Vector2 drawPosition = p.projectile.Center - Main.screenPosition + new Vector2(p.drawOffsetX, 0);

			spriteBatch.Draw(texture, drawPosition, frame, lightColor, p.projectile.rotation, origin, p.projectile.scale, effects, 0f);

			return (false);
		}

		#region After Effects

		public static void DrawProjectileTrailCentered(this ModProjectile p, SpriteBatch spriteBatch, Color drawColor, float initialOpacity = 0.8f, float opacityDegrade = 0.2f, int stepSize = 1)
		{
			Texture2D texture = Main.projectileTexture[p.projectile.type];

			p.DrawProjectileTrailCenteredWithTexture(texture, spriteBatch, drawColor, initialOpacity, opacityDegrade, stepSize);
		}

		public static void DrawProjectileTrailCenteredWithTexture(this ModProjectile p, Texture2D texture, SpriteBatch spriteBatch, Color drawColor, float initialOpacity = 0.8f, float opacityDegrade = 0.2f, int stepSize = 1)
		{
			Rectangle frame = texture.Frame(1, Main.projFrames[p.projectile.type], 0, p.projectile.frame);
			Vector2 origin = frame.Size() / 2;
			SpriteEffects effects = p.projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

			for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[p.projectile.type]; i += stepSize)
			{
				float opacity = initialOpacity - opacityDegrade * i;
				spriteBatch.Draw(texture, p.projectile.oldPos[i] + p.projectile.Hitbox.Size() / 2 - Main.screenPosition, frame, drawColor * opacity, p.projectile.oldRot[i], origin, p.projectile.scale, effects, 0f);
			}
		}

		#endregion After Effects
	}
}