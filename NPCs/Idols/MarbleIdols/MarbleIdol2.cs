using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Idols.MarbleIdols
{
    public class MarbleIdol2 : ModNPC
    {
        private bool Left = false;
        private bool Right = true;
        private bool Spin = false;
        private bool Drop = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Marble Idol");
        }

        public override void SetDefaults()
        {
            npc.width = 36;
            npc.height = 48;

            npc.lifeMax = 100;
            npc.damage = 20;
            npc.defense = 15;
            npc.lifeRegen = 4;
            npc.value = 50;

            npc.aiStyle = -1;
            npc.noGravity = false;

            if (Main.hardMode)
            {
                npc.lifeMax *= 3;
                npc.defense *= 2;
            }
        }

        public override void AI()
        {
            Rotation(npc);
            MovementSpeed(npc);
            Jumping(npc);

            npc.spriteDirection = npc.direction;
        }

        private void MovementSpeed(NPC npc)
        {
            npc.TargetClosest(true);

            Vector2 target = Main.player[npc.target].Center - npc.Center;

            if (Spin)
            {
                float num1276 = target.Length(); //This seems totally useless, not used anywhere.
                float MoveSpeedMult = 6f; //How fast it moves and turns. A multiplier maybe?
                MoveSpeedMult += num1276 / 150f; //Balancing the speed. Lowering the division value makes it have more sharp turns.
                int MoveSpeedBal = 60; //This does the same as the above.... I do not understand.
                target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
                target *= MoveSpeedMult;

                npc.velocity.X = (npc.velocity.X * (float)(MoveSpeedBal - 1) + target.X) / (float)MoveSpeedBal;
            }
            else
            {
                float num1276 = target.Length(); //This seems totally useless, not used anywhere.
                float MoveSpeedMult = 3f; //How fast it moves and turns. A multiplier maybe?
                MoveSpeedMult += num1276 / 300f; //Balancing the speed. Lowering the division value makes it have more sharp turns.
                int MoveSpeedBal = 120; //This does the same as the above.... I do not understand.
                target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
                target *= MoveSpeedMult;

                npc.velocity.X = (npc.velocity.X * (float)(MoveSpeedBal - 1) + target.X) / (float)MoveSpeedBal;
            }
        }

        private void Jumping(NPC npc)
        {
            if (npc.velocity.Y == 0f)
            {
                if (Main.rand.NextFloat() < .1f)
                {
                    npc.velocity = new Vector2(npc.velocity.X, -10f);
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Idols/IceIdols/IceIdolJump").WithPitchVariance(.7f), npc.position);

                    if (!Left && Right && !Spin)
                    {
                        Left = true;
                        Right = false;
                    }
                    else if (Left && !Right && !Spin)
                    {
                        Left = false;
                        Right = true;
                    }
                }
                else
                {
                    npc.velocity = new Vector2(npc.velocity.X, -5f);
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Idols/IceIdols/IceIdolJump").WithPitchVariance(.7f), npc.position);

                    if (!Left && Right && !Spin)
                    {
                        Left = true;
                        Right = false;
                    }
                    else if (Left && !Right && !Spin)
                    {
                        Left = false;
                        Right = true;
                    }
                }
            }
        }

        private void Rotation(NPC npc)
        {
            if (Right && !Spin)
            {
                npc.rotation += MathHelper.ToRadians(1);

                npc.rotation = MathHelper.Clamp(npc.rotation, MathHelper.ToRadians(-10), MathHelper.ToRadians(10));
            }
            else if (Left && !Spin)
            {
                npc.rotation -= MathHelper.ToRadians(1);

                npc.rotation = MathHelper.Clamp(npc.rotation, MathHelper.ToRadians(-10), MathHelper.ToRadians(10));
            }

            if (npc.life <= npc.lifeMax * .25f)
            {
                npc.rotation += MathHelper.ToRadians(30) * npc.spriteDirection;
                Spin = true;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i <= 5; i++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Dirt, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
        }

        #region NPCLoot

        public override void NPCLoot()
        {
            if (Main.rand.NextFloat() < 0.3f)
            {
                Item.NewItem(npc.getRect(), ItemID.Ruby, 1);

                Drop = true;
            }
        }

        #endregion NPCLoot

        public override bool CheckDead()
        {
            if (Drop == false)
            {
                int goreIndex = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Idols/MarbleIdols/MarbleIdol2/MarbleIdol2_Gore1"), 1f);
            }
            int goreIndex2 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Idols/MarbleIdols/MarbleIdol2/MarbleIdol2_Gore2"), 1f);
            int goreIndex3 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Idols/MarbleIdols/MarbleIdol2/MarbleIdol2_Gore3"), 1f);
            int goreIndex4 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Idols/MarbleIdols/MarbleIdol2/MarbleIdol2_Gore4"), 1f);

            for (int i = 0; i <= 20; i++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Dirt, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }

            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode == true && spawnInfo.player.ZoneDesert || spawnInfo.player.ZoneUndergroundDesert)
            {
                return .2f;
            }

            return 0f;
        }
    }
}