using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Specialized;

namespace EpicBattleFantasyUltimate.NPCs.Idols.WoodenIdols
{
    public class WoodenIdol1 : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wooden Idol");

        }

        public override void SetDefaults()
        {
            npc.width = 36;
            npc.height = 48;

            npc.lifeMax = 125;
            npc.damage = 22;
            npc.defense = 35;
            npc.lifeRegen = 4;

            npc.aiStyle = -1;
            npc.noGravity = false;




        }

        public override void AI()
        {

            MovementSpeed(npc);
            Jumping(npc);

            npc.spriteDirection = npc.direction;

        }


        private void MovementSpeed(NPC npc)
        {
            npc.TargetClosest(true);

            Vector2 target = Main.player[npc.target].Center - npc.Center;
            float num1276 = target.Length(); //This seems totally useless, not used anywhere.
            float MoveSpeedMult = 3f; //How fast it moves and turns. A multiplier maybe?
            MoveSpeedMult += num1276 / 300f; //Balancing the speed. Lowering the division value makes it have more sharp turns.
            int MoveSpeedBal = 120; //This does the same as the above.... I do not understand.
            target.Normalize(); //Makes the vector2 for the target have a lenghth of one facilitating in the calculation
            target *= MoveSpeedMult;
            npc.velocity.X = (npc.velocity.X * (float)(MoveSpeedBal - 1) + target.X) / (float)MoveSpeedBal;

        }

        private void Jumping(NPC npc)
        {
            if (npc.velocity.Y == 0f)
            {
                npc.velocity = new Vector2(npc.velocity.X, -5f);
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

            int goreIndex = Gore.NewGore(npc.position, (npc.velocity * npc.direction) , mod.GetGoreSlot("Gores/Idols/WoodenIdols/WoodenIdol1_Gore1"), 1f);
            int goreIndex2 = Gore.NewGore(npc.position, (npc.velocity * npc.direction) * -1, mod.GetGoreSlot("Gores/Idols/WoodenIdols/WoodenIdol1_Gore2"), 1f);

            for(int i = 0; i <= 20; i++)
            {
                Dust.NewDustDirect(npc.Center, npc.width, npc.height, DustID.t_LivingWood, Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f), Scale: 1);
            }





            return true;
        }







    }
}
