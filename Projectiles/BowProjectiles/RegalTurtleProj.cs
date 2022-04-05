using EpicBattleFantasyUltimate.ClassTypes;
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

namespace EpicBattleFantasyUltimate.Projectiles.BowProjectiles
{
    class RegalTurtleProj : EpicPiercingBowProj
    {
        public override void SetSafeDefaults()
        {
            Projectile.width = 66;
            Projectile.height = 26;

            velocity = 1f;
            maxTime = 70;
            MinimumDrawTime = 30;
            DamageMultiplier = 1f;
            weaponDamage = 20;
            weaponKnockback = 5f;
        }
    }
}
