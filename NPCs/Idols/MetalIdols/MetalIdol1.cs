using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace EpicBattleFantasyUltimate.NPCs.Idols.MetalIdols
{
    class MetalIdol1 : ModNPC
    {
        public static readonly SoundStyle IdolHit = new("EpicBattleFantasyUltimate/Assets/Sounds/NPCHit/MetalIdolHit")
		{
			Volume = 2f,
			PitchVariance = 1f
		};

        public static readonly SoundStyle IdolJump = new("EpicBattleFantasyUltimate/Assets/Sounds/Custom/Idols/MetalIdols/MetalIdolJump1")
        {
            Volume = 2f,
            PitchVariance = 1f
        };

        public static readonly SoundStyle IdolHighJump = new("EpicBattleFantasyUltimate/Assets/Sounds/Custom/Idols/MetalIdols/MetalIdolJump2")
        {
            Volume = 2f,
            PitchVariance = 1f
        };


        private bool Left = false;
        private bool Right = true;
        private bool Spin = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metal Idol");
        }

        public override void SetDefaults()
        {
            NPC.width = 36;
            NPC.height = 48;

            NPC.lifeMax = 95;
            NPC.damage = 16;
            NPC.defense = 5;
            NPC.lifeRegen = 4;
            NPC.value = 50;

            NPC.aiStyle = -1;
            NPC.noGravity = false;
            if (!Main.dedServ)
                NPC.HitSound = IdolHit;

            if (Main.hardMode)
            {
                NPC.lifeMax *= 2;
                NPC.damage *= 2;
                NPC.defense *= 2;
            }
        }

        public override bool PreAI()
        {

            Rotation(NPC);
            MovementSpeed(NPC);
            Jumping(NPC);

            NPC.spriteDirection = NPC.direction;

            return false;
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

        /*public override void NPCLoot()
        {
            base.NPCLoot();
        }*/
    }
}
