﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.PaintSplatteredBrush
{
    public class RedBall : ModProjectile
    {



        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Paint");
        }




        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 5;
            projectile.ranged = true;
            projectile.aiStyle = 2;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
        }


























    }
}
