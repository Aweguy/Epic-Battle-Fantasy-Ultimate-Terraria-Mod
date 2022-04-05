#region Using

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#endregion Using

namespace EpicBattleFantasyUltimate.Projectiles.SpellProjectiles.FirestormSpell
{
	public class Firestorm : ModProjectile
	{
		private int timer = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fireball");
			Main.projFrames[Projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;

			Projectile.penetrate = -1;
			Projectile.magic = true;
			Projectile.knockBack = 1f;

			Projectile.timeLeft = 51;
			Projectile.tileCollide = false;
			Projectile.hide = true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.NextFloat() < 0.9f)
			{
				target.AddBuff(BuffID.OnFire, 300, false);
			}
		}

		public override void AI()
		{
			Projectile.damage = 0;

			timer--;

			if (timer <= 0)
			{
				int randomizer = Main.rand.Next(3);

				float X = Main.rand.NextFloat(-100f, 100f);
				float Y = Main.rand.NextFloat(-100f, 100f);

				if (randomizer == 0)
				{
					int a = Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(),Projectile.Center.X + X, Projectile.Center.Y + Y, 0f, 0f, ModContent.ProjectileType<FireballSmall>(), 70, 0, Projectile.owner);
				}
				else if (randomizer == 2)
				{
					int a = Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(),Projectile.Center.X + X, Projectile.Center.Y + Y, 0f, 0f, ModContent.ProjectileType<FireballMed>(), 70, 0, Projectile.owner);
				}
				else
				{
					int a = Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(),Projectile.Center.X + X, Projectile.Center.Y + Y, 0f, 0f, ModContent.ProjectileType<Fireball>(), 70, 0, Projectile.owner);
				}

				timer = 5;
			}
		}
        public override bool PreDraw(ref Color lightColor)
        {
			Main.spriteBatch.End();
			Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

			return true;
		}
	}
}