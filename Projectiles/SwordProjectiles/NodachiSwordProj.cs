using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EpicBattleFantasyUltimate.HelperClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

using System.Threading.Tasks;

namespace EpicBattleFantasyUltimate.Projectiles.SwordProjectiles
{
    public class NodachiSwordProj: ModProjectile
    {
        bool SwordShoot = false;
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.aiStyle = -1;

            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.penetrate = -1;

            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override bool? CanDamage() => false;

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
           // if (player.channel)
            //{
                //Projectile.rotation = 225 * player.direction;
                Projectile.Center = new Vector2(player.Center.X, player.Center.Y + 0);
                Projectile.timeLeft = 10;
                //return false;
            //}
            Projectile.rotation -= 3 * player.direction;
            if (!SwordShoot)
            {
                Vector2 velocity = Vector2.Normalize(Main.MouseWorld - Projectile.Center);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Main.MouseWorld + (Vector2.Normalize(velocity)) * 80f, velocity, ModContent.ProjectileType<NodachiSlash>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            }
            SwordShoot = true;

            return false;
        }
    }
}
