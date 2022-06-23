using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.Wraith;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
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
        private bool shoot = false; //Definition of the bool that makes the NPC to move slower when it's ready to shoot.
        private int currentIcicles = 0; //How many icicles are currently alive
        private readonly int maxIcicles = 5; //The maximum amount of icicles that will be spawned

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Wraith");
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Wraith);
            NPC.width = (int)(53 * 0.8f);
            NPC.height = (int)(78 * 0.8f);

            NPC.lifeMax = 185;
            NPC.damage = 25;
            NPC.defense = 40;
            NPC.lifeRegen = 4;

            NPC.aiStyle = 22;
            AIType = NPCID.Wraith;
            NPC.noTileCollide = true;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Lesser ghosts that prowl frigid caves. Its eyes stare so deeply, it has no need for lamps.")
            });
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 60 * 2);
        }

        #region AI

        public override void AI()
        {
            Player player = Main.player[NPC.target]; //Target
            Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.FrostHydra, 0f, 0f, 0, new Color(0, 255, 142), 0.4605263f);

            #region Movement Direction

            if (NPC.velocity.X > 0f) // This is the code that makes the sprite turn. Based on the vanilla one.
            {
                NPC.direction = 1;
            }
            else if (NPC.velocity.X < 0f)
            {
                NPC.direction = -1;
            }
            else if (NPC.velocity.X == 0)
            {
                NPC.direction = NPC.oldDirection;
            }

            if (NPC.direction == 1)
            {
                NPC.spriteDirection = 1;
            }
            else if (NPC.direction == -1)
            {
                NPC.spriteDirection = -1;
            }

            #endregion Movement Direction

            #region Shooting

            Shooting(player);
            icetimer--;

            if (icetimer <= 0)
            {
                Icicles(NPC);
            }


            #endregion Shooting

        }

        #endregion AI

        private void Shooting(Player player)
        {
            

            if (--timer == 60) //Here the shoot bool becomes true, 60 ticks before it shoots
            {
                shoot = true;
            }

            if (player.statLife > 0)
            {
                if (timer <= 0) //If timer is 0 or less it shoots.
                {

                    if (NPC.direction == 1)  //I did not find a better way to do this. This defines the positions the projectile based on its direction.
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(),new Vector2(NPC.Center.X + 20f, NPC.Center.Y), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<FrostBoneShot>(), 30, 2, Main.myPlayer, 0, 1);
                    }
                    else if (NPC.direction == -1)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(),new Vector2(NPC.Center.X - 28f, NPC.Center.Y), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<FrostBoneShot>(), 30, 2, Main.myPlayer, 0, 1);
                    }

                    timer = 120; //Resetting the timer to 120 ticks (2 seconds).
                }

                if (--timer2 <= 0)
                {

                    if (NPC.direction == 1)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(),new Vector2(NPC.Center.X + 11f, NPC.Center.Y + 9f), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<FrostBoneShot>(), 30, 2, Main.myPlayer, 0, 1);
                    }
                    else if (NPC.direction == -1)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromAI(),new Vector2(NPC.Center.X - 21f, NPC.Center.Y + 9f), NPC.DirectionTo(Main.player[NPC.target].Center) * 10f, ModContent.ProjectileType<FrostBoneShot>(), 30, 2, Main.myPlayer, 0, 1);
                    }

                    timer2 = 120;
                }
            }

            if (shoot) //If the shoot bool is true, then redcue the shoot timer otherwise do nothing.
            {
                shootTimer--;
            }

            if (shootTimer <= 0) //If it becomes 0 or less, reset the shoot bool to false.
            {
                shoot = false;

                shootTimer = 60; //Resets the timer to 60 ticks (1 second)
            }

            if (shoot) //If the shoot bool is true, its X speed is reduced by 75% of its initial. That is to generate the effects of it stopping a little before shooting.
            {
                NPC.velocity.X *= 0.9f;
            }

        }

        private void Icicles(NPC NPC)
        {
            float fullRotationInFrames = 240;

            if (++icetimer2 >= fullRotationInFrames / maxIcicles)
            {
                // Do not attempt to spawn the projectile on clients. Only in singleplayer and server instances.
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, Vector2.Zero, ModContent.ProjectileType<SpinIcicle>(), 20, 2, Main.myPlayer, NPC.whoAmI);
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
            if (Main.hardMode && spawnInfo.Player.ZoneSnow)
            {
                return 0.03f;
            }

            return 0f;
        }

        #region NPCLoot
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Wool>()));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SilkScrap>(), 5, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SolidWater>(), 5, 1, 3));
        }
        #endregion NPCLoot
    }
}