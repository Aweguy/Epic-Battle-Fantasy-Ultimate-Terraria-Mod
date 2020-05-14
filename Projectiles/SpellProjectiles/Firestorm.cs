using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;



namespace EpicBattleFantasyUltimate.Projectiles.SpellProjectiles
{
    public class Firestorm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireball 2");
            Main.projFrames[projectile.type] = 5;
        }

        #region SetDefaults

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.magic = true;
            projectile.knockBack = 1f;
            projectile.arrow = true;
            projectile.timeLeft = 100;
        }
        #endregion



        #region OnHitNPC
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat() < 0.9f)
            {
                target.AddBuff(BuffID.OnFire, 300, false);
            }
        }
        #endregion

        #region AI

        public override void AI()
        {

            if (Main.rand.Next(3) == 0)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position;
                dust = Dust.NewDustDirect(position, projectile.width, projectile.height, 55, 0.2631578f, -2.368421f, 0, new Color(255, 251, 0), 1.25f);

            }
            if (++projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 5)
                {
                    projectile.frame = 0;
                }
            }
        }
        #endregion


        #region kill
        public override void Kill(int timeLeft)
        {
            int a = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y - 16f, 0f, 0f, mod.ProjectileType("FirestormBig"), 70, 0, projectile.owner);
            if (Main.netMode != NetmodeID.Server)
            {
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Fireball").WithVolume(.7f).WithPitchVariance(.5f));
            }


        }
        #endregion

        #region PreDraw
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);



            return true;
        }
        #endregion
    }
}
