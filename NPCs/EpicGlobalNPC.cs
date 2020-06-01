using EpicBattleFantasyUltimate.Buffs.Debuffs;
using EpicBattleFantasyUltimate.Items;
using EpicBattleFantasyUltimate.Items.Consumables;
using Microsoft.Xna.Framework;
using MonoMod.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;



namespace EpicBattleFantasyUltimate.NPCs
{
    public class EpicGlobalNPC : GlobalNPC
    {

        #region Rampant Bleed Variables

        public bool RBleed = true;
        public int RBleedStacks;

        public override bool InstancePerEntity => true;

        #endregion

        #region Weakened Variables

        public bool Weakened = true;
        public int WeakenedStacks;
        double WeakenedPower;
        double WeakenedMult;
       

        #endregion


        #region ResetEffects

        public override void ResetEffects(NPC npc)
        {

            #region Feral Bleed Reset

            if (RBleed == false)
            {
                RBleedStacks = 0;
            }
            RBleed = false;

            #endregion

            #region Weakened Reset

            if (Weakened == false)
            {
                WeakenedStacks = 0;
            }

            Weakened = false;

            #endregion

        }

        #endregion

        #region ModifyHitPlayer

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {

            #region Weakened Weakening

            if (Weakened && WeakenedStacks <= 5)
            {
                WeakenedMult = 0.1 * WeakenedStacks;
                WeakenedPower = 1 - WeakenedMult;
                damage = (int)(damage * WeakenedPower);
            }
            else if (Weakened && WeakenedStacks > 5)
            {
                damage = (int)(damage * 0.75f);
            }

            #endregion

        }

        #endregion




        #region UpdateLifeRegen

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {

            #region Feral Bleed Effects

            if (RBleed == true)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
             
                npc.lifeRegen -= RBleedStacks * 10;
            }

            #endregion

        }

        #endregion

        #region SetupShop
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.PartyGirl)
            {

                shop.item[nextSlot].SetDefaults(ItemType<Cake>());
                shop.item[nextSlot].shopCustomPrice = 1000000;
                nextSlot++;
            }


        }

        #endregion

        #region NPCLoot

        public override void NPCLoot(NPC npc)
        {
            if(npc.boss)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("DarkMatter"), Main.rand.Next(2) + 1);
            }


        }

        #endregion

        #region DrawEffects

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {

            #region Rampant Bleeding Dust

            if (RBleed)
            {
                if(RBleedStacks <= 5)
                {
                    if(Main.rand.NextFloat() <= .1f)
                    {                        
                        Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 5, 0f, 0f, 0, new Color(255, 255, 255), 1f);
                    }
                }
                else if (RBleedStacks > 5 && RBleedStacks <= 10)
                {
                    if (Main.rand.NextFloat() <= .2f)
                    {
                        Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 5, 0f, 0f, 0, new Color(255, 255, 255), 1f);
                    }
                }
                else if (RBleedStacks > 10 && RBleedStacks <= 20)
                {
                    if (Main.rand.NextFloat() <= .4f)
                    {
                        Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 5, 0f, 0f, 0, new Color(255, 255, 255), 1f);
                    }
                }
                else if (RBleedStacks > 20)
                {                   
                    Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 5, 0f, 0f, 0, new Color(255, 255, 255), 1f);                    
                }

                


            }
            #endregion




        }

        #endregion





    }
}
