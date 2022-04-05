using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
	public class LeafShot : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leaf Shot");
			Main.projFrames[Projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			Projectile.width = 2;
			Projectile.height = 2;
			Projectile.aiStyle = -1;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 1;
			DrawOffsetX = -2;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.tileCollide = false;
			Projectile.alpha = 255;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.Poisoned, 2 * 60);
		}

		public override void AI()
		{
			float velRotation = Projectile.velocity.ToRotation();
			Projectile.rotation = velRotation + MathHelper.ToRadians(90f);
			Projectile.spriteDirection = Projectile.direction;

			if (++Projectile.frameCounter >= 2)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame == 6)
				{
					Projectile.frame = 0;
				}
			}
		}

		public override void Kill(int timeLeft)
		{
			float numberProjectiles = 5 + Main.rand.Next(3); // 5, 6, or 7 shots
			float rotation = MathHelper.ToRadians(45);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(Projectile.velocity.X, Projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f; // Watch out for dividing by 0 if there is only 1 Projectile.
				Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(),Projectile.Center, perturbedSpeed, ModContent.ProjectileType<LeafSplinter>(), Projectile.damage, 0, Projectile.owner, 0, 1);
			}
		}
	}
}