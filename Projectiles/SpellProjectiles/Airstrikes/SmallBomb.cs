using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.SpellProjectiles.Airstrikes
{
	class SmallBomb : ModProjectile
	{
		bool HasGoneDown = false;
		int GlowmaskOpacity = 255;

		bool ShakeLeft = true;
		bool ShakeRight = false;

		bool HasGottenBig = false;
		bool FromNPC = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Airstrike Bomb");
		}

		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.ranged = true;
			projectile.damage = 10;
			projectile.knockBack = 1f;
			projectile.tileCollide = true;
			projectile.hide = true;

			projectile.extraUpdates = 2;
			drawOffsetX = -25;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			FromNPC = true;
			Explode();
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (!HasGoneDown)
			{
				projectile.position += Vector2.Normalize(oldVelocity) * 24f;
				projectile.velocity = Vector2.Zero;
				projectile.timeLeft = 60;

				HasGoneDown = true;
			}

			return false;
		}

		public override bool PreAI()
		{
			if (projectile.timeLeft > 60)
			{
				float velRotation = projectile.velocity.ToRotation();
				projectile.rotation = velRotation;
			}
            if (HasGoneDown)
            {
				GlowmaskOpacity -= 255/100;

				if (Main.GameUpdateCount % 2 == 0)
				{
					if (ShakeLeft)
					{
						projectile.Center -= new Vector2(-2, 0);

						ShakeLeft = false;
						ShakeRight = true;

					}
					else if (ShakeRight)
					{
						projectile.Center -= new Vector2(2, 0);

						ShakeLeft = true;
						ShakeRight = false;
					}

				}
			}
			if (projectile.timeLeft < 3)
			{
				Explode();
			}
			return false;
		}

		private void Explode()
        {
			projectile.tileCollide = false;

			projectile.position = projectile.Center;

			if (!HasGottenBig)
			{
				projectile.width += 100;
				projectile.height += 100;

				HasGottenBig = true;
			}

			projectile.penetrate = -1;
			projectile.Center = projectile.position;

			if (FromNPC)
			{
				projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			// Play explosion sound
			Main.PlaySound(SoundID.Item15, projectile.position);
			// Smoke Dust spawn
			for (int i = 0; i < 25; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			// Fire Dust spawn
			for (int i = 0; i < 40; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Fire, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}
			// Large Smoke Gore spawn
			for (int g = 0; g < 2; g++)
			{
				int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
			}
			// reset size to normal width and height.
			projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
			projectile.width = 48;
			projectile.height = 48;
			projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
			projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);

		}
		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindNPCsAndTiles.Add(index);
		}
		public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
		{

			Texture2D texture = mod.GetTexture("Projectiles/SpellProjectiles/Airstrikes/SmallBomb_Glowmask");

            if (HasGoneDown)
            {
				spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle(0, 0, texture.Width, texture.Height), new Color(255, 255, 255) * ((255 - GlowmaskOpacity) / 255f), projectile.rotation, texture.Size() / 2, projectile.scale, SpriteEffects.None, 0);
			}
		}
	}
}
