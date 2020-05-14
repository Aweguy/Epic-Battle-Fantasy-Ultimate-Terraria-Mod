using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;



namespace EpicBattleFantasyUltimate.Projectiles.SpellProjectiles
{
    public class FirestormBig : ModProjectile
    {
        int timer = 1;


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Big Fireball 2");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.magic = true;
            projectile.damage = 10;
            projectile.knockBack = 1f;
            projectile.timeLeft = 100;
            projectile.tileCollide = false;
        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.NextFloat() < 0.4f)
            {
                target.AddBuff(BuffID.OnFire, 300, false);
            }
        }

        public override void AI()
        {

            if (Main.rand.Next(3) == 0)
            {
                Dust dust;
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 position = projectile.position;
                dust = Dust.NewDustDirect(position, projectile.width, projectile.height, 55, 0.2631578f, -2.368421f, 0, new Color(255, 251, 0), 1.25f);

            }




            timer--;
            if(timer == 0)
            {
                Vector2 vel = new Vector2(Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(-5, 5));
                Projectile.NewProjectile(projectile.Center, vel, mod.ProjectileType("Fireball"), 65, 0, projectile.owner, 0, 1);
                timer = 1;
            }




            if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }

        }


        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);



            return true;
        }
    }
}
