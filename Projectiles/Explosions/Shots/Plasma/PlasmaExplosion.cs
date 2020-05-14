using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EpicBattleFantasyUltimate.Projectiles.Explosions.Shots.Plasma
{
    public class PlasmaExplosion : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasma Explosion");
            Main.projFrames[projectile.type] = 8;
        }
        int timer2 = 1;
        int shrink = 0;
        int baseWidth;
        int baseHeight;

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
            projectile.tileCollide = false;
            baseWidth = projectile.width;
            baseHeight = projectile.height;
        }



        public override void AI()
        {

            Vector2 oldSize = projectile.Size;

            timer2--;
            shrink++;
            if (timer2 == 0)
            {
                if (shrink < 5)
                {
                    projectile.scale += 0.1f;


                    projectile.width = (int)(baseWidth * projectile.scale);
                    projectile.height = (int)(baseHeight * projectile.scale);
                    projectile.position = projectile.position - (projectile.Size - oldSize) / 2f;

                    timer2 = 1;
                }
                else if (shrink >= 5)
                {
                    projectile.scale -= 0.05f;


                    projectile.width = (int)(baseWidth * projectile.scale);
                    projectile.height = (int)(baseHeight * projectile.scale);
                    projectile.position = projectile.position - (projectile.Size - oldSize) / 2f;

                    timer2 = 1;
                }
            }




            if (++projectile.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 7)
                {
                    projectile.Kill();
                }
            }

        }



        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

            Texture2D texture = Main.projectileTexture[projectile.type];

            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle(0, projectile.frame * 64, 64, 64), Color.White, projectile.rotation, new Vector2(32, 32), projectile.scale, SpriteEffects.None, 0);

            return false;

            //return true;
        }







    }
}