using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;
using EpicBattleFantasyUltimate.Buffs.Debuffs;

namespace EpicBattleFantasyUltimate.NPCs.Wraiths
{
    public class MasterWraith : ModNPC
    {

        #region Variables

        bool Enraged = false;//Determines whether the Wraith is enraged or not.

        int Spiketimer = 60;   //The timer that makes the first projectile be shot.
        int Spiketimer2 = 60;  //The timer that makes the second projectile be shot.

        int Firetimer = 60 * 5;//Defines when the fireballs will start spawning
        int spintimer = 8; //A timer that sets wthe interval between the orbiting fireballs.
        int fireballs = 11;//The number of the fireballs.
        float FireVel = 3f;//The velocity of the fireballs when launched.



        int LeafStartTimer = 240;//The timer that determines when the Leaves will be shot
        int LeafTimer = 10;//The interval between Special shots.
        int LeafEndStacks = 0;//The stacks that will define when the Wraith will stop the special attack



        int SparkTimer = 120;// The timer that determines when the Spark will be shot



        int icetimer = 60 * 4; //The timer that makes the icicles spawn. (4.5 seconds)
        int icetimer2 = 4; //The timer that defines the interval between each icicle.
        int fmis = 20; //Defines how many icicles will drop. (Falling Magical Ice Spikes)
        float IceOffsetx; //Definition of the number that will define where the icicle will spawn.

        int BlinkTimer = 60 * 20;//Determines when the wraith will blink
        bool Blinking = false;//Determines when the Wraith will not attack and blink in a random location around the player
        bool Blinked = false;//Determines if the Wraith has blinked
        int WraithToBlinkSpawn;//Determines what NPC will spawn when the Master Wraith blinks

        int ChoiceTimer;//The interval between attack decision
        int Choice = 0;//The current attack choice
        int PrevChoice = 0;//The previous attack choice

        int SpecChoiceTimer;//The interval between the special attack decision
        int SpecChoice = 0;//The current special attack choice
        int PrevSpecChoice = 0;//The previous special attack choice

        int CursingTimer = 60 * 5;//The timer that makes the Wraith Curse you.


        Vector2 velocity;

        #endregion


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Master Wraith");
        }


        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.Wraith);
            npc.width = (int)(72 * 0.8);
            npc.height = (int)(96 * 0.8);

            npc.lifeMax = 2500;
            npc.damage = 100;
            npc.defense = 50;
            npc.lifeRegen = 4;
            npc.alpha = 100;


            npc.aiStyle = 22;
            aiType = NPCID.Wraith;
            npc.noTileCollide = true;




        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(Main.rand.Next(EpicBattleFantasyUltimate.MasterWraithTouchDebuffs.ToArray()), 60 * 20);

        }



        #region AI

        public override void AI()
        {
            Player player = Main.player[npc.target]; //Target

            if(npc.life <= (int)(npc.lifeMax * 0.25f))
            {
                Enraged = true;
            }

            Blink(npc, player);

            MovementDirection(npc);


            if (!Enraged)
            {
                if (Choice == 0)
                {
                    ChoiceTimer--;

                }

                if (SpecChoice == 0)
                {
                    SpecChoiceTimer--;
                }



                if (ChoiceTimer <= 0 && Choice == 0)
                {
                    Choice = Choosing(npc);
                }

                if (SpecChoiceTimer <= 0 && SpecChoice == 0)
                {
                    SpecChoice = SpecChoosing(npc);
                }

                if (!Blinking && player.statLife > 0)
                {
                    Shooting(npc);


                    if (SpecChoice == 1)
                    {
                        Fireballs(npc);
                    }
                    else if (SpecChoice == 2)
                    {
                        Icicles(npc);
                    }
                    else if (SpecChoice == 3)
                    {
                        Curse(npc);
                    }


                    if (Choice == 1)
                    {
                        SparkBall(npc);
                    }
                    else if (Choice == 2)
                    {
                        Leaves(npc);
                    }

                }

            }
            else if (Enraged)
            {
                Shooting(npc);
                Fireballs(npc);
                Icicles(npc);
                Leaves(npc);
                Curse(npc);
                SparkBall(npc);
            }




        }
        #endregion

        #region Blink

        private void Blink(NPC npc, Player player)
        {
            if (Enraged)
            {
                BlinkTimer -= 2;

            }
            else
            {
                BlinkTimer--;
            }

            if (BlinkTimer <= 0)
            {
                Blinking = true;

                if (!Blinked)
                {
                    npc.alpha += 2;
                }

                if (npc.alpha >= 255)
                {
                    if (!Blinked)
                    {
                        int SpawnChoice = Main.rand.Next(1, 7);

                        if(SpawnChoice == 1)
                        {
                            WraithToBlinkSpawn = ModContent.NPCType<FlameWraith>();
                        }
                        if (SpawnChoice == 2)
                        {
                            WraithToBlinkSpawn = ModContent.NPCType<FlameWraith2>();
                        }
                        if (SpawnChoice == 3)
                        {
                            WraithToBlinkSpawn = ModContent.NPCType<FrostWraith>();
                        }
                        if (SpawnChoice == 4)
                        {
                            WraithToBlinkSpawn = ModContent.NPCType<FrostWraith2>();
                        }
                        if (SpawnChoice == 5)
                        {
                            WraithToBlinkSpawn = ModContent.NPCType<LeafWraith>();
                        }
                        if (SpawnChoice == 6)
                        {
                            WraithToBlinkSpawn = ModContent.NPCType<SteelWraith>();
                        }
                        if (SpawnChoice == 7)
                        {
                            WraithToBlinkSpawn = ModContent.NPCType<SparkWraith>();
                        }





                        int npcIndex = NPC.NewNPC((int)(npc.Center.X), (int)(npc.Center.Y), WraithToBlinkSpawn, 0, 0f, 0f, 0f, 0f, 255);

                        npc.position = new Vector2(player.Center.X + Main.rand.Next(-1000, 1000) * player.direction, player.Center.Y - Main.rand.Next(100, 300));


                    }

                    Blinked = true;
                }

                if(Blinked && npc.alpha > 100)
                {
                    npc.alpha -= 2;

                    if(npc.alpha <= 100)
                    {
                        Blinked = false;
                        Blinking = false;
                        BlinkTimer = 60 * 20;
                    }
                }


            }
        }

        #endregion




        #region MovementDirection

        private void MovementDirection(NPC npc)
        {


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


        }

        #endregion

        #region Shooting

        private void Shooting(NPC npc)
        {

            Player player = Main.player[npc.target]; //Target

            Spiketimer--;
            Spiketimer2--;

            if(Spiketimer <= 60)
            {
                npc.velocity.X *= 0.9f;
            }

            if(Spiketimer <= 0)
            {
                int Shot = Main.rand.Next(EpicBattleFantasyUltimate.MasterWraithBasic.ToArray());

                if (npc.direction == 1)  //I did not find a better way to do this. This defines the positions the projectile based on its direction.
                {
                    int proj = Projectile.NewProjectile(new Vector2(npc.Center.X + 20f, npc.Center.Y), npc.DirectionTo(Main.player[npc.target].Center) * 10f, Shot, 30, 2, Main.myPlayer, 0, 1);
                }
                else if (npc.direction == -1)
                {
                    int proj = Projectile.NewProjectile(new Vector2(npc.Center.X - 28f, npc.Center.Y), npc.DirectionTo(Main.player[npc.target].Center) * 10f, Shot, 30, 2, Main.myPlayer, 0, 1);
                }

                Spiketimer = 120;

            }


            if(Spiketimer2 <= 0)
            {

                int Shot = Main.rand.Next(EpicBattleFantasyUltimate.MasterWraithBasic.ToArray());

                if (npc.direction == 1)
                {
                   int proj2 = Projectile.NewProjectile(new Vector2(npc.Center.X + 11f, npc.Center.Y + 12f), npc.DirectionTo(Main.player[npc.target].Center) * 10f, Shot, 30, 2, Main.myPlayer, 0, 1);
                }
                else if (npc.direction == -1)
                {
                   int proj2 = Projectile.NewProjectile(new Vector2(npc.Center.X - 21f, npc.Center.Y + 12f), npc.DirectionTo(Main.player[npc.target].Center) * 10f, Shot, 30, 2, Main.myPlayer, 0, 1);
                }

                Spiketimer2 = 120;
            }





        }

        #endregion


        #region Fireballs

        private void Fireballs(NPC npc)
        {

            Player player = Main.player[npc.target]; //Target

            if (Enraged)
            {
                Firetimer--;
            }

            if (!Enraged)
            {
                spintimer--;

                if (spintimer <= 0)
                {




                    int proj2 = Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<SpinFireball>(), 20, 2, Main.myPlayer, npc.whoAmI, FireVel);

                    spintimer = 8;//resets the interval
                    fireballs--;
                }



                else if (fireballs <= 0)
                {
                    fireballs = 11;
                    SpecChoice = 0;
                    SpecChoiceTimer = 60 * 25;
                }
            }
            else if (Enraged && Firetimer <= 0)
            {
                spintimer--;

                if (spintimer <= 0)
                {




                    int proj2 = Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<SpinFireball>(), 20, 2, Main.myPlayer, npc.whoAmI, FireVel);

                    spintimer = 8;//resets the interval
                    fireballs--;
                }



                else if (fireballs <= 0)
                {
                    fireballs = 11;

                    Firetimer = 60 * 25;
                }
            }
            



        }

        #endregion

        #region Leaves

        private void Leaves(NPC npc)
        {

            if (Enraged)
            {
                LeafStartTimer--;
            }

            if (!Enraged)
            {
                if (LeafEndStacks <= 3)
                {
                    LeafTimer--;

                    if (LeafTimer <= 0)
                    {
                        float mult = Main.rand.NextFloat(5f, 10f); //velocity randomization



                        velocity = npc.DirectionTo(new Vector2(Main.player[npc.target].Center.X, Main.player[npc.target].Center.Y + 18)) * mult; //Leaf velocity

                        Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 18f), velocity, mod.ProjectileType("LeafShot"), 20, 2, Main.myPlayer, 0, 1); //Leaf spawning/



                        LeafTimer = 40;
                        LeafEndStacks++;

                    }

                    if (LeafEndStacks == 3)
                    {
                        LeafEndStacks = 0;
                        Choice = 0;
                        ChoiceTimer = 150;
                    }


                }

            }
            else if (Enraged && LeafStartTimer <= 0)
            {
                if (LeafEndStacks <= 3)
                {
                    LeafTimer--;

                    if (LeafTimer <= 0)
                    {
                        float mult = Main.rand.NextFloat(5f, 10f); //velocity randomization



                        velocity = npc.DirectionTo(new Vector2(Main.player[npc.target].Center.X, Main.player[npc.target].Center.Y + 18)) * mult; //Leaf velocity

                        Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 18f), velocity, mod.ProjectileType("LeafShot"), 20, 2, Main.myPlayer, 0, 1); //Leaf spawning/



                        LeafTimer = 40;
                        LeafEndStacks++;

                    }

                    if (LeafEndStacks == 3)
                    {
                        LeafEndStacks = 0;
                        LeafStartTimer = 60 * 5;
                    }


                }

            }


        }

        #endregion

        #region SparkBall

        private void SparkBall(NPC npc)
        {
            Player player = Main.player[npc.target]; //Target

            if (Enraged)
            {
                SparkTimer--;
            }

            if (!Enraged)
            {
                int proj4 = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 11), npc.DirectionTo(Main.player[npc.target].Center) * 10f, ModContent.ProjectileType<Sparkle>(), 18, 2, Main.myPlayer, 0, 1);

                Choice = 0;
                ChoiceTimer = 100;

            }
            else if (Enraged && SparkTimer <= 0)
            {
                int proj4 = Projectile.NewProjectile(new Vector2(npc.Center.X, npc.Center.Y - 11), npc.DirectionTo(Main.player[npc.target].Center) * 10f, ModContent.ProjectileType<Sparkle>(), 18, 2, Main.myPlayer, 0, 1);

                SparkTimer = 60 * 2;
            }




        }


        #endregion

        #region Icicles

        private void Icicles (NPC npc)
        {

            if (Enraged)
            {
                icetimer--;
            }

            if (!Enraged)
            {
                icetimer2--;

                if (icetimer2 <= 0)
                {



                    IceOffsetx = Main.rand.NextFloat(-100, 100);

                    Vector2 spawnPosition = new Vector2(Main.player[npc.target].Center.X - IceOffsetx, Main.player[npc.target].Center.Y - 500);
                    Vector2 speed = new Vector2(0, 10);

                    int proj5 = Projectile.NewProjectile(spawnPosition, speed, ModContent.ProjectileType<SpinIcicle>(), 30, 2, Main.myPlayer, npc.whoAmI, 1);

                    icetimer2 = 4;
                    fmis--;
                }



                else if (fmis <= 0)
                {
                    fmis = 20;
                    SpecChoice = 0;
                    SpecChoiceTimer = 60 * 25;
                }

            }
            else if (Enraged && icetimer <= 0)
            {
                icetimer2--;

                if (icetimer2 <= 0)
                {



                    IceOffsetx = Main.rand.NextFloat(-100, 100);

                    Vector2 spawnPosition = new Vector2(Main.player[npc.target].Center.X - IceOffsetx, Main.player[npc.target].Center.Y - 500);
                    Vector2 speed = new Vector2(0, 10);

                    int proj5 = Projectile.NewProjectile(spawnPosition, speed, ModContent.ProjectileType<SpinIcicle>(), 30, 2, Main.myPlayer, npc.whoAmI, 1);

                    icetimer2 = 4;
                    fmis--;
                }



                else if (fmis <= 0)
                {
                    fmis = 20;
                    icetimer = 60 * 25;
                }

            }



        }







        #endregion

        #region Curse

        private void Curse(NPC npc)
        {

            Player player = Main.player[npc.target]; //Target

            if (Enraged)
            {
                CursingTimer--;
            }



            if (!Enraged)
            {
                Projectile.NewProjectile(new Vector2(player.Center.X, player.Center.Y), Vector2.Zero, ModContent.ProjectileType<CursingRune>(), 1, 2, Main.myPlayer, 0, 1);

                SpecChoice = 0;
                SpecChoiceTimer = 60 * 10;
            }
            else if (Enraged && CursingTimer <= 0)
            {
                Projectile.NewProjectile(new Vector2(player.Center.X, player.Center.Y), Vector2.Zero, ModContent.ProjectileType<CursingRune>(), 1, 2, Main.myPlayer, 0, 1);

                CursingTimer = 60 * 5;
            }

        }

        #endregion

        private int Choosing(NPC npc)
        {

            Choice = Main.rand.Next(1, 3);

            if (Choice == PrevChoice)
            {
                if (Choice == 1)
                {
                    Choice += 1;
                }
                else if (Choice == 2)
                {
                    Choice -= 1;
                }
            }

            PrevChoice = Choice;



            return Choice;
        }



        private int SpecChoosing(NPC npc)
        {

            SpecChoice = Main.rand.Next(1, 4);

            if (SpecChoice == PrevSpecChoice)
            {
                if(SpecChoice == 1)
                {
                    SpecChoice += 1;
                }
                else if (SpecChoice == 2)
                {
                    SpecChoice = (Main.rand.NextFloat() > .5f) ? SpecChoice + 1 : SpecChoice - 1;
                }
                else if(SpecChoice == 3)
                {
                    SpecChoice -= 1;
                }
            }

            PrevSpecChoice = SpecChoice;

            return SpecChoice;
        }

        






        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {

            if (Blinking)
            {
                return false;
            }
            else
            {
                return null;
            }

        }






    }
}
