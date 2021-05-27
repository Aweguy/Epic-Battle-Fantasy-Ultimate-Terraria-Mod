using EpicBattleFantasyUltimate.Projectiles.NPCProj.OreExplosions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Ores
{
    public class TopazOre : ModNPC
    {
        private Vector2 center;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Topaz Ore");
            Main.npcFrameCount[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 40;

            npc.lifeMax = 60;
            npc.damage = 10;
            npc.defense = 3;

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
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<TopazExplosion>(), 20, 5f, Main.myPlayer, 0, 1);

                    int goreIndex = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore1"), 1f);
                    int goreIndex2 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore2"), 1f);
                    int goreIndex3 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore3"), 1f);
                    int goreIndex4 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore4"), 1f);
                    int goreIndex5 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore5"), 1f);
                    int goreIndex6 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore6"), 1f);

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
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<TopazExplosion>(), 20, 5f, Main.myPlayer, 0, 1);

                int goreIndex = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore1"), 1f);
                int goreIndex2 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore2"), 1f);
                int goreIndex3 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore3"), 1f);
                int goreIndex4 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore4"), 1f);
                int goreIndex5 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore5"), 1f);
                int goreIndex6 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore6"), 1f);

                npc.life = 0;
            }

            #endregion Death Check

            for (int i = 0; i <= 5; i++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
        }

        #endregion OnHitPlayer

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i <= 3; i++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
            for (int j = 0; j <= 2; j++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Ice, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
        }

        #region AI

        public override void AI()
        {
            Direction(npc);
            Movement(npc);
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

        private void Movement(NPC npc)
        {
            Vector2 target = Main.player[npc.target].Center - npc.Center;
            float num1276 = target.Length();
            float MoveSpeedMult = 8f; //How fast it moves and turns. A multiplier maybe?
            MoveSpeedMult += num1276 / 100f; //Balancing the speed. Lowering the division value makes it have more sharp turns.
            int MoveSpeedBal = 50; //This does the same as the above.... I do not understand.
            target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
            target *= MoveSpeedMult;
            npc.velocity = (npc.velocity * (float)(MoveSpeedBal - 1) + target) / (float)MoveSpeedBal;

            npc.noGravity = true;
            npc.TargetClosest(true);
        }

        #endregion Movement

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

        public override bool CheckDead()
        {
            int goreIndex = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore1"), 1f);
            int goreIndex2 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore2"), 1f);
            int goreIndex3 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore3"), 1f);
            int goreIndex4 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore4"), 1f);
            int goreIndex5 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore5"), 1f);
            int goreIndex6 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Ores/TopazOre/TopazOre_Gore6"), 1f);

            for (int i = 0; i <= 15; i++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Stone, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
            for (int j = 0; j <= 5; j++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Ice, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }

            return true;
        }

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

        public override void NPCLoot()
        {
            EpicWorld.OreKills += 1;
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
            }

            Item.NewItem(npc.getRect(), ItemID.Ruby, 1);
        }
    }
}