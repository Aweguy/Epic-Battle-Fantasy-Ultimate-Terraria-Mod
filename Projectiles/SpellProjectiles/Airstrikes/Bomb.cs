﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EpicBattleFantasyUltimate.HelperClasses;
using Terraria.Audio;

namespace EpicBattleFantasyUltimate.Projectiles.SpellProjectiles.Airstrikes
{
	public class Bomb : ModProjectile
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
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.damage = 10;
			Projectile.knockBack = 1f;
			Projectile.tileCollide = true;
			Projectile.hide = true;
			Projectile.extraUpdates = 2;
			DrawOffsetX = -13;
			DrawOriginOffsetY = -4;

			Projectile.localNPCHitCooldown = -1;
			Projectile.usesLocalNPCImmunity = true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			FromNPC = true;
			Explode();//Exploding after hitting an npc
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (!HasGoneDown)
			{
				Projectile.position += Vector2.Normalize(oldVelocity) * 15f;
				Projectile.velocity = Vector2.Zero;
				Projectile.timeLeft = 60;

				HasGoneDown = true;
			}

			return false;
		}

		public override bool PreAI()
		{
			if(Projectile.timeLeft > 60)
			{
				float velRotation = Projectile.velocity.ToRotation();
				Projectile.rotation = velRotation;
			}

			if (HasGoneDown)
			{
				GlowmaskOpacity -= 255/100;

				if (Main.GameUpdateCount % 2 == 0)
				{
					if (ShakeLeft)
					{
						Projectile.Center -= new Vector2(-2, 0);

						ShakeLeft = false;
						ShakeRight = true;

					}
					else if (ShakeRight)
					{
						Projectile.Center -= new Vector2(2, 0);

						ShakeLeft = true;
						ShakeRight = false;
					}
				}
			}

			if(Projectile.timeLeft < 3)//Exploding after some time after hitting the ground
			{
				Explode();
			}

			return false;
		}

		private void Explode()
		{ 	
			Projectile.tileCollide = false;

			Projectile.position = Projectile.Center;

			if (!HasGottenBig)
			{
				Projectile.width += 200;
				Projectile.height += 200;

				HasGottenBig = true;
			}

			Projectile.penetrate = -1;
			Projectile.Center = Projectile.position;

			if (FromNPC)
			{
				Projectile.Kill();
			}

		}

		public override void Kill(int timeLeft)
		{
			// Play explosion sound
			SoundEngine.PlaySound(SoundID.Item15, Projectile.position);
			// Smoke Dust spawn
			for (int i = 0; i < 50; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Smoke, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			// Fire Dust spawn
			for (int i = 0; i < 80; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Firefly, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, DustID.Firefly, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}
			// Large Smoke Gore spawn
			for (int g = 0; g < 2; g++)
			{
				int goreIndex = Gore.NewGore(new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(new Vector2(Projectile.position.X + (float)(Projectile.width / 2) - 24f, Projectile.position.Y + (float)(Projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 1.5f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			}
		}
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
			behindNPCsAndTiles.Add(index);
		}
        public override void PostDraw(Color lightColor)
        {
			Texture2D texture = ModContent.Request<Texture2D>("EpicBattleFantasyUltimate/Projectiles/SpellProjectiles/Airstrikes/Bomb_Glowmask").Value;

			if (HasGoneDown)
			{
				Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, new Rectangle(0, 0, texture.Width, texture.Height), new Color(255, 255, 255) * ((255 - GlowmaskOpacity) / 255f), Projectile.rotation, texture.Size() / 2, Projectile.scale, SpriteEffects.None, 0);

			}
		}
	}
}