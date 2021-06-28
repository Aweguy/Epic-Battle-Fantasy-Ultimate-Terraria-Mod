using EpicBattleFantasyUltimate.Projectiles.NPCProj.Idols.IceIdol;
using Microsoft.Xna.Framework;
using Terraria;
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
            npc.width = 36;
            npc.height = 48;

            npc.lifeMax = 95;
            npc.damage = 17;
            npc.defense = 3;
            npc.lifeRegen = 4;
            npc.value = 50;

            npc.aiStyle = -1;
            npc.noGravity = false;

            if (Main.hardMode)
            {
                npc.lifeMax *= 3;
                npc.damage = (int)(npc.damage * 1.5f);
            }
        }

        public override void AI()
        {
            Rotation(npc);
            MovementSpeed(npc);
            Jumping(npc);
            //IceAttack(npc);

            npc.spriteDirection = npc.direction;
        }

        #region MovementSpeed

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

        #endregion MovementSpeed

        #region Jumping

        private void Jumping(NPC npc)
        {
            if (npc.collideY)
            {
                if (Main.rand.NextFloat() < .1f)
                {
                    npc.velocity = new Vector2(npc.velocity.X, -10f);
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Idols/IceIdols/IceIdolJump").WithPitchVariance(.7f), npc.position);

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

        #endregion Jumping

        #region IceAttack

        private void IceAttack(NPC npc)
        {
            if (Ice)
            {
                IceTimer--;

                Dust.NewDustDirect(npc.position, npc.width, npc.height, DustID.Frost, 0, 0, 0, Scale: .7f);

                if (IceTimer <= 0)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/Idols/IceIdols/IceIdolMagic").WithPitchVariance(.2f).WithVolume(.3f), npc.position);

                    if (Spin)
                    {
                        for (int i = 0; i <= 360 / 40; i++)
                        {
                            Vector2 velocity = new Vector2(Main.rand.Next(7, 14), 0).RotatedBy(MathHelper.ToRadians(IceRotation));

                            int a = Projectile.NewProjectile(npc.Center, velocity, ModContent.ProjectileType<IceSpike>(), npc.damage, 10f, Main.myPlayer, (int)(npc.spriteDirection), 0);

                            IceRotation += 40;
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= 360 / 120; i++)
                        {
                            Vector2 velocity = new Vector2(Main.rand.Next(5, 10), 0).RotatedBy(MathHelper.ToRadians(IceRotation));

                            int a = Projectile.NewProjectile(npc.Center, velocity, ModContent.ProjectileType<IceSpike>(), npc.damage, 10f, Main.myPlayer, (int)(npc.spriteDirection), 0);

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

        #endregion Rotation

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i <= 5; i++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Ice, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }
        }

        public override bool CheckDead()
        {
            int goreIndex = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Idols/IceIdols/IceIdol1/IceIdol1_Gore1"), 1f);
            int goreIndex2 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Idols/IceIdols/IceIdol1/IceIdol1_Gore2"), 1f);
            int goreIndex3 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Idols/IceIdols/IceIdol1/IceIdol1_Gore3"), 1f);
            int goreIndex4 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Idols/IceIdols/IceIdol1/IceIdol1_Gore4"), 1f);
            int goreIndex5 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Idols/IceIdols/IceIdol1/IceIdol1_Gore5"), 1f);
            int goreIndex6 = Gore.NewGore(npc.position, (npc.velocity * npc.direction), mod.GetGoreSlot("Gores/Idols/IceIdols/IceIdol1/IceIdol1_Gore6"), 1f);

            for (int i = 0; i <= 20; i++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.Ice, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }

            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.player.ZoneSnow)
            {
                return .02f;
            }

            return 0f;
        }

        #region NPCLoot

        public override void NPCLoot()
        {
            if (Main.rand.NextFloat() < .33f)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("SolidWater"), 1);
            }
        }

        #endregion NPCLoot
    }
}