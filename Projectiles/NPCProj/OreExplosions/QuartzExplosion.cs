using EpicBattleFantasyUltimate.Buffs.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.OreExplosions
{
    public class QuartzExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Quartz Explosion");
            Main.projFrames[projectile.type] = 13;
        }

        private int timer2 = 1;
        private int shrink = 0;
        private int baseWidth;
        private int baseHeight;

        public override void SetDefaults()
        {
            projectile.width = 64;
            projectile.height = 64;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.alpha = 1;
            baseWidth = projectile.width;
            baseHeight = projectile.height;
            projectile.scale = 1.5f;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            float chance = Main.rand.NextFloat(1f);

            if (chance <= 0.3f)
            {
                for (int i = 0; i < Player.MaxBuffs; ++i)
                {
                    if (target.buffType[i] != 0 && !Main.debuff[target.buffType[i]] && !target.HasBuff(ModContent.BuffType<BlessedBuff>()))
                    {
                        target.DelBuff(i);
                        i--;
                    }
                }
            }
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
                if (++projectile.frame >= 12)
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