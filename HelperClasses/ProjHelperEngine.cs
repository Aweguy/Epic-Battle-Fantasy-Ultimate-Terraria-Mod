using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.HelperClasses
{
    public partial class ProjHelperEngine 
    {

        private EpicBattleFantasyUltimate mod;

        public void DoProjectile_OrbitPosition(ModProjectile modProjectile, Vector2 position, double distance, double speed = 1.75)
        {
            Projectile projectile = modProjectile.projectile;

            double deg = speed * (double)projectile.ai[1];
            double rad = deg * (Math.PI / 180);

            projectile.ai[1] += 1f;

            projectile.position.X = position.X - (int)(Math.Cos(rad) * distance) - projectile.width / 2;
            projectile.position.Y = position.Y - (int)(Math.Sin(rad) * distance) - projectile.height / 2;
        }

        public ProjHelperEngine(EpicBattleFantasyUltimate mod)
        {
            


            this.mod = mod;
        }






    }
}
