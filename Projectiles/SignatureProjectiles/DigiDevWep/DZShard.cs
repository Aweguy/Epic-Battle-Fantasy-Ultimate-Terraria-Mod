/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Dark;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;



namespace EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.DigiDevWep
{
    public class DZShard :ModProjectile
    {
        bool Returning = false;//Whether the projectile is returning to the owner
        float Acceleration = 1.1f;//How fast the projectiles returning to the player will accelerate
        float Velocity = 1f;//How fast the projectiles will move while returning to the player
        int ReturnTimer = 90;//When the shards will return to the player.
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height =1 ;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }

        public override bool PreAI()
        {
            Player player = Main.player[(int)Projectile.ai[0]];

            if (!Returning)
            {
                Projectile.velocity *= 0.9f;
            }
            else
            {
                Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * Velocity;

                if (Velocity <= 10f)
                {
                    Velocity *= Acceleration;
                }
                if(Velocity > 10f)
                {
                    Velocity = 10f;    
                }

                if(Projectile.Center == player.Center)
                {
                    Projectile.Kill();
                }
            }

            if (--ReturnTimer <= 0)
            {
                Returning = true;
            }
            return false;
        }
    }
}
*/