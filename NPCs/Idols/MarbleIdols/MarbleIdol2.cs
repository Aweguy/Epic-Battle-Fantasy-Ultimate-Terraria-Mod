using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Idols.MarbleIdols
{
    public class MarbleIdol2 : ModNPC
    {
        public static readonly SoundStyle IdolHit = new("EpicBattleFantasyUltimate/Assets/Sounds/NPCHit/MarbleIdolHit")
        {
            Volume = 2f,
            PitchVariance = 1f
        };

        public static readonly SoundStyle IdolJump = new("EpicBattleFantasyUltimate/Assets/Sounds/Custom/Idols/IceIdols/IceIdolJump")
        {
            Volume = 2f,
            PitchVariance = 1f
        };

        public static readonly SoundStyle IdolHighJump = new("EpicBattleFantasyUltimate/Assets/Sounds/Custom/Idols/IceIdols/IceIdolJump")
        {
            Volume = 2f,
            PitchVariance = 1f
        };

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
            NPC.width = 36;
            NPC.height = 48;

            NPC.lifeMax = 100;
            NPC.damage = 20;
            NPC.defense = 15;
            NPC.lifeRegen = 4;
            NPC.value = 50;

            NPC.aiStyle = -1;
            NPC.noGravity = false;
            if (!Main.dedServ)
                NPC.HitSound = IdolHit;

            if (Main.hardMode)
            {
                NPC.lifeMax *= 2;
                NPC.defense *= 2;
            }
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
            {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Marble,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Ornate idols found in ancient ruins. Possess a tidbit of magical power in their gem.")
            });
        }
        public override void AI()
        {
            Rotation(NPC);
            MovementSpeed(NPC);
            Jumping(NPC);

            NPC.spriteDirection = NPC.direction;
        }

        private void MovementSpeed(NPC NPC)
        {
            NPC.TargetClosest(true);

            Vector2 target = Main.player[NPC.target].Center - NPC.Center;

            if (Spin)
            {
                float num1276 = target.Length(); //This seems totally useless, not used anywhere.
                float MoveSpeedMult = 6f; //How fast it moves and turns. A multiplier maybe?
                MoveSpeedMult += num1276 / 150f; //Balancing the speed. Lowering the division value makes it have more sharp turns.
                int MoveSpeedBal = 60; //This does the same as the above.... I do not understand.
                target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
                target *= MoveSpeedMult;

                NPC.velocity.X = (NPC.velocity.X * (float)(MoveSpeedBal - 1) + target.X) / (float)MoveSpeedBal;
            }
            else
            {
                float num1276 = target.Length(); //This seems totally useless, not used anywhere.
                float MoveSpeedMult = 3f; //How fast it moves and turns. A multiplier maybe?
                MoveSpeedMult += num1276 / 300f; //Balancing the speed. Lowering the division value makes it have more sharp turns.
                int MoveSpeedBal = 120; //This does the same as the above.... I do not understand.
                target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
                target *= MoveSpeedMult;

                NPC.velocity.X = (NPC.velocity.X * (float)(MoveSpeedBal - 1) + target.X) / (float)MoveSpeedBal;
            }
        }

        private void Jumping(NPC NPC)
        {
            Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.ai[0], ref NPC.ai[1]);

            if (NPC.collideY)
            {
                if (Main.rand.NextFloat() < .1f)
                {
                    NPC.velocity = new Vector2(NPC.velocity.X, -10f);
                    if (!Main.dedServ)
                        SoundEngine.PlaySound(IdolHighJump, NPC.Center);

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
                    NPC.velocity = new Vector2(NPC.velocity.X, -5f);
                    if (!Main.dedServ)
                        SoundEngine.PlaySound(IdolJump, NPC.Center);

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

        private void Rotation(NPC NPC)
        {
            if (Right && !Spin)
            {
                NPC.rotation += MathHelper.ToRadians(1);

                NPC.rotation = MathHelper.Clamp(NPC.rotation, MathHelper.ToRadians(-10), MathHelper.ToRadians(10));
            }
            else if (Left && !Spin)
            {
                NPC.rotation -= MathHelper.ToRadians(1);

                NPC.rotation = MathHelper.Clamp(NPC.rotation, MathHelper.ToRadians(-10), MathHelper.ToRadians(10));
            }

            if (NPC.life <= NPC.lifeMax * .25f)
            {
                NPC.rotation += MathHelper.ToRadians(30) * NPC.spriteDirection;
                Spin = true;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i <= 5; i++)
            {
                Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.Dirt, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
        }

        #region NPCLoot
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Emerald, 3));
            Drop = true;
        }

        #endregion NPCLoot

        public override bool CheckDead()
        {
            if (Drop == false)
            {
                int goreIndex = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction), Mod.Find<ModGore>("MarbleIdol2_Gore1").Type, 1f);
            }
            int goreIndex2 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction) * -1, Mod.Find<ModGore>("MarbleIdol2_Gore2").Type, 1f);
            int goreIndex3 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction), Mod.Find<ModGore>("MarbleIdol2_Gore3").Type, 1f);
            int goreIndex4 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction) * -1, Mod.Find<ModGore>("MarbleIdol2_Gore4").Type, 1f);

            for (int i = 0; i <= 20; i++)
            {
                Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.Dirt, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }

            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneDesert || spawnInfo.Player.ZoneUndergroundDesert || spawnInfo.Player.ZoneMarble)
            {
                return .2f;
            }

            return 0f;
        }
    }
}