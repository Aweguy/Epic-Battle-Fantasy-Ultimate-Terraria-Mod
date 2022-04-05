using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
	public class LeafSplinter : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leaf Splinter");
			Main.projFrames[Projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			Projectile.width = 9;
			Projectile.height = 9;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			DrawOffsetX = -2;
			Projectile.hostile = true;
			Projectile.friendly = false;
			Projectile.tileCollide = false;
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
	}
}