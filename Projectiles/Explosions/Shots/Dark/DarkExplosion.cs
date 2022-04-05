using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Dark
{
    public class DarkExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Explosion");
            Main.projFrames[Projectile.type] = 8;
        }

        private int timer2 = 1;
        private int shrink = 0;
        private int baseWidth;
        private int baseHeight;

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;

            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;

            Projectile.alpha = 1;
            baseWidth = Projectile.width;
            baseHeight = Projectile.height;

            Projectile.localNPCHitCooldown = -1;
            Projectile.usesLocalNPCImmunity = true;
        }

        public override void AI()
        {
            Vector2 oldSize = Projectile.Size;

            timer2--;
            shrink++;
            if (timer2 == 0)
            {
                if (shrink < 5)
                {
                    Projectile.scale += 0.1f;

                    Projectile.width = (int)(baseWidth * Projectile.scale);
                    Projectile.height = (int)(baseHeight * Projectile.scale);
                    Projectile.position = Projectile.position - (Projectile.Size - oldSize) / 2f;

                    timer2 = 1;
                }
                else if (shrink >= 5)
                {
                    Projectile.scale -= 0.05f;

                    Projectile.width = (int)(baseWidth * Projectile.scale);
                    Projectile.height = (int)(baseHeight * Projectile.scale);
                    Projectile.position = Projectile.position - (Projectile.Size - oldSize) / 2f;

                    timer2 = 1;
                }
            }

            if (++Projectile.frameCounter >= 2)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 7)
                {
                    Projectile.Kill();
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