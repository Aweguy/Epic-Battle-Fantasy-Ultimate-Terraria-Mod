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
			Main.projFrames[projectile.type] = 6;
		}

		public override void SetDefaults()
		{
			projectile.width = 44;
			projectile.height = 104;

			projectile.tileCollide = false;
		}

		public override bool PreAI()
		{
			if (++projectile.frameCounter >= 4)
            {
				projectile.frameCounter = 0;
				if(++projectile.frame >= 6)
                {
					projectile.Kill();
                }
            }

			return false;
		}
	}
}
