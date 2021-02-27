using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using EpicBattleFantasyUltimate.HelperClasses;

namespace EpicBattleFantasyUltimate.Projectiles.LimitBreaks.MothEarth
{
    public class NatureBlast : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("NatureBlast");
            Main.projFrames[projectile.type] = 24;

        }



        Vector2 SpawnPosition;
        Vector2 CachedPosition;
        bool PositionCheck = false;

        bool collision = false;

        Vector2 origin;
        float rotation;

        float BlastVel = 5f;
        bool Veloc = false;



        public override bool CanDamage()
    => projectile.frame >= 23;


        public override void SetDefaults()
        {
            projectile.width = 0;
            projectile.height = 0;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 60 * 10;
            projectile.tileCollide = false;
            projectile.magic = true;
        }


        public override bool OnTileCollide(Vector2 oldVelocity)
        {


            if(projectile.frame >= 23)
            {


                if(projectile.tileCollide == true)
                {
                    Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);

                    if (projectile.velocity.X != oldVelocity.X)
                    {
                        projectile.velocity.X = -oldVelocity.X;
                    }

                    if (projectile.velocity.Y != oldVelocity.Y)
                    {
                        projectile.velocity.Y = -oldVelocity.Y;
                    }

                }










            }




            return false;
        }




        public override void AI()
        {





            if (++projectile.frameCounter >= 5) //reducing the frame timer
            {
                projectile.frameCounter = 0; //resetting it



                if (++projectile.frame >= 24) //Animation loop
                {
                    projectile.frame = 23;
                }
            }

            if(projectile.frame < 23) //Positioning and shooting control
            {


                Positioning();

            }
            else
            {

                if (!Collision.SolidCollision(projectile.position, projectile.width, projectile.height) && collision == false)
                {
                    projectile.tileCollide = true;

                    collision = true;
                }


                Positioning();

                Dusting();

                PlayerCollision();

                gravity();

            }

        }


        private void Positioning()
        {

            #region PosCheck

            if (PositionCheck == false)
            {

                origin = new Vector2(projectile.ai[0], projectile.ai[1]);

                rotation = Main.rand.NextFloat() * (float)Math.PI * 2; //random angle

                SpawnPosition = origin + (new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * BlastVel) * 20f;


                CachedPosition = SpawnPosition - Main.screenPosition;



                PositionCheck = true;

            }

            #endregion

            #region Positioning

            if (projectile.frame < 23)//While the projectile is forming it will stay on the screen position it spawned
            {
                projectile.position = CachedPosition + Main.screenPosition;


            }
            else
            {
                if(Veloc == false)//Making sure that this won't run more than once
                {
                    projectile.velocity =new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * BlastVel;

                    Veloc = true;
                }
            }
            #endregion


        }

        private void Dusting()
        {
            if(Main.rand.NextFloat(2f) < 1f)
            {
                Dust.NewDustDirect(projectile.Center, projectile.width, projectile.height, 61, 0, 0, 0, default, 1);
            }
        }

        private void PlayerCollision()
        {
            for (int i = 0; i < 255; ++i) //looping through the players
            {
                if (Main.player[i].active && !Main.player[i].dead && projectile.Hitbox.Intersects(Main.player[i].Hitbox)) //checking if the player is alive and if the hitbox touches the player's
                {
                    Player player = Main.player[i];

                    var epicPlayer = EpicPlayer.ModPlayer(player);

                    if (player.statLife < player.statLifeMax2)
                    {
                        player.statLife += 10;
                        player.HealEffect(10);
                    }
                    if(player.statMana < player.statManaMax2)
                    {
                        player.statMana += 20;
                        player.ManaEffect(20);
                    }
                    if(epicPlayer.LimitCurrent < epicPlayer.MaxLimit2 && Main.rand.NextFloat(2f) > 1f)
                    {
                        epicPlayer.LimitCurrent += 3;

                        MathHelper.Clamp(epicPlayer.LimitCurrent, 0, epicPlayer.MaxLimit2);
                    }


                    // Heal the player.
                    projectile.Kill();
                    break;
                }
            }
        }



        private void gravity()
        {
            projectile.velocity.Y += 0.3f; //gravity

            projectile.velocity.Y = MathHelper.Clamp(projectile.velocity.Y, -16, 16);

        }









        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            return this.DrawProjectileCentered(spriteBatch, lightColor);

        }











    }
}
