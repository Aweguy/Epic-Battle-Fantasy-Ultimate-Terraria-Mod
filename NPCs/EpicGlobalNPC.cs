using EpicBattleFantasyUltimate.Buffs.Debuffs;
using EpicBattleFantasyUltimate.Items;
using EpicBattleFantasyUltimate.Items.Consumables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        #region Cursed Variables

        public bool Cursed = false;
        public bool CursedAlphaCheck = true;
        public int CursedStacks;
        public int CursedAlpha = 0;
        double CursedDefense;
        double CursedMult;

        #endregion

        public int bossesDefeated = 0;

        #region Electrified Variables

        public bool Electrified = false;


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

            #region Cursed Reset

            if(Cursed == false)
            {
                CursedStacks = 0;
            }

            Cursed = false;


            #endregion


            Electrified = false;

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
                    damage = (int)(damage * 0.50f);
                }
            
            

            #endregion

        }

        #endregion

        #region PostAI

        public override void PostAI(NPC npc)
        {
            #region Cursed Effects

            if (Cursed)
            {
                if (CursedStacks <= 5)
                {
                    CursedMult = 0.1 * CursedStacks;
                    CursedDefense = 1 - CursedMult;
                    npc.defense = (int)(npc.defense * CursedDefense);
                }
                else
                {
                    npc.defense = (int)(npc.defense * 0.5);
                }

            }
            else
            {
                npc.defense = npc.defDefense;
            }

            if(CursedStacks == 1)
            {
                CursedAlpha = 25;
            }
            else if (CursedStacks == 2)
            {
                CursedAlpha = 75;
            }
            else if (CursedStacks == 3)
            {
                CursedAlpha = 150;
            }
            else if (CursedStacks == 4)
            {
                CursedAlpha = 255;
            }
            else if (CursedStacks >= 5)
            {
                if (CursedAlphaCheck == true)
                {
                    CursedAlpha -= 10;
                }
                else if (CursedAlphaCheck == false)
                {
                    CursedAlpha += 10;
                }

                if (CursedAlpha >= 255)
                {
                    CursedAlphaCheck = true;
                }
                else if (CursedAlpha <= 0)
                {
                    CursedAlphaCheck = false;
                }
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

            #region Electrified Effects

            if (Electrified)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }


                npc.lifeRegen -= 8;
                if (npc.velocity.X != 0f)
                {
                    npc.lifeRegen -= 32;
                }
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

            #region if boss
            if (npc.boss)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("DarkMatter"), Main.rand.Next(2) + 1);

                #region unique boss count
                //unique boss count

                if (NPC.killCount[npc.type] <= 0)
                {
                    EpicWorld.bossesDefeated++;
                }


                /*for (int i = 0; i < NPCLoader.NPCCount; ++i)
                {
                    npc.SetDefaults(i);

                    if (npc.boss && NPC.killCount[i] > 0)
                    {
                        EpicWorld.bossesDefeated++;
                    }
                }*/

                #endregion

            }

            #endregion
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

            #region Electrified Dust

            if (Electrified)
            {
                Dust dust = Dust.NewDustDirect(npc.position - new Vector2(2f, 2f), npc.width, npc.height, 226, 0f, 0f, 0, new Color(255, 255, 255), 1f);
                dust.noGravity = true;
            }



            #endregion


        }

        #endregion

        #region PostDraw

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            if (Cursed)
            {
                Texture2D tex = mod.GetTexture("Buffs/Debuffs/CursedEffect");
                Vector2 drawOrigin = new Vector2(tex.Width * 0.5f, tex.Height * 0.5f);

                Vector2 drawPos = npc.Center - new Vector2(0, 15 + npc.height / 2) - Main.screenPosition;

                spriteBatch.Draw(tex, drawPos, new Rectangle(0, 0, tex.Width, tex.Height), new Color(255, 255, 255, CursedAlpha), 0, drawOrigin, 1, SpriteEffects.None, 0f);              
            }
        }

        #endregion



    }
}
