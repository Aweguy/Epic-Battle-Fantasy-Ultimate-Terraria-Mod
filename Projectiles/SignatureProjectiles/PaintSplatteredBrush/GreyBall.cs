using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.SignatureProjectiles.PaintSplatteredBrush
{
    public class GreyBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Grey Paint");
        }

        public override void SetDefaults()
        {
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 2;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
        }

        public override void Kill(int timeLeft)
        {
            int a = Projectile.NewProjectile(Projectile.GetSource_FromThis(),Projectile.Center.X, Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<GreyExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
        }
    }
}