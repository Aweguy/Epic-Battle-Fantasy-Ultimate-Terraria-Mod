using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Flybots.RedFlybot
{
    public class BrokenRedFlybot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Red Flybot");
        }

        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 46;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.hostile = true;
            projectile.penetrate = -1;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.alpha = 255;

            projectile.tileCollide = false;

            projectile.timeLeft = 2;

            projectile.width *= 2;
            projectile.height *= 2;

            Main.PlaySound(SoundID.Item15, projectile.position);

            for (int i = 0; i <= 40; i++)
            {
                Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Fire, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
            return false;
        }

        public override void AI()
        {
            int NpcSpriteDirection = (int)(projectile.ai[0]);

            projectile.rotation += MathHelper.ToRadians(10) * NpcSpriteDirection;

            if (projectile.velocity.Y < 10f)
            {
                projectile.velocity.Y += 0.15f;
                projectile.velocity.X *= 0.95f;
            }
            else
            {
                projectile.velocity.Y = 9f;
                projectile.velocity.X *= 0.95f;
            }
        }
    }
}