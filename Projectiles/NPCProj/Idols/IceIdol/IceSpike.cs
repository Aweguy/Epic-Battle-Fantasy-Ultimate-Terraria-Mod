using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Idols.IceIdol
{
    public class IceSpike : ModProjectile
    {
        private bool Frame = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wraith Icicle");
            Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 5;
            Projectile.height = 5;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 360;
            Projectile.hostile = true;
            Projectile.friendly = false;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (!Frame)
            {
                Projectile.frame = Main.rand.Next(0, 3);

                Frame = true;
            }

            float velRotation = Projectile.velocity.ToRotation();
            Projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            Projectile.spriteDirection = Projectile.direction;
        }
    }
}