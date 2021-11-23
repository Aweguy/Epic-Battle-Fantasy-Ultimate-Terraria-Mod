using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
	public class BoneShot : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bone Spike");
		}

		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 18;
			projectile.aiStyle = -1;
			projectile.penetrate = 1;
			projectile.ranged = true;
			aiType = ProjectileID.Bullet;
			projectile.timeLeft = 120;
			drawOffsetX = -2;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.tileCollide = false;
			projectile.alpha = 100;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 2 * 60);
		}

		public override void AI()
		{
			Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire, 0.2631578f, -2.368421f, 0, Color.Orange, 0.5f);

			float velRotation = projectile.velocity.ToRotation();
			projectile.rotation = velRotation + MathHelper.ToRadians(90f);
			projectile.spriteDirection = projectile.direction;
		}
	}
}