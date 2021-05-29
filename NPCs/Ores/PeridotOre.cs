using EpicBattleFantasyUltimate.Projectiles.NPCProj.OreExplosions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Ores
{
    public class PeridotOre : ModNPC
    {
        private const float MAX_CHARGE = 40f;

        private const int MAX_DASH_COOLDOWN = 60 * 10;
        //the range of the ore starting to charge its attack
        public float DashAttackRange = 16f * 15f;
        // The actual charge value is stored in the localAI0 field
        public float Charge
        {
            get => npc.localAI[0];
            set => npc.localAI[0] = value;
        }
        //The speed of the dash
        float DashSpeed = 17f;

        float DashCooldown;

        //When the ore is max charged or not
        public bool IsAtMaxCharge => Charge == MAX_CHARGE;
        //Whether the ore is in range or not
        bool InRange;
        //Whether the dash attack is on cooldown or not
        bool IsOnCooldown => DashCooldown > 0;



        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Peridot Ore");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 40;

            npc.lifeMax = 190;
            npc.damage = 23;
            npc.defense = 25;
            npc.lifeRegen = 4;
            npc.knockBackResist = -0.2f;

            npc.noTileCollide = true;
            npc.aiStyle = -1;
        }

        #region OnHitPlayer

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            //npc.life = 0;

            #region Death Check

            if (npc.life >= npc.lifeMax * 0.40)
            {
                if (Main.rand.NextFloat() < .1f)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<PeridotExplosion>(), 35, 5f, Main.myPlayer, 0, 1);

                    Vector2 vel1 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                    Vector2 vel2 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                    Vector2 vel3 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                    Vector2 vel4 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                    Vector2 vel5 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                    Vector2 vel6 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                    Vector2 vel7 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                    Vector2 vel8 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                    Vector2 vel9 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                    int goreIndex = Gore.NewGore(npc.position, npc.velocity * vel1, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore1"), 1f);

                    int goreIndex2 = Gore.NewGore(npc.position, npc.velocity * vel2, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore2"), 1f);

                    int goreIndex3 = Gore.NewGore(npc.position, npc.velocity * vel3, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore3"), 1f);

                    int goreIndex4 = Gore.NewGore(npc.position, npc.velocity * vel4, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore4"), 1f);

                    int goreIndex5 = Gore.NewGore(npc.position, npc.velocity * vel5, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore5"), 1f);

                    int goreIndex6 = Gore.NewGore(npc.position, npc.velocity * vel6, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore6"), 1f);

                    int goreIndex7 = Gore.NewGore(npc.position, npc.velocity * vel7, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore7"), 1f);

                    int goreIndex8 = Gore.NewGore(npc.position, npc.velocity * vel8, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore8"), 1f);

                    int goreIndex9 = Gore.NewGore(npc.position, npc.velocity * vel9, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore9"), 1f);

                    npc.life = 0;
                }
                else
                {
                    Vector2 relativePosition = npc.Center - target.Center;
                    float absRelativeRotation = Math.Abs(relativePosition.ToRotation());

                    bool leftRightCollision = false;

                    if (absRelativeRotation <= MathHelper.PiOver4 || // Projectile is on right side of NPC.
                        absRelativeRotation >= MathHelper.PiOver4 * 3) // Projectile is on left side of NPC.
                    {
                        leftRightCollision = true;
                    }

                    if (leftRightCollision)
                    {
                        npc.velocity.X *= -2;
                    }
                    else
                    {
                        npc.velocity.Y *= -2;
                    }
                }
            }
            else
            {
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<PeridotExplosion>(), 30, 5f, Main.myPlayer, 0, 1);

                Vector2 vel1 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                Vector2 vel2 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                Vector2 vel3 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                Vector2 vel4 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                Vector2 vel5 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                Vector2 vel6 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                Vector2 vel7 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                Vector2 vel8 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                Vector2 vel9 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

                int goreIndex = Gore.NewGore(npc.position, npc.velocity * vel1, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore1"), 1f);

                int goreIndex2 = Gore.NewGore(npc.position, npc.velocity * vel2, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore2"), 1f);

                int goreIndex3 = Gore.NewGore(npc.position, npc.velocity * vel3, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore3"), 1f);

                int goreIndex4 = Gore.NewGore(npc.position, npc.velocity * vel4, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore4"), 1f);

                int goreIndex5 = Gore.NewGore(npc.position, npc.velocity * vel5, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore5"), 1f);

                int goreIndex6 = Gore.NewGore(npc.position, npc.velocity * vel6, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore6"), 1f);

                int goreIndex7 = Gore.NewGore(npc.position, npc.velocity * vel7, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore7"), 1f);

                int goreIndex8 = Gore.NewGore(npc.position, npc.velocity * vel8, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore8"), 1f);

                int goreIndex9 = Gore.NewGore(npc.position, npc.velocity * vel9, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore9"), 1f);

                npc.life = 0;
            }

            #endregion Death Check

            for (int i = 0; i <= 5; i++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
        }

        #endregion OnHitPlayer

        #region HitEffect

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i <= 3; i++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
            for (int j = 0; j <= 2; j++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, 3, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
        }

        #endregion HitEffect

        #region AI

        public override void AI()
        {
            Player player = Main.player[npc.target];

            Direction(npc);
            Movement(npc, player);
            Charging(npc);
        }

        #endregion AI

        #region Direction

        private void Direction(NPC npc)
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

        #endregion Direction

        #region Movement

        private void Movement(NPC npc, Player player)
        {
            Vector2 target = player.Center - npc.Center;
            float num1276 = target.Length(); //This seems totally useless, not used anywhere.
            float MoveSpeedMult = 2f; //How fast it moves and turns. A multiplier maybe?
            MoveSpeedMult += num1276 / 200f; //Balancing the speed. Lowering the division value makes it have more sharp turns.
            int MoveSpeedBal = 100; //This does the same as the above.... I do not understand.
            target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
            target *= MoveSpeedMult;
            npc.velocity = (npc.velocity * (float)(MoveSpeedBal - 1) + target) / (float)MoveSpeedBal;

            //Smoot stop for the dash
            if (Vector2.Distance(player.Center, npc.Center) <= DashAttackRange && !IsOnCooldown)
            {
                npc.velocity *= 0.90f;

                InRange = true;
            }
            else
            {
                InRange = false;
            }

            //if it's max charged, dash towards the player
            if (IsAtMaxCharge)
            {
                npc.velocity = Vector2.Normalize(player.Center - npc.Center) * DashSpeed;

                Charge = 0;

                DashCooldown = MAX_DASH_COOLDOWN;
            }

            //cooldown reduction
            if (IsOnCooldown)
            {
                DashCooldown--;
            }


            npc.noGravity = true;
            npc.TargetClosest(true);
        }

        #endregion Movement

        private void Charging(NPC npc)
        {
            if (InRange && !IsOnCooldown)
            {
                Charge++;
            }
        }


        #region FindFrame

        public override void FindFrame(int frameHeight)
        {
            if (++npc.frameCounter >= 7)
            {
                npc.frameCounter = 0;
                npc.frame.Y = (npc.frame.Y + frameHeight) % (frameHeight * Main.npcFrameCount[npc.type]);
            }
        }

        #endregion FindFrame

        #region CheckDead

        public override bool CheckDead()
        {
            Vector2 vel1 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

            Vector2 vel2 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

            Vector2 vel3 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

            Vector2 vel4 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

            Vector2 vel5 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

            Vector2 vel6 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

            Vector2 vel7 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

            Vector2 vel8 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

            Vector2 vel9 = new Vector2(Main.rand.NextFloat(-6f, 6f), Main.rand.NextFloat(-6f, 6f));

            int goreIndex = Gore.NewGore(npc.position, npc.velocity * vel1, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore1"), 1f);

            int goreIndex2 = Gore.NewGore(npc.position, npc.velocity * vel2, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore2"), 1f);

            int goreIndex3 = Gore.NewGore(npc.position, npc.velocity * vel3, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore3"), 1f);

            int goreIndex4 = Gore.NewGore(npc.position, npc.velocity * vel4, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore4"), 1f);

            int goreIndex5 = Gore.NewGore(npc.position, npc.velocity * vel5, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore5"), 1f);

            int goreIndex6 = Gore.NewGore(npc.position, npc.velocity * vel6, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore6"), 1f);

            int goreIndex7 = Gore.NewGore(npc.position, npc.velocity * vel7, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore7"), 1f);

            int goreIndex8 = Gore.NewGore(npc.position, npc.velocity * vel8, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore8"), 1f);

            int goreIndex9 = Gore.NewGore(npc.position, npc.velocity * vel9, mod.GetGoreSlot("Gores/Ores/PeridotOre/PeridotOre_Gore9"), 1f);

            for (int i = 0; i <= 15; i++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
            for (int j = 0; j <= 5; j++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, 3, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }

            return true;
        }

        #endregion CheckDead

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (EpicWorld.OreEvent)
            {
                return 35f;
            }
            else if (Main.hardMode == true && spawnInfo.player.ZoneRockLayerHeight)
            {
                return 0.02f;
            }
            else
            {
                return 0f;
            }
        }

        #region NPCLoot

        public override void NPCLoot()
        {
            EpicWorld.OreKills += 1;
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
            }

            Item.NewItem(npc.getRect(), ItemID.Emerald, 2);
        }

        #endregion NPCLoot
    }
}