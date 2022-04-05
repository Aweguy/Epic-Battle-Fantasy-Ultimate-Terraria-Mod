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

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Monoliths.CosmicMonolith
{
	class Cosmolith_Teleport : ModProjectile
	{

        public override void SetStaticDefaults()
        {
			Main.projFrames[Projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			Projectile.width = 44;
			Projectile.height = 104;

			Projectile.tileCollide = false;
		}

		public override bool PreAI()
		{
			if (++Projectile.frameCounter >= 4)
            {
				Projectile.frameCounter = 0;
				if(++Projectile.frame >= 6)
                {
					Projectile.Kill();
                }
            }

			return false;
		}
	}
}
