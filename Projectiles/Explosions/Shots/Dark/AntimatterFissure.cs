using EpicBattleFantasyUltimate.Projectiles.Bullets.Shots.Dark;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
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
            Main.projFrames[Projectile.type] = 13;
        }

        private int timer = 20;

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType= DamageClass.Ranged;
            Projectile.damage = 0;
            Projectile.knockBack = 1f;
            Projectile.timeLeft = 65;
            Projectile.tileCollide = false;
            Projectile.alpha = 1;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[Projectile.owner] = 7;
        }

        public override void AI()
        {
            timer--;

            if (timer <= 0)
            {
                Vector2 velocity = new Vector2(3, 0).RotatedBy(MathHelper.ToRadians(rotation));

                Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(),Projectile.Center, velocity, ModContent.ProjectileType<AntimatterSpawn>(), Projectile.damage, 0, Projectile.owner, 0, 1);

                rotation += 16f;

                Vector2 velocity2 = new Vector2(3, 0).RotatedBy(MathHelper.ToRadians(rotation2));

                Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(),Projectile.Center, velocity2, ModContent.ProjectileType<AntimatterSpawn>(), Projectile.damage, 0, Projectile.owner, 0, 1);

                rotation2 += 16f;

                timer = 2;
            }

            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 12)
                {
                    Projectile.frame = 0;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, new Rectangle(0, Projectile.frame * 64, 64, 64), Color.White, Projectile.rotation, new Vector2(32, 32), Projectile.scale, SpriteEffects.None, 0);

            return false;
        }
    }
}