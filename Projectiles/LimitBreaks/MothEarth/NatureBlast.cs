using EpicBattleFantasyUltimate.HelperClasses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace EpicBattleFantasyUltimate.Projectiles.LimitBreaks.MothEarth
{
    public class NatureBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("NatureBlast");
            Main.projFrames[Projectile.type] = 24;
        }

        private Vector2 SpawnPosition;
        private Vector2 CachedPosition;
        private bool PositionCheck = false;

        private bool collision = false;

        private Vector2 origin;
        private float rotation;

        private float BlastVel = 5f;
        private bool Veloc = false;

        public override bool? CanDamage()
    => Projectile.frame >= 23;

        public override void SetDefaults()
        {
            Projectile.width = 0;
            Projectile.height = 0;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 60 * 10;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.frame >= 23)
            {
                if (Projectile.tileCollide)
                {
                    Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);

                    if (Projectile.velocity.X != oldVelocity.X)
                    {
                        Projectile.velocity.X = -oldVelocity.X;
                    }

                    if (Projectile.velocity.Y != oldVelocity.Y)
                    {
                        Projectile.velocity.Y = -oldVelocity.Y;
                    }
                }
            }
            return false;
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 5) //reducing the frame timer
            {
                Projectile.frameCounter = 0; //resetting it

                if (++Projectile.frame >= 24) //Animation loop
                {
                    Projectile.frame = 23;
                }
            }

            if (Projectile.frame < 23) //Positioning and shooting control
            {
                Positioning();
            }
            else
            {
                if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height) && collision == false)
                {
                    Projectile.tileCollide = true;

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
                origin = new Vector2(Projectile.ai[0], Projectile.ai[1]);

                rotation = Main.rand.NextFloat() * (float)Math.PI * 2; //random angle

                SpawnPosition = origin + (new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * BlastVel) * 20f;

                CachedPosition = SpawnPosition - Main.screenPosition;

                PositionCheck = true;
            }

            #endregion PosCheck

            #region Positioning

            if (Projectile.frame < 23)//While the Projectile is forming it will stay on the screen position it spawned
            {
                Projectile.position = CachedPosition + Main.screenPosition;
            }
            else
            {
                if (Veloc == false)//Making sure that this won't run more than once
                {
                    Projectile.velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * BlastVel;

                    Veloc = true;
                }
            }

            #endregion Positioning
        }

        private void Dusting()
        {
            if (Main.rand.NextFloat(2f) < 1f)
            {
                Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.GreenTorch, 0, 0, 0, default, 1);
            }
        }

        private void PlayerCollision()
        {
            for (int i = 0; i < 255; ++i) //looping through the players
            {
                if (Main.player[i].active && !Main.player[i].dead && Projectile.Hitbox.Intersects(Main.player[i].Hitbox)) //checking if the player is alive and if the hitbox touches the player's
                {
                    Player player = Main.player[i];

                    var epicPlayer = EpicPlayer.ModPlayer(player);

                    if (player.statLife < player.statLifeMax2)
                    {
                        player.statLife += 10;
                        player.HealEffect(10);
                    }
                    if (player.statMana < player.statManaMax2)
                    {
                        player.statMana += 20;
                        player.ManaEffect(20);
                    }
                    if (epicPlayer.LimitCurrent < epicPlayer.MaxLimit2 && Main.rand.NextFloat(2f) > 1f)
                    {
                        epicPlayer.LimitCurrent += 3;

                        MathHelper.Clamp(epicPlayer.LimitCurrent, 0, epicPlayer.MaxLimit2);
                    }

                    // Heal the player.
                    Projectile.Kill();
                    break;
                }
            }
        }

        private void gravity()
        {
            Projectile.velocity.Y += 0.3f; //gravity

            Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.Y, -16, 16);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return this.DrawProjectileCentered(Main.spriteBatch, lightColor);
        }
        
    }
}