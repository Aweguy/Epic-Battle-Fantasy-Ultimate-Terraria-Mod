#region Using
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
#endregion
namespace EpicBattleFantasyUltimate.Projectiles.SwordProjectiles
{
	public class BulletBob : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bullet Bob");
		}

		public override void SetDefaults()
		{
			projectile.width = 26;
			projectile.height = 26;

			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = 100;
			projectile.melee = true;

			projectile.knockBack = 7f;
			drawOffsetX = 0;
		}



		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
		}

		public override void AI()
		{	
			projectile.tileCollide = true;
  
			float velXMult = 1f;
			projectile.velocity.X *= velXMult;

			projectile.ai[0]++;
			if(projectile.ai[0] == 30)
			{
				projectile.velocity *= 4.3f;
			}

			float velRotation = projectile.velocity.ToRotation();
			projectile.rotation = velRotation + MathHelper.ToRadians(90f);
			projectile.spriteDirection = projectile.direction;

		}
	}
}
