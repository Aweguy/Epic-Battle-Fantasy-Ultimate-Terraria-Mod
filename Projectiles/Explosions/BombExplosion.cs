using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.Explosions
{
    public class BombExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bomb Explosion");
        }

        #region SetDefaults

        public override void SetDefaults()
        {
            projectile.width = 300;
            projectile.height = 300;
            projectile.damage = 200;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 100;
            projectile.magic = true;
            projectile.knockBack = 10f;
            projectile.timeLeft = 5;
            projectile.alpha = 255;
            projectile.tileCollide = false;
        }

        #endregion SetDefaults
    }
}