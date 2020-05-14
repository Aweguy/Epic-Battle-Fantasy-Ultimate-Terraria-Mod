
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;


namespace EpicBattleFantasyUltimate.Projectiles
{
    public class EpicProjectile : GlobalProjectile
    {
        #region Spawn
        bool spawn = false;
        #endregion
        public override bool InstancePerEntity => true;

        #region Kill

        public override void Kill(Projectile projectile, int timeLeft)
        {

            #region Shadow Blaster Effect

            Player player = Main.player[projectile.owner];



            if (player.GetModPlayer<EpicPlayer>().shadow == true && projectile.type != mod.ProjectileType("AntimatterExplosion"))
            {
                

                int a = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("AntimatterExplosion"), 70, 0, projectile.owner);

                spawn = true;

            }
            #endregion

        }

        #endregion


























    }
}
