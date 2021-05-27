using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Wraiths
{
    public class FrostWraith : ModNPC
    {
        private int timer = 10;   //The timer that makes the first projectile be shot.
        private int timer2 = 12;  //The timer that makes the second projectile be shot. The two frames difference is there on purpose.
        private int icetimer = (60 * 4) + 30; //The timer that makes the icicles spawn. (4.5 seconds)
        private int icetimer2 = 10; //The timer that defines the interval between each icicle.
        private int shootTimer = 60; //The timer that sets the shoot bool to false again.
        private bool shoot = false; //Definition of the bool that makes the npc to move slower when it's ready to shoot.
        private int currentIcicles = 0; //How many icicles are currently alive
        private readonly int maxIcicles = 5; //The maximum amount of icicles that will be spawned

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Wraith");
        }

        public override void SetDefaults()
        {
            npc.CloneDefaults(NPCID.Wraith);
            npc.width = (int)(53 * 0.8f);
            npc.height = (int)(78 * 0.8f);

            npc.lifeMax = 185;
            npc.damage = 25;
            npc.defense = 40;
            npc.lifeRegen = 4;

            npc.aiStyle = 22;
            aiType = NPCID.Wraith;
            npc.noTileCollide = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 60 * 2);
        }

        #region AI

        public override void AI()
        {
            Player player = Main.player[npc.target]; //Target
            int proj;
            int proj2;

            Dust.NewDustDirect(npc.position, npc.width, npc.height, 185, 0f, 0f, 0, new Color(0, 255, 142), 0.4605263f);

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

            icetimer--;

            if (icetimer <= 0)
            {
                Icicles(npc);
            }

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
                        proj = Projectile.NewProjectile(new Vector2(npc.Center.X + 20f, npc.Center.Y), npc.DirectionTo(Main.player[npc.target].Center) * 10f, mod.ProjectileType("FrostBoneShot"), 20, 2, Main.myPlayer, 0, 1);
                    }
                    else if (npc.direction == -1)
                    {
                        proj = Projectile.NewProjectile(new Vector2(npc.Center.X - 28f, npc.Center.Y), npc.DirectionTo(Main.player[npc.target].Center) * 10f, mod.ProjectileType("FrostBoneShot"), 20, 2, Main.myPlayer, 0, 1);
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
                        proj2 = Projectile.NewProjectile(new Vector2(npc.Center.X + 11f, npc.Center.Y + 9f), npc.DirectionTo(Main.player[npc.target].Center) * 10f, mod.ProjectileType("FrostBoneShot"), 20, 2, Main.myPlayer, 0, 1);
                    }
                    else if (npc.direction == -1)
                    {
                        proj2 = Projectile.NewProjectile(new Vector2(npc.Center.X - 21f, npc.Center.Y + 9f), npc.DirectionTo(Main.player[npc.target].Center) * 10f, mod.ProjectileType("FrostBoneShot"), 20, 2, Main.myPlayer, 0, 1);
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

        #endregion AI

        private void Icicles(NPC npc)
        {
            float fullRotationInFrames = 240;

            if (++icetimer2 >= fullRotationInFrames / maxIcicles)
            {
                // Do not attempt to spawn the projectile on clients. Only in singleplayer and server instances.
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<SpinIcicle>(), 20, 2, Main.myPlayer, npc.whoAmI);
                }

                icetimer2 = 0;
                currentIcicles++;
            }

            if (currentIcicles >= maxIcicles)
            {
                currentIcicles = 0;

                icetimer = 60 * 25; //Higher than the base value for balance purposes
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode == true && spawnInfo.player.ZoneSnow)
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
            if (Main.rand.NextFloat() < .30f)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("SolidWater"), Main.rand.Next(2) + 1);
            }
        }

        #endregion NPCLoot
    }
}