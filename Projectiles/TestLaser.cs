using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
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
			Projectile.width = 4;
			Projectile.height = 4;
			Projectile.timeLeft = 60;
			Projectile.penetrate = -1;
			Projectile.hostile = true;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
		}

		public override void AI()
		{
			Player target = Main.player[(int)Projectile.ai[0]];
			Projectile.Center = target.Center;
			//Projectile.localAI[0] += 1f;
			//Projectile.alpha = (int)Projectile.localAI[0] * 2;
			/*if (Projectile.localAI[0] > 90f)
			{
				Projectile.damage = 0;
			}
			if (Projectile.localAI[0] > 120f)
			{
				Projectile.Kill();
			}*/
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			float point = 0f;
			Vector2 endPoint = Main.npc[(int)Projectile.ai[1]].Center;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, endPoint, 4f, ref point);
		}

        public override bool PreDraw(ref Color lightColor)
        {

			Vector2 endPoint = Main.npc[(int)Projectile.ai[1]].Center;
			Vector2 unit = endPoint - Projectile.Center;
			float length = unit.Length();
			unit.Normalize();
			for (float k = 0; k <= length; k += 4f)
			{
				Vector2 drawPos = Projectile.Center + unit * k - Main.screenPosition;
				Main.spriteBatch.Draw(TextureAssets.Projectile[Projectile.type].Value, drawPos, null, Color.Indigo, k, new Vector2(2, 2), 1f, SpriteEffects.None, 0f);
			}
			return false;
		}
	}
}
