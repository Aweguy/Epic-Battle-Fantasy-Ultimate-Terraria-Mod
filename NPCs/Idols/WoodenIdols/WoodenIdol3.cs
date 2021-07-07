using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Idols.WoodenIdols
{
    public class WoodenIdol3 : ModNPC
    {
        private bool Left = false;
        private bool Right = true;
        private bool Spin = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wooden Idol");
        }

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 46;

            npc.lifeMax = 120;
            npc.damage = 18;
            npc.defense = 9;
            npc.lifeRegen = 4;
            npc.value = 50;

            npc.aiStyle = -1;
            npc.noGravity = false;

            if (Main.hardMode)
            {
                npc.lifeMax *= 2;
                npc.damage *= 2;
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
            Collision.StepUp(ref npc.position, ref npc.velocity, npc.width, npc.height, ref npc.ai[0], ref npc.ai[1]);

            if (npc.collideY)
            {
                if (Main.rand.NextFloat() < .1f)
                {
                    npc.velocity = new Vector2(npc.velocity.X, -10f);
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Idols/WoodenIdols/WoodenIdolJump").WithPitchVariance(.7f), npc.position);

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
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Idols/StoneIdols/StoneIdolJump2").WithPitchVariance(.7f), npc.position);

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
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.t_LivingWood, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
        }

        public override bool CheckDead()
        {
            int goreIndex = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Idols/WoodenIdols/WoodenIdol3/WoodenIdol3_Gore1"), 1f);
            int goreIndex2 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Idols/WoodenIdols/WoodenIdol3/WoodenIdol3_Gore2"), 1f);

            for (int i = 0; i <= 20; i++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.t_LivingWood, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }

            return true;
        }

        public static bool PlayerIsInForest(Player player)
        {
            return !player.ZoneJungle
                && !player.ZoneDungeon
                && !player.ZoneCorrupt
                && !player.ZoneCrimson
                && !player.ZoneHoly
                && !player.ZoneSnow
                && !player.ZoneDesert
                && !player.ZoneUndergroundDesert
                && !player.ZoneGlowshroom
                && !player.ZoneMeteor
                && !player.ZoneBeach
                && player.ZoneOverworldHeight;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Player player = Main.player[spawnInfo.player.whoAmI];
            if (PlayerIsInForest(player) && Main.dayTime && !spawnInfo.invasion)
            {
                return 0.1f;
            }

            return 0f;
        }

        #region NPCLoot

        public override void NPCLoot()
        {
            Item.NewItem(npc.getRect(), ItemID.Wood, 5);
        }

        #endregion NPCLoot
    }
}