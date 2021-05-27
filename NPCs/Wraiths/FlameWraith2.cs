using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Wraiths
{
    public class FlameWraith2 : ModNPC
    {
        private int timer = 10;   //The timer that makes the first projectile be shot.
        private int timer2 = 12;  //The timer that makes the second projectile be shot. The two frames difference is there on purpose.
        private int spectimer = 60 * 5;//Defines when the fireballs will start spawning
        private int spintimer; //A timer that sets wthe interval between the orbiting fireballs.
        private int fireballs = 11;//The number of the fireballs.
        private int shootTimer = 60; //The timer that sets the shoot bool to false again.
        private float FireVel = 3f;//The velocity of the fireballs when launched.
        private bool shoot = false; //Definition of the bool that makes the npc to move slower when it's ready to shoot
        private int currentFireballs = 0;
        private readonly int maxFireballs = 11;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sacred Flame Wraith");
        }

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.Wraith);
            npc.width = (int)(53 * 0.8f);
            npc.height = (int)(78 * 0.8f);

            npc.lifeMax = 260;
            npc.damage = 50;
            npc.defense = 45;
            npc.lifeRegen = 4;

            npc.aiStyle = 22;
            aiType = NPCID.Wraith;
            npc.noTileCollide = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 60 * 10);
        }

        #region AI

        public override void AI()
        {
            Player player = Main.player[npc.target]; //Target
            int proj;
            int proj2;

            Dust.NewDustDirect(npc.position, npc.width, npc.height, 6, 0.2631578f, -2.368421f, 0, Color.Orange, 0.6f);

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

            #endregion Movement Direction

            #region Shooting

            #region Fireballs

            spectimer--;

            if (spectimer <= 0)
            {
                Fireballs(npc);
            }

            spectimer--;
            if (spectimer <= 0)
            {
                // Eldrazi: I've done some explicit variable statements, so you know what each of these variables is supposed to do.
                // You could shrink this code down, but I'd only do that if you're comfortable with understanding it.

                float fullRotationInFrames = 240;

                if (++spintimer >= fullRotationInFrames / maxFireballs)
                {
                    // Do not attempt to spawn the projectile on clients. Only in singleplayer and server instances.
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int npcIndex = NPC.NewNPC((int)(npc.Center.X), (int)(npc.Center.Y), ModContent.NPCType<OrbitingFireball>(), 0, npc.whoAmI, 0f, 0f, 0f, 255);//Spawnign the Wraith
                    }

                    spintimer = 0;
                    currentFireballs++;
                }

                if (currentFireballs >= maxFireballs)
                {
                    currentFireballs = 0;
                    spectimer = 60 * 25; //Higher than the base value for balance purposes
                }
            }

            #endregion Fireballs

            timer--;

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
                        proj = Projectile.NewProjectile(new Vector2(npc.Center.X + 22f, npc.Center.Y), npc.DirectionTo(Main.player[npc.target].Center) * 10f, mod.ProjectileType("BoneShot"), 40, 2, Main.myPlayer, 0, 1);
                    }
                    else if (npc.direction == -1)
                    {
                        proj = Projectile.NewProjectile(new Vector2(npc.Center.X - 30f, npc.Center.Y), npc.DirectionTo(Main.player[npc.target].Center) * 10f, mod.ProjectileType("BoneShot"), 40, 2, Main.myPlayer, 0, 1);
                    }
                }

                timer = 120; //Resetting the timer to 120 ticks (2 seconds).
            }

            timer2--; // Same logic as the first timer.

            if (timer2 <= 0)
            {
                if (player.statLife > 0)
                {
                    if (npc.direction == 1)
                    {
                        proj2 = Projectile.NewProjectile(new Vector2(npc.Center.X + 11f, npc.Center.Y + 9f), npc.DirectionTo(Main.player[npc.target].Center) * 10f, mod.ProjectileType("BoneShot"), 40, 2, Main.myPlayer, 0, 1);
                    }
                    else if (npc.direction == -1)
                    {
                        proj2 = Projectile.NewProjectile(new Vector2(npc.Center.X - 21f, npc.Center.Y + 9f), npc.DirectionTo(Main.player[npc.target].Center) * 10f, mod.ProjectileType("BoneShot"), 40, 2, Main.myPlayer, 0, 1);
                    }
                }

                timer2 = 120;
            }

            #endregion Shooting

            #region Logic Control

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

            #endregion Logic Control
        }

        private void Fireballs(NPC npc)
        {
            Player player = Main.player[npc.target]; //Target

            // Eldrazi: I've done some explicit variable statements, so you know what each of these variables is supposed to do.
            // You could shrink this code down, but I'd only do that if you're comfortable with understanding it.

            float fullRotationInFrames = 240;

            if (++spintimer >= fullRotationInFrames / maxFireballs)
            {
                // Do not attempt to spawn the projectile on clients. Only in singleplayer and server instances.
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<SpinFireball>(), 20, 2, Main.myPlayer, npc.whoAmI);
                }

                spintimer = 0;
                currentFireballs++;
            }

            if (currentFireballs >= maxFireballs)
            {
                currentFireballs = 0;
                spectimer = 60 * 25; //Higher than the base value for balance purposes
            }
        }

        #endregion AI

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode == true && spawnInfo.player.ZoneUnderworldHeight && NPC.downedPlantBoss)
            {
                return 0.02f;
            }

            return 0f;
        }

        #region NPCLoot

        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), mod.ItemType("Wool"), 2);

            if (Main.rand.NextFloat() < .20f)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("SilkScrap"), Main.rand.Next(2) + 2);
            }
        }

        #endregion NPCLoot
    }
}