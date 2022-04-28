using EpicBattleFantasyUltimate.Items.Materials;
using EpicBattleFantasyUltimate.Projectiles.NPCProj.Idols.IceIdol;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Idols.IceIdols
{
    public class IceIdol1 : ModNPC
    {
        private int IceTimer = 30;
        private float IceRotation = 0;
        private bool Left = false;
        private bool Right = true;
        private bool Ice = false;
        private bool Spin = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Idol");
        }

        public override void SetDefaults()
        {
            NPC.width = 36;
            NPC.height = 48;

            NPC.lifeMax = 95;
            NPC.damage = 17;
            NPC.defense = 3;
            NPC.lifeRegen = 4;
            NPC.value = 50;

            NPC.aiStyle = -1;
            NPC.noGravity = false;

            if (Main.hardMode)
            {
                NPC.lifeMax *= 3;
                NPC.damage = (int)(NPC.damage * 1.5f);
            }
        }

        public override void AI()
        {
            Rotation(NPC);
            MovementSpeed(NPC);
            Jumping(NPC);
            //IceAttack(NPC);

            NPC.spriteDirection = NPC.direction;
        }

        #region MovementSpeed

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

        #endregion MovementSpeed

        #region Jumping

        private void Jumping(NPC NPC)
        {
            Collision.StepUp(ref NPC.position, ref NPC.velocity, NPC.width, NPC.height, ref NPC.ai[0], ref NPC.ai[1]);

            if (NPC.collideY)
            {
                if (Main.rand.NextFloat() < .1f)
                {
                    NPC.velocity = new Vector2(NPC.velocity.X, -10f);
                    if (!Main.dedServ)
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Custom/Idols/IceIdols/IceIdolJump").WithPitchVariance(2f), NPC.Center);

                    if (Main.rand.NextFloat() < .1f)
                    {
                        Ice = true;
                    }

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
                        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Assets/Sounds/Custom/Idols/IceIdols/IceIdolJump").WithPitchVariance(2f), NPC.Center);

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

        #endregion Jumping

        #region IceAttack

        private void IceAttack(NPC NPC)
        {
            if (Ice)
            {
                IceTimer--;

                Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Frost, 0, 0, 0, Scale: .7f);

                if (IceTimer <= 0)
                {
                    if (!Main.dedServ)
                        SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Custom/Idols/IceIdols/IceIdolMagic");

                    if (Spin)
                    {
                        for (int i = 0; i <= 360 / 40; i++)
                        {
                            Vector2 velocity = new Vector2(Main.rand.Next(7, 14), 0).RotatedBy(MathHelper.ToRadians(IceRotation));

                            int a = Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, velocity, ModContent.ProjectileType<IceSpike>(), NPC.damage, 10f, Main.myPlayer, (int)(NPC.spriteDirection), 0);

                            IceRotation += 40;
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= 360 / 120; i++)
                        {
                            Vector2 velocity = new Vector2(Main.rand.Next(5, 10), 0).RotatedBy(MathHelper.ToRadians(IceRotation));

                            int a = Projectile.NewProjectile(NPC.GetSource_FromAI(),NPC.Center, velocity, ModContent.ProjectileType<IceSpike>(), NPC.damage, 10f, Main.myPlayer, (int)(NPC.spriteDirection), 0);

                            IceRotation += 120;
                        }
                    }

                    IceTimer = 30;
                    Ice = false;
                }
            }
        }

        #endregion IceAttack

        #region Rotation

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

        #endregion Rotation

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i <= 5; i++)
            {
                Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.Ice, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
        }

        public override bool CheckDead()
        {
            int goreIndex = Gore.NewGore(NPC.GetSource_Death(),NPC.position, (NPC.velocity * NPC.direction), Mod.Find<ModGore>("IceIdol1_Gore1").Type, 1f);
            int goreIndex2 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction) * -1, Mod.Find<ModGore>("IceIdol1_Gore2").Type, 1f);
            int goreIndex3 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction), Mod.Find<ModGore>("IceIdol1_Gore3").Type, 1f);
            int goreIndex4 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction) * -1, Mod.Find<ModGore>("IceIdol1_Gore4").Type, 1f);
            int goreIndex5 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction), Mod.Find<ModGore>("IceIdol1_Gore5").Type, 1f);
            int goreIndex6 = Gore.NewGore(NPC.GetSource_Death(), NPC.position, (NPC.velocity * NPC.direction), Mod.Find<ModGore>("IceIdol1_Gore6").Type, 1f);

            for (int i = 0; i <= 20; i++)
            {
                Dust.NewDustDirect(NPC.Center, NPC.width, NPC.height, DustID.Ice, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }

            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.Player.ZoneSnow)
            {
                return .02f;
            }

            return 0f;
        }

        #region NPCLoot
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SolidWater>(),3));
        }
        #endregion NPCLoot
    }
}