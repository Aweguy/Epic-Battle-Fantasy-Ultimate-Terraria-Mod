using EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Dark;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Dark
{
    public class AntimatterFissure : ModProjectile
    {
        private float rotation = 0;
        private float rotation2 = 180;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Explosion");
            Main.projFrames[projectile.type] = 13;
        }

        private int timer = 20;

        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.ranged = true;
            projectile.damage = 0;
            projectile.knockBack = 1f;
            projectile.timeLeft = 65;
            projectile.tileCollide = false;
            projectile.alpha = 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 7;
        }

        public override void AI()
        {
            timer--;

            if (timer <= 0)
            {
                Vector2 velocity = new Vector2(3, 0).RotatedBy(MathHelper.ToRadians(rotation));

                Projectile.NewProjectile(projectile.Center, velocity, ModContent.ProjectileType<AntimatterSpawn>(), projectile.damage, 0, projectile.owner, 0, 1);

                rotation += 16f;

                Vector2 velocity2 = new Vector2(3, 0).RotatedBy(MathHelper.ToRadians(rotation2));

                Projectile.NewProjectile(projectile.Center, velocity2, ModContent.ProjectileType<AntimatterSpawn>(), projectile.damage, 0, projectile.owner, 0, 1);

                rotation2 += 16f;

                timer = 2;
            }

            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 12)
                {
                    projectile.frame = 0;
                }
            }
        }

        /*public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            Texture2D texture = Main.projectileTexture[projectile.type];

            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle(0, projectile.frame * 64, 64, 64), Color.White, projectile.rotation, new Vector2(32, 32), projectile.scale, SpriteEffects.None, 0);

            return false;

            //return true;
        }*/
    }
}