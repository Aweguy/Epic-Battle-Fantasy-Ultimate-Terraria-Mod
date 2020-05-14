using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.Utilities;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace EpicBattleFantasyUltimate.NPCs.TownNPCs
{
    public class MaterialMerchant : ModNPC
    {

        [AutoloadHead]
        public class Matt : ModNPC
        {

            public override string Texture => "EpicBattleFantasyUltimate/NPCs/TownNPCs/MaterialMerchant";

            public override bool Autoload(ref string name)
            {
                name = "Archeologist";
                return mod.Properties.Autoload;
            }


            public override void SetStaticDefaults()
            {
                // DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
                //DisplayName.SetDefault("Matt");
                Main.npcFrameCount[npc.type] = 25;
                NPCID.Sets.ExtraFramesCount[npc.type] = 9;
                NPCID.Sets.AttackFrameCount[npc.type] = 4;
                NPCID.Sets.DangerDetectRange[npc.type] = 600;
                NPCID.Sets.AttackType[npc.type] = 1;
                NPCID.Sets.AttackTime[npc.type] = 30;
                NPCID.Sets.AttackAverageChance[npc.type] = 30;
                NPCID.Sets.HatOffsetY[npc.type] = 4;
            }


            public override void SetDefaults()
            {
                npc.townNPC = true;
                npc.friendly = true;
                npc.width = 30;
                npc.height = 50;
                npc.aiStyle = 7;
                npc.damage = 30;
                npc.defense = 20;
                npc.lifeMax = 250;
                npc.HitSound = SoundID.NPCHit1;
                npc.DeathSound = SoundID.NPCDeath1;
                npc.knockBackResist = 0.1f;
                animationType = NPCID.Guide;
            }

            public override string GetChat()
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        return "My wares are the rarest materials you will ever find. All acquired by me, of course!";
                    case 1:
                        return "I have goods from exotic places, like Town! What do you mean, 'that’s a silly name'?!";
                    default:
                        return "I’m sorry, I don’t sell Key Items or stat boosting items.";

                           }
            }


            public override void SetupShop(Chest shop, ref int nextSlot)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("P2Processor"));
                shop.item[nextSlot].shopCustomPrice = 250000;
                nextSlot++;

                shop.item[nextSlot].SetDefaults(mod.ItemType("LeckoBrick"));
                shop.item[nextSlot].shopCustomPrice = 250000;
                nextSlot++;

                shop.item[nextSlot].SetDefaults(mod.ItemType("PlutoniumCore"));
                shop.item[nextSlot].shopCustomPrice = 250000;
                nextSlot++;

                shop.item[nextSlot].SetDefaults(mod.ItemType("RawTitanium"));
                shop.item[nextSlot].shopCustomPrice = 250000;
                nextSlot++;

                shop.item[nextSlot].SetDefaults(mod.ItemType("SteelPlate"));
                shop.item[nextSlot].shopCustomPrice = 25000;
                nextSlot++;


            }

            public override bool CanTownNPCSpawn(int numTownNPCs, int money)
            {

                return true;
            }






            public override string TownNPCName()
            {
                return "Bob";
            }



            public override void SetChatButtons(ref string button, ref string button2)
            {
                button = Language.GetTextValue("LegacyInterface.28");
            }




            public override void OnChatButtonClicked(bool firstButton, ref bool shop)
            {

                if (firstButton)
                {
                    shop = true;
                }

            }




            public override void TownNPCAttackStrength(ref int damage, ref float knockback)
            {
                damage = 60;
                knockback = 5f;
            }


            public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
            {
                cooldown = 10;
                randExtraCooldown = 10;
            }

            public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
            {
                itemWidth = itemHeight = 10;
            }









            public override bool CheckDead()
            {

                int goreIndex = Gore.NewGore(new Vector2(npc.position.X + (float)(npc.width / 2) - 24f, npc.position.Y + (float)(npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[goreIndex].scale = 1.5f;
                Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
                Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;



                return true;
            }




            public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
            {
                projType = ProjectileID.ChainKnife;
                attackDelay = 1;
            }








        }
    }
}
