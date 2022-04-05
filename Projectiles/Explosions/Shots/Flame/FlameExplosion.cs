using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Flame
{
	public class FlameExplosion : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flame Explosion");
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
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Ranged;

			Projectile.tileCollide = false;
			Projectile.alpha = 1;
			baseWidth = Projectile.width;
			baseHeight = Projectile.height;

			Projectile.localNPCHitCooldown = -1;
			Projectile.usesLocalNPCImmunity = true;
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

			if (Main.rand.Next(3) == 0)
			{
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = Projectile.position;
				dust = Dust.NewDustDirect(position, Projectile.width, Projectile.height, DustID.Pixie, 0.2631578f, -2.368421f, 0, new Color(255, 251, 0), 1.25f);
			}

			if (++Projectile.frameCounter >= 2)
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