using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EpicBattleFantasyUltimate.HelperClasses;
using System.Collections.Generic;
using EpicBattleFantasyUltimate.Dusts;

namespace EpicBattleFantasyUltimate.Projectiles.LimitBreaks.MothEarth
{
    public class MotherEarth : ModProjectile
    {
        int StartDamage = 60 * 1; //When it will start damaging enemies.
        int DamageTimer = 30; //The interval between each damage tick

        #region End Animation Variables

        int EndDamage = 60 * 7;//When the end animation will start.
        bool EndParticles = false;//When the leaves will be launched.
        float[] scaleCache = new float[7]; //After effect based on scale.

        #endregion

        #region Nature Blast Variables

        int BlastTimer = 0;

        #endregion


        #region Animation Dust Variables

        public int timer = 60 * 7;
        List<Dust> effectDusts = new List<Dust>(); // create a list of dust object
        float dustSpeed = 2.5f; //how fast the dust moves
        float dustBoost = 3.25f; //The offset from the center from which the dust will spawn
        int cooldownTime = 100;
        int dustSpawnTime = 50; //how many frames after starting that the dust will be spawning for
        int dustSpawnRate = 1; //bigger = more dust, total dust is dustSpawnTime*dustSpawnRate

        #endregion

        #region Kill Dust variables

        List<Dust> effectDusts2 = new List<Dust>(); // create a list of dust object
        float dustSpeed2 = 5f; //how fast the dust moves
        float dustBoost2 = 2f; //The offset from the center from which the dust will spawn
        int dustSpawnRate2 = 100; //bigger = more dust, total dust is dustSpawnTime*dustSpawnRate

        #endregion


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mother Earth");

            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 7;

        }




        public override void SetDefaults()
        {
            projectile.width = 0;
            projectile.height = 0;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.scale = 1.6f;
            projectile.alpha = 255;
        }



        public override void AI()
        {



            Player player = Main.player[projectile.owner];

            projectile.position = Main.screenPosition + new Vector2(Main.screenWidth / 2, (Main.screenHeight / 2) - 256);


            if(StartDamage > 0)
            {
                projectile.scale -= 0.01f;
                projectile.alpha -= 204 / 60;
            }


            StartDamage--;

            if (EndDamage > 0)
            {
                


                    for (int i = scaleCache.Length - 1; i > 0; --i)
                    {
                        scaleCache[i] = scaleCache[i - 1];
                    }
                    scaleCache[0] = projectile.scale;

                
            }





            if (StartDamage <= 0)
            {

                Particles();

                NatureBlasts(player);


                EndDamage--;

                DamageTimer--;

                if (DamageTimer <= 0 && EndDamage > 0)
                {


                    for (int i = 0; i < Main.maxNPCs; i++)
                    {

                        NPC npc = Main.npc[i];


                        if (!Main.npc[i].active)
                        {
                            continue;
                        }

                        Damage(npc, player);



                    }

                }
                else if (EndDamage <= 0)
                {
                    EndAnimation();
                }


            }




        }


        private void NatureBlasts(Player player)
        {
            BlastTimer--;


            if(BlastTimer <= 0)
            {
                Vector2 origin = new Vector2(projectile.Center.X, projectile.Center.Y);

                float rotation = Main.rand.NextFloat() * (float)Math.PI * 2; //random angle


                Projectile.NewProjectile(origin, Vector2.Zero, ModContent.ProjectileType<NatureBlast>(), 20, 2, Main.myPlayer, projectile.Center.X, projectile.Center.Y);

                BlastTimer = 15;
            }


        }

        private void Damage (NPC npc, Player player)
        {


            npc.AddBuff(BuffID.Poisoned, 60 * 600);

            if (player.whoAmI == Main.myPlayer)
            {


                player.ApplyDamageToNPC(npc, projectile.damage + (100 * EpicWorld.bossesDefeated), 0f, (npc.Center.X - player.Center.X > 0f).ToDirectionInt(), true);


                DamageTimer = 60;
            }

        }

        private void Particles()
        {

            Vector2 origin = new Vector2(projectile.Center.X, projectile.Center.Y);
            if (timer > 0)
            {
                if (timer > cooldownTime - dustSpawnTime)
                {
                    for (int i = 0; i < dustSpawnRate; i++)
                    {
                        float rot = Main.rand.NextFloat() * (float)Math.PI * 2; //random angle
                        effectDusts.Add(Dust.NewDustPerfect(origin + (new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot)) * dustBoost) * 20f, ModContent.DustType<NatureDust>(), new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot)) * dustSpeed, 0, new Color(255, 255, 255), 2.5f)); //add new dust to list
                        effectDusts[effectDusts.Count - 1].noGravity = true;  //modify the newly created dust
                        effectDusts[effectDusts.Count - 1].scale = 0.5f;
                    }
                }

                timer--;
                /*for (int d = 0; d < effectDusts.Count; d++)
                {
                    effectDusts[d].scale = 1;
                    /*if (timer < ringRadius / dustSpeed) //when the timer is about to run out
                    {
                        effectDusts[d].velocity = (origin - effectDusts[d].position).SafeNormalize(Vector2.UnitY) * dustSpeed; // move dust toward center
                    }
                    else
                    {
                        if ((origin - effectDusts[d].position).Length() > ringRadius)
                        {
                            float tangentDirection = (origin - effectDusts[d].position).ToRotation() + (float)Math.PI / 2 + (d % 2 == 0 ? 0 : (float)Math.PI); //the direction the dust will fly in
                            effectDusts[d].velocity = new Vector2((float)Math.Cos(tangentDirection), (float)Math.Sin(tangentDirection)) * dustSpeed; // change dust velocity
                        }
                    }
                }
                if (timer == 1)
                {
                    projectile.Kill();
                }
            }
            else
            {
                for (int d = 0; d < effectDusts.Count; d++)
                {
                    effectDusts[d].scale = .1f; // make the dust almost disapear
                }
                effectDusts.Clear(); //empty out the list*/
            }






        }

        private void EndAnimation()
        {

            if (EndParticles == false)
            {
                Vector2 origin = new Vector2(projectile.Center.X, projectile.Center.Y);


                for (int i = 0; i < dustSpawnRate2; i++)
                {
                    float rot = Main.rand.NextFloat() * (float)Math.PI * 2; //random angle

                    dustSpeed2 = Main.rand.NextFloat(3f, 10f);

                    effectDusts2.Add(Dust.NewDustPerfect(origin + (new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot)) * dustBoost2) * 20f, ModContent.DustType<NatureLeaves>(), new Vector2((float)Math.Cos(rot), (float)Math.Sin(rot)) * dustSpeed2, 0, new Color(255, 255, 255), 2.5f)); //add new dust to list
                    effectDusts2[effectDusts2.Count - 1].noGravity = true;  //modify the newly created dust
                    effectDusts2[effectDusts2.Count - 1].scale = 1f;
                }




                EndParticles = true;

            }

            if (++projectile.localAI[0] >= 2)
            {

                projectile.localAI[0] = 0;

                for (int i = scaleCache.Length - 1; i > 0; --i)
                {
                    scaleCache[i] = scaleCache[i - 1];
                }
                scaleCache[0] = projectile.scale;

            }






            projectile.scale += 0.005f;
            projectile.alpha += 204 / 60;


            if(projectile.alpha >= 255)
            {
                projectile.Kill();
            }



        }








        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            float initialOpacity = 0.8f;
            float opacityDegrade = 0.2f;
            Texture2D texture = Main.projectileTexture[projectile.type];
            Rectangle frame = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
            Vector2 origin = frame.Size() / 2;
            SpriteEffects effects = projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i ++)
            {
                float opacity = initialOpacity - opacityDegrade * i;
                spriteBatch.Draw(texture, projectile.position + projectile.Hitbox.Size() / 2 - Main.screenPosition, frame, lightColor * projectile.Opacity, projectile.rotation, origin, scaleCache[i], effects, 0f);
            }


            return this.DrawProjectileCentered(spriteBatch, lightColor * projectile.Opacity);
        }









    }
}
