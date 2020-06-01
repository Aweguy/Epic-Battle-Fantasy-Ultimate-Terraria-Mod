using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using System;
using EpicBattleFantasyUltimate.Items.Materials;


namespace EpicBattleFantasyUltimate.NPCs.Wraiths
{
    public class LeafWraith : ModNPC
    {

        #region variables

        int timer = 10;   //The timer that makes the first projectile be shot.
        int timer2 = 12;  //The timer that makes the second projectile be shot. The two frames difference is there on purpose.
        int shootTimer = 60; //The timer that sets the shoot bool to false again.
        int specialStacks = 0;//The stacks that will define when the wraith will use its special attack
        int specialTimer = 10;//The interval between Special shots.
        int specialEndStacks = 0;//The stacks that will define when the Wraith will stop the special attack
        bool shoot = false; //Definition of the bool that makes the npc to move slower when it's ready to shoot
        bool special = false;//Definition of the bool that will make the wraith use its special attack
        bool attack = true;//Definition of the bool that will make the wraith use its normal attack.

        public Vector2 velocity;

        #endregion




        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.Wraith);
            npc.width = (int)(53 * 0.8f);
            npc.height = (int)(78 * 0.8f);

            npc.lifeMax = 150;
            npc.damage = 25;
            npc.defense = 15;

            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath52;

            npc.aiStyle = 22;
            aiType = NPCID.Wraith;
            npc.noTileCollide = true;




        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Poisoned, 60 * 2);
        }





        

        #region AI

        public override void AI()
        {

            Player player = Main.player[npc.target]; //Target
            int proj;
            int proj2;





            #region Movement Direction

            if (npc.velocity.X > 0f) // This is the code that makes the sprite turn. Based on the vanilla one.
            {
                npc.direction = 1;
            }
            else if (npc.velocity.X < 0f)
            {
                npc.direction = -1;
            }
            else if (npc.velocity.X == 0)
            {
                npc.direction = npc.oldDirection;
            }


            if (npc.direction == 1)
            {
                npc.spriteDirection = 1;
            }
            else if (npc.direction == -1)
            {
                npc.spriteDirection = -1;
            }

            #endregion


            #region Shooting
            if(attack == true)
            {
                timer--;
            }


            if (timer == 60) //Here the shoot bool becomes true, 60 ticks before it shoots
            {
                shoot = true;
            }




            if (timer <= 0) //If timer is 0 or less it shoots.
            {

                if (player.statLife > 0)
                {



                    if (npc.direction == 1)  //I did not find a better way to do this. This defines the positions the projectile based on its direction.
                    {
                        proj = Projectile.NewProjectile(new Vector2(npc.Center.X + 20f, npc.Center.Y), npc.DirectionTo(Main.player[npc.target].Center) * 10f, mod.ProjectileType("ThornSpike"), 20, 2, Main.myPlayer, 0, 1);
                    }
                    else if (npc.direction == -1)
                    {
                        proj = Projectile.NewProjectile(new Vector2(npc.Center.X - 28f, npc.Center.Y), npc.DirectionTo(Main.player[npc.target].Center) * 10f, mod.ProjectileType("ThornSpike"), 20, 2, Main.myPlayer, 0, 1);
                    }



                }

                timer = 120; //Resetting the timer to 120 ticks (2 seconds).
            }


            if (attack == true)
            {
                timer2--; // Same logic as the first timer.
            }


            if (timer2 <= 0)
            {

                if (player.statLife > 0)
                {


                    if (npc.direction == 1)
                    {
                        proj2 = Projectile.NewProjectile(new Vector2(npc.Center.X + 11f, npc.Center.Y + 9f), npc.DirectionTo(Main.player[npc.target].Center) * 10f, mod.ProjectileType("ThornSpike"), 20, 2, Main.myPlayer, 0, 1);
                    }
                    else if (npc.direction == -1)
                    {
                        proj2 = Projectile.NewProjectile(new Vector2(npc.Center.X - 21f, npc.Center.Y + 9f), npc.DirectionTo(Main.player[npc.target].Center) * 10f, mod.ProjectileType("ThornSpike"), 20, 2, Main.myPlayer, 0, 1);
                    }
                }


                timer2 = 120;
                specialStacks++;
            }
            #endregion

            #region Special Attack

            if (special == true)
            {
                specialTimer--;

                if(specialTimer <=0)
                {
                    float mult = Main.rand.NextFloat(5f, 10f); //velocity randomization



                    velocity = npc.DirectionTo(new Vector2(Main.player[npc.target].Center.X, Main.player[npc.target].Center.Y + 18)) * mult; //Leaf velocity

                    Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 18f), velocity, mod.ProjectileType("LeafShot"), 20, 2, Main.myPlayer, 0, 1); //Leaf spawning/

                    

                    specialTimer = 40;
                    specialEndStacks++;

                }


            }

            #endregion





            #region Logic Control



            #region Attack Logic

            if (shoot == true) //If the shoot bool is true, then redcue the shoot timer otherwise do nothing.
            {
                shootTimer--;
            }





            if (shootTimer <= 0) //If it becomes 0 or less, reset the shoot bool to false.
            {
                shoot = false;

                shootTimer = 60; //Resets the timer to 60 ticks (1 second)
            }

            if (shoot == true) //If the shoot bool is true, its X speed is reduced by 75% of its initial. That is to generate the effects of it stopping a little before shooting.
            {
                npc.velocity.X *= 0.9f;
            }

            #endregion

            #region Special Logic

            if (specialStacks == 5)
            {
                special = true;
                attack = false;
            }

            if (specialEndStacks == 2)
            {
                special = false;
                attack = true;
                specialStacks = 0;
                specialEndStacks = 0;

            }

            #endregion

            #endregion


        }
        #endregion



        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode == true && spawnInfo.player.ZoneJungle)
            {
                return 0.03f;
            }


            return 0f;
        }

        #region NPCLoot

        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), mod.ItemType("Wool"), 1);

            if (Main.rand.NextFloat() < .10f)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("SilkScrap"), Main.rand.Next(2) + 1);
            }

            if (Main.rand.NextFloat() < .20f)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<GrapeLeaf>(), Main.rand.Next(3) + 1);
            }



        }

        #endregion






    }
}
