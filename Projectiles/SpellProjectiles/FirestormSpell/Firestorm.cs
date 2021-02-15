using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;



namespace EpicBattleFantasyUltimate.Projectiles.SpellProjectiles.FirestormSpell
{
    public class Firestorm : ModProjectile
    {



        int timer = 0;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fireball");
            Main.projFrames[projectile.type] = 5;
        }

        #region SetDefaults

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.magic = true;
            projectile.knockBack = 1f;
            projectile.arrow = true;
            projectile.timeLeft = 51;
            projectile.tileCollide = false;
            projectile.hide = true;
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
            projectile.damage = 0;
           
            timer--;

            if(timer <= 0)
            {


                int randomizer = Main.rand.Next(3);

                float X = Main.rand.NextFloat(-100f, 100f);
                float Y = Main.rand.NextFloat(-100f, 100f);


                if (randomizer == 0)
                {
                    int a = Projectile.NewProjectile(projectile.Center.X + X, projectile.Center.Y + Y, 0f, 0f, ModContent.ProjectileType<FireballSmall>(), 70, 0, projectile.owner);

                }
                else if (randomizer == 2)
                {
                    int a = Projectile.NewProjectile(projectile.Center.X + X, projectile.Center.Y + Y, 0f, 0f, ModContent.ProjectileType<FireballMed>(), 70, 0, projectile.owner);
                }
                else
                {
                    int a = Projectile.NewProjectile(projectile.Center.X + X, projectile.Center.Y + Y, 0f, 0f, ModContent.ProjectileType<Fireball>(), 70, 0, projectile.owner);
                }




                timer = 5;

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
