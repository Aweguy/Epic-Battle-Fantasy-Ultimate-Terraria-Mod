using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.HelperClasses
{
    public static class ProjectileExtensions
    {


        public static void DoProjectile_OrbitPosition(this Projectile projectile, Vector2 position, float distance, float rotationPerSecond = MathHelper.PiOver2)
        {
            projectile.ai[1] += rotationPerSecond / 60;

            projectile.position.X = position.X - (int)(Math.Cos(projectile.ai[1]) * distance) - projectile.width / 2;
            projectile.position.Y = position.Y - (int)(Math.Sin(projectile.ai[1]) * distance) - projectile.height / 2;
        }







    }
}
