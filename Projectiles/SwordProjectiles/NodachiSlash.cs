using EpicBattleFantasyUltimate.HelperClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.SwordProjectiles
{
    public class NodachiSlash: ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nodachi Slash");
        }
        public override void SetDefaults()
        {
            //Projectile.CloneDefaults(ProjectileID.Arkhalis);
            Projectile.width = 192;
            Projectile.height = 182;

            Projectile.aiStyle = -1;

            Projectile.friendly = true;
            Projectile.hostile = false;  

            Projectile.penetrate = -1;         

            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            float velRotation = Projectile.velocity.ToRotation();
            Projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            Projectile.spriteDirection = Projectile.direction;
        }
    }
}
