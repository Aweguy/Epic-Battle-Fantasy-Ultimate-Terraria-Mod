using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;

namespace EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith
{
    public class LeafShot : ModProjectile
    {



        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leaf Shot");
            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.ranged = true;
            projectile.timeLeft = 1;
            drawOffsetX = -2;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.alpha = 255;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 2 * 60);
        }










        public override void AI()
        {


            



            float velRotation = projectile.velocity.ToRotation();
            projectile.rotation = velRotation + MathHelper.ToRadians(90f);
            projectile.spriteDirection = projectile.direction;




            if (++projectile.frameCounter >= 2)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame == 6)
                {
                    projectile.frame = 0;
                }
            }






        }

        public override void Kill(int timeLeft)
        {
            float numberProjectiles = 5 + Main.rand.Next(3); // 5, 6, or 7 shots
            float rotation = MathHelper.ToRadians(45);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(projectile.Center, perturbedSpeed, mod.ProjectileType("LeafSplinter"), projectile.damage, 0, projectile.owner, 0, 1);
            }
        }





    }
}
