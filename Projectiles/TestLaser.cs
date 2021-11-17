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

namespace EpicBattleFantasyUltimate.Projectiles
{
	class TestLaser : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Elemental Laser");
		}

		public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.timeLeft = 60;
			projectile.penetrate = -1;
			projectile.hostile = true;
			projectile.magic = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}

		public override void AI()
		{
			Player target = Main.player[(int)projectile.ai[0]];
			projectile.Center = target.Center;
			//projectile.localAI[0] += 1f;
			//projectile.alpha = (int)projectile.localAI[0] * 2;
			/*if (projectile.localAI[0] > 90f)
			{
				projectile.damage = 0;
			}
			if (projectile.localAI[0] > 120f)
			{
				projectile.Kill();
			}*/
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			float point = 0f;
			Vector2 endPoint = Main.npc[(int)projectile.ai[1]].Center;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, endPoint, 4f, ref point);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 endPoint = Main.npc[(int)projectile.ai[1]].Center;
			Vector2 unit = endPoint - projectile.Center;
			float length = unit.Length();
			unit.Normalize();
			for (float k = 0; k <= length; k += 4f)
			{
				Vector2 drawPos = projectile.Center + unit * k - Main.screenPosition;
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, Color.Indigo, k, new Vector2(2, 2), 1f, SpriteEffects.None, 0f);
			}
			return false;
		}
	}
}
