﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.PaintSplatteredBrush
{
    public class GreenBall : ModProjectile
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


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 60 * 2);
            target.AddBuff(BuffID.Venom, 60 * 2);
            target.AddBuff(BuffID.Frostburn, 60 * 2);
        }


















    }
}
