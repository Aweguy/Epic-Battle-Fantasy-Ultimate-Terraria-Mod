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

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Flybots.BlueFlybot
{
    class BlueCannonBehind : ModProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override bool PreAI()
        {
            return false;
        }

        /*public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);
        }*/
    }
}
