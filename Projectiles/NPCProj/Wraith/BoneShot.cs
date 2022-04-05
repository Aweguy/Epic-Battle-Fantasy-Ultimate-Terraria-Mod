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
			Projectile.width = 18;
			Projectile.height = 18;
			Projectile.aiStyle = -1;
			Projectile.penetrate = 1;
			AIType = ProjectileID.Bullet;
			Projectile.timeLeft = 120;
			DrawOffsetX = -2;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.tileCollide = false;
			Projectile.alpha = 100;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.OnFire, 2 * 60);
		}

		public override void AI()
		{
			Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Firefly, 0.2631578f, -2.368421f, 0, Color.Orange, 0.5f);

			float velRotation = Projectile.velocity.ToRotation();
			Projectile.rotation = velRotation + MathHelper.ToRadians(90f);
			Projectile.spriteDirection = Projectile.direction;
		}
	}
}