/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EpicBattleFantasyUltimate.Buffs.Debuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;



namespace EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.DigiDevWep
{
    public class DigiDevWep_Proj: ModProjectile
    {
        float RandomRot = 0;
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
        }

        public override bool? CanDamage()
        {
            return false;
        }

        public override bool PreAI()
        {
            Projectile.velocity *= 0.9f;

            Projectile.rotation = Vector2.Normalize(Main.MouseWorld - Projectile.Center).ToRotation();

            return false;
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[(int)Projectile.ai[0]];
            for(int i = 0; i < 5; i++)
            {
                RandomRot = Main.rand.NextFloat(0f, 360f);
                Vector2 Velocity = new Vector2(1, 0).RotatedBy(RandomRot) * Main.rand.NextFloat(0.2f, 16f);

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Velocity, ModContent.ProjectileType<DZShard>(), Projectile.damage / 3, Projectile.knockBack, Projectile.owner,player.whoAmI);
            }
        }
    }
}
*/