using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.SpellProjectiles.FirestormSpell
{
	public class Fireball : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 13;
		}

		public override void SetDefaults()
		{
			projectile.width = 64;
			projectile.height = 64;
			projectile.aiStyle = -1;

			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.magic = true;
			projectile.knockBack = 1f;

			projectile.timeLeft = 100;
			projectile.tileCollide = false;
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
			if (Main.rand.Next(3) == 0)
			{
				Dust dust;
				// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				Vector2 position = projectile.position;
				dust = Dust.NewDustDirect(position, projectile.width, projectile.height, DustID.Pixie, 0.2631578f, -2.368421f, 0, new Color(255, 251, 0), 1.25f);
			}

			if (++projectile.frameCounter > 3)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 14)
				{
					projectile.Kill();
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

			return true;
		}
	}
}